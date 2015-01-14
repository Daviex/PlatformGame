using System;
using System.Collections.Generic;
using System.Linq;
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
        public Vector2 Velocity;
        public Vector2 Direction;

        public Player(Vector2 position, Texture2D image, Vector2 velocity) : base(position, image)
        {
            Velocity = velocity;
            Position = position;
            _Image = image;
        }

        public override void Update(GameTime gt)
        {
            var keyboard = Keyboard.GetState();

            if (keyboard.IsKeyDown(Keys.Right))
            {
                Direction.X = 1;
                Position.X = Position.X + Velocity.X * Direction.X;
            }
            if (keyboard.IsKeyDown(Keys.Left))
            {
                Direction.X = -1;
                Position.X = Position.X + Velocity.X * Direction.X;
            }
            if (keyboard.IsKeyDown(Keys.Up))
            {
                Direction.Y = -1;
                Position.Y = Position.Y + Velocity.Y * Direction.Y;
            }
            if (keyboard.IsKeyDown(Keys.Down))
            {
                Direction.Y = 1;
                Position.Y = Position.Y + Velocity.Y * Direction.Y;
            }

            base.Update(gt);
        }

        public override void Draw(SpriteBatch sb)
        {
            sb.Draw(_Image, BoundingBox, Color.White);
        }
    }
}
