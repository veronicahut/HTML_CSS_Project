/* *********************************************************************************
author: Veronica Hutchins
course: CITP 280
assignment: project part 5
date: 12/05/19
purpose: a static class that will extend the PreschoolGames App capabilities
notes: programmer-created extension method starts at line 19
********************************************************************************** */
using System;

namespace PreschoolGames
{
    static class GameExtension
    {
        //variable
        private static string today;

        //method; to be used as an extension method to format score record with date
        public static string FormatScore(this String strScore)
        {
            today = DateTime.Now.ToString();
            return strScore.Insert(strScore.Length - 1, today);
        }
    }
}
