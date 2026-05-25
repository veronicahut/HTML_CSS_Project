/* **********************************************************************************************************
author: Veronica Hutchins
course: CITP 280
assignment: project
date: 12/05/19
notes: a derived class for a child's sequence game
********************************************************************************************************** */
using System;
using System.Drawing;
using System.Threading.Tasks; // Added for modern web async tasks

namespace PreschoolGames
{
    public class SequenceGame : PreschoolGame
    {
        // declare instance variables
        private Random aRnd2 = new Random();
        private int aQuestionNum;
        private int[] aAnswer = new int[5];
        private int[] aUserAnswer = new int[5];

        // constructors
        public SequenceGame()
        { } // default

        public SequenceGame(int gameType) : base(gameType)
        { 
            this.AGameType = gameType; 
        }

        // properties
        public int AQuestionNum 
        { get => aQuestionNum; }

        public int[] AAnswer 
        { get => aAnswer; }

        public int[] AUserAnswer
        { get; set; }

        // instance methods
        public override int GetQuestion()  // polymorphism; gets number to go in array
        {
            aQuestionNum = aRnd2.Next(1, 5);
            return aQuestionNum;
        }

        public int[] GetAnswer()  // fills array
        {
            for (int i = 0; i < aAnswer.Length; i++)
            {
                aAnswer[i] = GetQuestion();
            }
            return aAnswer;
        }

        public bool CheckAnswer(int[] aAnswer, int[] aUserAnswer, int count)
        {
            bool aCheckAnswer;
            int check = 0; // adds 1 for wrong answer

            for (int i = 0; i <= count; i++)
            {
                if (aUserAnswer[i] == aAnswer[i])
                {
                    check = check + 0;
                }
                else
                    check = check + 1;
            }
            if (check == 0) aCheckAnswer = true;
            else aCheckAnswer = false;

            return aCheckAnswer;
        }

        // UPDATED: Modern async task for browser-friendly pausing
        // This replaces the restrictive desktop Thread.Sleep lock
        public async Task UseThreadMethodAsync()
        {
            await Task.Delay(1000); // Gracefully pauses for 1 second in the browser
        }

        // UPDATE: Return a clean string for web CSS instead of colors
        public override string Image(int aGameType)
        {
            return "lightblue"; // Blazor can not inject a color directly into an HTML style attribute
        }
    }
}