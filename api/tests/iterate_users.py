import requests
import json
import pandas as pd
import random


def send_request(PageIndex, courseId, courseProfileId, searchText, access_token):
    return requests.get(f"https://csh.itslearning.com/restapi/personal/courses/{courseId}/participants/v1"
                 f"?PageIndex={PageIndex}&PageSize=100&courseProfileId={courseProfileId}&searchText={searchText}"
                 f"&access_token={access_token}").text

def create_new_list():
    courseProfileId = input("Would you like to filter for specific user roles? Please enter the RoleId (leave blank if not): ")
    searchText = input("Would you like to search for specific user? Please enter the search text (leave blank if not): ")
    courseId = input("Please enter the course ID (Lernlandschaft 6 is 1083): ")
    access_token = input("Please enter your access token: ")
    access_token = "efrUyUQbyFJIzeylZV6bZMLwhI6lgg1jeGy2xqEBS9evA9MkYErd3bOIDwuU1wTx0OZLT3tFcW1-K-NS8WO5JbdQnf4HmWj6VFjtrniDt7AyDkim1_ZTDt-TXtBqxy-B"

    PageIndex = 0
    req = send_request(PageIndex, courseId, courseProfileId, searchText, access_token)

    with open("users.csv", "w") as csv:
        csv_head = ""
        for key in list(json.loads(req)["EntityArray"][0].keys()):
            csv_head += f"{key},"
        csv.write(csv_head[:-1] + "\n")
    with open("users.csv", "a") as csv:
        for e in json.loads(req)["EntityArray"]:
            csv.write(f"{e['PersonId']},{e['FullName']},{e['LastVisited']},{e['LastVisitedRelative']},{e['PictureUrl']},{e['Role']},{e['RoleId']},{e['CompletedTasks']},{e['TotalTasks']},{e['ExtraInformation']},{e['CanHaveTasks']},{e['Groups']},{e['TemporaryAccess']}\n")

    # Update users
    while(json.loads(req)["EntityArray"] != []):
        PageIndex += 1
        req = send_request(PageIndex, courseId, courseProfileId, searchText, access_token)
        with open("users.csv", "a") as csv:
            for e in json.loads(req)["EntityArray"]:
                csv.write(f"{e['PersonId']},{e['FullName']},{e['LastVisited']},{e['LastVisitedRelative']},{e['PictureUrl']},{e['Role']},{e['RoleId']},{e['CompletedTasks']},{e['TotalTasks']},{e['ExtraInformation']},{e['CanHaveTasks']},{e['Groups']},{e['TemporaryAccess']}\n")

create_new_list()



def insert_users(): 
    with open("usersNEW.csv", "r") as csv: 
        for l in csv: 
            l = l.split(",")
            toDoItems.set(func.SqlRow({"Id": str(uuid.uuid4()), "ItslPersonId": l[0], "PinHash": "",
                                        "FullName": l[1], "PictureUrl": l[4], "Role": l[5], "ExtraInformation": [],
                                        "Banned": 0, "TemporaryAccess": 0, "HasUnpaidInvoices": 0}))