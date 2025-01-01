// game.js

document.addEventListener('DOMContentLoaded', () => {
    // Yorum Cevaplama Butonları
    const replyButtons = document.querySelectorAll('.reply-btn');

    replyButtons.forEach(button => {
        button.addEventListener('click', () => {
            const commentItem = button.closest('.comment-item');
            const replyForm = commentItem.querySelector('.reply-form');

            if (replyForm.style.display === 'none' || replyForm.style.display === '') {
                replyForm.style.display = 'block';
            } else {
                replyForm.style.display = 'none';
            }
        });
    });

    // Like, Share ve Save İkonları İçin Etkileşim (Opsiyonel)
    const likeButtons = document.querySelectorAll('.like-btn');
    likeButtons.forEach(btn => {
        btn.addEventListener('click', () => {
            btn.classList.toggle('liked');
            // Beğeni sayısını artırma/azaltma işlemleri burada yapılabilir
        });
    });

    const saveButtons = document.querySelectorAll('.save-btn');
    saveButtons.forEach(btn => {
        btn.addEventListener('click', () => {
            btn.classList.toggle('saved');
            // Kaydetme işlemleri burada yapılabilir
        });
    });

    // Share İkonu İçin Basit Paylaşma İşlevi
    const shareButtons = document.querySelectorAll('.share-btn');
    shareButtons.forEach(btn => {
        btn.addEventListener('click', () => {
            navigator.clipboard.writeText(window.location.href)
                .then(() => {
                    alert('Game link copied to clipboard!');
                })
                .catch(err => {
                    console.error('Failed to copy: ', err);
                });
        });
    });

    // Tam Ekran Butonu İşlevselliği
    const fullscreenBtn = document.getElementById('fullscreenBtn');
    const gamePlayer = document.querySelector('.game-player');

    fullscreenBtn.addEventListener('click', () => {
        if (!document.fullscreenElement) {
            if (gamePlayer.requestFullscreen) {
                gamePlayer.requestFullscreen();
            } else if (gamePlayer.webkitRequestFullscreen) { /* Safari */
                gamePlayer.webkitRequestFullscreen();
            } else if (gamePlayer.msRequestFullscreen) { /* IE11 */
                gamePlayer.msRequestFullscreen();
            }
            fullscreenBtn.innerHTML = '<i class="fas fa-compress"></i>';
        } else {
            if (document.exitFullscreen) {
                document.exitFullscreen();
            } else if (document.webkitExitFullscreen) { /* Safari */
                document.webkitExitFullscreen();
            } else if (document.msExitFullscreen) { /* IE11 */
                document.msExitFullscreen();
            }
            fullscreenBtn.innerHTML = '<i class="fas fa-expand"></i>';
        }
    });

    // Tam Ekran Modundan Çıkıldığında Buton İkonunu Güncelle
    document.addEventListener('fullscreenchange', () => {
        if (!document.fullscreenElement) {
            fullscreenBtn.innerHTML = '<i class="fas fa-expand"></i>';
        }
    });

    document.addEventListener('webkitfullscreenchange', () => {
        if (!document.webkitFullscreenElement) {
            fullscreenBtn.innerHTML = '<i class="fas fa-expand"></i>';
        }
    });

    document.addEventListener('msfullscreenchange', () => {
        if (!document.msFullscreenElement) {
            fullscreenBtn.innerHTML = '<i class="fas fa-expand"></i>';
        }
    });
});
