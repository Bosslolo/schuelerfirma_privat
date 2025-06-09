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

def calculate_points(consumption: dict) -> int:
    """Calculate total reward points based on consumption data.

    Points are awarded per drink consumed. The function sums all numeric
    values in the consumption dictionary except for metadata fields.

    Args:
        consumption (dict): Data returned from the API ``get_consumption``
            endpoint.

    Returns:
        int: Total number of points for the current dataset.
    """

    ignore_keys = {"PersonId", "Month", "UserName", "MonthName", "UserPoints"}
    points = 0
    for key, value in consumption.items():
        if key in ignore_keys:
            continue
        if isinstance(value, (int, float)):
            points += int(value)
    return points
  
# --- Cost Tracking Utilities ---

# Default drink prices used for cost calculations (EUR).
DRINK_PRICES = {
    "coffee": 1.00,
    "chocolate": 0.50,
    "tea": 0.50,
    "juices": 1.50,
    "water": 1.50,
}


def calculate_cost(consumption: dict) -> float:
    """Calculate the total cost from a consumption mapping.

    Parameters
    ----------
    consumption: dict
        Mapping of drink type to amount consumed. Keys are compared in
        lower case. Unknown drink types are ignored.

    Returns
    -------
    float
        Total cost rounded to two decimals.
    """

    total = 0.0
    for drink, amount in consumption.items():
        try:
            amount_val = float(amount)
        except (TypeError, ValueError):
            continue
        price = DRINK_PRICES.get(str(drink).lower())
        if price is not None:
            total += price * amount_val

    return round(total, 2)
