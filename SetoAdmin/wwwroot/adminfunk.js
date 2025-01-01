
function updatePalette(paletteName) {
    console.log("Renk paleti güncelleniyor:", paletteName);
    document.body.className = paletteName.toLowerCase();
}

function updateFavicon(faviconUrl) {
    console.log("Favicon güncelleniyor:", faviconUrl);
    let link = document.querySelector("link[rel~='icon']");
    if (!link) {
        link = document.createElement('link');
        link.rel = 'icon';
        document.head.appendChild(link);
    }
    link.href = faviconUrl;
}
