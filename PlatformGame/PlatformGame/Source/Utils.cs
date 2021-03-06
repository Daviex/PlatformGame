﻿using System;
using System.Linq;
using System.Security.Cryptography;
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
            if (r1.Bottom <= r2.Top)
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
            if (r1.Top >= r2.Bottom)
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
            if (r1.Right <= r2.Left)
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
            if (r1.Left >= r2.Right)
                return true;

            return false;
        }

        public static float MinIntersectionX(Rectangle r1, Rectangle r2)
        {
            var pLeft_tRight = r1.Left - r2.Right;
            var pRight_tLeft = r1.Right - r2.Left;

            if (Math.Abs(pLeft_tRight) > Math.Abs(pRight_tLeft))
            {
                //We are on the right
                return pRight_tLeft;
            }
            else if (Math.Abs(pLeft_tRight) < Math.Abs(pRight_tLeft))
            {
                //We are on the left
                return pLeft_tRight;
            }

            return 0.0f;
        }

        public static float MinIntersectionY(Rectangle r1, Rectangle r2)
        {
            var pTop_tBottom = r1.Top - r2.Bottom;
            var pBottom_tTop = r1.Bottom - r2.Top;

            if (Math.Abs(pTop_tBottom) > Math.Abs(pBottom_tTop))
            {
                //We are on the right
                return pBottom_tTop;
            }
            else if (Math.Abs(pTop_tBottom) < Math.Abs(pBottom_tTop))
            {
                //We are on the left
                return pTop_tBottom;
            }

            return 0.0f;
        }

        public static Vector2 CalculateVectors(Player player, Rectangle r2, Rectangle oldPos)
        {
            var topV = new Vector2(0, player.BoundingBox.Bottom - r2.Top);
            var botV = new Vector2(0, player.BoundingBox.Top - r2.Bottom);
            var leftV = new Vector2(player.BoundingBox.Right - r2.Left, 0);
            var rightV = new Vector2(player.BoundingBox.Left - r2.Right, 0);

            if (topV.Y > 0 && player.Velocity.Y > 0 && IsAboveOf(oldPos, r2))
                return topV;
            if (botV.Y < 0 && player.Velocity.Y < 0 && IsUnderOf(oldPos, r2))
                return botV;
            if (leftV.X > 0 && player.Velocity.X > 0 && IsLeftOf(oldPos, r2))
               return leftV;
            if (rightV.X < 0 && player.Velocity.X < 0 && IsRightOf(oldPos, r2))
               return rightV;

            return Vector2.Zero;
        }

        public static int OverlapX(Rectangle r1, Rectangle r2)
        {
            return r1.Left < r2.Left ? r1.Right - r2.Left : r2.Right - r1.Left;
        }

        public static int OverlapY(Rectangle r1, Rectangle r2)
        {
            return r1.Top < r2.Top ? r1.Bottom - r2.Top : r2.Bottom - r1.Top;
        }

        public static float OverlapArea(Rectangle r1, Rectangle r2)
        {
            var x = OverlapX(r1, r2);
            var y = OverlapY(r1, r2);

            return x*y;
        }
    }
}
