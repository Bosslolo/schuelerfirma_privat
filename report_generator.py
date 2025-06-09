import csv
from datetime import datetime
from pathlib import Path
from typing import List, Dict, Any, Optional

import requests

from config import config


def fetch_usernames(session: requests.Session = requests) -> List[str]:
    """Fetch all registered usernames from the API."""
    url = config.api.get_url("get_usernames")
    response = session.get(url, timeout=30)
    response.raise_for_status()
    data = response.json()
    return data.get("EntityArray", [])


def fetch_person_id(name: str, session: requests.Session = requests) -> str:
    """Resolve a user's full name to a person ID via the API."""
    url = config.api.get_url("name_to_personid")
    response = session.post(url, json={"fullName": name}, timeout=30)
    response.raise_for_status()
    data = response.json()
    return data["PersonId"]


def fetch_consumption(person_id: str, session: requests.Session = requests) -> Dict[str, Any]:
    """Fetch monthly consumption data for a given person ID."""
    url = config.api.get_url("get_consumption")
    response = session.post(url, json={"personId": person_id}, timeout=30)
    response.raise_for_status()
    return response.json()


def generate_monthly_report(output_path: Optional[Path] = None, session: requests.Session = requests) -> Path:
    """Generate a CSV report of monthly consumption for all users."""
    if output_path is None:
        month = datetime.now().strftime("%Y_%m")
        output_path = Path("reports") / f"monthly_report_{month}.csv"

    output_path.parent.mkdir(parents=True, exist_ok=True)

    usernames = fetch_usernames(session=session)
    records: List[Dict[str, Any]] = []
    for name in usernames:
        try:
            person_id = fetch_person_id(name, session=session)
            consumption = fetch_consumption(person_id, session=session)
            consumption["FullName"] = name
            records.append(consumption)
        except Exception:
            # Skip users that cause errors
            continue

    fieldnames = ["FullName", "PersonId", "Month", "Coffee", "Tea", "Chocolate", "Water", "Juices"]
    with output_path.open("w", newline="", encoding="utf-8") as csvfile:
        writer = csv.DictWriter(csvfile, fieldnames=fieldnames)
        writer.writeheader()
        for record in records:
            writer.writerow({k: record.get(k, "") for k in fieldnames})

    return output_path


if __name__ == "__main__":
    path = generate_monthly_report()
    print(f"Report written to {path}")
