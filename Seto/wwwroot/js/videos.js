// videos.js

document.addEventListener('DOMContentLoaded', () => {
    // Tam Ekran Butonları
    const fullscreenButtons = document.querySelectorAll('.fullscreen-btn');

    fullscreenButtons.forEach(button => {
        button.addEventListener('click', (e) => {
            e.preventDefault();
            const img = button.parentElement.querySelector('img');

            if (!document.fullscreenElement) {
                if (img.requestFullscreen) {
                    img.requestFullscreen();
                } else if (img.webkitRequestFullscreen) { /* Safari */
                    img.webkitRequestFullscreen();
                } else if (img.msRequestFullscreen) { /* IE11 */
                    img.msRequestFullscreen();
                }
                button.innerHTML = '<i class="fas fa-compress"></i>';
            } else {
                if (document.exitFullscreen) {
                    document.exitFullscreen();
                } else if (document.webkitExitFullscreen) { /* Safari */
                    document.webkitExitFullscreen();
                } else if (document.msExitFullscreen) { /* IE11 */
                    document.msExitFullscreen();
                }
                button.innerHTML = '<i class="fas fa-expand"></i>';
            }
        });
    });

    // Tam Ekran Modundan Çıkıldığında Buton İkonunu Güncelle
    document.addEventListener('fullscreenchange', () => {
        fullscreenButtons.forEach(button => {
            if (!document.fullscreenElement) {
                button.innerHTML = '<i class="fas fa-expand"></i>';
            }
        });
    });

    document.addEventListener('webkitfullscreenchange', () => {
        fullscreenButtons.forEach(button => {
            if (!document.webkitFullscreenElement) {
                button.innerHTML = '<i class="fas fa-expand"></i>';
            }
        });
    });

    document.addEventListener('msfullscreenchange', () => {
        fullscreenButtons.forEach(button => {
            if (!document.msFullscreenElement) {
                button.innerHTML = '<i class="fas fa-expand"></i>';
            }
        });
    });
});
