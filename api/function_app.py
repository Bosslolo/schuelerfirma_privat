from datetime import datetime
import azure.functions as func
import logging
from azure.functions.decorators.core import DataType
import pyodbc
import json
import os
import requests
import itslearning as itsl

import Helper as hash

app = func.FunctionApp()

connectionString = os.environ["SQLODBCConnectionString"]

@app.function_name(name="VerifyPin")
@app.route(route="verify_pin", auth_level=func.AuthLevel.ANONYMOUS)
@app.generic_output_binding(arg_name="toDoItems", type="sql", CommandText="dbo.users", ConnectionStringSetting="SqlConnectionString",data_type=DataType.STRING)
def verify_pin(req: func.HttpRequest, toDoItems: func.Out[func.SqlRow]) -> func.HttpResponse:
    logging.info('Python HTTP trigger function processed a request.')
    try: 
        personId = req.get_json().get("personId") 
        fullName = req.get_json().get("fullName")
        pin = req.get_json().get("pin") 
    except ValueError: 
        # HTTP request does not contain valid JSON data
        return func.HttpResponse("Please pass valid data on the query string.", status_code=400)
     
    # Check personId 
    with pyodbc.connect(connectionString) as conn: 
        with conn.cursor() as cursor: 
            if personId and type(personId) is str and len(personId) == 6:
                cursor.execute("SELECT TOP (1) PinHash FROM dbo.users WHERE ItslPersonId=?", personId)
                row = cursor.fetchone()
            elif fullName and type(fullName) is str:
                cursor.execute("SELECT TOP (1) PinHash FROM dbo.users WHERE FullName=?", fullName)
                row = cursor.fetchone()
            else:
                return func.HttpResponse("Please pass a valid 'personId' or 'fullName' on the query string. Make sure all parameters are formatted correctly (e.g. personId has 6 digits).", status_code=400)

            # Verify pin
            if hash.matchHashedText(row.PinHash, pin):
                return func.HttpResponse("True", status_code=200)
            else:
                return func.HttpResponse("False", status_code=401)

@app.function_name(name="SearchName")
@app.route(route="search_name", auth_level=func.AuthLevel.FUNCTION)
@app.generic_output_binding(arg_name="toDoItems", type="sql", CommandText="dbo.users", ConnectionStringSetting="SqlConnectionString",data_type=DataType.STRING)
def search_name(req: func.HttpRequest, toDoItems: func.Out[func.SqlRow]) -> func.HttpResponse:
    """Return the first 5 names of users that BEGIN with the given searchText string."""
    try: 
        searchText = req.get_json().get("searchText") 
    except ValueError: 
        # HTTP request does not contain valid JSON data
        return func.HttpResponse("Please pass valid data on the query string.", status_code=400)
    
    if not searchText: 
        return func.HttpResponse("Please pass a valid 'fullName' on the query string.", status_code=400)
    with pyodbc.connect(connectionString) as conn:
        with conn.cursor() as cur:
            cur.execute("SELECT TOP (5) FullName FROM dbo.users WHERE FullName LIKE ?", searchText + "%")
            rows = cur.fetchall()
            results = {
                "EntityArray": [r.FullName for r in rows],
                "Gesamt": len(rows)
            }
            results = json.dumps(results)
            return func.HttpResponse(results, status_code=200)


@app.function_name(name="GetNames")
@app.route(route="get_usernames", auth_level=func.AuthLevel.FUNCTION)
@app.generic_output_binding(arg_name="toDoItems", type="sql", CommandText="dbo.users", ConnectionStringSetting="SqlConnectionString",data_type=DataType.STRING)
def get_usernames(req: func.HttpRequest, toDoItems: func.Out[func.SqlRow]) -> func.HttpResponse:
    """Returns an Entity Array with the names of the top 10,000 registered users."""
    with pyodbc.connect(connectionString) as con:
        with con.cursor() as cur:
            cur.execute("SELECT TOP (10000) FullName FROM dbo.users")
            rows = cur.fetchall()
            results = {
                "EntityArray": [r.FullName for r in rows],
                "Gesamt": len(rows)
            }
            results = json.dumps(results)
            return func.HttpResponse(results, status_code=200)


@app.function_name(name="PersonInformation")
@app.route(route="person_information", auth_level=func.AuthLevel.FUNCTION)
@app.generic_output_binding(arg_name="toDoItems", type="sql", CommandText="dbo.users", ConnectionStringSetting="SqlConnectionString",data_type=DataType.STRING)
def person_information(req: func.HttpRequest, toDoItems: func.Out[func.SqlRow]) -> func.HttpResponse:
    try: 
        personId = req.get_json().get("personId") 
        fullName = req.get_json().get("fullName") 
    except ValueError: 
        # HTTP request does not contain valid JSON data
        return func.HttpResponse("Please pass valid data on the query string.", status_code=400)
    
    with pyodbc.connect(connectionString) as conn:
        with conn.cursor() as cur:
            if personId and type(personId) is str:
                cur.execute(
                    "SELECT TOP (1) ItslPersonId, FullName, PictureUrl, Role, ExtraInformation, Banned, TemporaryAccess, HasUnpaidInvoices "
                    "FROM dbo.users WHERE ItslPersonId = ?",
                    personId,
                )
            elif fullName and type(fullName) is str:
                cur.execute(
                    "SELECT TOP (1) ItslPersonId, FullName, PictureUrl, Role, ExtraInformation, Banned, TemporaryAccess, HasUnpaidInvoices "
                    "FROM dbo.users WHERE FullName = ?",
                    fullName,
                )
            else: 
                return func.HttpResponse("Please pass a valid 'personId' or 'fullName' on the query string.", status_code=400)
            
            row = cur.fetchone() 
            
            if row is None: 
                return func.HttpResponse("User does not exist. Please check name and spelling.", status_code=404)
            results = {
                "PersonId": row.ItslPersonId,
                "FullName": row.FullName, 
                "PictureUrl": row.PictureUrl, 
                "Role": row.Role, 
                "ExtraInformation": row.ExtraInformation, 
                "Banned": row.Banned, 
                "TemporaryAccess": row.TemporaryAccess, 
                "HasUnpaidInvoices": row.HasUnpaidInvoices
            }
            results = json.dumps(results)
            return func.HttpResponse(results, status_code=200)
        

@app.function_name(name="FullNameToPersonId")
@app.route(route="name_to_personid", auth_level=func.AuthLevel.FUNCTION)
@app.generic_output_binding(arg_name="toDoItems", type="sql", CommandText="dbo.users", ConnectionStringSetting="SqlConnectionString",data_type=DataType.STRING)
def name_to_personid(req: func.HttpRequest, toDoItems: func.Out[func.SqlRow]) -> func.HttpResponse:
    try: 
        name = req.get_json().get("fullName") 
    except ValueError: 
        # HTTP request does not contain valid JSON data
        return func.HttpResponse("Please pass valid data on the query string.", status_code=400)
    
    if not name or type(name) is not str: 
        return func.HttpResponse("Please pass a valid 'fullName' on the query string.", status_code=400)
    with pyodbc.connect(connectionString) as con:
        with con.cursor() as cur:
            cur.execute(
                "SELECT TOP (1) FullName, ItslPersonId, Role FROM dbo.users WHERE FullName=?",
                name,
            )
            row = cur.fetchone()
            if row is None: 
                return func.HttpResponse("User not found. Please check name and spelling.", status_code=404)
            result = { "FullName": str(row.FullName), "PersonId": str(row.ItslPersonId), "Role": str(row.Role) }
            result = json.dumps(result)
            return func.HttpResponse(result, status_code=200)

@app.function_name(name="GetConsumption") 
@app.route(route="get_consumption", auth_level=func.AuthLevel.FUNCTION)
@app.generic_output_binding(arg_name="toDoItems", type="sql", CommandText="dbo.users", ConnectionStringSetting="SqlConnectionString",data_type=DataType.STRING)
def get_consumption(req: func.HttpRequest, toDoItems: func.Out[func.SqlRow]) -> func.HttpResponse:
    """Returns the consumption for the requested user for the current month. If no record for the requested user is found, returns 404 Not Found. Values can be both NULL or 0."""
    try: 
        personId = req.get_json().get("personId") 
    except ValueError: 
        # HTTP request does not contain valid JSON data
        return func.HttpResponse("Please pass valid data on the query string.", status_code=400)
    
    if not personId or type(personId) is not str: 
        return func.HttpResponse("Please pass a valid 'personId' on the query string.", status_code=400)
    with pyodbc.connect(connectionString) as conn:
        with conn.cursor() as cur:
            cur.execute(
                "SELECT TOP (1) ItslPersonId, Coffee, Tea, Chocolate, Water, Juices FROM dbo.consumption WHERE ItslPersonId=?",
                personId,
            )
            row = cur.fetchone()
            if row is None: 
                cur.execute("INSERT INTO dbo.consumption (ItslPersonId, Coffee, Chocolate, Tea, Juices, Water) VALUES (?, 0, 0, 0, 0, 0)", personId)
                return func.HttpResponse("No record for the requested user found. Created user. Send request again to get desired output.", status_code=201)
            result = { "PersonId": str(row.ItslPersonId), "Month": datetime.now().month, "Coffee": row.Coffee, "Tea": row.Tea, "Chocolate": row.Chocolate, "Water": row.Water, "Juices": row.Juices }
            result = json.dumps(result) 
            return func.HttpResponse(result, status_code=200)
        

@app.function_name(name="AddConsumption")
@app.route(route="add_consumption", auth_level=func.AuthLevel.FUNCTION)
@app.generic_output_binding(arg_name="toDoItems", type="sql", CommandText="dbo.users", ConnectionStringSetting="SqlConnectionString",data_type=DataType.STRING)
def add_consumption(req: func.HttpRequest, toDoItems: func.Out[func.SqlRow]) -> func.HttpResponse:
    """Adds the sent consumption to the database entry of the respective user. If no user with that personId was found, returns 404 Not Found."""
    try: 
        personId = req.get_json().get("personId") 
        
        coffee = req.get_json().get("coffee")
        tea = req.get_json().get("tea")
        choco = req.get_json().get("chocolate")
        water = req.get_json().get("water")
        juices = req.get_json().get("juices")
    except ValueError: 
        # HTTP request does not contain valid JSON data
        return func.HttpResponse("Please pass valid data on the query string.", status_code=400)
    except AttributeError: 
        return func.HttpResponse("Please pass valid data on the query string.", status_code=400)
    # logging.info(personId, coffee, tea, choco, water, juices)
    
    if not personId or type(personId) is not str: 
        return func.HttpResponse("Please pass a valid 'personId' on the query string.", status_code=400)
    with pyodbc.connect(connectionString) as con: 
        with con.cursor() as cur: 
            try: 
                cur.execute("MERGE INTO dbo.consumption AS t USING (SELECT ? AS ItslPersonId, ? AS Coffee, ? AS Tea, ? AS Chocolate, ? AS Water, ? AS Juices) AS s ON t.ItslPersonId = s.ItslPersonId WHEN MATCHED THEN UPDATE SET t.Coffee = s.Coffee, t.Tea = s.Tea, t.Chocolate = s.Chocolate, t.Water = s.Water, t.Juices = s.Juices WHEN NOT MATCHED THEN INSERT (ItslPersonId, Coffee, Tea, Chocolate, Water, Juices) VALUES (s.ItslPersonId, s.Coffee, s.Tea, s.Chocolate, s.Water, s.Juices);", personId, coffee, tea, choco, water, juices)
                return func.HttpResponse("Update successfull.", status_code=200)
            except ValueError:
                return func.HttpResponse("Please check if all values are passed correctly.", status_code=500)



@app.function_name(name="GetTotalConsumption")
@app.route(route="get_total_consumption", auth_level=func.AuthLevel.FUNCTION)
@app.generic_output_binding(arg_name="toDoItems", type="sql", CommandText="dbo.users", ConnectionStringSetting="SqlConnectionString",data_type=DataType.STRING)
def get_total_consumption(req: func.HttpRequest, toDoItems: func.Out[func.SqlRow]) -> func.HttpResponse:
    """Return the total consumption across all users."""
    with pyodbc.connect(connectionString) as con:
        with con.cursor() as cur:
            cur.execute(
                "SELECT SUM(Coffee) AS Coffee, SUM(Tea) AS Tea, SUM(Chocolate) AS Chocolate, SUM(Water) AS Water, SUM(Juices) AS Juices FROM dbo.consumption"
            )
            row = cur.fetchone()
            if row is None:
                result = {"Coffee": 0, "Tea": 0, "Chocolate": 0, "Water": 0, "Juices": 0}
            else:
                result = {
                    "Coffee": int(row.Coffee or 0),
                    "Tea": int(row.Tea or 0),
                    "Chocolate": int(row.Chocolate or 0),
                    "Water": int(row.Water or 0),
                    "Juices": int(row.Juices or 0),
                }
            return func.HttpResponse(json.dumps(result), status_code=200)

@app.function_name(name="GetReport")
@app.route(route="get_report", auth_level=func.AuthLevel.FUNCTION)
def get_report(req: func.HttpRequest) -> func.HttpResponse:
    """Return aggregated drink data for a custom date range."""
    try:
        body = req.get_json()
        start_date = body.get("start_date")
        end_date = body.get("end_date")
    except ValueError:
        return func.HttpResponse("Please pass valid data on the query string.", status_code=400)

    if not start_date or not end_date:
        return func.HttpResponse("Please pass 'start_date' and 'end_date'.", status_code=400)

    try:
        from datetime import datetime
        start_dt = datetime.fromisoformat(start_date)
        end_dt = datetime.fromisoformat(end_date)
    except ValueError:
        return func.HttpResponse("Invalid date format. Use YYYY-MM-DD", status_code=400)

    with pyodbc.connect(connectionString) as conn:
        with conn.cursor() as cur:
            cur.execute(
                """SELECT ItslPersonId, DrinkType, COUNT(*) AS Cnt
                       FROM dbo.drink_entries
                       WHERE Timestamp BETWEEN ? AND ?
                       GROUP BY ItslPersonId, DrinkType""",
                start_dt,
                end_dt,
            )
            row = cur.fetchone()
            results = []
            while row:
                results.append(
                    {
                        "PersonId": row.ItslPersonId,
                        "DrinkType": row.DrinkType,
                        "Count": row.Cnt,
                    }
                )
                row = cur.fetchone()
            return func.HttpResponse(json.dumps(results), status_code=200)            

@app.function_name(name="LoginItsl") 
@app.route(route="itsl_login", auth_level=func.AuthLevel.FUNCTION)
@app.generic_output_binding(arg_name="toDoItems", type="sql", CommandText="dbo.users", ConnectionStringSetting="SqlConnectionString",data_type=DataType.STRING)
def login_itsl(req: func.HttpRequest, toDoItems: func.Out[func.SqlRow]) -> func.HttpResponse:
    """"""
    try: 
        username = req.get_json().get("username") 
        print(username)
        password = req.get_json().get("password")
        print(password)
    except ValueError: 
        return func.HttpResponse("Please pass valid data on the query string.", status_code=400)
    except AttributeError: 
        return func.HttpResponse("Please pass valid data on the query string.", status_code=400)

    if not username or type(username) is not str: 
        return func.HttpResponse("Please pass a valid 'username' on the query string.", status_code=400) 
    if not password or type(password) is not str: 
        return func.HttpResponse("Please pass a valid 'password' on the query string.", status_code=400) 
    
    # Get itslearning api access token.
    at = itsl.access_token(username, password) # type: ignore
    print(at)
    # Get user's information 
    # at = "0HI_nkfheLcyFldayrVJJxGvS7vKFlhR2-LLZLZmO2iIdsZj7LKgbow-I7s-2Ul8YxUdV-S3QAKQS5QX-BQ7h83wJbhzvinlVkud2SmOyYKnCEkIjqPTTyO23qDAvpTQ"
    info = itsl.person_information(at[0])

    # Check if user is registered with us. 
    with pyodbc.connect(connectionString) as con:
        with con.cursor() as cur:
            try:
                pId = info["PersonId"]
                cur.execute(
                    "SELECT TOP (1) 1 FROM [dbo].[users] WHERE [ItslPersonId]=? AND [Role]='Administrator'",
                    pId,
                )
                row = cur.fetchone()
                if row is None: 
                    return func.HttpResponse("No record for the requested user found. Probably, you are not yet registered with us. " \
                    "Please write a itslearning message to one of the following people to get you registered. Thank for your interest in the Schülerfirma API!<br>" \
                    "Niklas Dinkel, Julian Domening, Merlin Grahl or Fabian Schuler", status_code=401)
                response = {
                    "FullName": info["FullName"], 
                    "PersonId": info["PersonId"], 
                    "CanReceiveMsg": info["CanAccessInstantMessageSystem"], 
                    "access_token": at[0], 
                    "refresh_token": at[1], 
                    "registered": True
                }
                return func.HttpResponse(json.dumps(response), status_code=200)
            except ValueError: 
                return func.HttpResponse("Please check if all values are passed correctly.", status_code=500)

    
