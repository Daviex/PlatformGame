using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace PlatformGame.Engine
{
    public class SpriteManager
    {
        public Texture2D _Image;
        public Vector2 Position;

        public Rectangle BoundingBox
        {
            get
            {
                return new Rectangle(
                    (int)Position.X,
                    (int)Position.Y,
                    _Image.Width,
                    _Image.Height);
            }
        }

        public Vector2 Center
        {
            get
            {
                var X = (BoundingBox.Right + BoundingBox.Left) / 2;
                var Y = (BoundingBox.Bottom + BoundingBox.Top) / 2;

                return new Vector2(X, Y);
            }
        }

        public SpriteManager(Vector2 position, Texture2D image)
        {
            Position = position;
            _Image = image;
        }
        
        public virtual void Update(GameTime gt)
        {
        }

        public virtual void Draw(SpriteBatch sb)
        {
            sb.Draw(_Image, Position, Color.White);
        }
    }
}
