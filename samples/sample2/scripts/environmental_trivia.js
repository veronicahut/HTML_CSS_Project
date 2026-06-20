// name: environmental_trivia.js
// date: 06/19/2026
// author: Veronica Hutchins and Gemini
// description: Handles trivia question loading and displays facts and answers

// DOM Elements
const questionEl = document.getElementById('question');
const feedbackEl = document.getElementById('feedback-message');
const trueButtonEl = document.getElementById('true-answer');
const falseButtonEl = document.getElementById('false-answer');
const nextButtonEl = document.getElementById('next-btn');
const scoreEl = document.getElementById('points');
const timerEl = document.getElementById('timer');
const restartButtonEl = document.getElementById('restart-btn');

// Global Game Variables
let questionsArray = [];       
let currentQuestionIndex = 0;  
let score = parseInt(localStorage.getItem('triviaScore') || '0', 10);
let timerInterval; // Store interval globally so we can clear it
let timerEndTimestamp; // Store end time globally

if (scoreEl) scoreEl.textContent = score;

// Core Gameplay Functions
async function loadTriviaQuestion() {
    try {
        // JSON file located in includes folder
        const response = await fetch('includes/environmental_trivia.json').catch(() => 
            fetch('../includes/environmental_trivia.json')
        );

        if (!response.ok) {
            throw new Error(`HTTP error! status: ${response.status}`);
        }
        
        questionsArray = await response.json();
        
        // Ensure data received before trying to render
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
        feedbackEl.style.color = ""; // Clear 
    }
    if (nextButtonEl) nextButtonEl.style.display = 'none';
    if (restartButtonEl) restartButtonEl.style.display = 'none'; // Hide by default
    
    if (currentQuestionIndex >= questionsArray.length) {
        if (questionEl) questionEl.textContent = " 🎉 You've completed all available trivia questions!";
        setButtonsDisabled(true);
        clearInterval(timerInterval); // Stop the timer on a win condition!
        if (restartButtonEl) restartButtonEl.style.display = 'inline-block'; // Show restart option
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

    // Pull the clean reference fact out of new JSON field setup
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

function startTimer() {
    // Clear any active timer intervals running beforehand
    clearInterval(timerInterval); 
    
    timerEndTimestamp = new Date().getTime() + 120000; 

    timerInterval = setInterval(function() {
        var now = new Date().getTime();
        var distance = timerEndTimestamp - now;

        var minutes = Math.floor((distance % (1000 * 60 * 60)) / (1000 * 60));
        var seconds = Math.floor((distance % (1000 * 60)) / 1000);

        if (timerEl) {
            timerEl.textContent = minutes + "m " + seconds + "s ";
        }

        if (distance < 0) {
            clearInterval(timerInterval);
            if (timerEl) timerEl.textContent = "Out of time!";

            // Disable buttons
            setButtonsDisabled(true);            

            // Hide next button so they can't continue
            if (nextButtonEl) nextButtonEl.style.display = 'none';
            //if (nextButtonEl) nextButtonEl.disabled = true;
            
            score = 0;
            localStorage.setItem('triviaScore', score);
            if (scoreEl) scoreEl.textContent = score;
            
            // Show play again button if they run out of time
            if (restartButtonEl) restartButtonEl.style.display = 'inline-block';
        }
    }, 1000);
}

function resetGame() {
    currentQuestionIndex = 0;
    score = 0;
    localStorage.setItem('triviaScore', score);
    if (scoreEl) scoreEl.textContent = score;
    
    // Restart timer and show first question
    startTimer();
    displayCurrentQuestion();
}

// Event Listeners
if (trueButtonEl) trueButtonEl.addEventListener('click', () => submitAnswer('True'));
if (falseButtonEl) falseButtonEl.addEventListener('click', () => submitAnswer('False'));
if (nextButtonEl) nextButtonEl.addEventListener('click', loadNextQuestion);
if (restartButtonEl) restartButtonEl.addEventListener('click', resetGame); // New listener

// Bootstrap Initial Run
loadTriviaQuestion();
startTimer(); // Start timer