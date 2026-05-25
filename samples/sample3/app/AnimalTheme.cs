/* ***********************************************************************************
author: Veronica Hutchins
course: CITP 280
assignment: project part 5
date: 12/05/19
purpose: a class for the background animal theme
notes: used as a type in the generic class, BackgroundOptions
************************************************************************************ */
using System;

namespace PreschoolGames
{
    class AnimalTheme : Theme
    {
        //declare variable
        private static int animalNum;

        //static constructor
        static AnimalTheme()
        { animalNum = 4; }

        //instance constructors
        public AnimalTheme()
        { } //default

        public AnimalTheme(int number) : base (number)
        { this.ANumber = number; }

        //method
        public int AnimalThemeCheck()
        {
            if (ANumber == animalNum)
            { return animalNum; }
            else
                return ANumber;
        }
    }
}