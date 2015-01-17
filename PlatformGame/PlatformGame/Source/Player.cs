using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
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
            var oldPosition = this.BoundingBox;
            Position += Velocity * (float)gt.ElapsedGameTime.TotalMilliseconds / 15;

            Console.WriteLine(Velocity.X + " " + Velocity.Y);

            //Check for collisions
            Vector2 newPos;
            HasCollided(oldPosition);

            base.Update(gt);
        }

        public void HasCollided(Rectangle oldPos)
        {
            var inCollide = _map.TileBounds.Where(x => x.title == "border").Where(tile => this.BoundingBox.Intersects(tile.bound)).ToList();

            if (inCollide.Count > 0)
            {
                List<Rectangle> boundsTest = new List<Rectangle>();

                foreach (var tile in inCollide)
                {
                    boundsTest.Add(tile.bound);
                }

                boundsTest.Sort((x, y) => Utils.OverlapArea(y, BoundingBox).CompareTo(Utils.OverlapArea(x, BoundingBox)));

                foreach (var tile in boundsTest)
                {
                    var value = Utils.CalculateVectors(this, tile, oldPos);
                    Position -= value;
                }
            }

            /*
            foreach (var tile in _map.TileBounds.Where(x => x.title == "border").Where(tile => this.BoundingBox.Intersects(tile.bound)))
            {
                //TODO: Calculate all the distance from all sides
                var value = Utils.CalculateVectors(this, tile.bound, oldPos);

                Position -= value;

                /*
                if (interX > 0) // Collision was on the left
                {
                    var newPosition = new Vector2(interX, 0);
                    return newPosition;
                }
                else if (interX < 0) //Collision was on the right
                {
                    var newPosition = new Vector2(interX, 0);
                    return newPosition;
                }*/

                /*
                if (Velocity.X > Velocity.Y && (Utils.IsRightOf(player.BoundingBox, tile.bound) || Utils.IsLeftOf(player.BoundingBox, tile.bound)))
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

                if (Velocity.X < Velocity.Y && (Utils.IsAboveOf(player.BoundingBox, tile.bound) || Utils.IsUnderOf(player.BoundingBox, tile.bound)))
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
                }*/
            //}
        }

        public override void Draw(SpriteBatch sb)
        {
            sb.Draw(_Image, BoundingBox, Color.White);
        }
    }
}
