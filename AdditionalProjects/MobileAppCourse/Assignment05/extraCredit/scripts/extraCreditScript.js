// Veronica Hutchins
// 10/14/2025
// Assignment 05 - extra credit

$(function()
{
    var imageNum = 6; // default

    $('#displayMe').append("<img src='images/veronica-hutchins-cloud-pic-ms-paint.jpg' class='img-fluid rounded-circle' alt='artwork with - see what the future brings'>");
    $('#displayMe').append("<p>Original Photo and Digital art by Veronica Hutchins</p>"); 

    $('#btnFortune').click(function()
    {
        $('#displayMe').empty();
        imageNum = Math.floor(Math.random() * 6) + 1;

        if (imageNum == 1) 
        { 
            $('#displayMe').append("<img src='images/florian-olivo-min-unsplash.jpg' class='img-fluid rounded-circle' alt='code on computer screen with - Your lucky number is pi'>");
            $('#displayMe').append("<p>Original Photo by <a href='https://unsplash.com/@florianolv?utm_content=creditCopyText&utm_medium=referral&utm_source=unsplash'>Florian Olivo</a> on <a href='https://unsplash.com/photos/lines-of-html-codes-4hbJ-eymZ1o?utm_content=creditCopyText&utm_medium=referral&utm_source=unsplash'>Unsplash</a> </p>"); 
        }
        else if (imageNum == 2) 
        { 
            $('#displayMe').append("<img src='images/altumcode-min-unsplash.jpg' class='img-fluid rounded-circle' alt='computer screen on desk with - Don't leave your house today'>");
            $('#displayMe').append("<p>Original Photo by <a href='https://unsplash.com/@altumcode?utm_content=creditCopyText&utm_medium=referral&utm_source=unsplash'>AltumCode</a> on <a href='https://unsplash.com/photos/opened-black-laptop-computer-oZ61KFUQsus?utm_content=creditCopyText&utm_medium=referral&utm_source=unsplash'>Unsplash</a></p>"); 
        }
        else if (imageNum == 3) 
        { 
            $('#displayMe').append("<img src='images/markus-spiske-min-unsplash.jpg' class='img-fluid rounded-circle' alt='green shapes on black screen with - Luck is on your side today'>");
            $('#displayMe').append("<p>Original Photo by <a href='https://unsplash.com/@markusspiske?utm_content=creditCopyText&utm_medium=referral&utm_source=unsplash'>Markus Spiske</a> on <a href='https://unsplash.com/photos/green-and-black-stripe-textile-gcgves5H_Ac?utm_content=creditCopyText&utm_medium=referral&utm_source=unsplash'>Unsplash</a></p>"); 
        }
        else if (imageNum == 4) 
        { 
            $('#displayMe').append("<img src='images/michael-dziedzic-min-unsplash.jpg' class='img-fluid rounded-circle' alt='red and black artwork with - Beware of Mondays'>");
            $('#displayMe').append("<p>Original Photo by <a href='https://unsplash.com/@lazycreekimages?utm_content=creditCopyText&utm_medium=referral&utm_source=unsplash'>Michael Dziedzic</a> on <a href='https://unsplash.com/photos/red-and-black-abstract-illustration-aQYgUYwnCsM?utm_content=creditCopyText&utm_medium=referral&utm_source=unsplash'>Unsplash</a></p>"); 
        }
        else if (imageNum == 5) 
        { 
            $('#displayMe').append("<img src='images/veronica-hutchins-chlng-mspaint.jpg' class='img-fluid rounded-circle' alt='artwork with - I see challenges ahead'>");
            $('#displayMe').append("<p>Digital art by Veronica Hutchins</p>"); 
        }
        else 
        { 
           $('#displayMe').append("<img src='images/veronica-hutchins-good-mspaint.jpg' class='img-fluid rounded-circle' alt='artwork with - I see good things coming'>");
           $('#displayMe').append("<p>Digital art by Veronica Hutchins</p>");
        }
    });
});