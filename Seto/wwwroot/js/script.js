// script.js

// Dark Mode Toggle
const darkModeToggle = document.getElementById('darkModeToggle');
const bodyElement = document.body;

darkModeToggle.addEventListener('click', () => {
    bodyElement.classList.toggle('dark-mode');
    // Icon değişimi
    const icon = darkModeToggle.querySelector('i');
    if(bodyElement.classList.contains('dark-mode')) {
        icon.classList.remove('fa-moon');
        icon.classList.add('fa-sun');
    } else {
        icon.classList.remove('fa-sun');
        icon.classList.add('fa-moon');
    }
});

// login-register.js

document.addEventListener('DOMContentLoaded', () => {
    const openLoginBtn = document.getElementById('openLoginPopup');
    const closeLoginBtn = document.getElementById('closePopup');
    const closeRegisterBtn = document.getElementById('closePopupRegister');
    const popupOverlay = document.getElementById('popupOverlay');
    const loginPopup = document.getElementById('loginPopup');
    const registerPopup = document.getElementById('registerPopup');
    const switchToRegisterBtn = document.getElementById('switchToRegister');
    const switchToLoginBtn = document.getElementById('switchToLogin');

    // Open Login Popup
    openLoginBtn.addEventListener('click', () => {
        popupOverlay.classList.add('active');
        loginPopup.style.display = 'block';
        registerPopup.style.display = 'none';
    });

    // Close Popups
    closeLoginBtn.addEventListener('click', () => {
        popupOverlay.classList.remove('active');
    });

    closeRegisterBtn.addEventListener('click', () => {
        popupOverlay.classList.remove('active');
    });

    // Switch to Register Popup
    switchToRegisterBtn.addEventListener('click', () => {
        loginPopup.style.display = 'none';
        registerPopup.style.display = 'block';
    });

    // Switch to Login Popup
    switchToLoginBtn.addEventListener('click', () => {
        registerPopup.style.display = 'none';
        loginPopup.style.display = 'block';
    });

    // Close Popup when clicking outside the popup
    popupOverlay.addEventListener('click', (e) => {
        if (e.target === popupOverlay) {
            popupOverlay.classList.remove('active');
        }
    });

    // Toggle Password Visibility
    const togglePasswordIcons = document.querySelectorAll('.toggle-password');

    togglePasswordIcons.forEach(icon => {
        icon.addEventListener('click', () => {
            const input = icon.previousElementSibling;
            if (input.type === 'password') {
                input.type = 'text';
                icon.classList.remove('fa-eye');
                icon.classList.add('fa-eye-slash');
            } else {
                input.type = 'password';
                icon.classList.remove('fa-eye-slash');
                icon.classList.add('fa-eye');
            }
        });
    });
});

// Scroll to Top Butonu
const scrollToTopBtn = document.getElementById('scrollToTopBtn');

window.addEventListener('scroll', () => {
    if(window.pageYOffset > 300) {
        scrollToTopBtn.classList.add('show');
    } else {
        scrollToTopBtn.classList.remove('show');
    }
});

scrollToTopBtn.addEventListener('click', () => {
    window.scrollTo({
        top: 0,
        behavior: 'smooth'
    });
});



document.querySelectorAll('.horizontal-scroll-wrapper').forEach(wrapper => {
    const content = wrapper.querySelector('.videos-grid, .games-grid');
    const leftBtn = wrapper.querySelector('.scroll-btn.left');
    const rightBtn = wrapper.querySelector('.scroll-btn.right');
    
    if (leftBtn && rightBtn && content) {
        leftBtn.addEventListener('click', () => {
            content.scrollBy({
                left: -300, // Scroll miktarını ihtiyaca göre ayarlayın
                behavior: 'smooth'
            });
        });

        rightBtn.addEventListener('click', () => {
            content.scrollBy({
                left: 300, // Scroll miktarını ihtiyaca göre ayarlayın
                behavior: 'smooth'
            });
        });
    }
});



// script.js

document.addEventListener('DOMContentLoaded', () => {
    const hamburger = document.getElementById('hamburger');
    const navMenu = document.querySelector('.nav-menu');

    hamburger.addEventListener('click', () => {
        navMenu.classList.toggle('active');
        hamburger.classList.toggle('active'); 
    });
});

