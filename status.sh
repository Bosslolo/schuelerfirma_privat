#!/bin/bash

# Status Check Script
echo "📊 Environment Status Check"
echo "=========================="

# Check if PID files exist and processes are running
check_server() {
    local env_name=$1
    local port=$2
    local pid_file="${env_name}_server.pid"
    
    if [ -f "$pid_file" ]; then
        local pid=$(cat $pid_file)
        if ps -p $pid > /dev/null 2>&1; then
            echo "✅ $env_name: Running (PID: $pid, Port: $port)"
            return 0
        else
            echo "❌ $env_name: PID file exists but process not running"
            rm -f $pid_file
            return 1
        fi
    else
        echo "❌ $env_name: Not running (no PID file)"
        return 1
    fi
}

# Check both environments
echo ""
echo "🔧 Development Environment:"
check_server "dev" "4050"

echo ""
echo "👥 User Environment:"
check_server "user" "4000"

echo ""
echo "🌐 Port Status:"
for port in 4000 4050; do
    if lsof -i:$port > /dev/null 2>&1; then
        echo "✅ Port $port: In use"
    else
        echo "❌ Port $port: Free"
    fi
done

echo ""
echo "🗄️  Database Files:"
for db in user.db local.db; do
    if [ -f "$db" ]; then
        size=$(ls -lh $db | awk '{print $5}')
        echo "✅ $db: $size"
    else
        echo "❌ $db: Not found"
    fi
done

echo ""
echo "📋 All Flask Processes:"
ps aux | grep flask | grep -v grep || echo "   None running"
