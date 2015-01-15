using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
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
            Velocity += Vector2.UnitY * .5f;

            //Friction
            Velocity -= Velocity * new Vector2(.1f, .1f);

            //Update the movement
            var oldPosition = Position;
            Position += Velocity * (float)gt.ElapsedGameTime.TotalMilliseconds / 15;

            //Check for collisions
            if (HasCollided(this))
            {
                Position = oldPosition;
            }

            base.Update(gt);
        }

        public bool HasCollided(Player player)
        {
            foreach (var tile in _map.TileBounds.Where(x => x.title == "border"))
            {
                if (player.BoundingBox.Intersects(tile.bound))
                {
                    Console.WriteLine(player.BoundingBox.X + ":" + player.BoundingBox.Y);
                    return true;
                }
            }
            return false;
        }

        public override void Draw(SpriteBatch sb)
        {
            sb.Draw(_Image, BoundingBox, Color.White);
        }
    }
}
