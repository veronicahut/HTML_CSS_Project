/* ************************************************************************************************************************************
author: Veronica Hutchins
course: CITP 280
assignment: project part 5 (final) 
date: 12/08/19
purpose: a form where a child's game can run
notes: Inheritance is used where FriendsGame, SequenceGame, & PlaySafeGame classes inherit from the PreschoolGame class; 
         4 themes classes inherit from the Theme class; the BackgroundEvent class inherits from EventArgs
       Polymorphism is used with FriendsGame and PlaySafeGame's GetQuestion method starting at lines 125, 218
       An Interface called IDesign was created to set a default background color and is used at lines 117, 186, 215
       Exception handling is used with a separate file (Scores) starting at line 929
       A File called "Scores" is used to keep a record of the player's name, score, and date starting at line 929
       LINQ is used in FriendsGame class to filter possible answer ids (besides the actual answer)
       A programmer-created Extension Method (see GameExtension class) adds the date to the score information & is used at line 938
       A Database with 6 tables (ie: Responses, GameType, Exceptions) is used in FriendsGame and PlaySafeGame classes
       Threading is used in the FriendsGame and SequenceGame classes and here starting at line 121, 1182, 1187
       A Generic class is the BackgroundOptions class (restricted to type Theme) 
          a Generic list is used in FriendsGame & PlaySafeGame; BackgroundOptions used at lines 54, 1090, 1112, 1134, 1156          
       A Delegate is used in GameOptions (the publisher class) to wire BackgroundEvent to the event handler method (ThemeChosen)
       A programmer-created Event is BackgroundEvent (BackgroundOptions has the subscriber method and event -handler)
       A static Constructor is used in TrainTheme, CarTheme, OceanTheme, and AnimalTheme classes
************************************************************************************************************************************ */
using System;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace PreschoolGames
{
    public partial class Games : Form
    {
        //declare variables; instantiate objects
        int[] question = new int[5];
        int[] userAns = new int[5];
        int[] userChoices;
        int userAnswer;
        int score;
        string userName = ""; //initialize name of player
        int a; //a is index of question array for Sequence Game
        int u; //u is used as the number of times user has played in a round for Friends and Play Safe Games, 
        //or u counts elements in userAns array for Sequence Game
        bool checkAnswer;
        int response;
        string pathBegin = Directory.GetCurrentDirectory();
        string pathEnd3;
        StreamWriter s3 = null;
        FriendsGame game1 = new FriendsGame(1);
        SequenceGame game2 = new SequenceGame(2);
        PlaySafeGame game3 = new PlaySafeGame(3);
        Random rnd = new Random();

        //constructor
        public Games()
        {
            InitializeComponent();
            DisplayChoices();
        }
		
        //method to display game choices
        public void DisplayChoices()
        {
            BackColor = Color.Lavender;
            lblGameType.Text = "0";
            lblGameType.Visible = false;
            //lblGameType.Visible = true; //testing
            lblOptions.Visible = false;
            //lblOptions.Visible = true; //testing

            btnChoice1.Visible = false;
            btnChoice2.Visible = false;
            btnChoice3.Visible = false;
            btnChoice4.Visible = false;
            btnChoice5.Visible = false;
            lblColor1.Visible = false;
            lblColor2.Visible = false;
            lblColor3.Visible = false;
            lblColor4.Visible = false;
            lblColor5.Visible = false;
            timer1.Enabled = false;
            lblResponse.Visible = false;

            lblQuestion.Visible = true;
            btnGameCh1.Visible = true;
            btnGameCh2.Visible = true;
            btnGameCh3.Visible = true;

            lblPlayer.Visible = true;
            txtPlayer.Visible = true;

            btnOption1.Visible = false;
            btnOption2.Visible = false;
            btnOption3.Visible = false;
            btnOption4.Visible = false;

            lblQuestion.Image = null;
            lblQuestion.Text = "Which game would you like to play? ";
            btnGameCh1.Text = "Friends Game";
            btnGameCh2.Text = "Sequence Game";
            btnGameCh3.Text = "Play Safe Game";
        }
		
        //method to play the game chosen
        public void PlayGame(int gameType)
        {
            lblQuestion.Visible = true;
            btnGameCh1.Visible = false;
            btnGameCh2.Visible = false;
            btnGameCh3.Visible = false;
            txtPlayer.Enabled = true;
            userName = txtPlayer.Text;
            lblQuestion.Text = "";

            if (gameType == 1) //Friends Game
            {
                SetFormGame(1); //sets up controls

                //this implements the interface
                this.BackColor = game1.Image(gameType);

                //this implements threading
                Thread thread = new Thread(new ThreadStart(game1.ThreadMethod));
                thread.Start();

                //the parameter game.GetQuestion() uses polymorphism
                userChoices = game1.GetChoices(game1.GetAnswer(game1.GetQuestion()));
                DisplayImage("lblQuestion", game1.AQuestionNum);

                string choice1 = userChoices[0].ToString();
                string choice2 = game1.AAnswerNum.ToString();
                string choice3 = userChoices[2].ToString();

                //questions, choices, and answers are in database tables
                lblQuestion.Text = "?";

                //get a random number between 1 & 4 to rearrange choices on buttons 
                int r = rnd.Next(1, 4);
                if (r == 1)
                {
                    btnChoice1.Text = choice1;
                    DisplayImage("btnChoice1", userChoices[0]);
                    btnChoice2.Text = choice3;
                    DisplayImage("btnChoice2", userChoices[2]);
                    btnChoice3.Text = choice2; //answer
                    DisplayImage("btnChoice3", game1.AAnswerNum);
                }
                else if (r == 2)
                {
                    btnChoice1.Text = choice2; //answer
                    DisplayImage("btnChoice1", game1.AAnswerNum);
                    btnChoice2.Text = choice1;
                    DisplayImage("btnChoice2", userChoices[0]);
                    btnChoice3.Text = choice3;
                    DisplayImage("btnChoice3", userChoices[2]);
                }
                else
                {
                    btnChoice1.Text = choice3;
                    DisplayImage("btnChoice1", userChoices[2]);
                    btnChoice2.Text = choice2; //answer
                    DisplayImage("btnChoice2", game1.AAnswerNum);
                    btnChoice3.Text = choice1;
                    DisplayImage("btnChoice3", userChoices[0]);
                }
            }
            else if (gameType == 2) //Sequence Game
            {
                //sets up controls; no score is kept in a file for this game
                SetFormGame(2);  

                btnChoice1.ForeColor = Color.Red;
                //btnChoice3.ForeColor = Color.DarkOrange;
                btnChoice3.ForeColor = Color.Green;
                btnChoice4.ForeColor = Color.Yellow;
                btnChoice5.ForeColor = Color.LightSteelBlue;

                a = 0;
                u = 0;
                question = game2.GetAnswer();
                //put numeric answers on label
                lblColor1.Text = question[0].ToString();
                lblColor2.Text = question[1].ToString();
                lblColor3.Text = question[2].ToString();
                lblColor4.Text = question[3].ToString();
                lblColor5.Text = question[4].ToString();

                //this implements the interface
                this.BackColor = game2.Image(gameType);

                if (game2.AAnswer[a] == 1) { lblColor1.BackColor = Color.Red; }
                //else if (game2.AAnswer[a] == 2) { lblColor1.BackColor = Color.Orange; }
                else if (game2.AAnswer[a] == 2) { lblColor1.BackColor = Color.Green; }
                else if (game2.AAnswer[a] == 3) { lblColor1.BackColor = Color.Yellow; }
                else { lblColor1.BackColor = Color.Blue; }

                //buttons for user to choose
                btnChoice1.Text = "1";
                DisplayImage("btnChoice1", 1);
                btnChoice2.Visible = false;
                btnChoice3.Text = "2";
                DisplayImage("btnChoice3", 2);
                btnChoice4.Text = "3";
                DisplayImage("btnChoice4", 3);
                btnChoice5.Text = "4";
                DisplayImage("btnChoice5", 4);

                //async method
                SequenceThreading();
            }
            else
            {
                gameType = 3; //Play Safe Game

                SetFormGame(3);  //sets up controls

                //this implements the interface
                this.BackColor = game3.Image(gameType);

                //the parameter game.GetQuestion() uses polymorphism
                userChoices = game3.GetChoices(game3.GetAnswer(game3.GetQuestion()));
                DisplayImage("lblQuestion", game3.AQuestionNum);

                string choice1 = userChoices[1].ToString(); //answer
                string choice2 = userChoices[0].ToString();

                //questions, choices, and answers are retrieved from database tables; this puts them on random buttons
                int r = rnd.Next(1, 3);
                if (r == 1)
                {
                    btnChoice1.Text = choice1; //answer
                    DisplayImage("btnChoice1", userChoices[1]);
                    btnChoice2.Text = choice2;
                    DisplayImage("btnChoice2", userChoices[0]);
                }
                else
                {
                    btnChoice1.Text = choice2;
                    DisplayImage("btnChoice1", userChoices[0]);
                    btnChoice2.Text = choice1; //answer
                    DisplayImage("btnChoice2", userChoices[1]);
                }
            }
        }
		
        //buttons to choose which game to play
        private void btnGameCh1_Click_1(object sender, EventArgs e)
        {
            int gameType = 1;
            lblGameType.Text = gameType.ToString();
            //set user times played to 0
            u = 0;
            PlayGame(gameType);
        }
		
        private void btnGameCh2_Click_1(object sender, EventArgs e)
        {
            int gameType = 2;
            lblGameType.Text = gameType.ToString();
            //set score to start at 0
            score = 0; 
            PlayGame(gameType);
        }
		
        private void btnGameCh3_Click_1(object sender, EventArgs e)
        {
            int gameType = 3;
            lblGameType.Text = gameType.ToString();
            //set user times played to 0
            u = 0;
            PlayGame(gameType);
        }
		
        //option buttons during games
        private void btnChoice1_Click_1(object sender, EventArgs e)
        {
            userAnswer = int.Parse(btnChoice1.Text);
            if (lblGameType.Text == "1" || lblGameType.Text == "3") //for Friends and Play Safe Games
            { DisplayResponses(userAnswer); }
            else if (u == a) //for Sequence Game
            {
                userAns[u] = userAnswer;
                DisplayResponses(userAns[u]);
            }
            else
            { if (u < a) { u = u + 1; } }
        }
		
        private void btnChoice2_Click(object sender, EventArgs e)
        {
            userAnswer = int.Parse(btnChoice2.Text); //only need for Friends and Play Safe Games
            DisplayResponses(userAnswer);
        }
		
        private void btnChoice3_Click_1(object sender, EventArgs e)
        {
            userAnswer = int.Parse(btnChoice3.Text);
            if (lblGameType.Text == "1" || lblGameType.Text == "3")
            { DisplayResponses(userAnswer); }
            else if (u == a)
            {
                userAns[u] = userAnswer;
                DisplayResponses(userAns[u]);
            }
            else
            { if (u < a) { u = u + 1; } }
        }
		
        private void btnChoice4_Click_1(object sender, EventArgs e)
        {
            userAnswer = int.Parse(btnChoice4.Text);
            if (lblGameType.Text == "1" || lblGameType.Text == "3")
            { DisplayResponses(userAnswer); }
            else if (u == a)
            {
                userAns[u] = userAnswer;
                DisplayResponses(userAns[u]);
            }
            else
            { if (u < a) { u = u + 1; } }
        }
		
        private void btnChoice5_Click_1(object sender, EventArgs e)
        {
            userAnswer = int.Parse(btnChoice5.Text);
            if (lblGameType.Text == "1" || lblGameType.Text == "3")
            { DisplayResponses(userAnswer); }
            else if (u == a)
            {
                userAns[u] = userAnswer;
                DisplayResponses(userAns[u]);
            }
            else
            { if (u < a) { u = u + 1; } }
        }
		
        //method to get images onto controls
        private void DisplayImage(string control, int imageNum)
        {
            //FriendsGame has questions 1- 19, PlaySafeGame has questions 20- 41
            if (lblGameType.Text == "1" || lblGameType.Text == "3") 
            {
                switch (imageNum)
                {
                    case 1:
                        if (control == "lblQuestion") { lblQuestion.Image = Properties.Resources.game1Q1; }
                        else if (control == "btnChoice1") { btnChoice1.Image = Properties.Resources.answer1; }
                        else if (control == "btnChoice2") { btnChoice2.Image = Properties.Resources.answer1; }
                        else if (control == "btnChoice3") { btnChoice3.Image = Properties.Resources.answer1; }
                        else //(control == "lblResponse") 
                        { lblResponse.Image = Properties.Resources.response1; }
                        break;

                    case 2:
                        if (control == "lblQuestion") { lblQuestion.Image = Properties.Resources.game1Q2; }
                        else if (control == "btnChoice1") { btnChoice1.Image = Properties.Resources.answer2; }
                        else if (control == "btnChoice2") { btnChoice2.Image = Properties.Resources.answer2; }
                        else if (control == "btnChoice3") { btnChoice3.Image = Properties.Resources.answer2; }
                        else //(control == "lblResponse") 
                        { lblResponse.Image = Properties.Resources.response2; }
                        break;

                    case 3:
                        if (control == "lblQuestion") { lblQuestion.Image = Properties.Resources.game1Q3; }
                        else if (control == "btnChoice1") { btnChoice1.Image = Properties.Resources.answer3; }
                        else if (control == "btnChoice2") { btnChoice2.Image = Properties.Resources.answer3; }
                        else if (control == "btnChoice3") { btnChoice3.Image = Properties.Resources.answer3; }
                        else //(control == "lblResponse") 
                        { lblResponse.Image = Properties.Resources.response3; }
                        break;

                    case 4:
                        if (control == "lblQuestion") { lblQuestion.Image = Properties.Resources.game1Q4; }
                        else if (control == "btnChoice1") { btnChoice1.Image = Properties.Resources.answer4; }
                        else if (control == "btnChoice2") { btnChoice2.Image = Properties.Resources.answer4; }
                        else if (control == "btnChoice3") { btnChoice3.Image = Properties.Resources.answer4; }
                        else //(control == "lblResponse") 
                        { lblResponse.Image = Properties.Resources.response4; }
                        break;

                    case 5:
                        if (control == "lblQuestion") { lblQuestion.Image = Properties.Resources.game1Q5; }
                        else if (control == "btnChoice1") { btnChoice1.Image = Properties.Resources.answer5; }
                        else if (control == "btnChoice2") { btnChoice2.Image = Properties.Resources.answer5; }
                        else if (control == "btnChoice3") { btnChoice3.Image = Properties.Resources.answer5; }
                        else //(control == "lblResponse") 
                        { lblResponse.Image = Properties.Resources.response5; }
                        break;

                    case 6:
                        if (control == "lblQuestion") { lblQuestion.Image = Properties.Resources.game1Q6; }
                        else if (control == "btnChoice1") { btnChoice1.Image = Properties.Resources.answer6; }
                        else if (control == "btnChoice2") { btnChoice2.Image = Properties.Resources.answer6; }
                        else if (control == "btnChoice3") { btnChoice3.Image = Properties.Resources.answer6; }
                        else //(control == "lblResponse") 
                        { lblResponse.Image = Properties.Resources.response6; }
                        break;
                        
                    case 7:
                        if (control == "lblQuestion") { lblQuestion.Image = Properties.Resources.game1Q7; }
                        else if (control == "btnChoice1") { btnChoice1.Image = Properties.Resources.answer7; }
                        else if (control == "btnChoice2") { btnChoice2.Image = Properties.Resources.answer7; }
                        else if (control == "btnChoice3") { btnChoice3.Image = Properties.Resources.answer7; }
                        else //(control == "lblResponse") 
                        { lblResponse.Image = Properties.Resources.response7; }
                        break;

                    case 8:
                        if (control == "lblQuestion") { lblQuestion.Image = Properties.Resources.game1Q8; }
                        else if (control == "btnChoice1") { btnChoice1.Image = Properties.Resources.answer8; }
                        else if (control == "btnChoice2") { btnChoice2.Image = Properties.Resources.answer8; }
                        else // (control == "btnChoice3") and there is no response higher than 7
                        { btnChoice3.Image = Properties.Resources.answer8; }
                        break;

                    case 9:
                        if (control == "lblQuestion") { lblQuestion.Image = Properties.Resources.game1Q9; }
                        else if (control == "btnChoice1") { btnChoice1.Image = Properties.Resources.answer9; }
                        else if (control == "btnChoice2") { btnChoice2.Image = Properties.Resources.answer9; }
                        else //(control == "btnChoice3")
                        { btnChoice3.Image = Properties.Resources.answer9; }
                        break;

                    case 10:
                        if (control == "lblQuestion") { lblQuestion.Image = Properties.Resources.game1Q10; }
                        else if (control == "btnChoice1") { btnChoice1.Image = Properties.Resources.answer10; }
                        else if (control == "btnChoice2") { btnChoice2.Image = Properties.Resources.answer10; }
                        else //(control == "btnChoice3") 
                        { btnChoice3.Image = Properties.Resources.answer10; }
                        break;

                    case 11:
                        if (control == "lblQuestion") { lblQuestion.Image = Properties.Resources.game1Q11; }
                        else if (control == "btnChoice1") { btnChoice1.Image = Properties.Resources.answer11; }
                        else if (control == "btnChoice2") { btnChoice2.Image = Properties.Resources.answer11; }
                        else { } //no answer numbers after 10 apply to FriendsGame or after 12 apply to PlaySafeGame
                        break;

                    case 12:
                        if (control == "lblQuestion") { lblQuestion.Image = Properties.Resources.game1Q12; }
                        else if (control == "btnChoice1") { btnChoice1.Image = Properties.Resources.answer12; }
                        else if (control == "btnChoice2") { btnChoice2.Image = Properties.Resources.answer12; }
                        else { }
                        break;

                    case 13:
                        if (control == "lblQuestion") { lblQuestion.Image = Properties.Resources.game1Q13; }
                        else { }//no answer numbers after 12 apply for FriendsGame or PlaySafeGame
                        break;

                    case 14:
                        if (control == "lblQuestion") { lblQuestion.Image = Properties.Resources.game1Q14; }
                        else { }
                        break;

                    case 15:
                        if (control == "lblQuestion") { lblQuestion.Image = Properties.Resources.game1Q15; }
                        else { }
                        break;

                    case 16:
                        if (control == "lblQuestion") { lblQuestion.Image = Properties.Resources.game1Q16; }
                        else { }
                        break;

                    case 17:
                        if (control == "lblQuestion") { lblQuestion.Image = Properties.Resources.game1Q17; }
                        else { }
                        break;

                    case 18:
                        if (control == "lblQuestion") { lblQuestion.Image = Properties.Resources.game1Q18; }
                        else { }
                        break;

                    case 19:
                        if (control == "lblQuestion") { lblQuestion.Image = Properties.Resources.game1Q19; }
                        else { }
                        break;

                    case 20:
                        if (control == "lblQuestion") { lblQuestion.Image = Properties.Resources.game3Q1; }
                        else if (control == "btnChoice1") { btnChoice1.Image = Properties.Resources.answer1; }
                        else if (control == "btnChoice2") { btnChoice2.Image = Properties.Resources.answer1; }
                        else //(control == "lblResponse") 
                        { lblResponse.Image = Properties.Resources.response1; }
                        break;

                    case 21:
                        if (control == "lblQuestion") { lblQuestion.Image = Properties.Resources.game3Q1; }
                        else if (control == "btnChoice1") { btnChoice1.Image = Properties.Resources.answer1; }
                        else if (control == "btnChoice2") { btnChoice2.Image = Properties.Resources.answer1; }
                        else //(control == "lblResponse") 
                        { lblResponse.Image = Properties.Resources.response1; }
                        break;

                    case 22:
                        if (control == "lblQuestion") { lblQuestion.Image = Properties.Resources.game3Q2; }
                        else if (control == "btnChoice1") { btnChoice1.Image = Properties.Resources.answer2; }
                        else if (control == "btnChoice2") { btnChoice2.Image = Properties.Resources.answer2; }
                        else //(control == "lblResponse") 
                        { lblResponse.Image = Properties.Resources.response2; }
                        break;

                    case 23:
                        if (control == "lblQuestion") { lblQuestion.Image = Properties.Resources.game3Q3; }
                        else if (control == "btnChoice1") { btnChoice1.Image = Properties.Resources.answer3; }
                        else if (control == "btnChoice2") { btnChoice2.Image = Properties.Resources.answer3; }
                        else //(control == "lblResponse") 
                        { lblResponse.Image = Properties.Resources.response3; }
                        break;

                    //case 24 is the default

                    case 25:
                        if (control == "lblQuestion") { lblQuestion.Image = Properties.Resources.game3Q5; }
                        else if (control == "btnChoice1") { btnChoice1.Image = Properties.Resources.answer5; }
                        else if (control == "btnChoice2") { btnChoice2.Image = Properties.Resources.answer5; }
                        else //(control == "lblResponse") 
                        { lblResponse.Image = Properties.Resources.response5; }
                        break;

                    case 26:
                        if (control == "lblQuestion") { lblQuestion.Image = Properties.Resources.game3Q6; }
                        else if (control == "btnChoice1") { btnChoice1.Image = Properties.Resources.answer6; }
                        else if (control == "btnChoice2") { btnChoice2.Image = Properties.Resources.answer6; }
                        else //(control == "lblResponse") 
                        { lblResponse.Image = Properties.Resources.response6; }
                        break;

                    case 27:
                        if (control == "lblQuestion") { lblQuestion.Image = Properties.Resources.game3Q7; }
                        else if (control == "btnChoice1") { btnChoice1.Image = Properties.Resources.answer7; }
                        else if (control == "btnChoice2") { btnChoice2.Image = Properties.Resources.answer7; }
                        else //(control == "lblResponse") 
                        { lblResponse.Image = Properties.Resources.response7; }
                        break;

                    case 28:
                        if (control == "lblQuestion") { lblQuestion.Image = Properties.Resources.game3Q8; }
                        else if (control == "btnChoice1") { btnChoice1.Image = Properties.Resources.answer8; }
                        else //(control == "btnChoice2") and no responses beyond 7
                        { btnChoice2.Image = Properties.Resources.answer8; }
                        break;

                    case 29:
                        if (control == "lblQuestion") { lblQuestion.Image = Properties.Resources.game3Q9; }
                        else if (control == "btnChoice1") { btnChoice1.Image = Properties.Resources.answer9; }
                        else //(control == "btnChoice2") 
                        { btnChoice2.Image = Properties.Resources.answer9; }
                        break;

                    case 30:
                        if (control == "lblQuestion") { lblQuestion.Image = Properties.Resources.game3Q10; }
                        else if (control == "btnChoice1") { btnChoice1.Image = Properties.Resources.answer10; }
                        else //(control == "btnChoice2") 
                        { btnChoice2.Image = Properties.Resources.answer10; }
                        break;

                    case 31:
                        if (control == "lblQuestion") { lblQuestion.Image = Properties.Resources.game3Q11; }
                        else if (control == "btnChoice1") { btnChoice1.Image = Properties.Resources.answer11; }
                        else //(control == "btnChoice2")
                        { btnChoice2.Image = Properties.Resources.answer11; }
                        break;

                    case 32:
                        if (control == "lblQuestion") { lblQuestion.Image = Properties.Resources.game3Q12; }
                        else if (control == "btnChoice1") { btnChoice1.Image = Properties.Resources.answer12; }
                        else //(control == "btnChoice2")
                        { btnChoice2.Image = Properties.Resources.answer12; }
                        break;

                    case 33:
                        if (control == "lblQuestion") { lblQuestion.Image = Properties.Resources.game3Q13; }
                        else //(control == "btnChoice2") no answers beyond 12 and no responses beyond 7
                        { }
                        break;

                    case 34:
                        if (control == "lblQuestion") { lblQuestion.Image = Properties.Resources.game3Q14; }
                        else { }
                        break;

                    case 35:
                        if (control == "lblQuestion") { lblQuestion.Image = Properties.Resources.game3Q15; }
                        else { }
                        break;

                    case 36:
                        if (control == "lblQuestion") { lblQuestion.Image = Properties.Resources.game3Q16; }
                        else { }
                        break;

                    case 37:
                        if (control == "lblQuestion") { lblQuestion.Image = Properties.Resources.game3Q17; }
                        else { }
                        break;

                    case 38:
                        if (control == "lblQuestion") { lblQuestion.Image = Properties.Resources.game3Q18; }
                        else { }
                        break;

                    case 39:
                        if (control == "lblQuestion") { lblQuestion.Image = Properties.Resources.game3Q19; }
                        else { }
                        break;

                    case 40:
                        if (control == "lblQuestion") { lblQuestion.Image = Properties.Resources.game3Q20; }
                        else { }
                        break;

                    case 41:
                        if (control == "lblQuestion") { lblQuestion.Image = Properties.Resources.game3Q21; }
                        else { }
                        break;

                    //default: //case 24
                    case 24:
                        if (control == "lblQuestion") { lblQuestion.Image = Properties.Resources.game3Q4; }
                        else if (control == "btnChoice1") { btnChoice1.Image = Properties.Resources.answer4; }
                        else if (control == "btnChoice2") { btnChoice2.Image = Properties.Resources.answer4; }
                        else //(control == "lblResponse") 
                        { lblResponse.Image = Properties.Resources.response4; }
                        break;
                }
            }
            else  //Sequence Game
            {
                btnChoice1.Image = Properties.Resources.game2Q1; //red
                //btnChoice3.Image = Properties.Resources.game2Q2; //orange
                btnChoice3.Image = Properties.Resources.game2Q5; //green
                btnChoice4.Image = Properties.Resources.game2Q3; //yellow
                btnChoice5.Image = Properties.Resources.game2Q4; //blue

                switch (imageNum)
                {

                    case 1:
                        if (control == "lblQuestion") { lblQuestion.Image = Properties.Resources.game2Q1; }
                        else //(control == "lblResponse") 
                        { lblResponse.Image = Properties.Resources.response1; }
                        break;

                    case 2: //game2Q5 is question 2 instead of game2Q2
                        if (control == "lblQuestion") { lblQuestion.Image = Properties.Resources.game2Q5; }
                        else //(control == "lblResponse")
                        { lblResponse.Image = Properties.Resources.response2; }
                        break;

                    //case 3 is default

                    case 4:
                        if (control == "lblQuestion") { lblQuestion.Image = Properties.Resources.game2Q4; }
                        else //(control == "lblResponse")
                        { lblResponse.Image = Properties.Resources.response4; }
                        break;

                    case 5:
                        if (control == "lblResponse") { lblResponse.Image = Properties.Resources.response5; }
                        else { }
                        break;

                    case 6:
                        if (control == "lblResponse") { lblResponse.Image = Properties.Resources.response6; }
                        else { }
                        break;

                    case 7:
                        if (control == "lblResponse") { lblResponse.Image = Properties.Resources.response7; }
                        else { }
                        break;

                    default: //case 3
                        if (control == "lblQuestion") { lblQuestion.Image = Properties.Resources.game2Q3; }
                        else //(control == "lblResponse") 
                        { lblResponse.Image = Properties.Resources.response3; }
                        break;
                }
            }
        }
		
        //method for playing Sequence Game
        private void Sequence()
        {
            a = a + 1;
            string display = ""; //initialize
            string display1;
            string display2;

            if (a == question.Length - 1)
            {
                question[a] = game2.AAnswer[a];
                for (int i = 0; i < a; i++)
                {
                    if (game2.AAnswer[i] == 1) { display1 = "red "; }
                    //else if (game2.AAnswer[i] == 2) { display1 = "orange "; }
                    else if (game2.AAnswer[i] == 2) { display1 = "green "; }
                    else if (game2.AAnswer[i] == 3) { display1 = "yellow "; }
                    else { display1 = "blue "; }
                    display = display + display1 + ", ";
                }
                if (game2.AAnswer[a] == 1) { display2 = "red "; }
                //else if (game2.AAnswer[a] == 2) { display2 = "orange "; }
                else if (game2.AAnswer[a] == 2) { display2 = "green "; }
                else if (game2.AAnswer[a] == 3) { display2 = "yellow "; }
                else { display2 = "blue "; }
                if (display2 == "red ") { lblColor5.BackColor = Color.Red; }
                //else if (display2 == "orange ") { lblColor5.BackColor = Color.Orange; }
                else if (display2 == "green ") { lblColor5.BackColor = Color.Green; }
                else if (display2 == "yellow ") { lblColor5.BackColor = Color.Yellow; }
                else { lblColor5.BackColor = Color.Blue; }
                display2 = display + display2;
                SequenceThreading(); //threading
            }
            else
            {
                question[a] = game2.AAnswer[a];
                for (int i = 0; i < a; i++)
                {
                    if (game2.AAnswer[i] == 1) { display1 = "red "; }
                    //else if (game2.AAnswer[i] == 2) { display1 = "orange "; }
                    else if (game2.AAnswer[i] == 2) { display1 = "green "; }
                    else if (game2.AAnswer[i] == 3) { display1 = "yellow "; }
                    else { display1 = "blue "; }
                    display = display + display1 + ", ";
                }
                if (game2.AAnswer[a] == 1) { display2 = "red "; }
                //else if (game2.AAnswer[a] == 2) { display2 = "orange "; }
                else if (game2.AAnswer[a] == 2) { display2 = "green "; }
                else if (game2.AAnswer[a] == 3) { display2 = "yellow "; }
                else { display2 = "blue "; }
                if (a == 1)
                {
                    //add *****************************************************************
                    //lblColor1.Text = "1";
                    if (game2.AAnswer[0] == 1) { lblColor1.BackColor = Color.Red; }
                    else if (game2.AAnswer[0] == 2) { lblColor1.BackColor = Color.Green; }
                    else if (game2.AAnswer[0] == 3) { lblColor1.BackColor = Color.Yellow; }
                    else { lblColor1.BackColor = Color.Blue; }
                    Thread threadA = new Thread(new ThreadStart(game2.UseThreadMethod));
                    threadA.Priority = ThreadPriority.Highest;
                    threadA.Start();
                    SequenceThreading();
                    //end add *************************************************************
                    if (display2 == "red ") { lblColor2.BackColor = Color.Red; }
                    //else if (display2 == "orange ") { lblColor2.BackColor = Color.Orange; }
                    else if (display2 == "green ") { lblColor2.BackColor = Color.Green; }
                    else if (display2 == "yellow ") { lblColor2.BackColor = Color.Yellow; }
                    else { lblColor2.BackColor = Color.Blue; }
                    Thread threadB = new Thread(new ThreadStart(game2.UseThreadMethod));
                    threadB.Priority = ThreadPriority.AboveNormal;
                    threadB.Start();
                    SequenceThreading();
                }
                else if (a == 2)
                {

                    //add *****************************************************************
                    if (game2.AAnswer[0] == 1) { lblColor1.BackColor = Color.Red; }
                    else if (game2.AAnswer[0] == 2) { lblColor1.BackColor = Color.Green; }
                    else if (game2.AAnswer[0] == 3) { lblColor1.BackColor = Color.Yellow; }
                    else { lblColor1.BackColor = Color.Blue; }
                    Thread threadA = new Thread(new ThreadStart(game2.UseThreadMethod));
                    threadA.Priority = ThreadPriority.Highest;
                    threadA.Start();
                    SequenceThreading();
                    //lblColor3.Text = "2";
                    if (game2.AAnswer[1] == 1) { lblColor2.BackColor = Color.Red; }
                    else if (game2.AAnswer[1] == 2) { lblColor2.BackColor = Color.Green; }
                    else if (game2.AAnswer[1] == 3) { lblColor2.BackColor = Color.Yellow; }
                    else { lblColor2.BackColor = Color.Blue; }
                    Thread threadB = new Thread(new ThreadStart(game2.UseThreadMethod));
                    threadB.Priority = ThreadPriority.AboveNormal;
                    threadB.Start();
                    SequenceThreading();
                    //end add *************************************************************
                    //lblColor3.Text = "3";
                    if (display2 == "red ") { lblColor3.BackColor = Color.Red; }
                    //else if (display2 == "orange ") { lblColor4.BackColor = Color.Orange; }
                    else if (display2 == "green ") { lblColor3.BackColor = Color.Green; }
                    else if (display2 == "yellow ") { lblColor3.BackColor = Color.Yellow; }
                    else { lblColor3.BackColor = Color.Blue; }
                    Thread threadC = new Thread(new ThreadStart(game2.UseThreadMethod));
                    threadC.Priority = ThreadPriority.Normal;
                    threadC.Start();
                    SequenceThreading();
                }
                else //if (a == 3)
                {
                    //add *****************************************************************
                    if (game2.AAnswer[0] == 1) { lblColor1.BackColor = Color.Red; }
                    else if (game2.AAnswer[0] == 2) { lblColor1.BackColor = Color.Green; }
                    else if (game2.AAnswer[0] == 3) { lblColor1.BackColor = Color.Yellow; }
                    else { lblColor1.BackColor = Color.Blue; }
                    SequenceThreading(); 
                    if (game2.AAnswer[1] == 1) { lblColor2.BackColor = Color.Red; }
                    else if (game2.AAnswer[1] == 2) { lblColor2.BackColor = Color.Green; }
                    else if (game2.AAnswer[1] == 3) { lblColor2.BackColor = Color.Yellow; }
                    else { lblColor2.BackColor = Color.Blue; }
                    SequenceThreading(); 
                    if (game2.AAnswer[2] == 1) { lblColor3.BackColor = Color.Red; }
                    else if (game2.AAnswer[2] == 2) { lblColor3.BackColor = Color.Green; }
                    else if (game2.AAnswer[2] == 3) { lblColor3.BackColor = Color.Yellow; }
                    else { lblColor3.BackColor = Color.Blue; }
                    SequenceThreading(); 
                    //end add *************************************************************
                    //lblColor4.Text = "4";
                    if (display2 == "red ") { lblColor4.BackColor = Color.Red; }
                    //else if (display2 == "orange ") { lblColor5.BackColor = Color.Orange; }
                    else if (display2 == "green ") { lblColor4.BackColor = Color.Green; }
                    else if (display2 == "yellow ") { lblColor4.BackColor = Color.Yellow; }
                    else { lblColor4.BackColor = Color.Blue; }
                    SequenceThreading();
                }
            }
        }
		
        //method to display whether the answer was correct or not & keeps score
        public void DisplayResponses(int userAnswer)
        {
            lblResponse.Visible = true;
            int r = rnd.Next(2, 4);

            //game1
            if (lblGameType.Text == "1")
            {
                lblQuestion.Visible = false;
                checkAnswer = game1.CheckAnswer(game1.AAnswerNum, userAnswer);
                response = game1.DisplayResponse(checkAnswer);
            }
            //game2
            else if (lblGameType.Text == "2")
            {
                checkAnswer = game2.CheckAnswer(game2.AAnswer, userAns, u);
                if (checkAnswer == true)
                {
                    score += 1;
                    lblResponse.Text = "Good job!";
                    DisplayImage("lblResponse", r);
                    if (u == game2.AAnswer.Length - 1)
                    {
                        lblResponse.Text = "You won!";
                        if (MessageBox.Show("Your Score is: " + score + "\nPlay again?", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        { DisplayChoices(); }
                        else
                            Games.ActiveForm.Close();
                    }
                    else
                    {
                        u = 0;
                        Sequence();
                    }
                }
                else
                {
                    string seqScore = score.ToString();
                    lblResponse.Text = "Sorry. Try again.";
                    DisplayImage("lblResponse", 1);
                    if (MessageBox.Show("Your score: " + seqScore + "\nPlay again?", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        u = 0;
                        DisplayChoices();
                    }
                    else
                        Games.ActiveForm.Close();
                }
            }
            else //game 3
            {
                lblQuestion.Visible = false;
                checkAnswer = game3.CheckAnswer(game3.AAnswerNum, userAnswer);
                response = game3.DisplayResponse(checkAnswer);
            }
			
            //responses for games 1 & 3; score counted
            if (lblGameType.Text == "1" || lblGameType.Text == "3")
            {
                if (response == 1)
                {
                    lblResponse.Text = "Sorry. Try again.";
                    DisplayImage("lblResponse", 1);
                    u = u + 1;
                    score = score + 0;
                }
                else if (response == 2)
                {
                    lblResponse.Text = "Keep up the good work!.";
                    DisplayImage("lblResponse", r);
                    u = u + 1;
                    score = score + 1;
                }
                else
                {
                    lblResponse.Text = "Good job!";
                    DisplayImage("lblResponse", r);
                    u = u + 1;
                    score = score + 1;
                }
                if (u == 4)
                {
                    lblPlayer.Visible = false;
                    txtPlayer.Enabled = false;
                    txtPlayer.Visible = false;
                }
                if (u == 5)
                {
                    score = scoreKeeping(score);
                    SequenceThreading();
                    if (MessageBox.Show("Would you like to play another game?", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        lblResponse.Visible = false;
                        DisplayChoices();
                    }
                    else
                    { Games.ActiveForm.Close(); }
                }
                else
                {
                    if (lblGameType.Text == "1")
                    {
                        PlayGame(1);
                    }
                    else
                    {
                        PlayGame(3);
                    }
                }
            }
        }
		
        //method to keep score in a text file
        private int scoreKeeping(int score)
        {
            lblPlayer.Visible = false;
            string game = lblGameType.Text; //initialize with default
            if (lblGameType.Text == "1") { game = "Friends Game"; }
            else if(lblGameType.Text == "3"){ game = "Play Safe Game"; }
            else { }
            if (userName == "") { userName = "Player"; }
            //open file, write gameType, userName & score to txt file, close file with exception handling
            try
            {
                pathEnd3 = @"\Scores.txt";
                string path3 = pathBegin + pathEnd3;
                StreamWriter s3 = new StreamWriter(new FileStream(path3, FileMode.Append, FileAccess.Write));
                string score1;
                string scores;
                score1 = "Game type: " + game + "\t" + "Name: " + userName + "\t" + "Score: " + score.ToString() + "\tDate: ";
                //extension method FormatScore is called
                scores = score1.FormatScore();
                s3.WriteLine(scores);
                //display score on form
                txtScores.Text = scores;
                //close StreamWriter
                s3.Close();
            }
            catch (IOException)
            {
                MessageBox.Show("Input or output error.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //write error to exceptions table?
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message + " There was an error.");
                //write error to exceptions table?
            }
            finally
            {
                if (s3 != null)
                { s3.Close(); }
            }
            //reset to 0 for next time
            score = 0;
            return score;
        }
		
        //reset score if the user doesn't finish previous game, etc.
        private void Games_Load(object sender, EventArgs e)
        {
            //reset score on load
            score = 0;
        }
        
        private void SetOptions()
        {
            //hide game controls
            lblQuestion.Visible = false;
            lblResponse.Visible = false;
            btnGameCh1.Visible = false;
            btnGameCh2.Visible = false;
            btnGameCh3.Visible = false;
            btnChoice1.Visible = false;
            btnChoice2.Visible = false;
            btnChoice3.Visible = false;
            btnChoice4.Visible = false;
            btnChoice5.Visible = false;
            lblColor1.Visible = false;
            lblColor2.Visible = false;
            lblColor3.Visible = false;
            lblColor4.Visible = false;
            lblColor5.Visible = false;

            //display option controls
            lblOptions.Visible = false;
            //lblOptions.Visible = true; //testing
            btnOption1.Visible = true;
            btnOption2.Visible = true;
            btnOption3.Visible = true;
            btnOption4.Visible = true;
        }
		
        private void SetFormGame(int gameType)
        {
            //hide option controls
            btnOption1.Visible = false;
            btnOption2.Visible = false;
            btnOption3.Visible = false;
            btnOption4.Visible = false;
            lblOptions.Visible = false;
            //lblOptions.Visible = true; //testing

            //show game controls based on game type
            if (gameType == 1)
            {
                lblColor1.Visible = false;
                //lblColor2.Visible = false;
                lblColor3.Visible = false;
                lblColor4.Visible = false;
                lblColor5.Visible = false;
                lblColor2.Visible = false;
                btnChoice1.Visible = true;
                btnChoice2.Visible = true;
                btnChoice3.Visible = true;
                btnChoice4.Visible = false;
                btnChoice5.Visible = false;
                lblQuestion.Visible = true;

                lblScore.Visible = true;
                txtScores.Visible = true;

                btnChoice1.ForeColor = Color.Yellow;
                btnChoice2.ForeColor = Color.Yellow;
                btnChoice3.ForeColor = Color.Yellow;
            }
            else if (gameType == 2)
            {
                lblPlayer.Visible = false;
                txtPlayer.Visible = false;
                lblScore.Visible = false;
                txtScores.Visible = false;

                lblColor1.Visible = true;
                //lblColor2.Visible = true;
                lblColor3.Visible = true;
                lblColor4.Visible = true;
                lblColor5.Visible = true;
                lblColor2.Visible = true;

                btnChoice1.Visible = true;
                btnChoice2.Visible = false;
                btnChoice3.Visible = true;
                btnChoice4.Visible = true;
                btnChoice5.Visible = true;
                lblQuestion.Visible = true;
                lblQuestion.Text = "Copy the small squares by pressing the large squares in the same order";
            }
            else if (gameType == 3)
            {
                lblColor1.Visible = false;
                //lblColor2.Visible = false;
                lblColor3.Visible = false;
                lblColor4.Visible = false;
                lblColor5.Visible = false;
                lblColor2.Visible = false;
                btnChoice1.Visible = true;
                btnChoice2.Visible = true;
                btnChoice3.Visible = false;
                btnChoice4.Visible = false;
                btnChoice5.Visible = false;
                lblQuestion.Visible = true;

                lblScore.Visible = true;
                txtScores.Visible = true;

                btnChoice1.ForeColor = Color.Yellow;
                btnChoice2.ForeColor = Color.Yellow;
            }
            else { DisplayChoices(); }
        }
        
        private void btnOptions_Click(object sender, EventArgs e)
        {
            //hide current controls, bring up controls with options
            SetOptions();
        }
		
        private void btnOption1_Click(object sender, EventArgs e)
        {
            //create objects
              //publisher class
            GameOptions gOpt = new GameOptions();
              //TrainTheme has a static constructor
            TrainTheme train = new TrainTheme(1); //this button is choice 1
              //programmer-created generic class; subscriber class
            BackgroundOptions<TrainTheme> tOption = new BackgroundOptions<TrainTheme>(train.TrainThemeCheck(), train);

            //call subscribe method
            tOption.Subscribe(gOpt);

            //change options label text based on event call
            lblOptions.Text = gOpt.ChooseOptions(1); //will be "train"

            //call method that changes background based on option label text
            BackgroundMethod();

            //reset form controls to be visible
            SetFormGame(int.Parse(lblGameType.Text));
        }
		
        private void btnOption2_Click(object sender, EventArgs e)
        {
            //create objects
              //publisher class
            GameOptions gOpt = new GameOptions();
              //CarTheme has a static constructor
            CarTheme car = new CarTheme(2); //this button is choice 1
              //programmer-created generic class; subscriber class
            BackgroundOptions<CarTheme> cOption = new BackgroundOptions<CarTheme>(car.CarThemeCheck(), car);

            //subscribe to GameOptions class
            cOption.Subscribe(gOpt);

            //change options label text based on event call
            lblOptions.Text = gOpt.ChooseOptions(2); //will be "train"

            //call method that changes background based on option label text
            BackgroundMethod();

            //reset form controls to be visible
            SetFormGame(int.Parse(lblGameType.Text));
        }
		
        private void btnOption3_Click(object sender, EventArgs e)
        {
            //create objects
              //publisher class
            GameOptions gOpt = new GameOptions();
            //OceanTheme has a static constructor
            OceanTheme ocean = new OceanTheme(3); //this button is choice 1
              //programmer-created generic class; subscriber class
            BackgroundOptions<OceanTheme> oOption = new BackgroundOptions<OceanTheme>(ocean.OceanThemeCheck(), ocean);

            //subscribe to GameOptions class
            oOption.Subscribe(gOpt);

            //change options label text based on event call
            lblOptions.Text = gOpt.ChooseOptions(3); //will be "train"

            //call method that changes background based on option label text
            BackgroundMethod();

            //reset form controls to be visible
            SetFormGame(int.Parse(lblGameType.Text));
        }
		
        private void btnOption4_Click(object sender, EventArgs e)
        {
            //create objects
              //publisher class
            GameOptions gOpt = new GameOptions();
              //AnimalTheme has a static constructor
            AnimalTheme animal = new AnimalTheme(4); //this button is choice 1
              //programmer-created generic class; subscriber class
            BackgroundOptions<AnimalTheme> aOption = new BackgroundOptions<AnimalTheme>(animal.AnimalThemeCheck(), animal);

            //subscribe to GameOptions class
            aOption.Subscribe(gOpt);

            //change options label text based on event call
            lblOptions.Text = gOpt.ChooseOptions(4); //will be "train"

            //call method that changes background based on option label text
            BackgroundMethod();

            //reset form controls to be visible
            SetFormGame(int.Parse(lblGameType.Text));
        }
		
        public void BackgroundMethod()
        {
            if (lblOptions.Text == "train")
            {
                pbxBackground.BackgroundImage = Properties.Resources.background1;
                BackgroundImageLayout = ImageLayout.Stretch;
            }
            else if (lblOptions.Text == "car")
            {
                pbxBackground.BackgroundImage = Properties.Resources.background2;
                BackgroundImageLayout = ImageLayout.Tile;
            }
            else if (lblOptions.Text == "ocean")
            {
                pbxBackground.BackgroundImage = Properties.Resources.background3;
                BackgroundImageLayout = ImageLayout.Stretch;
            }
            else //animal
            {
                pbxBackground.BackgroundImage = Properties.Resources.background4;
                BackgroundImageLayout = ImageLayout.Tile;
            }
        }

        async Task SequenceColors() //threading
        {
            //async method
            await Task.Delay(3000);
        }
		
        private void SequenceThreading() //threading
        {
            EventArgs e = new EventArgs();
            //this implements threading using SequenceGame method called UseThreadMethod           
            Thread thread2 = new Thread(new ThreadStart(game2.UseThreadMethod));
            thread2.Start();

            timer1.Enabled = true;
            timer1.Start();
        }
		
        private void timer1_Tick(object sender, EventArgs e)
        {
            //labels change to white after interval
            if (lblColor1.BackColor != Color.WhiteSmoke)
            {
                var wait = SequenceColors();
                lblColor1.BackColor = Color.WhiteSmoke;
            }
            else if (lblColor2.BackColor != Color.WhiteSmoke)
            {
                lblColor2.BackColor = Color.WhiteSmoke;
            }
            else if (lblColor3.BackColor != Color.WhiteSmoke)
            {
                lblColor3.BackColor = Color.WhiteSmoke;
            }
            else if (lblColor4.BackColor != Color.WhiteSmoke)
            {
                lblColor4.BackColor = Color.WhiteSmoke;
            }
            else if (lblColor5.BackColor != Color.WhiteSmoke)
            {
                lblColor5.BackColor = Color.WhiteSmoke;
            }
            else //pause
            {
                var wait = SequenceColors();
            }
        }
    }
}
