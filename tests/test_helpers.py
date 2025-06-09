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


def test_parse_date_range_valid():
    start, end = helpers.parse_date_range("2024-01-01", "2024-01-31")
    assert start.day == 1 and end.day == 31


def test_parse_date_range_invalid():
    import pytest
    with pytest.raises(ValueError):
        helpers.parse_date_range("2024-02-01", "2024-01-01")
