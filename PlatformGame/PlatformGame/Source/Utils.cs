using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace PlatformGame.Source
{
    public class Utils
    {
        /// <summary>
        /// Check if r1 is above of r2
        /// </summary>
        /// <param name="r1">First object</param>
        /// <param name="r2">Second object</param>
        /// <returns>>True if is above else false</returns>
        public static bool IsAbove(Rectangle r1, Rectangle r2)
        {
            if (r1.Bottom < r2.Top)
                return true;

            return false;
        }

        public static bool IsLeftOf(Rectangle r1, Rectangle r2)
        {
            return false;
        }

        public static bool IsRightOf(Rectangle r1, Rectangle r2)
        {
            return false;
        }

        public static bool IsUnder(Rectangle r1, Rectangle r2)
        {
            return false;
        }
    }
}
