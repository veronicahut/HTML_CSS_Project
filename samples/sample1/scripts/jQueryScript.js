// Veronica Hutchins 
// 10/8/2025
// Assignment - part 2

// get year
var year = new Date().getFullYear();

// get the dates for Thanksgiving, Christmas, and New Years Eve of 2025 
const Thanksgiving25 = new Date("November 27, " + year + " 00:00:00").getTime();
const Christmas25 = new Date("December 25, " + year + " 00:00:00").getTime();
const NewYearsEve25 = new Date("December 31, " + year + " 00:00:00").getTime();

// prepare messages
var ThanksgivingMessage = "until Thanksgiving " + year;
var ChristmasMessage = "until Christmas " + year;
var NewYearsMessage = "until New Year's Eve " + year;

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
        if (days < 0)
        {   
            days = days * -1;
            message = "It has been " + days + " days, " + hours + " hours, " + minutes + " minutes, and " + seconds + " seconds " +  " since Thanksgiving";
        } 
        else 
        {
            message = "There are " + days + " days, " + hours + " hours, " + minutes + " minutes, and " + seconds + " seconds " +  ThanksgivingMessage;
        }        
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
            if (days < 0)
            {
                days = days * -1;
                message = "It has been " + days + " days, " + hours + " hours, " + minutes + " minutes, and " + seconds + " seconds " +  " since New Year's Eve";                
            }
            else 
            {
                message = "There are " + days + " days, " + hours + " hours, " + minutes + " minutes, and " + seconds + " seconds " + NewYearsMessage;
            }
        }
        else 
        {
            // set variable, calculate days, hours, minutes, and seconds
            timeLeft = Christmas25 - now;
            days = Math.floor(timeLeft / (1000 * 60 * 60 * 24));
            hours = Math.floor((timeLeft % (1000 * 60 * 60 * 24)) / (1000 * 60 * 60));
            minutes = Math.floor((timeLeft % (1000 * 60 * 60)) / (1000 * 60));
            seconds = Math.floor((timeLeft % (1000 * 60)) / 1000);
            if (days < 0)
            {
                days = days * -1;
                message = "It has been " + days + " days, " + hours + " hours, " + minutes + " minutes, and " + seconds + " seconds " +  " since Christmas";                
            }
            else 
            {
                message = "There are " + days + " days, " + hours + " hours, " + minutes + " minutes, and " + seconds + " seconds " + ChristmasMessage;
            }
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
    $('#hThree').animate({left: '250px'}, "slow");
    $('#pGreen').css("color", "green");

    $('p').click(function()
    {
        $(this).hide();
    });

    $('#btnSurprise').click(function()
    { 
        if (this.textContent === "Holidays Come So Quickly!")
        { this.textContent = "Surprise Holiday Countdown"; }
        else 
        { this.textContent = "Holidays Come So Quickly!"; }
        
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

        // Helper function to best fit the backgrounds uniformly
        function setBackground(imageUrl) 
        {
            $('body').css({
                'background-image': 'url(' + imageUrl + ')',
                'background-size': 'cover',
                'background-position': 'center center',
                'background-repeat': 'no-repeat',
                'background-attachment': 'fixed' // keeps background fixed while scrolling
            });
        }

        // Cycle through the images and credits, resetting after the last one
        if (i < 6)
        {
            if (i == 1)
            {
                setBackground('images/BarHarborMaine_20231012.jpg');
                $('#credits').append('Photo by Veronica Hutchins');
            }
            else if (i == 2)
            {
                setBackground('images/BarHarborMaine_20231013.jpg');
                $('#credits').append('Photo by Veronica Hutchins');
            }
            else if (i == 3)
            {
                setBackground('images/GrandHaven_20240716.jpg');
                $('#credits').append('Photo by Veronica Hutchins');
            }
            else if (i == 4) 
            {
                setBackground('images/GrandHaven_20240221.jpg');
                $('#credits').append('Photo by Veronica Hutchins');
            }
            else if (i == 5) 
            {
                setBackground('images/HawkIsland_20221022.jpg');
                $('#credits').append('Photo by Veronica Hutchins');
            }
        }
        else 
        {
            $('body').css('background-image', 'none');
            //reset
            i = 0;
        }

    });

});
