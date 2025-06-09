from cryptography.fernet import Fernet
from typing import Union

# From: https://stackoverflow.com/questions/2490334/simple-way-to-encode-a-string-according-to-a-password

def encrypt(message: Union[bytes, str], key: bytes) -> bytes:
    """
    Encrypt a message using Fernet symmetric encryption.
    
    Args:
        message (Union[bytes, str]): The message to encrypt. If a string is provided,
                                    it will be encoded to bytes using UTF-8.
        key (bytes): The encryption key. Must be a valid Fernet key.
    
    Returns:
        bytes: The encrypted message.
        
    Raises:
        cryptography.fernet.InvalidToken: If the key is invalid.
        TypeError: If the message cannot be converted to bytes.
    """
    if isinstance(message, str):
        message = message.encode('utf-8')
    return Fernet(key).encrypt(message)

def decrypt(token: bytes, key: bytes) -> bytes:
    """
    Decrypt a Fernet-encrypted message.
    
    Args:
        token (bytes): The encrypted message to decrypt.
        key (bytes): The encryption key used to encrypt the message.
    
    Returns:
        bytes: The decrypted message.
        
    Raises:
        cryptography.fernet.InvalidToken: If the token is invalid or the key is incorrect.
    """
    return Fernet(key).decrypt(token)