import sys
from pathlib import Path
sys.path.append(str(Path(__file__).resolve().parents[1]))

import helpers
from cryptography.fernet import Fernet


def test_encrypt_decrypt_roundtrip():
    key = Fernet.generate_key()
    message = "secret message"
    encrypted = helpers.encrypt(message, key)
    decrypted = helpers.decrypt(encrypted, key)
    assert decrypted == message.encode()

def test_calculate_points():
    consumption = {
        "PersonId": "123456",
        "Coffee": 2,
        "Tea": 1,
        "Water": 0,
        "Month": 5,
        "UserName": "Test User"
    }
    assert helpers.calculate_points(consumption) == 3

def test_calculate_cost():
    data = {
        "coffee": 2,
        "tea": 1,
        "chocolate": 0,
        "juices": 3,
        "water": 1,
    }
    expected = (
        helpers.DRINK_PRICES["coffee"] * 2
        + helpers.DRINK_PRICES["tea"] * 1
        + helpers.DRINK_PRICES["juices"] * 3
        + helpers.DRINK_PRICES["water"] * 1
    )
    assert helpers.calculate_cost(data) == round(expected, 2)
