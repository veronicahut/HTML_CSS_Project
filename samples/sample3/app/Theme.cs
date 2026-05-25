/* ***************************************************************************************
author: Veronica Hutchins
course: CITP 280
assignment: project part 5
date: 12/05/19
purpose: a class for the background theme
notes: is a programmer-created type to be used in the generic class (BackgroundOptions)
**************************************************************************************** */
using System;
using System.Windows.Forms; //for testing
namespace PreschoolGames
{
    public class Theme
    {
        private int aNumber;
        private string theme;

        public Theme()
        { } //default

        public Theme(int number)
        { aNumber = number; }

        //property
        public int ANumber
        { get; set; }

        //method
        public string ShowChoice(int number)
        {
            if (number != 0)
            {
                switch (number)
                {
                    case 1:
                        //MessageBox.Show("Theme = Train");
                        theme = "train";
                        return theme;
                    case 2:
                        //MessageBox.Show("Theme = Car");
                        theme = "car";
                        return theme;
                    case 3:
                        //MessageBox.Show("Theme = Ocean");
                        theme = "ocean";
                        return theme;
                    default: //case 4:
                        //MessageBox.Show("Theme = Animal");
                        theme = "animal";
                        return theme;
                }
            }
            else
             return "animal"; 
        }
    }
}