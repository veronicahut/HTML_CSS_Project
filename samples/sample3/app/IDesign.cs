/* **************************************************************************
author: Veronica Hutchins
course: CITP 280
assignment: project part 5
date: 12/05/19
purpose: an interface to be used in classes to set border color of game
*************************************************************************** */
using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

namespace PreschoolGames
{
    interface IDesign
    {
        //use an interface
        System.Drawing.Color Image(int aGameType);
    }
}