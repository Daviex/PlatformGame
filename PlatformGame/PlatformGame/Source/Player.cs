using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PlatformGame.Engine;
using PlatformGame.Source;

namespace PlatformGame
{
    public class Player : SpriteManager
    {
        private Map _map;
        public Vector2 Velocity;

        public Player(Vector2 position, Texture2D image, Map map) : base(position, image)
        {
            Velocity =  new Vector2(0, 0);
            Position = position;
            _map = map;
            _Image = image;
        }

        public override void Update(GameTime gt)
        {

            var keyboard = Keyboard.GetState();

            if (keyboard.IsKeyDown(Keys.Right))
            {
                Velocity += new Vector2(1, 0);
            }
            if (keyboard.IsKeyDown(Keys.Left))
            {
                Velocity += new Vector2(-1, 0);
            }
            if (keyboard.IsKeyDown(Keys.Up))
            {
                Velocity += new Vector2(0, -1);
            }
            if (keyboard.IsKeyDown(Keys.Down))
            {
                Velocity += new Vector2(0, 1);
            }

            //Gravity
            //Velocity += Vector2.UnitY * .5f;

            //Friction
            Velocity -= Velocity * new Vector2(.1f, .1f);

            //Update the movement
            var oldPosition = Position;
            Position += Velocity * (float)gt.ElapsedGameTime.TotalMilliseconds / 15;

            //Check for collisions
            Vector2 newPos;
            if ((newPos = HasCollided(this)) != Vector2.Zero)
            {
                Position -= newPos;
            }

            base.Update(gt);
        }

        public Vector2 HasCollided(Player player)
        {
            foreach (var tile in _map.TileBounds.Where(x => x.title == "border"))
            {
                if (player.BoundingBox.Intersects(tile.bound))
                {
                    if (Velocity.X > Velocity.Y)
                    {
                        if (player.BoundingBox.Right > tile.bound.Left && player.BoundingBox.Right < tile.bound.Right)
                        {
                            //Right Tile
                            Vector2 newPosition = new Vector2(player.BoundingBox.Right - tile.bound.Left, 0);
                            return newPosition;
                        }

                        if (player.BoundingBox.Left < tile.bound.Right && player.BoundingBox.Left > tile.bound.Left)
                        {
                            //Right Tile
                            Vector2 newPosition = new Vector2(-(tile.bound.Right - player.BoundingBox.Left), 0);
                            return newPosition;
                        }
                    }

                    if (Velocity.X < Velocity.Y)
                    {
                        if (player.BoundingBox.Bottom > tile.bound.Top && player.BoundingBox.Bottom < tile.bound.Bottom)
                        {
                            //Top Tile
                            Vector2 newPosition = new Vector2(0, player.BoundingBox.Bottom - tile.bound.Top);
                            return newPosition;
                        }

                        if (player.BoundingBox.Top < tile.bound.Bottom && player.BoundingBox.Top > tile.bound.Top)
                        {
                            //Bottom Tile
                            Vector2 newPosition = new Vector2(0, -(player.BoundingBox.Top - tile.bound.Bottom));
                            return newPosition;
                        }
                    }
                }
                /*
                if (player.BoundingBox.Intersects(tile.bound))
                {
                    Console.WriteLine(player.BoundingBox.Right + " " + tile.bound.Left);
                    if (Enumerable.Range(tile.bound.Top, tile.bound.Bottom).Contains(player.BoundingBox.Y))
                    {
                        if (tile.bound.Left > player.BoundingBox.Right)
                        {
                            //Right Tile
                            Vector2 newPosition = new Vector2(tile.bound.Left - player.BoundingBox.Right, 0);
                            return newPosition;
                        }
                        
                        if (tile.bound.Right > player.BoundingBox.Left)
                        {
                            //Left Tile
                            Vector2 newPosition = new Vector2(player.BoundingBox.Left - tile.bound.Right, 0);
                            return newPosition;
                        }
                    }
                    else if (Enumerable.Range(tile.bound.Left, tile.bound.Right).Contains(player.BoundingBox.X))
                    {
                        if (tile.bound.Left > player.BoundingBox.Right)
                        {
                            //Top Tile
                            //Vector2 newPosition = new Vector2(0, ;
                            //return newPosition;
                        }
                        else if (tile.bound.Right > player.BoundingBox.Left)
                        {
                            //Bottom Tile
                           // Vector2 newPosition = new Vector2(0, );
                            //return newPosition;
                        }
                    }
                    Console.WriteLine(player.BoundingBox.X + ":" + player.BoundingBox.Y);
                }*/

            }
            return Vector2.Zero;
        }

        public override void Draw(SpriteBatch sb)
        {
            sb.Draw(_Image, BoundingBox, Color.White);
        }
    }
}
