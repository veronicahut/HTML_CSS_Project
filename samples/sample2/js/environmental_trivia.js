// name: environmental_trivia.js
// date: 5/16/2026
// author: Google Gemini, Veronica Hutchins
// description: Handles trivia question loading and answer checking via a local JSON file

// 1. DOM Elements (Strictly matched to your HTML)
const questionEl = document.getElementById('question');
const feedbackEl = document.getElementById('feedback-message');
const trueButtonEl = document.getElementById('true-answer');
const falseButtonEl = document.getElementById('false-answer');
const nextButtonEl = document.getElementById('next-btn');
const scoreEl = document.getElementById('points');
const timerEl = document.getElementById('timer');

// 2. Global Game Variables
let questionsArray = [];       // Holds all questions loaded from JSON
let currentQuestionIndex = 0;  // Keeps track of which question the user is on
let score = parseInt(localStorage.getItem('triviaScore') || '0', 10);

// Initialize score counter on page load
if (scoreEl) scoreEl.textContent = score;

// 3. Core Gameplay Functions
async function loadTriviaQuestion() {
    try {
        const response = await fetch('../includes/environmental_trivia.json');
        if (!response.ok) {
            throw new Error(`HTTP error! status: ${response.status}`);
        }
        
        // Store all questions globally
        questionsArray = await response.json();
        
        // Start the game loop by showing the first question
        displayCurrentQuestion();
        
    } catch (error) {
        console.error("Could not load trivia questions:", error);
        if (questionEl) questionEl.textContent = "Error loading trivia questions. Please try again.";
    }
}

function displayCurrentQuestion() {
    // Clear out old feedback and hide the next button
    if (feedbackEl) {
        feedbackEl.textContent = "";
        feedbackEl.className = "response"; // resets colors
    }
    if (nextButtonEl) nextButtonEl.style.display = 'none';
    
    // Check if we ran out of questions in the array
    if (currentQuestionIndex >= questionsArray.length) {
        if (questionEl) questionEl.textContent = " 🎉 You've completed all available trivia questions!";
        setButtonsDisabled(true);
        return;
    }

    // Load up the text for the current item
    const currentQuestion = questionsArray[currentQuestionIndex];
    if (questionEl) questionEl.textContent = currentQuestion.question;
    
    // Unlock the answer inputs for the player
    setButtonsDisabled(false);
}

function submitAnswer(selectedOption) {
    const currentQuestion = questionsArray[currentQuestionIndex];
    if (!currentQuestion || !feedbackEl) return;

    // Lock selections so answers can't be changed mid-question
    setButtonsDisabled(true);

    // Look inside the JSON's "answers" array to find the item marked correct
    const correctAnswerObj = currentQuestion.answers.find(ans => ans.correct === true);
    const correctAnswerText = correctAnswerObj ? correctAnswerObj.text : "";

    // Compare the user's choice ('True' or 'False') to the text of the correct answer
    if (selectedOption === correctAnswerText) {
        score++;
        localStorage.setItem('triviaScore', score);
        if (scoreEl) scoreEl.textContent = score;
        
        feedbackEl.textContent = "Correct! 🌱 Excellent job.";
        feedbackEl.style.color = "#2e7d32"; // Modern flat green
    } else {
        feedbackEl.textContent = `Incorrect. The correct answer was: ${correctAnswerText}`;
        feedbackEl.style.color = "#c62828"; // Modern flat red
    }

    // Reveal the "Next Question" button
    if (nextButtonEl) nextButtonEl.style.display = 'inline-block';
}

function loadNextQuestion() {
    currentQuestionIndex++;
    displayCurrentQuestion();
}

function setButtonsDisabled(v) {
    if (trueButtonEl) trueButtonEl.disabled = v;
    if (falseButtonEl) falseButtonEl.disabled = v;
}

// 4. Countdown Timer Logic
var end = new Date().getTime() + 120000; // 2 minutes

var x = setInterval(function() {
    var now = new Date().getTime();
    var distance = end - now;

    var minutes = Math.floor((distance % (1000 * 60 * 60)) / (1000 * 60));
    var seconds = Math.floor((distance % (1000 * 60)) / 1000);

    if (timerEl) {
        timerEl.textContent = minutes + "m " + seconds + "s ";
    }

    if (distance < 0) {
        clearInterval(x);
        if (timerEl) timerEl.textContent = "Out of time!";
        setButtonsDisabled(true);
        
        // Reset score
        score = 0;
        localStorage.setItem('triviaScore', score);
        if (scoreEl) scoreEl.textContent = score;
    }
}, 1000);

// 5. Explicit Event Listeners
if (trueButtonEl) trueButtonEl.addEventListener('click', () => submitAnswer('True'));
if (falseButtonEl) falseButtonEl.addEventListener('click', () => submitAnswer('False'));
if (nextButtonEl) nextButtonEl.addEventListener('click', loadNextQuestion);

// 6. Bootstrap Initial Setup
loadTriviaQuestion();
