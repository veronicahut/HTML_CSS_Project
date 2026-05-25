/* ****************************************************************************************************
author: Veronica Hutchins
course: CITP 280
assignment: project part 5
date: 12/05/19
purpose: a derived class inheriting from the PreschoolGame class
        for a child's game application
notes:   Inheritance is used in this file at line 29, 47
         Polymorphism is used in this file starting at line 70
         An Interface is used in this file starting at line 152
         Exception handling is used in Games.cs
         A File is used in Games.cs
         LINQ is used in FriendsGame class
         A programmer-created Extension Method is the GameExtension class
         A Database is used in this file starting at line 82, 119
         Threading is used in SequenceGame.cs and Games.cs
         A programmer-created Generic class is the GameOptions.cs file
         A Delegate is used in GameOptions.cs to wire BackgroundEvent to event handler
         A programmer-created Event is BackgroundEvent (BackgroundOptions.cs has the subscriber method)
         A static Constructor is used in TrainTheme, CarTheme, OceanTheme, and AnimalTheme classes
***************************************************************************************************** */
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace PreschoolGames
{
    class PlaySafeGame : PreschoolGame
    {
        //declare instance variables
        private Random aRnd3 = new Random();
        private int aQuestionNum;
        private int aAnswerNum;
        private int[] aUserChoices = new int[2];
        //establish connection, command using variables
        private static string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\PreschoolGames.mdf;Integrated Security=True";
        private SqlConnection sqlConnection = new SqlConnection(connectionString);
        private SqlDataAdapter sqlDataAdapter;
        private string command;
        private SqlCommand sqlCommand;
        
        //constructors
        public PlaySafeGame()
        { } //default

        public PlaySafeGame(int gameType) : base(gameType)
        { this.AGameType = gameType; }

        //properties
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

        //instance methods
        public override int GetQuestion() //polymorphism used
        {
            //query database
            command = "SELECT Id FROM Questions WHERE gameTypeID = 3;";

            //create the command, data adapter, and dataset
            sqlCommand = new SqlCommand(command, sqlConnection);
            sqlDataAdapter = new SqlDataAdapter(sqlCommand);
            DataSet dataSet3 = new DataSet();

            //fill the dataset, get the table
            sqlDataAdapter.Fill(dataSet3); 
            DataTable dataTable = dataSet3.Tables[0]; //database used

            //set default question
            if (dataSet3.Tables.Count == 0)
            { aQuestionNum = 20; }

            //put questionId numbers in a Generic list so that a random one can be retrieved
            List<int> qst = new List<int>();
            foreach (DataRow dataRow in dataTable.Rows)
            {
                var q = dataRow["Id"];
                int qs = int.Parse(q.ToString());
                qst.Add(qs);
            }

            if (qst.Count >= 1)
            {
                int q = aRnd3.Next(1, qst.Count);
                aQuestionNum = qst[q];
            }
            else
            { aQuestionNum = 20; }

            return aQuestionNum;
        }
        public int GetAnswer(int aQuestionNum)
        {
            //command to query database
            string command = "SELECT answerID FROM Questions WHERE Id = " + aQuestionNum + ";";

            //create the command, data adapter, and dataset
            sqlCommand = new SqlCommand(command, sqlConnection);
            sqlDataAdapter = new SqlDataAdapter(sqlCommand);
            DataSet dataSet4 = new DataSet();

            //fill the dataset, get the table
            sqlDataAdapter.Fill(dataSet4);
            DataTable dataTable = dataSet4.Tables[0]; //database used
            foreach (DataRow dataRow in dataTable.Rows)
            {
                var a = dataRow[0];
                aAnswerNum = int.Parse(a.ToString());
            }
            return aAnswerNum;
        }
        public int[] GetChoices(int aAnswerNum)
        {
            int choice3; //this game only has 2 choices - Id 11 or Id 12
            if (aAnswerNum == 11)
            { choice3 = 12; }
            else
            { choice3 = 11; }

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
        //implement interface, which will be the default background
        public override System.Drawing.Color Image(int aGameType)
        {
            PlaySafeGame f = new PlaySafeGame(3);
            return System.Drawing.Color.LavenderBlush;
        }
    }
}