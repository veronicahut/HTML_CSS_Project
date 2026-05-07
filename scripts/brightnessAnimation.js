// title: brightnessAnimation.js
// author: ChatGPT, Veronica Hutchins
// date: 05/7/2026
// note: brightens contact links one at a time every 5 seconds

const links = [
    document.querySelector("#email a"),
    document.querySelector("#linkedin a"),
    document.querySelector("#github a")
];

let currentIndex = 0;

// normal and bright colors
const normalColor = "#19a2a7";
const brightColor = "#77fce6";

// smooth transition effect
links.forEach(link => {
    link.style.transition = "color 1.5s ease";
    link.style.color = normalColor;
});

function highlightNextLink() {

    // reset all links
    links.forEach(link => {
        link.style.color = normalColor;
    });

    // brighten current link
    links[currentIndex].style.color = brightColor;

    // move to next link
    currentIndex++;

    if (currentIndex >= links.length) {
        currentIndex = 0;
    }
}

// start immediately
highlightNextLink();

// repeat every 5 seconds
setInterval(highlightNextLink, 5000);
