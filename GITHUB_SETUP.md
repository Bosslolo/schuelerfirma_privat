# GitHub Setup & Deployment

## 1. Push to GitHub

### Initialize Git (if not already done)
```bash
git init
git add .
git commit -m "Initial commit: Clean beverage consumption system"
```

### Create GitHub Repository
1. Go to GitHub.com
2. Click "New repository"
3. Name: `schuelerfirma_privat`
4. Make it private
5. Don't initialize with README

### Push to GitHub
```bash
git remote add origin https://github.com/YOUR-USERNAME/schuelerfirma_privat.git
git branch -M main
git push -u origin main
```

## 2. Deploy on Server

### Clone on Server
```bash
git clone https://github.com/YOUR-USERNAME/schuelerfirma_privat.git
cd schuelerfirma_privat
```

### Quick Deploy
```bash
# Make scripts executable
chmod +x *.sh

# Deploy the system
./deploy_to_server.sh
```

### Manual Deploy
```bash
# Start the system
./start_laptop.sh

# Or use Docker Compose directly
docker-compose -f docker-compose.laptop.yml up -d
```

## 3. Access Your System

### URLs (replace with your server IP)
- **Admin**: http://YOUR-SERVER-IP:5001
- **User**: http://YOUR-SERVER-IP:5002
- **Database**: http://YOUR-SERVER-IP:8080

### Database Login
- Server: `schuelerfirma_db`
- Username: `user`
- Password: `password`
- Database: `db`

## 4. Import Your Data

### Copy Database Backup
```bash
# Copy your database_backup.sql to ./backups/
mkdir -p backups
# Copy your file here: ./backups/database_backup.sql
```

### Import Data
```bash
./import_complete_data.sh
```

## 5. Management

### Start/Stop
```bash
./start_laptop.sh    # Start
./stop_laptop.sh     # Stop
```

### View Logs
```bash
docker-compose -f docker-compose.laptop.yml logs -f
```

### Update from GitHub
```bash
git pull origin main
docker-compose -f docker-compose.laptop.yml restart
```

## 6. Port Configuration

The system uses these ports:
- **5001**: Admin interface
- **5002**: User interface  
- **8080**: Database admin
- **5432**: PostgreSQL database

If you need different ports, edit `docker-compose.laptop.yml`.

## 7. Features

### Admin View (Port 5001)
- User management
- Beverage management
- Price setting
- Monthly reports
- Historical data

### User View (Port 5002)
- Clean interface
- Current month only
- PIN authentication
- Guest access

### Monthly Reset
- Users see only current month
- Admins see all historical data
- No data is deleted
