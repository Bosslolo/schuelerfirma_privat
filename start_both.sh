#!/bin/bash

# Start Both Environments Script
echo "🚀 Starting both Dev and User Environments..."

# Kill any existing Flask processes
pkill -f flask 2>/dev/null || true

# Function to start environment in background
start_env() {
    local env_name=$1
    local port=$2
    local db_name=$3
    local flask_env=$4
    
    echo "📡 Starting $env_name on port $port with database $db_name..."
    
    # Set environment variables
    export FLASK_ENV=$flask_env
    export FLASK_DEBUG=$([ "$flask_env" = "development" ] && echo "1" || echo "0")
    export FLASK_APP=app/wsgi.py
    export DATABASE_URL=sqlite:///$db_name
    
    # Start in background
    nohup python3 -m flask run --host=0.0.0.0 --port=$port > ${env_name}_server.log 2>&1 &
    
    # Store PID
    echo $! > ${env_name}_server.pid
    
    echo "✅ $env_name started with PID $(cat ${env_name}_server.pid)"
}

# Start both environments
start_env "dev" "4050" "user.db" "development"
start_env "user" "4000" "user.db" "production"

echo ""
echo "🎉 Both environments are now running!"
echo ""
echo "📡 Access URLs:"
echo "   🔧 Development: http://localhost:4050 (with dev functions)"
echo "   👥 User:        http://localhost:4000 (production mode)"
echo ""
echo "📋 Management commands:"
echo "   Stop both:     ./stop_both.sh"
echo "   View logs:     tail -f dev_server.log (or user_server.log)"
echo "   Check status:  ps aux | grep flask"
echo ""
echo "🗄️  Database files:"
echo "   Shared:        user.db (both environments use the same database)"
