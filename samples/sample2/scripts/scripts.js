// name: environmental_trivia.js
// date: 06/19/2026
// author: Veronica Hutchins & Gemini
// description: Handles trivia question loading and displays educational facts alongside answers

// 1. DOM Elements
const questionEl = document.getElementById('question');
const feedbackEl = document.getElementById('feedback-message');
const trueButtonEl = document.getElementById('true-answer');
const falseButtonEl = document.getElementById('false-answer');
const nextButtonEl = document.getElementById('next-btn');
const scoreEl = document.getElementById('points');
const timerEl = document.getElementById('timer');

// 2. Global Game Variables
let questionsArray = [];       
let currentQuestionIndex = 0;  
let score = parseInt(localStorage.getItem('triviaScore') || '0', 10);

if (scoreEl) scoreEl.textContent = score;

// 3. Core Gameplay Functions
async function loadTriviaQuestion() {
    try {
        // If files are in the same directory now, use 'environmental_trivia.json'
        // Otherwise, fallback to your includes directory folder path
        const response = await fetch('../../includes/environmental_trivia.json').catch(() => 
            fetch('../includes/environmental_trivia.json')
        );

        if (!response.ok) {
            throw new Error(`HTTP error! status: ${response.status}`);
        }
        
        questionsArray = await response.json();
        
        // Ensure we actually received data before trying to render
        if (questionsArray && questionsArray.length > 0) {
            displayCurrentQuestion();
        } else {
            if (questionEl) questionEl.textContent = "No trivia questions found in data file.";
        }
        
    } catch (error) {
        console.error("Could not load trivia questions:", error);
        if (questionEl) questionEl.textContent = "Error loading trivia questions. Please check file path.";
    }
}

function displayCurrentQuestion() {
    if (feedbackEl) {
        feedbackEl.textContent = "";
        feedbackEl.style.color = ""; // Clear out previous color states
    }
    if (nextButtonEl) nextButtonEl.style.display = 'none';
    
    if (currentQuestionIndex >= questionsArray.length) {
        if (questionEl) questionEl.textContent = " 🎉 You've completed all available trivia questions!";
        setButtonsDisabled(true);
        return;
    }

    const currentQuestion = questionsArray[currentQuestionIndex];
    if (questionEl) questionEl.textContent = currentQuestion.question;
    
    setButtonsDisabled(false);
}

function submitAnswer(selectedOption) {
    const currentQuestion = questionsArray[currentQuestionIndex];
    if (!currentQuestion || !feedbackEl) return;

    setButtonsDisabled(true);

    const correctAnswerObj = currentQuestion.answers.find(ans => ans.correct === true);
    const correctAnswerText = correctAnswerObj ? correctAnswerObj.text : "";

    // DYNAMIC FACT PRESENTATION:
    // Pull the clean reference fact out of your new JSON field setup
    const educationalFact = currentQuestion.fact ? `\n\nFact: ${currentQuestion.fact}` : "";

    if (selectedOption === correctAnswerText) {
        score++;
        localStorage.setItem('triviaScore', score);
        if (scoreEl) scoreEl.textContent = score;
        
        feedbackEl.innerText = `Correct! 🌱 Excellent job.${educationalFact}`;
        feedbackEl.style.color = "#2e7d32"; 
    } else {
        feedbackEl.innerText = `Incorrect. The correct answer was: ${correctAnswerText}.${educationalFact}`;
        feedbackEl.style.color = "#c62828"; 
    }

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

// 4. Countdown Timer Logic (2 Minutes)
var end = new Date().getTime() + 120000; 

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
        
        score = 0;
        localStorage.setItem('triviaScore', score);
        if (scoreEl) scoreEl.textContent = score;
    }
}, 1000);

// 5. Event Listeners
if (trueButtonEl) trueButtonEl.addEventListener('click', () => submitAnswer('True'));
if (falseButtonEl) falseButtonEl.addEventListener('click', () => submitAnswer('False'));
if (nextButtonEl) nextButtonEl.addEventListener('click', loadNextQuestion);

// 6. Bootstrap Initial Run
loadTriviaQuestion();