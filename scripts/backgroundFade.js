// title: backgroundFade.js
// author: ChatGPT, Veronica Hutchins
// date: 05/7/2026
// note: smoothly fades background from black to dark gray and back every 10 seconds

const page = document.querySelector(".contactpage");

const minBrightness = 0;     // black
const maxBrightness = 40;    // dark gray
const cycleTime = 10000;     // 10 seconds for full cycle

let startTime = null;

function animateBackground(timestamp) {

    if (!startTime) {
        startTime = timestamp;
    }

    // elapsed time
    const elapsed = (timestamp - startTime) % cycleTime;

    // convert to value between 0 and 1
    const progress = elapsed / cycleTime;

    // sine wave for smooth fade in/out
    const wave = (Math.sin(progress * Math.PI * 2 - Math.PI / 2) + 1) / 2;

    // calculate brightness
    const brightness = Math.floor(
        minBrightness + (maxBrightness - minBrightness) * wave
    );

    // apply background color
    page.style.backgroundColor =
        `rgb(${brightness}, ${brightness}, ${brightness})`;

    requestAnimationFrame(animateBackground);
}

requestAnimationFrame(animateBackground);
