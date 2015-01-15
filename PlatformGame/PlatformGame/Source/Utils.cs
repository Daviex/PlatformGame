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
        /// <returns>True if it's above of</returns>
        public static bool IsAboveOf(Rectangle r1, Rectangle r2)
        {
            if (r1.Bottom < r2.Top)
                return true;

            return false;
        }

        /// <summary>
        /// Check if r1 is under of r2
        /// </summary>
        /// <param name="r1">First object</param>
        /// <param name="r2">Second object</param>
        /// <returns>True if it's under of</returns>
        public static bool IsUnderOf(Rectangle r1, Rectangle r2)
        {
            if (r1.Top > r2.Bottom)
                return true;
            return false;
        }

        /// <summary>
        /// Check if r1 is left of r2
        /// </summary>
        /// <param name="r1">First object</param>
        /// <param name="r2">Second object</param>
        /// <returns>True if it's left of</returns>
        public static bool IsLeftOf(Rectangle r1, Rectangle r2)
        {
            if (r1.Right < r2.Left)
                return true;

            return false;
        }

        /// <summary>
        /// Check if r1 is right of r2
        /// </summary>
        /// <param name="r1">First object</param>
        /// <param name="r2">Second object</param>
        /// <returns>True if it's right of</returns>
        public static bool IsRightOf(Rectangle r1, Rectangle r2)
        {
            if (r1.Left > r2.Right)
                return true;

            return false;
        }

        public static float MinIntersectionX(Rectangle r1, Rectangle r2)
        {
            var left_right = Math.Abs(r1.Left - r2.Right);
            var right_left = Math.Abs(r1.Right - r2.Left);

            if (left_right > right_left)
            {
                //We are on the right
                return right_left;
            }
            else if (right_left > left_right)
            {
                //We are on the left
                return left_right;
            }

            return 0.0f;
        }
    }
}
