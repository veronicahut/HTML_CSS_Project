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

    public class FriendsGame : PreschoolGame
    {
        private Random aRnd1 = new Random();
        private int aQuestionNum;
        private int aAnswerNum;
        private int[] aUserChoices = new int[3];

        private static readonly List<QuestionItem> StaticQuestions = new List<QuestionItem>
        {
            new QuestionItem { Id = 1, GameTypeId = 1, AnswerId = 1 },
            new QuestionItem { Id = 2, GameTypeId = 1, AnswerId = 3 },
            new QuestionItem { Id = 3, GameTypeId = 1, AnswerId = 5 },
            new QuestionItem { Id = 4, GameTypeId = 1, AnswerId = 7 },
            new QuestionItem { Id = 5, GameTypeId = 1, AnswerId = 9 },
            new QuestionItem { Id = 6, GameTypeId = 1, AnswerId = 11 },
            new QuestionItem { Id = 7, GameTypeId = 1, AnswerId = 13 },
            new QuestionItem { Id = 8, GameTypeId = 1, AnswerId = 15 },
            new QuestionItem { Id = 9, GameTypeId = 1, AnswerId = 17 },
            new QuestionItem { Id = 10, GameTypeId = 1, AnswerId = 19 },
            new QuestionItem { Id = 11, GameTypeId = 1, AnswerId = 1 },
            new QuestionItem { Id = 12, GameTypeId = 1, AnswerId = 3 },
            new QuestionItem { Id = 13, GameTypeId = 1, AnswerId = 5 },
            new QuestionItem { Id = 14, GameTypeId = 1, AnswerId = 7 },
            new QuestionItem { Id = 15, GameTypeId = 1, AnswerId = 9 },
            new QuestionItem { Id = 16, GameTypeId = 1, AnswerId = 11 },
            new QuestionItem { Id = 17, GameTypeId = 1, AnswerId = 13 },
            new QuestionItem { Id = 18, GameTypeId = 1, AnswerId = 15 },
            new QuestionItem { Id = 19, GameTypeId = 1, AnswerId = 17 },
            new QuestionItem { Id = 20, GameTypeId = 1, AnswerId = 19 }
        };

        private static readonly List<AnswerItem> StaticAnswers = new List<AnswerItem>
        {
            new AnswerItem { Id = 1, Type = "positive" },
            new AnswerItem { Id = 2, Type = "negative" },
            new AnswerItem { Id = 3, Type = "positive" },
            new AnswerItem { Id = 4, Type = "negative" },
            new AnswerItem { Id = 5, Type = "positive" },
            new AnswerItem { Id = 6, Type = "negative" },
            new AnswerItem { Id = 7, Type = "positive" },
            new AnswerItem { Id = 8, Type = "negative" },
            new AnswerItem { Id = 9, Type = "positive" },
            new AnswerItem { Id = 10, Type = "negative" },
            new AnswerItem { Id = 11, Type = "positive" },
            new AnswerItem { Id = 12, Type = "negative" },
            new AnswerItem { Id = 13, Type = "positive" },
            new AnswerItem { Id = 14, Type = "negative" },
            new AnswerItem { Id = 15, Type = "positive" },
            new AnswerItem { Id = 16, Type = "negative" },
            new AnswerItem { Id = 17, Type = "positive" },
            new AnswerItem { Id = 18, Type = "negative" },
            new AnswerItem { Id = 19, Type = "positive" },
            new AnswerItem { Id = 20, Type = "negative" }
        };

        public FriendsGame() { }

        public FriendsGame(int gameType) : base(gameType)
        { 
            this.AGameType = gameType; 
        }

        public int AQuestionNum { get => aQuestionNum; set { } }
        public int AAnswerNum { get => aAnswerNum; set { } }
        public int[] AUserChoices { get => aUserChoices; set { } }
        public int AUserAnswer { get; set; }

        public override int GetQuestion()
        {
            List<int> qst = StaticQuestions
                .Where(q => q.GameTypeId == 1)
                .Select(q => q.Id)
                .ToList();

            if (qst.Count >= 1)
            {
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

            // FIXED: If correct answer is odd (positive), decoy options are even (negative).
            // If correct answer is even (negative), decoy options are odd (positive).
            string targetDecoyType = (aAnswerNum % 2 == 0) ? "positive" : "negative";

            List<int> chFilter = StaticAnswers
                .Where(a => a.Type == targetDecoyType)
                .Select(a => a.Id)
                .ToList();

            // Select first random decoy
            int element1 = aRnd1.Next(0, chFilter.Count);
            choice1 = chFilter[element1];
            while (choice1 == aAnswerNum)
            {
                element1 = aRnd1.Next(0, chFilter.Count);
                choice1 = chFilter[element1];
            }

            // Select second random distinct decoy
            int element2 = aRnd1.Next(0, chFilter.Count);
            choice2 = chFilter[element2];
            while (choice2 == aAnswerNum || choice2 == choice1)
            {
                element2 = aRnd1.Next(0, chFilter.Count);
                choice2 = chFilter[element2];
            }

            // Randomly scatter correct answer and decoys across array slots
            int correctPosition = aRnd1.Next(0, 3);
            aUserChoices[correctPosition] = aAnswerNum;
            if (correctPosition == 0)
            {
                aUserChoices[1] = choice1;
                aUserChoices[2] = choice2;
            }
            else if (correctPosition == 1)
            {
                aUserChoices[0] = choice1;
                aUserChoices[2] = choice2;
            }
            else
            {
                aUserChoices[0] = choice1;
                aUserChoices[1] = choice2;
            }
            
            return aUserChoices;
        }

        public bool CheckAnswer(int aAnswerNum, int aUserAnswer)
        {
            this.ACheckAnswer = (aUserAnswer == aAnswerNum);
            return ACheckAnswer;
        }

        public override string Image(int aGameType)
        {
            return "lightgreen";
        }
    }
}