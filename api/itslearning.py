import json
import logging
from json import JSONDecodeError

import requests

version = '1.1.1'

#logging.basicConfig(level=logging.DEBUG, format='%(asctime)s - %(levelname)s - %(message)s', encoding='utf-8',
#                    filename='itslearning.log', filemode='itslcmd_itslearning_module.log')

# setup(name='itslearning-tools',
#       version=version,
#       packages=find_packages(),
#       description='Module to access the itslearning Rest-API',
#       author='Fabian Schuler',
#       author_email='schulerfabian327@gmail.com',
#       url='https://www.fabianschuler.com/itslearning',
#       license='MIT',
#       install_requires=['requests', 'setuptools', 'json']
# )

success_codes = [200, 201, 202, 203, 204, 205, 206, 207, 208, 226]
error_codes = [400, 401, 403, 404, 405, 406, 407, 408, 409, 410, 411,
               412, 413, 414, 415, 416, 417, 418, 421, 422, 423, 424,
               500, 501, 502, 503, 504, 505, 506, 507, 508, 510, 511,
               520, 521, 522, 523, 524, 525, 526, 527, 530, 531, 532,
               533, 534, 535, 536, 537, 538, 539, 540, 541, 542, 543]


def check_status_code(status_code):
    if status_code in success_codes:
        return True
    elif status_code in error_codes:
        return False
    else:
        return False


def access_token(username=str, password=str, client_id="10ae9d30-1853-48ff-81cb-47b58a325685", grant_type="password"):
    """Erstellt einen Zugriffstoken für die itslearning Rest-API.

    https://www.itslearning.com/restapi/help/Api/POST-restapi-oauth2-token"""
    global initial_request
    try:
        # Zugriffstoken für die itslearning Rest-API erstellen
        initial_request = requests.post("https://csh.itslearning.com/restapi/oauth2/token",
                                        data={'client_id': client_id,
                                              'grant_type': grant_type, 'username': username,
                                              'password': password})
        request = json.loads(initial_request.text)
        at = request["access_token"]
        refresh_token = request["refresh_token"]
        if check_status_code(initial_request.status_code):
            print(at)
            return at, refresh_token, initial_request.status_code
        else:
            return initial_request.status_code
    except ConnectionError:
        return initial_request.status_code
    except KeyError:
        return initial_request.status_code
    except TypeError:
        return initial_request.status_code

# def access_token(username = str, password = str, client_id = "10ae9d30-1853-48ff-81cb-47b58a325685", grant_type = "password"):
#     return "123456789123456789123456789123456789somesampleaccesstoken", "123456789123456789123456789123456789somesamplerefreshtoken", 200

def password_reset_link(email: str):
    """Sendet einen Link zum Zurücksetzen des Passworts an die angegebene E-Mail-Adresse.

    https://csh.itslearning.com/ForgottenPassword"""
    try:
        # Link zum Zurücksetzen des Passworts erstellen
        url = "https://csh.itslearning.com/restapi/forgottenpassword/api/email"

        payload = json.dumps({
            "email": email
        })
        headers = {
            'Content-Type': 'application/json',
            'Cookie': 'AWSALBTG=nIwthpRqW+oAgBQPjmHKJOAl3+CEhUpLDE0E6EgqH7uO45SYecpTgRdh1zlaMpmF+1GnEXQIm2JAEMQfuY4IiHq'
                      'XKFNQNX0XGY5taKbqcRSH+qBJ1CMxZidhRpGAI6MBLTXGg/D2ckrNieAIW0fpMBzRTr+Vn7SxPbcxd/Op67aF67DfDYU=; A'
                      'WSALBTGCORS=nIwthpRqW+oAgBQPjmHKJOAl3+CEhUpLDE0E6EgqH7uO45SYecpTgRdh1zlaMpmF+1GnEXQIm2JAEMQfuY4I'
                      'iHqXKFNQNX0XGY5taKbqcRSH+qBJ1CMxZidhRpGAI6MBLTXGg/D2ckrNieAIW0fpMBzRTr+Vn7SxPbcxd/Op67aF67DfDYU='
        }

        response = requests.request("POST", url, headers=headers, data=payload)
        if check_status_code(response.status_code):
            return True
        else:
            return False, response.status_code
    except ConnectionError:
        return False


def refresh_token(refresh_token:str, client_id="10ae9d30-1853-48ff-81cb-47b58a325685", grant_type="refresh_token"):
    """Erstellt einen neuen Zugriffstoken für die itslearning Rest-API aufgrund des alten Zugriffstokens.

    https://www.itslearning.com/restapi/help/Api/POST-restapi-oauth2-token"""
    try:
        request = json.loads(
            requests.post("https://csh.itslearning.com/restapi/oauth2/token", data={'client_id': client_id,
                                                                                    'grant_type': grant_type,
                                                                                    'refresh_token': refresh_token}).text)
        access_token = request["access_token"]
        ref_token = request["refresh_token"]
        if check_status_code(request.status_code):
            return access_token, ref_token
    except Exception:
        return False


def sso_url(access_token:str, url:str):
    """Erstellt eine Single-Sign-On-URL für das itslearning Dashboard.

    https://www.itslearning.com/restapi/help/Api/GET-restapi-personal-sso-url-v1_url"""

    if url.startswith("https://csh.itslearning.com/") == False:
        raise NameError('Url does not start with "https://csh.itslearning.com/".')

    global sso_url_request
    try:
        # Single-Sign-On-URL zum itslearning Dashboard erstellen
        sso_url_request = requests.get(
            "https://csh.itslearning.com/restapi/personal/sso/url/v1?url=" + url + "&access_token=" + access_token)
        if check_status_code(sso_url_request.status_code):
            return json.loads(sso_url_request.text)['Url'], sso_url_request.status_code
    except ConnectionError:
        return False, sso_url_request.status_code
    except KeyError:
        return False, sso_url_request.status_code
    except TypeError:
        return False, sso_url_request.status_code
    except NameError:
        return False, str(NameError)


def persnotifications_unread(access_token:str):
    """Enthält den Wert der ungelesenen Benachrichtigungen von itslearning.

    https://www.itslearning.com/restapi/help/Api/GET-restapi-personal-notifications-unread-count-v1"""
    try:
        # Die Anzahl der ungelesenen Benachrichtigungen holen
        notif_unread_count = json.loads(
            requests.get("https://csh.itslearning.com/restapi/personal/notifications/unread/count/v1"
                         "?access_token=" + access_token).text)
        if check_status_code(notif_unread_count.status_code):
            return notif_unread_count
    except ConnectionError:
        return False


psnot_ur = persnotifications_unread


def persnotifications_seenmark(access_token:str):
    """Markiert alle Benachrichtigungen von itslearning als gesehen.

    https://www.itslearning.com/restapi/help/Api/PUT-restapi-personal-notifications-seenmark-all-v1"""
    try:
        # Alle Benachrichtigunen als gelesen markieren
        notification_seenmark = json.loads(
            requests.put("https://csh.itslearning.com/restapi/personal/notifications/seenmark/all/v1"
                         "?access_token=" + access_token, data=None).text)
        if check_status_code(notification_seenmark.status_code):
            return notification_seenmark
    except ConnectionError:
        return False


psnot_sm = persnotifications_seenmark


def messagethreads_unread(access_token:str):
    """Enthält den Wer der ungelesenen Nachrichten (Instant Messages) von itslearning.

    https://www.itslearning.com/restapi/help/Api/GET-restapi-personal-instantmessages-messagethreads-unread-count-v1"""
    try:
        # Die Anzahl der ungelesenen Nachrichten holen
        count = json.loads(
            requests.get("https://csh.itslearning.com/restapi/personal/instantmessages/messagethreads/unread/count/v1"
                         "?access_token=" + access_token).text)
        if check_status_code(count.status_code):
            return count
    except ConnectionError:
        return False


msgth_ur = messagethreads_unread


def person_information(access_token: str): 
    """Get the currently signed in user's profile details. 
    https://www.itslearning.com/restapi/help/Api/GET-restapi-personal-person-v1"""
    try: 
        # Informationen holen 
        info = json.loads(
            requests.get("https://csh.itslearning.com/restapi/personal/person/v1" \
            f"?access_token={access_token}").text)
        return info
    except ConnectionError: 
        return False
    
if __name__ == "__main__":
    # Example usage for manual testing
    at = "N1wbgIw4YOkeWNTN1VvKIwAMJE2jqwKBNXaScck7K2N-Wh2Uz5wpg6Qat-Vxviwu-xRTkP516KQIUrE2EGGptt_j0d5TSeuApmL9XqrfDglgXvG0b_YWqTAVk7yxdLQ9"
    print(person_information(at))
