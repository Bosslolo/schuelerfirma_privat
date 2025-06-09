from flask import Flask, render_template, request, redirect, make_response, send_from_directory, Response
import requests
import json
import datetime
from cryptography.fernet import Fernet
import helpers
from json import JSONDecodeError
import os
import time
from typing import Dict, List, Union, Optional, Any, Tuple
from dataclasses import dataclass
from cachetools import TTLCache
from config import config
from logging_config import (
    logger_setup, app_logger, api_logger, auth_logger, error_logger,
    log_api_call, log_authentication_attempt, log_error
)

# Type definitions for better code clarity
@dataclass
class UserData:
    """Represents user data structure."""
    full_name: str
    person_id: Optional[str] = None
    pin: Optional[str] = None

@dataclass
class ConsumptionData:
    """Represents consumption data structure."""
    person_id: str
    drink_type: str
    timestamp: datetime.datetime
    user_name: Optional[str] = None
    month_name: Optional[str] = None

app = Flask(__name__, 
           static_folder=str(config.STATIC_FOLDER),
           template_folder=str(config.TEMPLATE_FOLDER))
app.config['SECRET_KEY'] = config.SECRET_KEY
app.config['DEBUG'] = config.DEBUG

# Set up Flask logging
flask_logger = logger_setup.setup_flask_logger(app)

# Simple TTL cache for API responses (5 minutes)
api_cache = TTLCache(maxsize=128, ttl=300)

def make_api_request(endpoint_name: str, json_data: Optional[Dict] = None,
                    method: str = "GET", use_cache: bool = False) -> requests.Response:
    """
    Make an API request with proper logging and error handling.
    
    Args:
        endpoint_name (str): Name of the API endpoint.
        json_data (Optional[Dict]): JSON data to send with the request.
        method (str): HTTP method to use.
        
    Returns:
        requests.Response: The API response.
        
    Raises:
        requests.RequestException: If the API request fails.
    """
    start_time = time.time()
    url = config.api.get_url(endpoint_name)
    cache_key = (endpoint_name,
                 json.dumps(json_data, sort_keys=True) if json_data else None,
                 method.upper())

    if use_cache and cache_key in api_cache:
        app_logger.debug(f"Cache hit for {endpoint_name}")
        return api_cache[cache_key]
    
    try:
        app_logger.info(f"Making API request to {endpoint_name}: {url}")
        
        if method.upper() == "GET":
            response = requests.get(url, json=json_data, timeout=30)
        elif method.upper() == "POST":
            response = requests.post(url, json=json_data, timeout=30)
        else:
            raise ValueError(f"Unsupported HTTP method: {method}")
            
        response_time = time.time() - start_time
        log_api_call(endpoint_name, method, response.status_code, response_time)

        app_logger.info(f"API request completed: {response.status_code} in {response_time:.3f}s")
        if use_cache and response.status_code == 200:
            api_cache[cache_key] = response
        return response
        
    except requests.RequestException as e:
        response_time = time.time() - start_time
        log_error(e, {
            'endpoint': endpoint_name,
            'url': url,
            'method': method,
            'response_time': response_time
        })
        raise

@app.route('/service-worker.js')
def service_worker() -> Response:
    """
    Serve the service worker JavaScript file.
    
    Returns:
        Response: Flask response object with service worker JavaScript file and appropriate headers.
    """
    app_logger.debug("Serving service worker file")
    response = make_response(send_from_directory('static', 'service-worker.js'))
    response.headers['Service-Worker-Allowed'] = '/'
    response.headers['Content-Type'] = 'application/javascript'
    return response

@app.route('/manifest.json')
def manifest() -> Response:
    """
    Serve the web app manifest file.
    
    Returns:
        Response: Flask response object containing the manifest.json file.
    """
    app_logger.debug("Serving manifest file")
    return send_from_directory('static', 'manifest.json')

@app.route("/", methods=["GET"]) 
def welcome() -> str:
    """
    Render the welcome page with a list of registered users.
    
    Makes an API call to fetch registered users and displays them on the index page.
    Handles API errors gracefully by showing an empty user list.
    
    Returns:
        str: Rendered HTML template with user list.
    """
    app_logger.info("Welcome page accessed")
    
    try:
        response = make_api_request("get_usernames", use_cache=True)
        if response.status_code != 200:
            app_logger.warning(f"Failed to fetch users: HTTP {response.status_code}")
            return render_template("index.html", all_users=[])
            
        data = response.json()
        all_users: List[str] = data.get('EntityArray', [])
        
        if not isinstance(all_users, list):
            app_logger.warning("Invalid user data format received from API")
            all_users = []
            
        app_logger.info(f"Successfully loaded {len(all_users)} users")
        
    except Exception as e:
        log_error(e, {'route': 'welcome', 'action': 'fetch_users'})
        all_users = []
        
    return render_template("index.html", all_users=all_users)

@app.route('/search', methods=['GET', 'POST'])
def search() -> str:
    """
    Handle user search functionality.
    
    Searches for users based on the provided query parameter and displays results.
    
    Returns:
        str: Rendered HTML template with search results.
    """
    query: Optional[str] = request.args.get('query')
    client_ip = request.environ.get('HTTP_X_FORWARDED_FOR', request.environ.get('REMOTE_ADDR', 'unknown'))
    
    app_logger.info(f"Search request from {client_ip} with query: {query}")
    
    if not query:
        app_logger.warning("Empty search query received")
        return render_template('index.html', results=[], searched=True)

    try:
        response = make_api_request("search_name", {"searchText": str(query)})
        data = response.json()
        results: List[Dict[str, Any]] = data.get('EntityArray', [])
        
        app_logger.info(f"Search completed: {len(results)} results for query '{query}'")
        
    except Exception as e:
        log_error(e, {
            'route': 'search',
            'query': query,
            'client_ip': client_ip
        })
        results = []
        
    return render_template('index.html', results=results, searched=True)

@app.route('/verify', methods=['POST'])
def verify_pin() -> Union[str, Response]:
    """
    Verify user PIN and display consumption data if successful.
    
    Args:
        selected_user (str): The full name of the selected user
        pin (str): The PIN code entered by the user
        
    Returns:
        Union[str, Response]: Either the consumption data page or an error message
    """
    selected_user: str = request.form.get('selected_user', '').strip()
    pin: str = request.form.get('pin', '').strip()
    if not selected_user or not pin.isdigit():
        app_logger.warning('Invalid verification input received')
        return Response(
            'Missing user or PIN. <a href="./">Back</a>',
            status=400
        )
    client_ip = request.environ.get('HTTP_X_FORWARDED_FOR', request.environ.get('REMOTE_ADDR', 'unknown'))

    app_logger.info(f"PIN verification attempt for user: {selected_user}")

    try:
        response = make_api_request("verify_pin", {"fullName": selected_user, "pin": pin})
        
        if response.text == "True":
            log_authentication_attempt(selected_user, True, client_ip)
            app_logger.info(f"PIN verification successful for user: {selected_user}")
            
            # Get person ID
            person_id_response = make_api_request("name_to_personid", {"fullName": selected_user}, use_cache=True)
            person_data = person_id_response.json()
            person_id: str = person_data["PersonId"]
            
            # Get consumption data
            consump_response = make_api_request("get_consumption", {"personId": person_id})

            try:
                consumption_data: Dict[str, Any] = consump_response.json()
                # Add user's name and formatted month to the data
                consumption_data['UserName'] = selected_user
                consumption_data['MonthName'] = datetime.datetime.now().strftime('%B').capitalize()
                consumption_data['UserPoints'] = helpers.calculate_points(consumption_data)

                # Calculate current total cost
                counts = {
                    'coffee': consumption_data.get('Coffee', 0),
                    'chocolate': consumption_data.get('Chocolate', 0),
                    'tea': consumption_data.get('Tea', 0),
                    'juices': consumption_data.get('Juices', 0),
                    'water': consumption_data.get('Water', 0),
                }
                consumption_data['TotalCost'] = helpers.calculate_cost(counts)

                app_logger.info(f"Consumption data loaded for user: {selected_user}")
                return render_template(
                    "drinks.html",
                    data=consumption_data,
                    drink_prices=helpers.DRINK_PRICES,
                )
                
            except Exception as ex:
                log_error(ex, {
                    'route': 'verify_pin',
                    'user': selected_user,
                    'action': 'parse_consumption_data'
                })
                return f"<h3>Fehler!</h3> Mehr Informationen: {consump_response} und {ex}"
        else:
            log_authentication_attempt(selected_user, False, client_ip)
            app_logger.warning(f"PIN verification failed for user: {selected_user}")
            return Response(
                f'PIN konnte für {selected_user} nicht verifiziert werden. <a href="./">Zurück</a>',
                status=401
            )
    except Exception as e:
        log_error(e, {
            'route': 'verify_pin',
            'user': selected_user,
            'client_ip': client_ip
        })
        return Response(f"Ein Fehler ist aufgetreten: {str(e)}", status=500)

@app.route("/submit", methods=["POST"])
def handle_submit() -> Union[str, Response]:
    """
    Handle consumption data submission.
    
    Processes the submitted consumption data and sends it to the API.
    
    Returns:
        Union[str, Response]: Redirect to home page on success, error message on failure
    """
    client_ip = request.environ.get('HTTP_X_FORWARDED_FOR', request.environ.get('REMOTE_ADDR', 'unknown'))
    
    try:
        data: Dict[str, Any] = json.loads(request.form.get("data", "{}"))
        app_logger.info(f"Consumption submission from {client_ip}: {data}")
        
        response = make_api_request("add_consumption", data)
        
        if response.status_code == 200:
            app_logger.info("Consumption data submitted successfully")
            return redirect("/")
            
        app_logger.error(f"Failed to submit consumption data: HTTP {response.status_code}")
        return f'Something went wrong while submitting the new data. Here is what you submitted: {data} <a href="./">Back to start page.</a>'
        
    except Exception as e:
        log_error(e, {
            'route': 'handle_submit',
            'client_ip': client_ip,
            'form_data': request.form.to_dict()
        })
        return Response(f"Error processing submission: {str(e)}", status=500)

@app.route("/login", methods=["GET"])
def itsl_login() -> str:
    """
    Handle itslearning login page.
    
    Returns:
        str: Rendered login page or authentication code display
    """
    access: Optional[str] = request.args.get("admin")
    client_ip = request.environ.get('HTTP_X_FORWARDED_FOR', request.environ.get('REMOTE_ADDR', 'unknown'))
    
    app_logger.info(f"Login page accessed from {client_ip}, admin access: {bool(access)}")
    
    if not access:
        return '<html>This feature is not yet implemented. <a href="./">Back to start page.</a></html>'
        
    if "at" in request.cookies and "rt" in request.cookies:
        at: str = request.cookies.get("at", "")
        rt: str = request.cookies.get("rt", "")
        if at and rt:
            app_logger.info("Existing authentication tokens found, displaying admin code")
            return f'''All right! Insert the following value into the Schülerfirma administrator dashboard to authenticate yourself:<br><br>
                    <code>[{at};{rt};{str(datetime.datetime.today())}]</code><br><br>
                    Do not share it with anyone. It can be used to sign in to your itslearning account.<br>
                    <a href="./">Help! The above code is not working.</a>'''
    return render_template('login.html', searched=True)

@app.route("/login_submit", methods=["POST", "GET"])
def itsl_login_submit_data() -> Response:
    """
    Handle itslearning login submission.
    
    Processes login credentials and sets authentication cookies.
    
    Returns:
        Response: Flask response with authentication cookies set
    """
    username: str = request.form.get("username", "").strip()
    password: str = request.form.get("password", "").strip()
    if not username or not password:
        app_logger.warning("Login attempt with missing credentials")
        return make_response(
            "<br>Username and password are required.<br>"
            "<a href='./login'>Back to login.</a>",
            400
        )
    client_ip = request.environ.get('HTTP_X_FORWARDED_FOR', request.environ.get('REMOTE_ADDR', 'unknown'))
    
    app_logger.info(f"itslearning login attempt for user: {username} from {client_ip}")
    
    try:
        response = make_api_request("itsl_login", {"username": username, "password": password})
        infos: Dict[str, str] = json.loads(response.text)
        
        resp = make_response(render_template("login.html"))
        at: str = infos["access_token"]
        rt: str = infos["refresh_token"]
        
        resp.set_cookie("at", str(at))
        resp.set_cookie("rt", str(rt))
        
        log_authentication_attempt(username, True, client_ip)
        app_logger.info(f"itslearning login successful for user: {username}")
        
        return resp
        
    except (ConnectionError, JSONDecodeError) as ex:
        log_authentication_attempt(username, False, client_ip)
        log_error(ex, {
            'route': 'itsl_login_submit',
            'username': username,
            'client_ip': client_ip
        })
        return make_response(
            f"<br>Something went wrong while trying to sign you in. Please check your username and password and try signing in again.<br>"
            f"<a href='./'>Back to start page.</a><br><br>More information: {str(ex)} {str(ex.args)}",
            status=401
        )

@app.route("/stats", methods=["GET"])
def stats() -> str:
    """Display real-time consumption statistics."""
    try:
        response = make_api_request("get_total_consumption")
        data = response.json() if response.status_code == 200 else {}
    except Exception as e:
        log_error(e, {'route': 'stats'})
        data = {}
    return render_template("stats.html", data=data)


@app.route("/stats_data", methods=["GET"])
def stats_data() -> Response:
    """Return consumption statistics as JSON."""
    try:
        response = make_api_request("get_total_consumption")
        return Response(response.text, status=response.status_code, mimetype="application/json")
    except Exception as e:
        log_error(e, {'route': 'stats_data'})
        return Response(status=500)
      
@app.route("/report", methods=["GET", "POST"])
def report() -> str:
    """Display drink report for a custom date range."""
    if request.method == "POST":
        start_date = request.form.get("start_date")
        end_date = request.form.get("end_date")
        try:
            response = make_api_request(
                "get_report",
                {"start_date": start_date, "end_date": end_date},
                method="POST",
            )
            results = response.json()
        except Exception as e:
            log_error(e, {"route": "report", "start": start_date, "end": end_date})
            results = []
        return render_template(
            "report.html", results=results, start_date=start_date, end_date=end_date
        )
    return render_template("report.html", results=None)

@app.route('/admin', methods=['GET'])
def admin_dashboard() -> str:
    """Display the admin dashboard with monthly consumption report."""
    app_logger.info("Admin dashboard accessed")
    try:
        response = make_api_request("admin_report")
        if response.status_code == 200:
            report_data = response.json().get("Report", [])
        else:
            app_logger.warning(
                f"Failed to fetch admin report: HTTP {response.status_code}"
            )
            report_data = []
    except Exception as e:
        log_error(e, {'route': 'admin_dashboard'})
        # Fallback sample data for development
        report_data = [
            {
                'FullName': 'Alice Example',
                'Coffee': 10,
                'HotChocolate': 3,
                'Tea': 2,
                'Total': 15.5,
            },
            {
                'FullName': 'Bob Example',
                'Coffee': 5,
                'HotChocolate': 1,
                'Tea': 0,
                'Total': 6.0,
            },
        ]

    return render_template('admin.html', report=report_data)

@app.errorhandler(404)
def not_found_error(error) -> Response:
    """Handle 404 errors."""
    app_logger.warning(f"404 error: {request.url}")
    return Response("Page not found", status=404)

@app.errorhandler(500)
def internal_error(error) -> Response:
    """Handle 500 errors."""
    log_error(error, {'route': request.endpoint, 'url': request.url})
    return Response("Internal server error", status=500)

if __name__ == '__main__':
    app_logger.info(f"Starting application on {config.HOST}:{config.PORT} (debug={config.DEBUG})")
    app.run(host=config.HOST, port=config.PORT, debug=config.DEBUG)
