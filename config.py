"""
Configuration module for the Schülerfirma application.

This module contains all API endpoints and configuration settings.
It uses environment variables for sensitive data and provides type-safe access to configuration values.
"""
from typing import Dict, Optional
from dataclasses import dataclass, field
import os
from pathlib import Path
from dotenv import load_dotenv

# Load environment variables from .env file
load_dotenv()

@dataclass
class APIConfig:
    """Configuration for API endpoints and their access codes."""
    base_url: str = field(default_factory=lambda: os.getenv("API_BASE_URL", "https://csh-schuelerfirma.azurewebsites.net/api"))
    
    # API endpoints with their access codes from environment variables
    endpoints: Dict[str, str] = field(default=None)
    
    def __post_init__(self) -> None:
        """Initialize endpoints after dataclass creation to use environment variables."""
        self.endpoints = {
            "get_usernames": f"/get_usernames?code={os.getenv('API_GET_USERNAMES_CODE', '')}",
            "search_name": f"/search_name?code={os.getenv('API_SEARCH_NAME_CODE', '')}",
            "verify_pin": "/verify_pin?",
            "name_to_personid": f"/name_to_personid?code={os.getenv('API_NAME_TO_PERSONID_CODE', '')}",
            "get_consumption": f"/get_consumption?code={os.getenv('API_GET_CONSUMPTION_CODE', '')}",
            "add_consumption": f"/add_consumption?code={os.getenv('API_ADD_CONSUMPTION_CODE', '')}",
            "itsl_login": f"/itsl_login?code={os.getenv('API_ITSL_LOGIN_CODE', '')}",
            "get_report": f"/get_report?code={os.getenv('API_GET_REPORT_CODE', '')}"
        }

    def get_url(self, endpoint_name: str) -> str:
        """
        Get the full URL for an API endpoint.
        
        Args:
            endpoint_name (str): The name of the endpoint as defined in the endpoints dictionary.
            
        Returns:
            str: The complete URL for the endpoint.
            
        Raises:
            KeyError: If the endpoint name is not found in the configuration.
            ValueError: If the required environment variable is not set.
        """
        if endpoint_name not in self.endpoints:
            raise KeyError(f"API endpoint '{endpoint_name}' not found in configuration")
        
        url = f"{self.base_url}{self.endpoints[endpoint_name]}"
        
        # Check if the endpoint has a proper code (not empty)
        if endpoint_name != "verify_pin" and "code=" in url and url.endswith("code="):
            raise ValueError(f"Environment variable for '{endpoint_name}' endpoint is not set or empty")
        
        return url

@dataclass
class LoggingConfig:
    """Configuration for application logging."""
    # Logging levels
    LOG_LEVEL: str = field(default_factory=lambda: os.getenv("LOG_LEVEL", "INFO"))
    
    # Log file settings
    LOG_DIR: Path = field(default_factory=lambda: Path(os.getenv("LOG_DIR", "logs")))
    MAX_LOG_SIZE: int = field(default_factory=lambda: int(os.getenv("MAX_LOG_SIZE", "10485760")))  # 10MB
    LOG_BACKUP_COUNT: int = field(default_factory=lambda: int(os.getenv("LOG_BACKUP_COUNT", "5")))
    
    # Enable/disable specific logging features
    ENABLE_JSON_LOGGING: bool = field(default_factory=lambda: os.getenv("ENABLE_JSON_LOGGING", "True").lower() == "true")
    ENABLE_CONSOLE_LOGGING: bool = field(default_factory=lambda: os.getenv("ENABLE_CONSOLE_LOGGING", "True").lower() == "true")
    ENABLE_FILE_LOGGING: bool = field(default_factory=lambda: os.getenv("ENABLE_FILE_LOGGING", "True").lower() == "true")

@dataclass
class AppConfig:
    """Application-wide configuration settings."""
    # Flask configuration
    SECRET_KEY: str = field(default_factory=lambda: os.getenv("FLASK_SECRET_KEY", "dev-secret-key-change-in-production"))
    DEBUG: bool = field(default_factory=lambda: os.getenv("FLASK_DEBUG", "False").lower() == "true")
    
    # Application settings
    HOST: str = field(default_factory=lambda: os.getenv("APP_HOST", "127.0.0.1"))
    PORT: int = field(default_factory=lambda: int(os.getenv("APP_PORT", "5000")))
    
    # File paths
    STATIC_FOLDER: Path = field(default_factory=lambda: Path(__file__).parent / "static")
    TEMPLATE_FOLDER: Path = field(default_factory=lambda: Path(__file__).parent / "templates")
    
    # Configuration components
    api: APIConfig = field(default_factory=APIConfig)
    logging: LoggingConfig = field(default_factory=LoggingConfig)
    
    def validate_config(self) -> None:
        """
        Validate that all required configuration values are set.
        
        Raises:
            ValueError: If required configuration values are missing.
        """
        required_env_vars = [
            "API_GET_USERNAMES_CODE",
            "API_SEARCH_NAME_CODE", 
            "API_NAME_TO_PERSONID_CODE",
            "API_GET_CONSUMPTION_CODE",
            "API_ADD_CONSUMPTION_CODE",
            "API_ITSL_LOGIN_CODE",
            "API_GET_REPORT_CODE"
        ]
        
        missing_vars = []
        for var in required_env_vars:
            if not os.getenv(var):
                missing_vars.append(var)
        
        if missing_vars:
            raise ValueError(f"Missing required environment variables: {', '.join(missing_vars)}")

# Create a global configuration instance
config = AppConfig()

# Validate configuration on import (optional - you can remove this if you want to handle validation elsewhere)
try:
    config.validate_config()
except ValueError as e:
    print(f"Configuration Warning: {e}")
    print("Please check your .env file and ensure all required variables are set.") 