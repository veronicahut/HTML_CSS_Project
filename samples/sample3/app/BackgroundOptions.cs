/* ***********************************************************************************
author: Veronica Hutchins
course: CITP 280
assignment: project part 5
date: 12/05/19
purpose: a class for the background
notes: a programmer-created generic class for programmer-created "Theme" class
       a subscriber class - see lines starting at 41
       method to handle event starting at line 45
************************************************************************************ */
using System;
using System.Windows.Forms; //for testing
namespace PreschoolGames
{
    //subscriber class; has event handler
    public class BackgroundOptions <T> where T: Theme
    {
        //variables
        private int number;
        private string themeString;
        private Theme theme = new Theme();

        //constructors
        public BackgroundOptions()
        { } //default

        public BackgroundOptions(int number)
        { this.number = number; }

        public BackgroundOptions(string themeString)
        { this.themeString = themeString; }

        public BackgroundOptions(int number, Theme theme)
        {
            this.number = number;
            this.theme = theme;
        }

        //methods
          //method to subscribe to programmer-created event for background
        public void Subscribe(GameOptions gameOptions)
        { gameOptions.optDel += new GameOptions.OptionsDelegate(ThemeChosen); }

          //method to handle the event for background
        public string ThemeChosen(object gameOptions, BackgroundEvent e)
        {
            TrainTheme train = new TrainTheme(1);
            CarTheme car = new CarTheme(2);
            OceanTheme ocean = new OceanTheme(3);
            AnimalTheme animal = new AnimalTheme(4);

            if (number == 1)
            {
                themeString = train.ShowChoice(number);
                //MessageBox.Show("(generic) Train");
            }
            else if (number == 2)
            {
                themeString = car.ShowChoice(number);
                //MessageBox.Show("(generic) Car");
            }
            else if (number == 3)
            {
                themeString = ocean.ShowChoice(number);
                //MessageBox.Show("(generic) Ocean");
            }
            else // number = 4
            {
                themeString = animal.ShowChoice(number);
                //MessageBox.Show("(generic) Animal");
            }

            return themeString;
        }
    }
}