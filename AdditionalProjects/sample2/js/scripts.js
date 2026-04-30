// name: trivia.js
// date: 12/13/2025;4/28/2026
// author: ChatGPT, Veronica Hutchins
// description: Handles trivia question loading and answer checking

// get DOM Elements
const questionEl = document.getElementById('question');
const feedbackEl = document.getElementById('feedback-message');
const trueButtonEl = document.getElementById('true-answer');
const falseButtonEl = document.getElementById('false-answer');
const nextButtonEl = document.getElementById('next-btn'); // may be null if removed from HTML
const scoreEl = document.getElementById('points');
const timerEl = document.getElementById('timer');

// initialize
//let now = new Date().getTime();
//let secondsLeft = 0;
let current = null;
let score = parseInt(localStorage.getItem('triviaScore') || '0',10);
if (scoreEl) scoreEl.textContent = score;
//startTimer();

function setButtonsDisabled(v){
  if (trueButtonEl) trueButtonEl.disabled = v;
  if (falseButtonEl) falseButtonEl.disabled = v;
}

// method to get question from db
async function loadQuestion()
{
  if (feedbackEl) { feedbackEl.textContent = ''; feedbackEl.className = 'response'; }
  if (questionEl) questionEl.textContent = 'Loading...';
  if (nextButtonEl) nextButtonEl.style.display = 'none';
  setButtonsDisabled(true);

  // method to get question from server-side API
  try {
    const res = await fetch('/sample2/app/public/get_question.php');    
    const json = await res.json();
    if (json.success && json.data)
    { 
        current = json.data; 
        if (questionEl) questionEl.textContent = current.question; 
        setButtonsDisabled(false);        
    }
    else { if (questionEl) questionEl.textContent = 'No question found'; }
  } 
  catch (err) {
    if (questionEl) questionEl.textContent = 'Error loading question: ' + (err.message ? ' ' + err.message : '');
  }
}

// method to submit answer to server-side check
async function submitAnswer(choice)
{
  if (!current || !current.id) return;
  setButtonsDisabled(true);
  if (feedbackEl) { feedbackEl.textContent = 'Checking...'; feedbackEl.className = 'response'; }

  try {
    const res = await fetch('/sample2/app/public/check_answer.php', {
      method: 'POST',
      headers: {'Content-Type':'application/json'},
      body: JSON.stringify({id: current.id, choice})
    });
    const json = await res.json();
    if (json.success){
      if (json.correct){
        if (feedbackEl) { feedbackEl.textContent = 'Correct!'; feedbackEl.className = 'response correct'; }
        score++;
      } else {
        if (feedbackEl) { feedbackEl.textContent = 'Incorrect — correct answer: ' + json.correctAnswer; feedbackEl.className = 'response incorrect'; }
      }
      localStorage.setItem('triviaScore', score);
      if (scoreEl) scoreEl.textContent = score;

      if (nextButtonEl) {
        nextButtonEl.style.display = 'block';
      } else {
        setTimeout(() => loadQuestion(), 1200);
      }
    } else {
      if (feedbackEl) feedbackEl.textContent = 'Error checking answer.';
    }
  } catch (err){
    if (feedbackEl) feedbackEl.textContent = 'Network error.';
  }
}


// timer code adapted from W3Schools countdown tutorial
// https://www.w3schools.com/howto/howto_js_countdown.asp#:~:text=Converters,is%20finished%2C%20write%20some%20text

// Set the time for 2 minutes from now
var end = new Date().getTime() + 120000; // milliseconds

// Update the count down every 1 second
var x = setInterval(function() 
{
  // Get today's date and time
  var now = new Date().getTime();
  var distance = end - now;

  // Time calculations for minutes and seconds
  var minutes = Math.floor((distance % (1000 * 60 * 60)) / (1000 * 60));
  var seconds = Math.floor((distance % (1000 * 60)) / 1000);

  // Display the result in the element
  timerEl.textContent = minutes + "m " + seconds + "s ";

  // Display when the count down is finished
  if (distance < 0) {
    clearInterval(x);
    timerEl.textContent = "Out of time!";
    //reset score and localStorage
    // previousScore = score;
    score = 0;
    localStorage.setItem('triviaScore', score);
    if (scoreEl) scoreEl.textContent = score;
  }
}, 1000);

// event listeners
if (trueButtonEl) trueButtonEl.addEventListener('click', ()=> submitAnswer('True'));
if (falseButtonEl) falseButtonEl.addEventListener('click', ()=> submitAnswer('False'));
if (nextButtonEl) nextButtonEl.addEventListener('click', loadQuestion);

// initial load
loadQuestion();