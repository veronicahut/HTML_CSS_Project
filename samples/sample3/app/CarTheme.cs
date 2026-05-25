/* ***********************************************************************************
author: Veronica Hutchins
course: CITP 280
assignment: project part 5
date: 12/05/19
purpose: a class for the background train theme
notes: used as a type in the generic class, BackgroundOptions
************************************************************************************ */
using System;
using System.Windows.Forms; //for testing
namespace PreschoolGames
{
    class CarTheme : Theme
    {
        //declare variable
        private static int carNum;

        //static constructor
        static CarTheme() 
        { carNum = 2; }

        //instance constructors
        public CarTheme()
        { } //default

        public CarTheme(int number) : base (number)
        { this.ANumber = number; }
        
        //method
        public int CarThemeCheck()
        {
            //MessageBox.Show("CarTheme Check!!!"); //testing
            if (ANumber == carNum)
            { return carNum; }
            else
                return ANumber;
        }
    }
}