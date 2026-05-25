/* ******************************************************************************************************
author: Veronica Hutchins
course: CITP 280
assignment: project part 5
date: 12/05/19
purpose: a derived class inheriting from the PreschoolGame class
        for a child's game application 
notes: Inheritance is used in this file at lines 27, 39
       Polymorphism is demonstrated at line 53
       An interface is implemented starting at line 97
       Exception handling is used in Games.cs file
       A File is used in Games.cs file
       LINQ is used in FriendsGame.cs
       A programmer-created Extension Method is the GameExtension.cs and used in Games.cs 
       A Database is used in FriendsGame.cs & PlaySafeGame.cs
       Threading is at line 87
       A Generic class is the GameOptions class & used in Games.cs file
       A Delegate is used in GameOptions.cs to wire BackgroundEvent to event handler
       A programmer-created Event is BackgroundEvent (BackgroundOptions.cs has the subscriber method)
       A static Constructor is used in TrainTheme, CarTheme, OceanTheme, and AnimalTheme classes
******************************************************************************************************* */
using System;
using System.Threading;

namespace PreschoolGames
{
    class SequenceGame : PreschoolGame
    {
        //declare instance variables
        private Random aRnd2 = new Random();
        private int aQuestionNum;
        private int[] aAnswer = new int[5];
        private int[] aUserAnswer = new int[5];

        //constructors
        public SequenceGame()
        { } //default

        public SequenceGame(int gameType) : base(gameType)
        { this.AGameType = gameType; }

        //properties
        public int AQuestionNum 
        { get => aQuestionNum; }

        public int[] AAnswer 
        { get => aAnswer; }

        public int[] AUserAnswer
        { get; set; }

        //instance methods
        public override int GetQuestion()  //polymorphism; gets number to go in array
        {
            aQuestionNum = aRnd2.Next(1, 5);
            return aQuestionNum;
        }
        public int[] GetAnswer()  //fills array
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
            int check = 0; //adds 1 for wrong answer

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
        //method using threading; allows color to stay visible for a short time
        //public int UseThreadMethod(int lbl)
        public void UseThreadMethod()
        {
            object aLock = new object();
            //the lock statement
            lock (aLock)
            {
                Thread.Sleep(1000);
            }
        }
        //implement interface, which will be the default background
        public override System.Drawing.Color Image(int aGameType)
        {
            SequenceGame f = new SequenceGame(2);
            return System.Drawing.Color.LightBlue;
        }
    }
}