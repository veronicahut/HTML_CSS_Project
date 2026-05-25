/* ***********************************************************************************
author: Veronica Hutchins
course: CITP 280
assignment: project part 5
date: 12/05/19
purpose: a class for the background train theme
notes: used as a type in the generic class, BackgroundOptions
************************************************************************************ */
using System;
//using System.Windows.Forms; //for testing
namespace PreschoolGames
{
    class OceanTheme : Theme
    {
        //declare variable
        private static int oceanNum;

        //static constructor
        static OceanTheme()
        { oceanNum = 3; }

        //instance constructors
        public OceanTheme()
        { } //default

        public OceanTheme(int number) : base (number)
        { this.ANumber = number; }
        
        //method
        public int OceanThemeCheck()
        {
            if (ANumber == oceanNum)
            { return oceanNum; }
            else
                return ANumber;
        }
    }
}
