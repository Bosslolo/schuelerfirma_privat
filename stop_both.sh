#!/bin/bash

# Stop Both Environments Script
echo "🛑 Stopping both Dev and User Environments..."

# Stop by PID files
if [ -f "dev_server.pid" ]; then
    DEV_PID=$(cat dev_server.pid)
    if ps -p $DEV_PID > /dev/null 2>&1; then
        kill $DEV_PID
        echo "✅ Development server (PID: $DEV_PID) stopped"
    else
        echo "⚠️  Development server was not running"
    fi
    rm -f dev_server.pid
else
    echo "⚠️  No development server PID file found"
fi

if [ -f "user_server.pid" ]; then
    USER_PID=$(cat user_server.pid)
    if ps -p $USER_PID > /dev/null 2>&1; then
        kill $USER_PID
        echo "✅ User server (PID: $USER_PID) stopped"
    else
        echo "⚠️  User server was not running"
    fi
    rm -f user_server.pid
else
    echo "⚠️  No user server PID file found"
fi

# Kill any remaining Flask processes
pkill -f flask 2>/dev/null || true

echo "🧹 Cleanup completed!"
echo "📋 Remaining Flask processes:"
ps aux | grep flask | grep -v grep || echo "   None"


