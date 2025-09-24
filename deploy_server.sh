#!/bin/bash

# Server Deployment Script
echo "🚀 Deploying to Server..."

# Set production environment variables
export FLASK_ENV=production
export FLASK_DEBUG=0
export DATABASE_URL=postgresql://user:password@db:5432/db
export SECRET_KEY=production-secret-key-change-this

echo "📡 Server will be available at:"
echo "   - http://10.100.5.89:5000"
echo ""
echo "🔒 Dev functions are disabled (production mode)"
echo "📱 Press Ctrl+C to stop the server"
echo ""

# Start the server
python3 -m flask run --host=0.0.0.0 --port=5000
