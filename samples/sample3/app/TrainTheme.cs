/* ***********************************************************************************
author: Veronica Hutchins
course: CITP 280
assignment: project part 5
date: 12/05/19
purpose: a class for the background train theme
notes: inherits from Theme class at line 16
       used as a type in the generic class (BackgroundOptions)
       has static constructor starting at line 21
************************************************************************************ */
using System;
using System.Windows.Forms; //for testing
namespace PreschoolGames
{
    class TrainTheme : Theme
    {
        //variable
        private static int trainNum;

        //static constructor
        static TrainTheme() 
        { trainNum = 1; }

        //instance constructors
        public TrainTheme()
        { } //default

        public TrainTheme(int number) : base (number)
        { this.ANumber = number; }

        //method
        public int TrainThemeCheck()
        {
            //MessageBox.Show("TrainTheme Check!!!"); //testing
            if (ANumber == trainNum)
            { return trainNum; }
            else
                return ANumber;
        }
    }
}