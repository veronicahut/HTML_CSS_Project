/* **********************************************************************************************************
author: Veronica Hutchins
course: CITP 280
assignment: project
date: 12/05/19
notes: this is a base class for a child's game programInheritance is used where PreschoolGame is the 
    base class for FriendsGame, SequenceGame, & PlaySafeGame classes 
********************************************************************************************************** */
using System;

namespace PreschoolGames
{
    public abstract class PreschoolGame : IDesign
    {
        // declare instance variables
        private int aGameType;
        private bool aCheckAnswer; 
        private int aResponse;
        private Random aRnd = new Random();

        // constructors
        public PreschoolGame()
        { } // default

        public PreschoolGame(int gameType)
        { 
            aGameType = gameType; 
        }

        // properties
        public int AGameType
        { get; set; }

        public bool ACheckAnswer
        { get; set; }
        
        public int AResponse
        { get; set; }

        // UPDATED: Now returns a CSS-friendly string instead of WinForms Color
        public abstract string Image(int aGameType);

        // instance method; the child classes will implement polymorphism
        public abstract int GetQuestion();

        // instance method to be inherited
        public int DisplayResponse(bool checkAnswer)
        {
            // if false, response number is 1; otherwise, a random reward number 2 - 7
            if (checkAnswer == false) 
                aResponse = 1;
            else 
                aResponse = aRnd.Next(2, 8);
                
            return aResponse;
        }
    }
}