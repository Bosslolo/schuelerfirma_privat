#!/bin/bash

# Import database backup and automatically fix all issues
echo "🎯 Import Database and Fix All Issues"
echo "======================================"

# Check if backup file exists
if [ ! -f "./backups/database_backup_complete.sql" ]; then
    echo "❌ Backup file not found at ./backups/database_backup_complete.sql"
    echo "Please ensure your backup file is in the ./backups/ directory"
    exit 1
fi

echo "📁 Found backup file: ./backups/database_backup_complete.sql"

# Check if Docker is running
if ! docker info > /dev/null 2>&1; then
    echo "❌ Docker is not running. Please start Docker Desktop first."
    exit 1
fi

# Stop and remove everything
echo "🛑 Stopping and removing all containers..."
docker-compose -f docker-compose.laptop.yml down -v

# Remove the database volume completely
echo "🗑️ Removing all database data..."
docker volume rm schuelerfirma_privat_pgdata 2>/dev/null || true

# Start fresh
echo "🚀 Starting completely fresh environment..."
./start_laptop.sh

# Wait for database to be ready
echo "⏳ Waiting for database to be ready..."
sleep 30

# Check if database container is running
if ! docker ps | grep -q "schuelerfirma_db"; then
    echo "❌ Database container is not running. Please check Docker setup."
    exit 1
fi

# Convert to UTF-8 if needed
echo "🔄 Converting backup to UTF-8..."
iconv -f UTF-16 -t UTF-8 ./backups/database_backup_complete.sql > ./backups/database_backup_complete_utf8.sql

# Import the data
echo "📥 Importing database backup..."
docker exec -i schuelerfirma_db psql -U user -d db < ./backups/database_backup_complete_utf8.sql

if [ $? -eq 0 ]; then
    echo "✅ Database import successful!"
    
    # Now run the comprehensive fix
    echo "🔧 Running comprehensive fix..."
    ./fix_all_issues.sh
    
    echo ""
    echo "🎉 Import and fix complete!"
    echo ""
    echo "📡 Access your system:"
    echo "   Admin: http://localhost:5001"
    echo "   User:  http://localhost:5002"
    echo "   Database: http://localhost:8080"
    echo ""
    echo "✨ All issues have been automatically fixed!"
    
else
    echo "❌ Import failed. Please check the backup file."
    echo "💡 Ensure the backup file is a plain SQL dump compatible with PostgreSQL."
    exit 1
fi
