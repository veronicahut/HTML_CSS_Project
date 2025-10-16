// Veronica Hutchins 
// 10/8/2025
// Assignment 05 - part 2

// get the dates for Thanksgiving, Christmas, and New Years Eve of 2025 
const Thanksgiving25 = new Date("November 27, 2025 00:00:00").getTime();
const Christmas25 = new Date("December 25, 2025 00:00:00").getTime();
const NewYearsEve25 = new Date("December 31, 2025 00:00:00").getTime();

// prepare messages
var ThanksgivingMessage = "until Thanksgiving 2025";
var ChristmasMessage = "until Christmas 2025";
var NewYearsMessage = "until New Year's Eve 2025";

// create variables
var timeLeft;
var days;
var hours;
var minutes;
var seconds;    
var message = "";

function surprise()
{
    // get today's date and time, create variables
    var now = new Date().getTime();    

    // get random numbers between 1 and 3 to determine which holiday to use
    var randomNum1 = Math.ceil(Math.random() * 3) + 1;
    var randomNum2 = Math.floor(Math.random() * 3) + 1;

    if(randomNum1 == 2)
    {
        // set variable, calculate days, hours, minutes, and seconds
        timeLeft = Thanksgiving25 - now;
        days = Math.floor(timeLeft / (1000 * 60 * 60 * 24));
        hours = Math.floor((timeLeft % (1000 * 60 * 60 * 24)) / (1000 * 60 * 60));
        minutes = Math.floor((timeLeft % (1000 * 60 * 60)) / (1000 * 60));
        seconds = Math.floor((timeLeft % (1000 * 60)) / 1000);
        message = "There are " + days + " days, " + hours + " hours, " + minutes + " minutes, and " + seconds + " seconds " +  ThanksgivingMessage;
    }
    else 
    {
        if(randomNum2 == 1)
        {
            // set variable, calculate days, hours, minutes, and seconds
            timeLeft = NewYearsEve25 - now;
            days = Math.floor(timeLeft / (1000 * 60 * 60 * 24));
            hours = Math.floor((timeLeft % (1000 * 60 * 60 * 24)) / (1000 * 60 * 60));
            minutes = Math.floor((timeLeft % (1000 * 60 * 60)) / (1000 * 60));
            seconds = Math.floor((timeLeft % (1000 * 60)) / 1000);
            message = "There are " + days + " days, " + hours + " hours, " + minutes + " minutes, and " + seconds + " seconds " + NewYearsMessage;
        }
        else 
        {
            // set variable, calculate days, hours, minutes, and seconds
            timeLeft = Christmas25 - now;
            days = Math.floor(timeLeft / (1000 * 60 * 60 * 24));
            hours = Math.floor((timeLeft % (1000 * 60 * 60 * 24)) / (1000 * 60 * 60));
            minutes = Math.floor((timeLeft % (1000 * 60 * 60)) / (1000 * 60));
            seconds = Math.floor((timeLeft % (1000 * 60)) / 1000);
            message = "There are " + days + " days, " + hours + " hours, " + minutes + " minutes, and " + seconds + " seconds " + ChristmasMessage;
        }
    }

    return message;
}

$(function()
{
    var letterStretch = false;
    var hOneVisible = false;
    var i = 0;
    var j = 0;

    $('h1').fadeOut(4000);
    $('h2').slideToggle(3000);
    $('div').animate({left: '250px'}, "slow");
    $('#pGreen').css("color", "green");

    $('p').click(function()
    {
        $(this).hide();
    });

     $('#btnSurprise').click(function()
    { 
        if (this.textContent === "It's Unbelievable!")
        { this.textContent = "Surprise Countdown"; }
        else 
        { this.textContent = "It's Unbelievable!"; }
        
        // show result of surprise function in alert box
        alert('Surprise! ' + surprise() + '.'); 
    });

    $('#btnShow').click(function()
    {
        $('p').toggle();        
    });

     $('#btnShift').click(function()
    {
        j = j + 1;

        if (j % 2 == 0) 
        { $('#hThree').css('position', 'relative').animate({left: '250px'}, "slow"); }
        else 
        { $('#hThree').css('position', 'relative').animate({left: '0px'}, "slow"); }
    });

    $('#btnStretch').click(function()
    {
        if (letterStretch == false) 
        { 
            $('#pTwo').css('letter-spacing', '10px'); 
            letterStretch = true;
        }
        else 
        { 
            $('#pTwo').css('letter-spacing', '0.5px'); 
            letterStretch = false; 
        }        
    });

    $('#btnFade').click(function()
    {
        if (hOneVisible == false) 
        {
            $('h1').css("color", "red");
            $('h1').fadeIn(4000);
            hOneVisible = true;
        }
        else 
        { 
            $('h1').css("color", "orange");
            $('h1').fadeOut(2000); 
            hOneVisible = false;
        }
    });

    $('#btnSink').click(function()
    {
        $('h2').slideToggle(3000); 
    });

    $('#btnScenery').click(function()
    {
        i = i + 1;
        $('#credits').empty();

        if (i % 2 == 0) 
        {
            $('body').css('background-image', 'url(images/jeshoots-com-9n1USijYJZ4-unsplash-min.jpg)');
            $('body').css('background-size', 'cover');
            $('#credits').append('Photo credits: Photo by JESHOOTS.COM on Unsplash');
            //Photo by <a href="https://unsplash.com/@jeshoots?utm_source=unsplash&utm_medium=referral&utm_content=creditCopyText">JESHOOTS.COM</a> on <a href="https://unsplash.com/photos/pencil-on-opened-notebook-9n1USijYJZ4?utm_source=unsplash&utm_medium=referral&utm_content=creditCopyText">Unsplash</a>      
        }
        else if (i % 3 == 0) 
        {
            $('body').css('background-image', 'none');
        }
        else 
        {
            $('body').css('background-image', 'url(images/towfiqu-barbhuiya-bwOAixLG0uc-unsplash-min.jpg)');
            $('body').css('background-size', 'cover');
            $('#credits').append('Photo credits: Photo by Towfiqu barbhuiya on Unsplash');
            // Photo by <a href="https://unsplash.com/@towfiqu999999?utm_source=unsplash&utm_medium=referral&utm_content=creditCopyText">Towfiqu barbhuiya</a> on <a href="https://unsplash.com/photos/a-calendar-with-red-push-buttons-pinned-to-it-bwOAixLG0uc?utm_source=unsplash&utm_medium=referral&utm_content=creditCopyText">Unsplash</a>      
        }
    });

});
