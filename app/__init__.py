from flask import Flask
from flask_sqlalchemy import SQLAlchemy
from .config import Config

db = SQLAlchemy()

def create_app():
    app = Flask(__name__)
    app.config.from_object(Config)

    db.init_app(app)

    # Register routes
    from .routes import bp as routes_bp
    app.register_blueprint(routes_bp)

    # Create tables on startup
    with app.app_context():
        db.create_all()
        print("✅ Database tables created/verified on startup")

    return app
