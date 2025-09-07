        // Fuzzy Search functionality for user cards
class UserSearch {
    constructor() {
        this.searchInput = document.getElementById('userSearch');
        this.clearButton = document.getElementById('clearSearch');
        this.resultsCount = document.getElementById('searchResultsCount');
        this.userCards = document.querySelectorAll('.user-card-container');
        this.userGrid = document.getElementById('userGrid');
        
        console.log('UserSearch initialized:', {
            searchInput: !!this.searchInput,
            clearButton: !!this.clearButton,
            resultsCount: !!this.resultsCount,
            userCards: this.userCards.length,
            userGrid: !!this.userGrid
        });
        
        this.init();
    }
    
    init() {
        if (!this.searchInput) {
            console.error('Search input not found!');
            return;
        }
        
        console.log('Setting up search event listeners...');
        
        // Add event listeners
        this.searchInput.addEventListener('input', (e) => this.handleSearch(e.target.value));
        this.searchInput.addEventListener('keydown', (e) => this.handleKeydown(e));
        this.clearButton.addEventListener('click', () => this.clearSearch());
        
        // Initial count
        this.updateResultsCount(this.userCards.length);
        
        console.log('Search functionality initialized successfully!');
    }
    
    handleSearch(query) {
        const searchTerm = query.toLowerCase().trim();
        console.log('Search triggered with term:', searchTerm);
        
        if (searchTerm === '') {
            this.showAllCards();
            this.clearButton.style.display = 'none';
            this.toggleNoResultsMessage(false); // Hide no results message when search is cleared
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
        
        // Show no results message if needed
        this.toggleNoResultsMessage(visibleCount === 0);
    }
    
    fuzzyMatch(text, pattern) {
        // Simple fuzzy matching algorithm
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
        this.toggleNoResultsMessage(false); // Hide no results message when clearing search
        this.updateResultsCount(this.userCards.length);
        this.searchInput.focus();
    }
}

// Initialize search when DOM is loaded
document.addEventListener('DOMContentLoaded', function() {
    new UserSearch();
});
