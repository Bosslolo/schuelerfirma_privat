// Beverage Consumption Management
class BeverageConsumption {
    constructor() {
        this.userId = this.getUserId();
        this.order = new Map(); // beverage_id -> quantity
        this.beverageData = new Map(); // beverage_id -> {name, price}
        this.consumedData = new Map(); // beverage_id -> consumed_count
        
        this.init();
    }
    
    init() {
        this.loadBeverageData();
        this.loadConsumedData();
        this.setupEventListeners();
        console.log('Beverage consumption initialized for user:', this.userId);
    }
    
    getUserId() {
        // Get user ID from backend data or URL parameters
        if (window.userData && window.userData.id) {
            return window.userData.id.toString();
        }
        
        // Fallback to URL parameters
        const urlParams = new URLSearchParams(window.location.search);
        return urlParams.get('user_id');
    }
    
    loadBeverageData() {
        // Load beverage data from the DOM
        const beverageItems = document.querySelectorAll('.beverage-item');
        beverageItems.forEach(item => {
            const beverageId = item.dataset.beverageId;
            const name = item.querySelector('.beverage-name').textContent;
            const priceText = item.querySelector('.beverage-price').textContent;
            const price = parseFloat(priceText.replace(' CHF', ''));
            
            this.beverageData.set(beverageId, { name, price });
        });
    }
    
    loadConsumedData() {
        // Load consumed data from the DOM (if available)
        const consumedCounts = document.querySelectorAll('.consumed-count');
        consumedCounts.forEach(count => {
            const beverageItem = count.closest('.beverage-item');
            const beverageId = beverageItem.dataset.beverageId;
            const consumed = parseInt(count.textContent) || 0;
            
            this.consumedData.set(beverageId, consumed);
        });
        
        // Also load from backend data if available
        if (window.consumptionsData) {
            window.consumptionsData.forEach(consumption => {
                this.consumedData.set(consumption.beverage_id.toString(), consumption.total_quantity || consumption.count);
            });
        }
        
        // Update consumed count displays
        this.updateConsumedDisplays();
    }
    
    updateConsumedDisplays() {
        this.consumedData.forEach((count, beverageId) => {
            const beverageItem = document.querySelector(`[data-beverage-id="${beverageId}"]`);
            if (beverageItem) {
                const consumedCountElement = beverageItem.querySelector('.consumed-count');
                if (consumedCountElement) {
                    consumedCountElement.textContent = count;
                }
            }
        });
    }
    
    setupEventListeners() {
        // Quantity control buttons
        document.querySelectorAll('.quantity-btn').forEach(btn => {
            btn.addEventListener('click', (e) => {
                const action = e.currentTarget.dataset.action;
                const beverageItem = e.currentTarget.closest('.beverage-item');
                const beverageId = beverageItem.dataset.beverageId;
                
                this.updateQuantity(beverageId, action);
            });
        });
        
        // Done button
        document.getElementById('doneBtn').addEventListener('click', () => {
            this.handleDoneClick();
        });
        
        // Confirm order button in modal
        document.getElementById('confirmOrderBtn').addEventListener('click', () => {
            this.confirmOrder();
        });
    }
    
    updateQuantity(beverageId, action) {
        const currentQuantity = this.order.get(beverageId) || 0;
        let newQuantity = currentQuantity;
        
        if (action === 'increase') {
            newQuantity = currentQuantity + 1;
        } else if (action === 'decrease' && currentQuantity > 0) {
            newQuantity = currentQuantity - 1;
        }
        
        this.order.set(beverageId, newQuantity);
        this.updateQuantityDisplay(beverageId, newQuantity);
    }
    
    updateQuantityDisplay(beverageId, quantity) {
        const beverageItem = document.querySelector(`[data-beverage-id="${beverageId}"]`);
        const quantityDisplay = beverageItem.querySelector('.quantity-display');
        quantityDisplay.textContent = quantity;
        
        // Update button states
        const decreaseBtn = beverageItem.querySelector('[data-action="decrease"]');
        decreaseBtn.disabled = quantity === 0;
    }
    
    
    handleDoneClick() {
        const hasItems = Array.from(this.order.values()).some(qty => qty > 0);
        
        if (!hasItems) {
            // No items selected, redirect to index
            window.location.href = '/';
        } else {
            // Show confirmation modal
            this.showConfirmationModal();
        }
    }
    
    showConfirmationModal() {
        const modal = new bootstrap.Modal(document.getElementById('confirmationModal'));
        const modalOrderItems = document.getElementById('modalOrderItems');
        
        // Clear existing modal items
        modalOrderItems.innerHTML = '';
        
        // Add items to modal
        this.order.forEach((quantity, beverageId) => {
            if (quantity > 0) {
                const beverage = this.beverageData.get(beverageId);
                
                const modalItem = document.createElement('div');
                modalItem.className = 'modal-order-item';
                modalItem.innerHTML = `
                    <div class="modal-order-item-name">${beverage.name}</div>
                    <div class="modal-order-item-quantity">${quantity}</div>
                `;
                
                modalOrderItems.appendChild(modalItem);
            }
        });
        
        // Show modal
        modal.show();
    }
    
    async confirmOrder() {
        const confirmBtn = document.getElementById('confirmOrderBtn');
        const originalText = confirmBtn.innerHTML;
        
        // Show loading state
        confirmBtn.disabled = true;
        confirmBtn.innerHTML = '<i class="fas fa-spinner fa-spin"></i> Processing...';
        
        try {
            // Process each beverage in the order
            const promises = [];
            
            this.order.forEach((quantity, beverageId) => {
                if (quantity > 0) {
                    // Send quantity in single API call instead of multiple calls
                    promises.push(this.addConsumption(beverageId, quantity));
                }
            });
            
            // Wait for all consumptions to be added
            await Promise.all(promises);
            
            // Show success message
            this.showSuccessMessage('Order confirmed successfully!');
            
            // Close modal and redirect
            const modal = bootstrap.Modal.getInstance(document.getElementById('confirmationModal'));
            modal.hide();
            
            setTimeout(() => {
                window.location.href = '/';
            }, 1500);
            
        } catch (error) {
            console.error('Error confirming order:', error);
            this.showErrorMessage('Failed to confirm order. Please try again.');
            
            // Reset button
            confirmBtn.disabled = false;
            confirmBtn.innerHTML = originalText;
        }
    }
    
    async addConsumption(beverageId, quantity = 1) {
        const response = await fetch('/add_consumption', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify({
                user_id: this.userId,
                beverage_id: beverageId,
                quantity: quantity
            })
        });
        
        if (!response.ok) {
            const errorData = await response.json();
            throw new Error(errorData.error || 'Failed to add consumption');
        }
        
        return response.json();
    }
    
    showSuccessMessage(message) {
        this.showMessage(message, 'success');
    }
    
    showErrorMessage(message) {
        this.showMessage(message, 'danger');
    }
    
    showMessage(message, type) {
        // Remove existing messages
        const existingMessages = document.querySelectorAll('.alert');
        existingMessages.forEach(msg => msg.remove());
        
        // Create new message
        const alertDiv = document.createElement('div');
        alertDiv.className = `alert alert-${type} alert-dismissible fade show`;
        alertDiv.style.position = 'fixed';
        alertDiv.style.top = '20px';
        alertDiv.style.right = '20px';
        alertDiv.style.zIndex = '9999';
        alertDiv.style.minWidth = '300px';
        
        alertDiv.innerHTML = `
            ${message}
            <button type="button" class="btn-close" data-bs-dismiss="alert"></button>
        `;
        
        document.body.appendChild(alertDiv);
        
        // Auto-hide success messages
        if (type === 'success') {
            setTimeout(() => {
                alertDiv.remove();
            }, 3000);
        }
    }
}

// Initialize beverage consumption when DOM is loaded
document.addEventListener('DOMContentLoaded', function() {
    new BeverageConsumption();
    console.log('Beverage consumption system initialized!');
});