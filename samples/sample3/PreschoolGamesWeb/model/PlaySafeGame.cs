/* ****************************************************************************************************
author: Veronica Hutchins
course: CITP 280
assignment: project
date: 12/05/19
notes: a derived class inheriting from the PreschoolGame class for a child's game application
***************************************************************************************************** */
using System;
using System.Collections.Generic;
using System.Linq;

namespace PreschoolGames
{
    public class PlaySafeGame : PreschoolGame
    {
        // declare instance variables
        private Random aRnd3 = new Random();
        private int aQuestionNum;
        private int aAnswerNum;
        private int[] aUserChoices = new int[2];

        // REPLACE DATABASE DATA: Replaces the original database
        private static readonly List<QuestionItem> StaticQuestions = new List<QuestionItem>
        {
            new QuestionItem { Id = 21, GameTypeId = 3, AnswerId = 12 },
            new QuestionItem { Id = 22, GameTypeId = 3, AnswerId = 11 },
            new QuestionItem { Id = 23, GameTypeId = 3, AnswerId = 12 },
            new QuestionItem { Id = 24, GameTypeId = 3, AnswerId = 11 },
            new QuestionItem { Id = 25, GameTypeId = 3, AnswerId = 12 },
            new QuestionItem { Id = 26, GameTypeId = 3, AnswerId = 11 },
            new QuestionItem { Id = 27, GameTypeId = 3, AnswerId = 12 },
            new QuestionItem { Id = 28, GameTypeId = 3, AnswerId = 11 },
            new QuestionItem { Id = 29, GameTypeId = 3, AnswerId = 12 },
            new QuestionItem { Id = 30, GameTypeId = 3, AnswerId = 11 },
            new QuestionItem { Id = 31, GameTypeId = 3, AnswerId = 12 },
            new QuestionItem { Id = 32, GameTypeId = 3, AnswerId = 11 },
            new QuestionItem { Id = 33, GameTypeId = 3, AnswerId = 12 },
            new QuestionItem { Id = 34, GameTypeId = 3, AnswerId = 11 },
            new QuestionItem { Id = 35, GameTypeId = 3, AnswerId = 12 },
            new QuestionItem { Id = 36, GameTypeId = 3, AnswerId = 11 },
            new QuestionItem { Id = 37, GameTypeId = 3, AnswerId = 12 },
            new QuestionItem { Id = 38, GameTypeId = 3, AnswerId = 11 },
            new QuestionItem { Id = 39, GameTypeId = 3, AnswerId = 12 },
            new QuestionItem { Id = 40, GameTypeId = 3, AnswerId = 11 },
            new QuestionItem { Id = 41, GameTypeId = 3, AnswerId = 12 },
            new QuestionItem { Id = 42, GameTypeId = 3, AnswerId = 11 }
        };
                
        // constructors
        public PlaySafeGame()
        { } // default

        public PlaySafeGame(int gameType) : base(gameType)
        { 
            this.AGameType = gameType; 
        }

        // properties
        public int AQuestionNum
        {
            get => aQuestionNum;
            set { }
        }
        public int AAnswerNum
        {
            get => aAnswerNum;
            set { }
        }
        public int[] AUserChoices
        {
            get => aUserChoices;
            set { }
        }
        public int AUserAnswer
        { get; set; }

        // instance methods
        public override int GetQuestion() // polymorphism used
        {
            // Filter our in-memory list for Game Type 3 using LINQ
            List<int> qst = StaticQuestions
                .Where(q => q.GameTypeId == 3)
                .Select(q => q.Id)
                .ToList();

            // set default question if list is empty
            if (qst.Count == 0)
            { 
                aQuestionNum = 20; 
            }
            else
            {
                // Using 0-indexed boundary matching for the random generator
                int q = aRnd3.Next(0, qst.Count);
                aQuestionNum = qst[q];
            }

            return aQuestionNum;
        }

        public int GetAnswer(int aQuestionNum)
        {
            // Find the answer directly matching the question ID from memory
            var question = StaticQuestions.FirstOrDefault(q => q.Id == aQuestionNum);
            if (question != null)
            {
                aAnswerNum = question.AnswerId;
            }
            else
            {
                aAnswerNum = 11; // Safe fallback default
            }
            
            return aAnswerNum;
        }

        public int[] GetChoices(int aAnswerNum)
        {
            int choice3; // this game only has 2 choices - Id 11 or Id 12
            if (aAnswerNum == 11)
            { 
                choice3 = 12; 
            }
            else
            { 
                choice3 = 11; 
            }

            aUserChoices[0] = choice3;
            aUserChoices[1] = aAnswerNum;

            return aUserChoices;
        }

        public bool CheckAnswer(int aAnswerNum, int aUserAnswer)
        {
            if (aUserAnswer == aAnswerNum)
            {
                this.ACheckAnswer = true;
            }
            else
                this.ACheckAnswer = false;

            return this.ACheckAnswer;
        }

        // UPDATED: Implements the interface using web-ready CSS layout colors
        public override string Image(int aGameType)
        {
            return "lavenderblush"; // Matches the exact hex/color string for HTML styling
        }
    }
}