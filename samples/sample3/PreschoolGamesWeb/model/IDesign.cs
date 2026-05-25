/* **************************************************************************
author: Veronica Hutchins
course: CITP 280
assignment: project part 5
date: 12/05/19
note: an interface to be used in classes to set border color of game
*************************************************************************** */
using System;

namespace PreschoolGames
{
    interface IDesign
    {
        //an interface
        string Image(int aGameType); // UPDATE: Return a clean string for web CSS instead of colors
    }
}