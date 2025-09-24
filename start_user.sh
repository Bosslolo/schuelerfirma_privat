#!/bin/bash

# User Environment Start Script
echo "👥 Starting Flask User Environment..."

# Set environment variables for production-like mode
export FLASK_ENV=production
export FLASK_DEBUG=0
export FLASK_APP=app/wsgi.py
export DATABASE_URL=sqlite:///user.db

# Kill any existing Flask processes
pkill -f flask 2>/dev/null || true

# Start Flask server
echo "📡 User interface will be available at:"
echo "   - http://localhost:4000"
echo "   - http://127.0.0.1:4000"
echo "   - http://0.0.0.0:4000"
echo ""
echo "🔒 Dev functions are disabled (production mode)"
echo "📱 Press Ctrl+C to stop the server"
echo ""

# Start the server
python3 -m flask run --host=0.0.0.0 --port=4000
