/* ****************************************************************************************************
author: Veronica Hutchins
course: CITP 280
assignment: project
date: 12/05/19
note: a derived class inheriting from the PreschoolGame class for a child's game application
***************************************************************************************************** */
using System;
using System.Collections.Generic;
using System.Linq;

namespace PreschoolGames
{
    // Define lightweight data structures to replace the SQL database tables
    public class QuestionItem
    {
        public int Id { get; set; }
        public int GameTypeId { get; set; }
        public int AnswerId { get; set; }
    }

    public class AnswerItem
    {
        public int Id { get; set; }
        public string Type { get; set; } // "positive" or "negative"
    }

    public class FriendsGame : PreschoolGame // inheritance
    {
        // declare instance variables
        private Random aRnd1 = new Random();
        private int aQuestionNum;
        private int aAnswerNum;
        private int[] aUserChoices = new int[3];

        // Replaces Database data - mock database in memory
        private static readonly List<QuestionItem> StaticQuestions = new List<QuestionItem>
        {
            new QuestionItem { Id = 1, GameTypeId = 1, AnswerId = 1 },
            new QuestionItem { Id = 2, GameTypeId = 1, AnswerId = 2 },
            new QuestionItem { Id = 3, GameTypeId = 1, AnswerId = 9 },
            new QuestionItem { Id = 4, GameTypeId = 1, AnswerId = 4 },
            new QuestionItem { Id = 5, GameTypeId = 1, AnswerId = 3 },
            new QuestionItem { Id = 6, GameTypeId = 1, AnswerId = 4 },
            new QuestionItem { Id = 7, GameTypeId = 1, AnswerId = 7 },
            new QuestionItem { Id = 8, GameTypeId = 1, AnswerId = 10 },
            new QuestionItem { Id = 9, GameTypeId = 1, AnswerId = 5 },
            new QuestionItem { Id = 10, GameTypeId = 1, AnswerId = 6 },
            new QuestionItem { Id = 11, GameTypeId = 1, AnswerId = 9 },
            new QuestionItem { Id = 12, GameTypeId = 1, AnswerId = 2 },
            new QuestionItem { Id = 13, GameTypeId = 1, AnswerId = 3 },
            new QuestionItem { Id = 14, GameTypeId = 1, AnswerId = 4 },
            new QuestionItem { Id = 15, GameTypeId = 1, AnswerId = 1 },
            new QuestionItem { Id = 16, GameTypeId = 1, AnswerId = 4 },
            new QuestionItem { Id = 17, GameTypeId = 1, AnswerId = 7 },
            new QuestionItem { Id = 18, GameTypeId = 1, AnswerId = 4 },
            new QuestionItem { Id = 19, GameTypeId = 1, AnswerId = 7 },
            new QuestionItem { Id = 20, GameTypeId = 1, AnswerId = 8 }
        };

        private static readonly List<AnswerItem> StaticAnswers = new List<AnswerItem>
        {
            new AnswerItem { Id = 1, Type = "positive" },
            new AnswerItem { Id = 2, Type = "negative" },
            new AnswerItem { Id = 3, Type = "positive" },
            new AnswerItem { Id = 4, Type = "negative" }
            // Need to add more mock answer records here!
        };

        // constructors
        public FriendsGame()
        { } // default

        public FriendsGame(int gameType) : base(gameType)
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
        public int AUserAnswer { get; set; }

        // methods
        public override int GetQuestion()  // polymorphism
        {
            // Instead of querying SQL, filter our in-memory list using LINQ
            List<int> qst = StaticQuestions
                .Where(q => q.GameTypeId == 1)
                .Select(q => q.Id)
                .ToList();

            if (qst.Count >= 1)
            {
                // Note: aRnd1.Next(0, count) works better for 0-indexed Lists
                int q = aRnd1.Next(0, qst.Count);
                aQuestionNum = qst[q];
            }
            else
            { 
                aQuestionNum = 1; 
            }

            return aQuestionNum;
        }

        public int GetAnswer(int aQuestionNum)
        {
            // Find the answer matching the question ID from memory
            var question = StaticQuestions.FirstOrDefault(q => q.Id == aQuestionNum);
            if (question != null)
            {
                aAnswerNum = question.AnswerId;
            }

            return aAnswerNum;
        }

        public int[] GetChoices(int aAnswerNum)
        {
            int choice1;
            int choice2;

            // Get answers using LINQ directly from our mock list
            List<int> aList = StaticAnswers
                .Where(a => a.Type == "positive" || a.Type == "negative")
                .Select(a => a.Id)
                .ToList();

            List<int> chFilter = new List<int>();
            
            // The original game selection logic stays exactly the same
            if (aAnswerNum % 2 == 0)
            {
                var choiceFilter = aList.Where(list => list % 2 != 0);

                foreach (var i in choiceFilter)
                {
                    chFilter.Add(i);
                }
                int element1 = aRnd1.Next(0, chFilter.Count);
                choice1 = chFilter[element1];
                while (choice1 == aAnswerNum)
                {
                    element1 = aRnd1.Next(0, chFilter.Count);
                    choice1 = chFilter[element1];
                }
                int element2 = aRnd1.Next(0, chFilter.Count);
                choice2 = chFilter[element2];
                while (choice2 == aAnswerNum || choice2 == choice1)
                {
                    element2 = aRnd1.Next(0, chFilter.Count);
                    choice2 = chFilter[element2];
                }
            }
            else
            {
                var choiceQuery = aList.Where(list => list % 2 == 0);
                foreach (var i in choiceQuery)
                {
                    chFilter.Add(i);
                }
                int element1 = aRnd1.Next(0, chFilter.Count);
                choice1 = chFilter[element1];
                while (choice1 == aAnswerNum)
                {
                    element1 = aRnd1.Next(0, chFilter.Count);
                    choice1 = chFilter[element1];
                }
                int element2 = aRnd1.Next(0, chFilter.Count);
                choice2 = chFilter[element2];
                while (choice2 == aAnswerNum || choice2 == choice1)
                {
                    element2 = aRnd1.Next(0, chFilter.Count);
                    choice2 = chFilter[element2];
                }
            }  
            
            // final choices:
            aUserChoices[0] = choice1;
            aUserChoices[1] = aAnswerNum; 
            aUserChoices[2] = choice2;
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
                
            return ACheckAnswer;
        }

        // Removed Thread.Sleep completely as it is handled natively via async/await UI side
        // public void ThreadMethod()
        // { }

        // UPDATED: Implementing the interface with web-safe CSS string layout colors
        public override string Image(int aGameType)
        {
            return "lightgreen";
        }
    }
}