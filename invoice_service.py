import json
import datetime
from pathlib import Path
from typing import Dict, List

import requests

from config import config
from logging_config import app_logger, log_error
from main import make_api_request

# Prices for each drink type (EUR)
PRICES = {
    "coffee": 1.0,
    "tea": 0.5,
    "chocolate": 0.5,
    "juices": 1.5,
    "water": 1.5,
}

INVOICE_DIR = Path("invoices")
INVOICE_DIR.mkdir(exist_ok=True)


def fetch_usernames() -> List[str]:
    """Return a list of all registered user names."""
    resp = make_api_request("get_usernames")
    if resp.status_code != 200:
        app_logger.warning("Failed to fetch usernames: %s", resp.status_code)
        return []
    data = json.loads(resp.text)
    return data.get("EntityArray", [])


def name_to_personid(name: str) -> str:
    resp = make_api_request("name_to_personid", {"fullName": name}, method="POST")
    data = json.loads(resp.text)
    return data.get("PersonId")


def get_consumption(person_id: str) -> Dict[str, int]:
    resp = make_api_request("get_consumption", {"personId": person_id}, method="POST")
    if resp.status_code != 200:
        app_logger.warning("Failed to get consumption for %s", person_id)
        return {}
    return json.loads(resp.text)


def generate_invoice_pdf(user_name: str, consumption: Dict[str, int], month: str) -> Path:
    """Generate invoice PDF and return its path."""
    from reportlab.lib.pagesizes import A4
    from reportlab.pdfgen import canvas
    from reportlab.lib.units import mm

    file_name = f"invoice_{user_name.replace(' ', '_')}_{month}.pdf"
    pdf_path = INVOICE_DIR / file_name

    c = canvas.Canvas(str(pdf_path), pagesize=A4)
    width, height = A4

    c.setFont("Helvetica-Bold", 16)
    c.drawString(20 * mm, height - 30 * mm, "Schülerfirma Monthly Invoice")
    c.setFont("Helvetica", 12)
    c.drawString(20 * mm, height - 40 * mm, f"Customer: {user_name}")
    c.drawString(20 * mm, height - 50 * mm, f"Month: {month}")

    y = height - 70 * mm
    total = 0.0
    c.setFont("Helvetica-Bold", 12)
    c.drawString(20 * mm, y, "Drink")
    c.drawString(80 * mm, y, "Amount")
    c.drawString(120 * mm, y, "Price")
    c.drawString(150 * mm, y, "Sum")
    c.setFont("Helvetica", 12)
    y -= 10 * mm

    for drink in ["coffee", "tea", "chocolate", "juices", "water"]:
        amount = consumption.get(drink.capitalize(), 0) or consumption.get(drink, 0)
        price = PRICES.get(drink, 0)
        sum_value = amount * price
        total += sum_value
        c.drawString(20 * mm, y, drink.capitalize())
        c.drawString(80 * mm, y, str(amount))
        c.drawString(120 * mm, y, f"{price:.2f} €")
        c.drawString(150 * mm, y, f"{sum_value:.2f} €")
        y -= 8 * mm

    c.setFont("Helvetica-Bold", 12)
    c.drawString(20 * mm, y - 5 * mm, f"Total: {total:.2f} €")
    c.save()
    return pdf_path


def send_invoice(person_id: str, access_token: str, pdf_path: Path) -> bool:
    """Upload invoice and send message via itslearning REST API."""
    try:
        with open(pdf_path, "rb") as f:
            files = {"attachment": (pdf_path.name, f, "application/pdf")}
            upload = requests.post(
                "https://csh.itslearning.com/restapi/personal/instantmessages/attachment/v1",
                params={"access_token": access_token},
                files=files,
                timeout=30,
            )
        if upload.status_code not in (200, 201):
            return False
        file_id = json.loads(upload.text).get("FileId")
        data = {
            "Recipients": [person_id],
            "Text": "Ihre monatliche Rechnung ist angehängt.",
            "Attachments": [{"FileId": file_id}],
        }
        msg = requests.post(
            "https://csh.itslearning.com/restapi/personal/instantmessages/v2",
            params={"access_token": access_token},
            json=data,
            timeout=30,
        )
        return msg.status_code in (200, 201)
    except Exception as ex:
        log_error(ex, {"service": "send_invoice", "person_id": person_id})
        return False


def run_monthly_invoicing(service_user: str, service_password: str) -> None:
    """Generate and send invoices for all users."""
    at, rt, status = None, None, None
    try:
        from api import itslearning as itsl
        at, rt, status = itsl.access_token(service_user, service_password)
        if status not in (200, 201):
            raise RuntimeError("Authentication failed")
    except Exception as ex:
        log_error(ex, {"service": "auth"})
        return

    month_name = datetime.date.today().strftime("%B %Y")

    for name in fetch_usernames():
        try:
            p_id = name_to_personid(name)
            consumption = get_consumption(p_id)
            pdf_path = generate_invoice_pdf(name, consumption, month_name)
            send_invoice(p_id, at, pdf_path)
            app_logger.info(f"Invoice sent to {name}")
        except Exception as ex:
            log_error(ex, {"service": "monthly_invoice", "user": name})

    requests.delete(
        "https://csh.itslearning.com/restapi/oauth2/token/v1",
        params={"access_token": at},
        timeout=30,
    )
