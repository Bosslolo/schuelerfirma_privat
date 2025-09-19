// Fuzzy Search functionality for user cards
class UserSearch {
    constructor() {
        this.searchInput = document.getElementById('userSearch');
        this.clearButton = document.getElementById('clearSearch');
        this.resultsCount = document.getElementById('searchResultsCount');
        this.userCards = document.querySelectorAll('.user-card-container');
        this.userGrid = document.getElementById('userGrid');
        
        this.init();
    }
    
    init() {
        if (!this.searchInput) return;
        
        // Add event listeners
        this.searchInput.addEventListener('input', (e) => this.handleSearch(e.target.value));
        this.searchInput.addEventListener('keydown', (e) => this.handleKeydown(e));
        this.clearButton.addEventListener('click', () => this.clearSearch());
        
        // Initial count
        this.updateResultsCount(this.userCards.length);
    }
    
    handleSearch(query) {
        const searchTerm = query.toLowerCase().trim();
        
        if (searchTerm === '') {
            this.showAllCards();
            this.clearButton.style.display = 'none';
            this.toggleNoResultsMessage(false);
        } else {
            this.filterCards(searchTerm);
            this.clearButton.style.display = 'block';
        }
        
        this.updateResultsCount(this.getVisibleCards().length);
    }
    
    filterCards(searchTerm) {
        let visibleCount = 0;
        
        this.userCards.forEach((card, index) => {
            const userName = card.dataset.userName.toLowerCase();
            const firstName = card.dataset.userFirst.toLowerCase();
            const lastName = card.dataset.userLast.toLowerCase();
            
            // Fuzzy search logic
            const isMatch = this.fuzzyMatch(userName, searchTerm) ||
                           this.fuzzyMatch(firstName, searchTerm) ||
                           this.fuzzyMatch(lastName, searchTerm);
            
            if (isMatch) {
                card.classList.remove('hidden');
                card.style.animationDelay = `${visibleCount * 0.1}s`;
                visibleCount++;
            } else {
                card.classList.add('hidden');
            }
        });
        
        this.toggleNoResultsMessage(visibleCount === 0);
    }
    
    fuzzyMatch(text, pattern) {
        let patternIdx = 0;
        let textIdx = 0;
        
        while (textIdx < text.length && patternIdx < pattern.length) {
            if (text[textIdx] === pattern[patternIdx]) {
                patternIdx++;
            }
            textIdx++;
        }
        
        return patternIdx === pattern.length;
    }
    
    showAllCards() {
        this.userCards.forEach((card, index) => {
            card.classList.remove('hidden');
            card.style.animationDelay = `${index * 0.1}s`;
        });
    }
    
    getVisibleCards() {
        return Array.from(this.userCards).filter(card => !card.classList.contains('hidden'));
    }
    
    toggleNoResultsMessage(show) {
        let noResultsMsg = document.getElementById('noResultsMessage');
        
        if (show && !noResultsMsg) {
            noResultsMsg = document.createElement('div');
            noResultsMsg.id = 'noResultsMessage';
            noResultsMsg.className = 'col-12 no-results';
            noResultsMsg.innerHTML = `
                <i class="fas fa-search"></i>
                <h4>No users found</h4>
                <p>Try adjusting your search terms</p>
            `;
            this.userGrid.appendChild(noResultsMsg);
        } else if (!show && noResultsMsg) {
            noResultsMsg.remove();
        }
    }
    
    clearSearch() {
        this.searchInput.value = '';
        this.showAllCards();
        this.clearButton.style.display = 'none';
        this.toggleNoResultsMessage(false);
        this.updateResultsCount(this.userCards.length);
        this.searchInput.focus();
    }
    
    updateResultsCount(count) {
        if (this.resultsCount) {
            this.resultsCount.textContent = `${count} user${count !== 1 ? 's' : ''} found`;
        }
    }
    
    handleKeydown(e) {
        if (e.key === 'Escape') {
            this.clearSearch();
        }
    }
}

// Simple PIN Authentication
class PinAuth {
    constructor() {
        this.pinOverlay = document.getElementById('pinOverlay');
        this.pinInput = document.getElementById('pinInput');
        this.pinSubmit = document.getElementById('pinSubmit');
        this.pinCancel = document.getElementById('pinCancel');
        this.pinError = document.getElementById('pinError');
        this.pinUserInfo = document.getElementById('pinUserInfo');
        this.currentUser = null;
        
        this.init();
    }
    
    init() {
        if (!this.pinOverlay) return;
        
        this.pinSubmit.addEventListener('click', () => this.submitPin());
        this.pinCancel.addEventListener('click', () => this.hideOverlay());
        this.pinInput.addEventListener('keydown', (e) => this.handleKeydown(e));
        this.pinInput.addEventListener('input', (e) => this.handleInput(e));
    }
    
    handleKeydown(e) {
        if (e.key === 'Enter') {
            e.preventDefault();
            this.submitPin();
        } else if (e.key === 'Escape') {
            e.preventDefault();
            this.hideOverlay();
        }
    }
    
    handleInput(e) {
        // Only allow digits
        e.target.value = e.target.value.replace(/[^0-9]/g, '');
    }
    
    showPinForUser(userId, userName) {
        this.currentUser = { id: userId, name: userName };
        this.pinUserInfo.textContent = `Please enter PIN for ${userName}`;
        this.pinInput.value = '';
        this.pinError.style.display = 'none';
        this.pinOverlay.style.display = 'flex';
        this.pinInput.focus();
        document.body.style.overflow = 'hidden';
    }
    
    async submitPin() {
        const pin = this.pinInput.value.trim();
        
        if (!pin) {
            this.showError('Please enter a PIN');
            return;
        }
        
        if (pin.length !== 4 || !/^\d{4}$/.test(pin)) {
            this.showError('PIN must be exactly 4 digits');
            return;
        }
        
        if (!this.currentUser) {
            this.showError('No user selected');
            return;
        }
        
        this.pinSubmit.disabled = true;
        this.pinSubmit.innerHTML = '<i class="fas fa-spinner fa-spin"></i>';
        
        try {
            const response = await fetch('/verify_pin', {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify({ 
                    user_id: this.currentUser.id,
                    pin: pin 
                })
            });
            
            const data = await response.json();
            
            if (response.ok && data.success) {
                // PIN verified successfully - redirect to entries
                window.location.href = `/entries?user_id=${this.currentUser.id}`;
            } else {
                this.showError(data.error || 'Invalid PIN');
                this.pinInput.value = '';
                this.pinInput.focus();
            }
        } catch (error) {
            this.showError('Network error. Please try again.');
        } finally {
            this.pinSubmit.disabled = false;
            this.pinSubmit.innerHTML = '<i class="fas fa-check"></i>';
        }
    }
    
    hideOverlay() {
        this.pinOverlay.style.display = 'none';
        document.body.style.overflow = '';
        this.currentUser = null;
    }
    
    showError(message) {
        this.pinError.querySelector('span').textContent = message;
        this.pinError.style.display = 'flex';
        
        setTimeout(() => {
            this.pinError.style.display = 'none';
        }, 3000);
    }
}

// Simple User Click Handler
class UserClickHandler {
    constructor() {
        this.pinAuth = new PinAuth();
        this.init();
    }
    
    init() {
        const userCards = document.querySelectorAll('.user-card');
        userCards.forEach(card => {
            card.addEventListener('click', (e) => this.handleUserClick(e));
        });
    }
    
    async handleUserClick(e) {
        e.preventDefault();
        
        const userCard = e.currentTarget;
        const userContainer = userCard.closest('.user-card-container');
        const userId = this.extractUserIdFromHref(userCard.href);
        const userName = userContainer.dataset.userName;
        
        // Check if user has PIN by making a simple request
        try {
            const response = await fetch('/check_user_pin', {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify({ user_id: userId })
            });
            
            const data = await response.json();
            
            if (response.ok && data.success) {
                if (data.has_pin) {
                    // User has PIN, show PIN overlay
                    this.pinAuth.showPinForUser(userId, userName);
                } else {
                    // User has no PIN, proceed directly
                    window.location.href = userCard.href;
                }
            } else {
                // Fallback: proceed directly
                window.location.href = userCard.href;
            }
        } catch (error) {
            // Fallback: proceed directly
            window.location.href = userCard.href;
        }
    }
    
    extractUserIdFromHref(href) {
        const url = new URL(href);
        return parseInt(url.searchParams.get('user_id'));
    }
}

// Initialize everything when DOM is loaded
document.addEventListener('DOMContentLoaded', function() {
    new UserSearch();
    new UserClickHandler();
});