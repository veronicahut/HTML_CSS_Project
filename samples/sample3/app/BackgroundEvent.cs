/* ***********************************************************************************
author: Veronica Hutchins
course: CITP 280
assignment: project part 5
date: 12/05/19
purpose: a class for the background
notes: A programmer-created Event; BackgroundOptions.cs has the subscriber method
       A Delegate is used in GameOptions.cs to wire BackgroundEvent to event handler
       Inherits from EventArgs at line 15
************************************************************************************ */
using System;

namespace PreschoolGames
{
    public class BackgroundEvent : EventArgs
    {
        //variable
        public int number;

        //constructors
        public BackgroundEvent()
        { } //default

        public BackgroundEvent(int number)
        { this.number = number; }
    }
}