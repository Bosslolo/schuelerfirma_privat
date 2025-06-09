"""
Logging configuration module for the Schülerfirma application.

This module provides centralized logging configuration with different log levels,
formatters, and handlers for development and production environments.
"""
import logging
import logging.handlers
import os
import sys
from pathlib import Path
from typing import Optional, Dict, Any
from datetime import datetime
import json

class JSONFormatter(logging.Formatter):
    """
    Custom JSON formatter for structured logging.
    
    This formatter outputs log records as JSON objects, making them easier
    to parse and analyze in log aggregation systems.
    """
    
    def format(self, record: logging.LogRecord) -> str:
        """
        Format a log record as JSON.
        
        Args:
            record (logging.LogRecord): The log record to format.
            
        Returns:
            str: JSON-formatted log message.
        """
        log_entry = {
            'timestamp': datetime.fromtimestamp(record.created).isoformat(),
            'level': record.levelname,
            'logger': record.name,
            'message': record.getMessage(),
            'module': record.module,
            'function': record.funcName,
            'line': record.lineno,
        }
        
        # Add exception information if present
        if record.exc_info:
            log_entry['exception'] = self.formatException(record.exc_info)
            
        # Add extra fields if present
        if hasattr(record, 'user_id'):
            log_entry['user_id'] = record.user_id
        if hasattr(record, 'request_id'):
            log_entry['request_id'] = record.request_id
        if hasattr(record, 'ip_address'):
            log_entry['ip_address'] = record.ip_address
            
        return json.dumps(log_entry, ensure_ascii=False)

class LoggerSetup:
    """
    Centralized logger setup and configuration.
    
    This class handles the creation and configuration of loggers for different
    parts of the application with appropriate handlers and formatters.
    """
    
    def __init__(self, app_name: str = "schuelerfirma", log_level: str = "INFO"):
        """
        Initialize the logger setup.
        
        Args:
            app_name (str): Name of the application for log identification.
            log_level (str): Default logging level (DEBUG, INFO, WARNING, ERROR, CRITICAL).
        """
        self.app_name = app_name
        self.log_level = getattr(logging, log_level.upper())
        self.log_dir = Path("logs")
        self.log_dir.mkdir(exist_ok=True)
        
    def setup_logger(self, logger_name: str, log_file: Optional[str] = None) -> logging.Logger:
        """
        Set up a logger with appropriate handlers and formatters.
        
        Args:
            logger_name (str): Name of the logger.
            log_file (Optional[str]): Optional specific log file name.
            
        Returns:
            logging.Logger: Configured logger instance.
        """
        logger = logging.getLogger(logger_name)
        logger.setLevel(self.log_level)
        
        # Prevent duplicate handlers
        if logger.handlers:
            return logger
            
        # Console handler for development
        console_handler = logging.StreamHandler(sys.stdout)
        console_handler.setLevel(logging.DEBUG)
        
        # File handler for persistent logging
        if log_file is None:
            log_file = f"{self.app_name}_{logger_name}.log"
        file_handler = logging.handlers.RotatingFileHandler(
            self.log_dir / log_file,
            maxBytes=10*1024*1024,  # 10MB
            backupCount=5
        )
        file_handler.setLevel(logging.INFO)
        
        # Error file handler for errors only
        error_handler = logging.handlers.RotatingFileHandler(
            self.log_dir / f"{self.app_name}_errors.log",
            maxBytes=10*1024*1024,  # 10MB
            backupCount=5
        )
        error_handler.setLevel(logging.ERROR)
        
        # Formatters
        console_formatter = logging.Formatter(
            '%(asctime)s - %(name)s - %(levelname)s - %(message)s'
        )
        file_formatter = logging.Formatter(
            '%(asctime)s - %(name)s - %(levelname)s - %(module)s:%(funcName)s:%(lineno)d - %(message)s'
        )
        json_formatter = JSONFormatter()
        
        # Set formatters
        console_handler.setFormatter(console_formatter)
        file_handler.setFormatter(file_formatter)
        error_handler.setFormatter(json_formatter)
        
        # Add handlers to logger
        logger.addHandler(console_handler)
        logger.addHandler(file_handler)
        logger.addHandler(error_handler)
        
        return logger
    
    def setup_flask_logger(self, app) -> logging.Logger:
        """
        Set up Flask application logger.
        
        Args:
            app: Flask application instance.
            
        Returns:
            logging.Logger: Configured Flask logger.
        """
        # Disable Flask's default logger
        app.logger.handlers.clear()
        
        # Set up custom Flask logger
        flask_logger = self.setup_logger("flask_app", "flask.log")
        app.logger = flask_logger
        
        return flask_logger

# Global logger instances
logger_setup = LoggerSetup(
    app_name="schuelerfirma",
    log_level=os.getenv("LOG_LEVEL", "INFO")
)

# Application loggers
app_logger = logger_setup.setup_logger("app")
api_logger = logger_setup.setup_logger("api", "api_calls.log")
auth_logger = logger_setup.setup_logger("auth", "authentication.log")
error_logger = logger_setup.setup_logger("error", "errors.log")

def log_api_call(endpoint: str, method: str, status_code: int, 
                response_time: float, user_id: Optional[str] = None) -> None:
    """
    Log API call information.
    
    Args:
        endpoint (str): The API endpoint called.
        method (str): HTTP method used.
        status_code (int): Response status code.
        response_time (float): Response time in seconds.
        user_id (Optional[str]): User ID if available.
    """
    extra_data = {
        'endpoint': endpoint,
        'method': method,
        'status_code': status_code,
        'response_time': response_time
    }
    if user_id:
        extra_data['user_id'] = user_id
        
    api_logger.info(f"API Call: {method} {endpoint} - {status_code} ({response_time:.3f}s)", extra=extra_data)

def log_authentication_attempt(username: str, success: bool, 
                             ip_address: Optional[str] = None) -> None:
    """
    Log authentication attempts.
    
    Args:
        username (str): Username attempting authentication.
        success (bool): Whether authentication was successful.
        ip_address (Optional[str]): IP address of the request.
    """
    extra_data = {
        'username': username,
        'success': success
    }
    if ip_address:
        extra_data['ip_address'] = ip_address
        
    level = logging.INFO if success else logging.WARNING
    message = f"Authentication {'successful' if success else 'failed'} for user: {username}"
    auth_logger.log(level, message, extra=extra_data)

def log_error(error: Exception, context: Optional[Dict[str, Any]] = None) -> None:
    """
    Log application errors with context.
    
    Args:
        error (Exception): The exception that occurred.
        context (Optional[Dict[str, Any]]): Additional context information.
    """
    extra_data = {
        'error_type': type(error).__name__,
        'error_message': str(error)
    }
    if context:
        extra_data.update(context)
        
    error_logger.error(f"Application error: {error}", exc_info=True, extra=extra_data) 