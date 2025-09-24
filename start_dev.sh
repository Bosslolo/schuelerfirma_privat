#!/bin/bash

# Development Server Start Script
echo "🚀 Starting Flask Development Server..."

# Set environment variables
export FLASK_ENV=development
export FLASK_DEBUG=1
export FLASK_APP=app/wsgi.py
export DATABASE_URL=sqlite:///user.db

# Kill any existing Flask processes
pkill -f flask 2>/dev/null || true

# Start Flask server
echo "📡 Server will be available at:"
echo "   - http://localhost:4050"
echo "   - http://127.0.0.1:4050"
echo "   - http://0.0.0.0:4050"
echo ""
echo "🔧 Dev functions are enabled"
echo "📱 Press Ctrl+C to stop the server"
echo ""

# Start the server
python3 -m flask run --host=0.0.0.0 --port=4050 --debug
