// video.js

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
                    alert('Video link copied to clipboard!');
                })
                .catch(err => {
                    console.error('Failed to copy: ', err);
                });
        });
    });
});
