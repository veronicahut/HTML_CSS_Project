/* **********************************************************************************************************
author: Veronica Hutchins
course: CITP 280
assignment: project part 5
date: 12/05/19
purpose: a base class for a child's game program 
notes: Inheritance is used where PreschoolGame is the base class for FriendsGame, SequenceGame,
         & PlaySafeGame classes (see those files) & at lines 
       Polymorphism will be used through the abstract method (to be overridden in child classes) at line 
       An Interface is used as abstract (to pass down to the child classes through inheritance) at line 
       Exception handling is used in Games.cs
       A File is used in Games.cs
       LINQ is used in FriendsGame.cs
       A programmer-created Extension Method is the GameExtension class
       A Database is used in FriendsGame and PlaySafeGame classes
       Threading is used in SequenceGame.cs and Games.cs
       A programmer-created Generic class is the BackgroundOptions class; a Generic list is used in the 
         FriendsGame and PlaySafeGame classes
       A Delegate is used in GameOptions.cs to wire BackgroundEvent to event handler
       A programmer-created Event is BackgroundEvent (BackgroundOptions.cs has the subscriber method)
       A static Constructor is used in TrainTheme, CarTheme, OceanTheme, and AnimalTheme classes
********************************************************************************************************** */
using System;

namespace PreschoolGames
{
    public abstract class PreschoolGame : IDesign
    {
        //declare instance variables
        private int aGameType;
        private bool aCheckAnswer;  //remove?
        private int aResponse;
        private Random aRnd = new Random();

        //constructors
        public PreschoolGame()
        { } //default

        public PreschoolGame(int gameType)
        { aGameType = gameType; }

        //properties
        public int AGameType
        { get; set; }

        public bool ACheckAnswer
        { get; set; } //{ get; }
        
        public int AResponse
        { get; set; }

        //interface will be a default background
        public abstract System.Drawing.Color Image(int aGameType);

        //instance method; the child classes will implement polymorphism
        public abstract int GetQuestion();

        //instance method to be inherited
        public int DisplayResponse(bool checkAnswer)
        {
            //if false, response number is 1 (refers to file); otherwise, it's a random number 2 - 8 (refers to reward file)
            if (checkAnswer == false) aResponse = 1;
            else aResponse = aRnd.Next(2, 8);
            return aResponse;
        }
    }
}