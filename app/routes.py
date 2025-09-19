from flask import Blueprint, jsonify, render_template, request, redirect, url_for, flash
from .models import roles, beverages, users, consumptions, invoices, beverage_prices
from . import db
from datetime import datetime, date
from sqlalchemy import func
from sqlalchemy.exc import IntegrityError
import hashlib

bp = Blueprint("routes", __name__)

def check_invoice_exists(user_id):
    """
    Get or create an invoice for the user and current month.
    Returns the invoice object.
    """
    # Get current month and year
    current_month_year = date.today().replace(day=1)
    
    # Check if invoice already exists for this user and month
    existing_invoice = invoices.query.filter_by(
        user_id=user_id,
        period=current_month_year
    ).first()
    
    if existing_invoice:
        return existing_invoice

    # Create new invoice for current month
    user = users.query.get(user_id)
    if not user:
        raise ValueError(f"User with ID {user_id} not found")
    
    # Count the number of invoices for the current month
    count = invoices.query.filter_by(period=current_month_year).count()

    # Generate unique invoice name: "INV-YYYY-MM_count"
    invoice_name = f"INV-{current_month_year.strftime('%Y-%m')}_{count + 1}"

    new_invoice = invoices(
        user_id=user_id,
        invoice_name=invoice_name,
        status="draft",
        period=current_month_year
    )

    try:
        db.session.add(new_invoice)
        db.session.commit()
        return new_invoice
    except IntegrityError:
        # Another concurrent request likely created the invoice; rollback and fetch it
        db.session.rollback()
        existing_invoice = invoices.query.filter_by(
            user_id=user_id,
            period=current_month_year
        ).first()
        if existing_invoice:
            return existing_invoice
        # If still not found, rethrow a generic error to surface the issue
        raise
    except Exception as e:
        db.session.rollback()
        raise Exception(f"Failed to create invoice: {str(e)}")

def hash_pin(pin):
    """Hash a PIN using SHA-256"""
    return hashlib.sha256(pin.encode()).digest()

def verify_pin(user_id, pin):
    """Verify a PIN against the stored hash"""
    user = users.query.get(user_id)
    if not user or not user.pin_hash:
        return False
    return user.pin_hash == hash_pin(pin)

@bp.route("/")
def index():
    # Get current month start date
    current_month = date.today().replace(day=1)
    
    # Fetch all users with their roles and total consumption for current month
    users_with_consumption = db.session.query(
        users,
        roles,
        func.coalesce(func.sum(consumptions.quantity), 0).label('total_consumption')
    ).join(roles, users.role_id == roles.id)\
     .outerjoin(consumptions, 
                db.and_(consumptions.user_id == users.id,
                       consumptions.created_at >= current_month))\
     .group_by(users.id, roles.id)\
     .order_by(func.coalesce(func.sum(consumptions.quantity), 0).desc())\
     .all()
    
    # Extract just the user objects for template
    sorted_users = [user_role_consumption[0] for user_role_consumption in users_with_consumption]
    
    return render_template("index.html", users=sorted_users)

@bp.route("/entries")
def entries():
    user_id = request.args.get('user_id', type=int)
    
    if not user_id:
        # Redirect to index if no user_id provided
        return redirect(url_for('routes.index'))
    
    # Fetch the specific user with their role
    user = users.query.join(roles, users.role_id == roles.id).filter(users.id == user_id).first()
    
    if not user:
        # Redirect to index if user not found
        return redirect(url_for('routes.index'))
    
    # Fetch user's beverage consumptions with counts per beverage
    consumption_results = db.session.query(
        consumptions.beverage_id,
        func.count(consumptions.id).label('count'),
        func.sum(consumptions.quantity).label('total_quantity')
    ).filter_by(user_id=user_id).group_by(consumptions.beverage_id).all()
    
    # Convert to list of dictionaries for JSON serialization
    user_consumptions = []
    for result in consumption_results:
        user_consumptions.append({
            'beverage_id': result.beverage_id,
            'count': result.count,
            'total_quantity': result.total_quantity
        })
    
    # Fetch all active beverages
    all_beverages = beverages.query.filter_by(status=True).all()
    
    # Fetch beverage prices for this user's role
    beverage_prices_for_role = beverage_prices.query.filter_by(role_id=user.role_id).all()
    
    # Create a dictionary for easy price lookup
    price_lookup = {bp.beverage_id: bp for bp in beverage_prices_for_role}
    
    # Convert user to dictionary for JSON serialization
    user_dict = {
        'id': user.id,
        'first_name': user.first_name,
        'last_name': user.last_name,
        'email': user.email,
        'role': {
            'id': user.role.id,
            'name': user.role.name
        } if user.role else None
    }
    
    return render_template("entries.html", 
                         user=user, 
                         user_data=user_dict,
                         consumptions=user_consumptions,
                         beverages=all_beverages,
                         price_lookup=price_lookup)

@bp.route("/verify_pin", methods=["POST"])
def verify_pin_route():
    """Verify PIN for a specific user"""
    try:
        data = request.get_json()
        user_id = data.get('user_id')
        pin = data.get('pin', '').strip()
        
        if not user_id or not pin:
            return jsonify({"error": "User ID and PIN are required"}), 400
        
        # Use existing verify_pin function
        if verify_pin(user_id, pin):
            return jsonify({"success": True})
        else:
            return jsonify({"error": "Invalid PIN"}), 401
        
    except Exception as e:
        return jsonify({"error": f"Failed to verify PIN: {str(e)}"}), 500

@bp.route("/check_user_pin", methods=["POST"])
def check_user_pin():
    """Check if a user has a PIN set"""
    try:
        data = request.get_json()
        user_id = data.get('user_id')
        
        if not user_id:
            return jsonify({"error": "User ID is required"}), 400
        
        user = users.query.get(user_id)
        if not user:
            return jsonify({"error": "User not found"}), 404
        
        return jsonify({
            "success": True,
            "has_pin": user.pin_hash is not None
        })
        
    except Exception as e:
        return jsonify({"error": f"Failed to check user PIN: {str(e)}"}), 500

@bp.route("/create_user_pin", methods=["POST"])
def create_user_pin():
    """Create PIN for a specific user"""
    try:
        data = request.get_json()
        if not data:
            return jsonify({"error": "No JSON data provided"}), 400
            
        user_id = data.get('user_id')
        pin = data.get('pin', '').strip()
        
        if not user_id:
            return jsonify({"error": "User ID is required"}), 400
            
        if not pin:
            return jsonify({"error": "PIN is required"}), 400
        
        # Get the specific user
        user = users.query.get(user_id)
        if not user:
            return jsonify({"error": "User not found"}), 404
        
        # Check if user already has a PIN set
        if user.pin_hash:
            return jsonify({"error": "User already has a PIN set"}), 400
        
        # Use existing hash_pin function to hash the PIN
        pin_hash = hash_pin(pin)
        user.pin_hash = pin_hash
        
        db.session.commit()
        
        return jsonify({
            "success": True,
            "message": "PIN created successfully",
            "user": {
                "id": user.id,
                "first_name": user.first_name,
                "last_name": user.last_name
            }
        })
        
    except Exception as e:
        db.session.rollback()
        return jsonify({"error": f"Failed to create PIN: {str(e)}"}), 500

@bp.route("/add_consumption", methods=["POST"])
def add_consumption():
    """
    Add a beverage consumption entry.
    Creates invoice if it doesn't exist for current month.
    """
    try:
        data = request.get_json()
        if not data:
            return jsonify({"error": "No JSON data provided"}), 400
            
        try:
            user_id = int(data.get('user_id'))
            beverage_id = int(data.get('beverage_id'))
            quantity = int(data.get('quantity', 1))
        except (ValueError, TypeError) as e:
            return jsonify({"error": f"Invalid data format: {str(e)}"}), 400
        
        if not user_id or not beverage_id:
            return jsonify({"error": "Missing required fields"}), 400
        
        # Validate user exists
        user = users.query.get(user_id)
        if not user:
            return jsonify({"error": "User not found"}), 404
        
        # Validate beverage exists and is active
        beverage = beverages.query.filter_by(id=beverage_id, status=True).first()
        if not beverage:
            return jsonify({"error": "Beverage not found or inactive"}), 404
        
        # Get beverage price for user's role
        beverage_price = beverage_prices.query.filter_by(
            role_id=user.role_id,
            beverage_id=beverage_id
        ).first()
        
        if not beverage_price:
            return jsonify({"error": "No price found for this beverage and role"}), 404
        
        # Get or create monthly invoice
        invoice = check_invoice_exists(user_id)
        
        # Create consumption entry
        consumption = consumptions(
            user_id=user_id,
            beverage_id=beverage_id,
            beverage_price_id=beverage_price.id,
            invoice_id=invoice.id,
            quantity=quantity,
            unit_price_cents=beverage_price.price_cents
        )
        
        db.session.add(consumption)
        db.session.commit()
        
        return jsonify({
            "success": True,
            "message": "Consumption added successfully",
            "consumption_id": consumption.id,
            "invoice_id": invoice.id
        })
        
    except ValueError as e:
        db.session.rollback()
        return jsonify({"error": str(e)}), 400
    except Exception as e:
        db.session.rollback()
        return jsonify({"error": f"Failed to add consumption: {str(e)}"}), 500
