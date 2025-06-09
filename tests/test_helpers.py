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
