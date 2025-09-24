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
        
        # Add category column to beverages table if it doesn't exist
        try:
            from sqlalchemy import text
            # Check if category column exists
            result = db.session.execute(text("PRAGMA table_info(beverages)"))
            columns = [row[1] for row in result.fetchall()]
            
            if 'category' not in columns:
                print("🔄 Adding category column to beverages table...")
                db.session.execute(text("ALTER TABLE beverages ADD COLUMN category VARCHAR(50) DEFAULT 'drink'"))
                db.session.commit()
                print("✅ Category column added successfully")
            else:
                print("✅ Category column already exists")
        except Exception as e:
            print(f"⚠️  Could not add category column: {e}")
            db.session.rollback()
        
        print("✅ Database tables created/verified on startup")

    return app
