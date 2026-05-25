/* *********************************************************************************
author: Veronica Hutchins
course: CITP 280
assignment: project part 5
date: 12/05/19
purpose: a class that will allow users to have options for PreschoolGames App
notes: A publisher class for a programmer-created event (BackgroundEvent)
       A Delegate is used starting at line 21
       method to raise event starts at line 27
********************************************************************************** */
using System;
using System.Windows.Forms; //for testing

namespace PreschoolGames
{
    //publisher class; will signal others when an BackgroundEvent event occurs
    public class GameOptions
    {
        //variables
          //declare delegate to wire event handler from subscriber class
        public delegate string OptionsDelegate(object gameOptions, BackgroundEvent bEvent);
          //create instance of delegate
        public event OptionsDelegate optDel;

        //constructors - can use default
        //method to raise event
        public string ChooseOptions(int number)
        {
            BackgroundEvent bEvent;
            if (number == 1)
            {
                //MessageBox.Show("Game option with a train: " + number.ToString());

                //create object of event
                bEvent = new BackgroundEvent(number);

                //see if there are subscribers
                if (optDel != null) { optDel(this, bEvent); }

                return "train";
            }
            else if (number == 2)
            {
                //MessageBox.Show("Game option with a car: " + number.ToString());

                //create object of event
                bEvent = new BackgroundEvent(number);

                //see if there are subscribers
                if (optDel != null) { optDel(this, bEvent); }

                return "car";
            }
            else if (number == 3)
            {
                //MessageBox.Show("Game option with the ocean: " + number.ToString());

                //create object of event
                bEvent = new BackgroundEvent(number);

                //see if there are subscribers
                if (optDel != null) { optDel(this, bEvent); }

                return "ocean";
            }
            else //aNumber is 4
            {
                //MessageBox.Show("Game option with animals: " + number.ToString());

                //create object of event
                bEvent = new BackgroundEvent(number);

                //see if there are subscribers
                if (optDel != null) { optDel(this, bEvent); }

                return "animal";
            }
        }
    }
}