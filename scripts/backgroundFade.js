// title: backgroundFade.js
// author: ChatGPT, Veronica Hutchins
// date: 05/7/2026
// note: this script creates a fading background effect by adjusting the brightness of the background color over time.

let brightness = 0;
let direction = 1;

function animateBackground() {
    // Adjust brightness
    brightness += direction * 0.5;

    // Reverse direction at limits
    if (brightness >= 80) {
        direction = -1;
    } else if (brightness <= 0) {
        direction = 1;
    }

    // Set background color
    document.body.style.backgroundColor = `rgb(${brightness}, ${brightness}, ${brightness})`;

    // Repeat animation
    requestAnimationFrame(animateBackground);
}

// Start animation
animateBackground();
