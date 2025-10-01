# Quick Fix Guide

## 🚨 When You Import a New Database Backup

### **Option 1: Automatic Fix (Recommended)**
```bash
# Just run this one command - it does everything!
./import_and_fix.sh
```

### **Option 2: Manual Fix**
```bash
# If you already imported and have issues
./fix_all_issues.sh
```

## 🔧 What These Scripts Fix

### **Database Issues:**
- ✅ Missing `category` column in beverages table
- ✅ All German character encoding (ö, ü, ä, ß)
- ✅ Complete database schema
- ✅ Essential roles (Teachers, Students, Staff, Guests)

### **Application Issues:**
- ✅ User selection screen not working
- ✅ Dashboard consumption display
- ✅ Character encoding in names
- ✅ Database connection problems

## 📋 Usage Instructions

### **For New Backups:**
1. Copy your `database_backup.sql` to `./backups/database_backup_complete.sql`
2. Run: `./import_and_fix.sh`
3. Done! Everything will work perfectly.

### **For Existing Issues:**
1. Run: `./fix_all_issues.sh`
2. Done! All issues fixed.

## 🎯 What You Get

After running either script:
- **Admin View**: http://localhost:5001 (works perfectly)
- **User View**: http://localhost:5002 (works perfectly)
- **Database**: http://localhost:8080 (all data correct)
- **German Characters**: All umlauts display correctly
- **Top Supporter**: Shows the correct person

## 🚀 No More Manual Fixes!

These scripts handle everything automatically:
- Database schema fixes
- Character encoding
- Missing columns
- Application restarts
- Data verification

**Just run the script and everything works!** 🎉
