/* ****************************************************************************************************
author: Veronica Hutchins
course: CITP 280
assignment: project part 5
date: 12/05/19
purpose: a derived class inheriting from the PreschoolGame class
        for a child's game application
notes: Inheritance is used in this file at line 31
       Polymorphism is used in this file starting at line 71
       An Interface is used in this file starting at line 230
       Exception handling is used in Games.cs file
       A File is used in Games.cs file
       LINQ is used in this file starting at lines 163, 187
       A LINQ Extension Method is used in this file starting at lines 169, 187
       A Database is used in this file starting at lines 83, 141
       Threading is used in this file starting at line 224
       A Generic list is used in this file at lines 147, 157
       A Delegate is used in GameOptions.cs to wire BackgroundEvent to event handler
       A programmer-created Event is BackgroundEvent (BackgroundOptions.cs has the subscriber method)
       A static Constructor is used in TrainTheme, CarTheme, OceanTheme, and AnimalTheme classes
***************************************************************************************************** */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Data;
using System.Data.SqlClient;

namespace PreschoolGames
{
    class FriendsGame : PreschoolGame //inheritance
    {
        //declare instance variables
        private Random aRnd1 = new Random();
        private int aQuestionNum;
        private int aAnswerNum;
        private int[] aUserChoices = new int[3];
        //establish connection with variables
        private static string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\PreschoolGames.mdf;Integrated Security=True";
        private SqlConnection sqlConnection = new SqlConnection(connectionString);
        private SqlDataAdapter sqlDataAdapter;
        private string command;
        private SqlCommand sqlCommand; 
        
        //constructors
        public FriendsGame()
        { } //default

        public FriendsGame(int gameType) : base(gameType)
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
        public int AUserAnswer {get; set;}

    //methods
    public override int GetQuestion()  //polymorphism
        {
            //query the database 
            command = "SELECT Id FROM Questions WHERE gameTypeID = 1;";

            //create the command, data adapter, and dataset
            sqlCommand = new SqlCommand(command, sqlConnection);
            sqlDataAdapter = new SqlDataAdapter(sqlCommand);
            DataSet dataSet = new DataSet();

            //fill the dataset, get the table
            sqlDataAdapter.Fill(dataSet);
            DataTable dataTable = dataSet.Tables[0]; //database used

            //set a default question
            if (dataSet.Tables.Count == 0)
            { aQuestionNum = 1; }

            //put questionId numbers in a Generic List so that a random one can be retrieved
            List<int> qst = new List<int>();
            foreach (DataRow dataRow in dataTable.Rows)
            {
                var q = dataRow["Id"];
                int qs = int.Parse(q.ToString());
                qst.Add(qs);
            }

            if (qst.Count >= 1)
            {
                int q = aRnd1.Next(1, qst.Count);
                aQuestionNum = qst[q];
            }
            else
            { aQuestionNum = 1; }

            return aQuestionNum;
        }
        public int GetAnswer(int aQuestionNum)
        {
            //use question number to get answer from database table
            string command = "SELECT answerID FROM Questions WHERE Id = " + aQuestionNum + ";";
            
            //create the command, data adapter, and dataset
            sqlCommand = new SqlCommand(command, sqlConnection);
            sqlDataAdapter = new SqlDataAdapter(sqlCommand);
            DataSet dataSet2 = new DataSet();

            //fill the dataset, get the table
            sqlDataAdapter.Fill(dataSet2);
            DataTable dataTable = dataSet2.Tables[0]; //database used
            foreach (DataRow dataRow in dataTable.Rows)
            {
                var a = dataRow[0];
                aAnswerNum = int.Parse(a.ToString());
            }

            return aAnswerNum;
        }
        public int[] GetChoices(int aAnswerNum)
        {
            //use connection string to access database //string command = "SELECT Id FROM Questions WHERE gameTypeID = 1;";
            string command = "SELECT Id FROM Answers WHERE type = 'positive' OR type = 'negative';";
            
            //create the command, data adapter, and dataset
            sqlCommand = new SqlCommand(command, sqlConnection);
            sqlDataAdapter = new SqlDataAdapter(sqlCommand);
            DataSet dataSet3 = new DataSet();
            
            //fill the dataset, get the table
            sqlDataAdapter.Fill(dataSet3);
            DataTable dataTable = dataSet3.Tables[0]; //database used

            int choice1;
            int choice2;

            //put answer numbers in a Generic list so that a random one can be retrieved
            List<int> aList = new List<int>();
            foreach (DataRow dataRow in dataTable.Rows)
            {
                var a = dataRow["Id"];
                int ans = int.Parse(a.ToString());
                aList.Add(ans);
            }

            //use LINQ to query Generic list; then use the LINQ extension method to filter results 
            //note that even Id numbers are "negative" and odd Id numbers are "positive" answer types
            List<int> chFilter = new List<int>();
            if (aAnswerNum % 2 == 0)
            {
                //var choiceFilter = from list in aList
                //                  where list % 2 != 0
                //                  select list;
                var choiceFilter = aList.Where(list => list % 2 != 0); //LINQ extension method replace above code

                foreach (var i in choiceFilter)
                {
                    chFilter.Add(i);
                }
                int element1 = aRnd1.Next(1, chFilter.Count); //LINQ extension method
                choice1 = chFilter[element1];
                while (choice1 == aAnswerNum)
                {
                    element1 = aRnd1.Next(1, chFilter.Count); //LINQ extension method
                    choice1 = chFilter[element1];
                }
                int element2 = aRnd1.Next(1, chFilter.Count); //LINQ extension method
                choice2 = chFilter[element2];
                while (choice2 == aAnswerNum || choice2 == choice1)
                {
                    element2 = aRnd1.Next(1, chFilter.Count); //LINQ extension method
                    choice2 = chFilter[element2];
                }
            }
            else
            {
                //use LINQ to query Generic list
                var choiceQuery = aList.Where(list => list % 2 == 0); //LINQ extension method
                foreach (var i in choiceQuery)
                {
                    chFilter.Add(i);
                }
                int element1 = aRnd1.Next(1, chFilter.Count); //LINQ extension method
                choice1 = chFilter[element1];
                while (choice1 == aAnswerNum)
                {
                    element1 = aRnd1.Next(1, chFilter.Count); //LINQ extension method
                    choice1 = chFilter[element1];
                }
                int element2 = aRnd1.Next(1, chFilter.Count); //LINQ extension method
                choice2 = chFilter[element2];
                while (choice2 == aAnswerNum || choice2 == choice1)
                {
                    element2 = aRnd1.Next(1, chFilter.Count); //LINQ extension method
                    choice2 = chFilter[element2];
                }
            }  
            //final choices:
            aUserChoices[0] = choice1;
            aUserChoices[1] = aAnswerNum; //this will always be the answer, but is displayed at random button
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
        //method using threading
        public void ThreadMethod()
        {
            //improve speed
            Thread.Sleep(1000);
        }
        //method implements interface (& uses polymorphism), which will be the default background
        public override System.Drawing.Color Image(int aGameType)
        {
            FriendsGame f = new FriendsGame(1);
            return System.Drawing.Color.LightGreen;
        }
    }
}
