using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using PlatformGame.Engine;

namespace PlatformGame.Source
{
    public class Map
    {
        private Texture2D _mapImage;

        private int[,] _mapMatrix;

        private int _width;
        private int _height;

        private int _scale;

        public class _tileBounds
        {
            public string title;
            public Rectangle bound;

            public _tileBounds(Rectangle bound, string title)
            {
                this.bound = bound;
                this.title = title;
            }
        }

        public List<_tileBounds> TileBounds;

        public int scaledWidth
        {
            get { return _width*_scale; }
        }

        public int scaledHeight
        {
            get { return _height * _scale; }
        }

        public Map(Texture2D image, int tileDimension)
        {
            _mapImage = image;  //Image file of the map

            _width = _mapImage.Width;   //Image width
            _height = _mapImage.Height; //Image height

            TileBounds = new List<_tileBounds>();

            _scale = tileDimension; //Tile dimension in px

            _mapMatrix = new int[_width,_height];   //Initialization Matrix

            LoadMap();  //Load the Map
        }

        public void LoadMap()
        {
            int cont = 0;
            Color[] MapInfo = new Color[_width * _height];  //Taking the colors from the images and store it in array
            _mapImage.GetData(MapInfo); //Loading the colors in Array

            for (int h = 0; h < _height; h++)
            {
                for (int w = 0; w < _width; w++)
                {
                    _mapMatrix[w, h] = ColorSelection(MapInfo[cont]);   //Give a value to colors
                    DefineTile(w, h);
                    cont++;
                }
            }
        }

        public void DefineTile(int i, int j)
        {
            if (_mapMatrix[i, j] == 0)
            {
                var bound = new Rectangle(i*_scale, j*_scale, _scale, _scale);
                var title = "border";
                TileBounds.Add(new _tileBounds(bound, title));
            }
            else
            {
                var bound = new Rectangle(i * _scale, j * _scale, _scale, _scale);
                var title = "neutral";
                TileBounds.Add(new _tileBounds(bound, title));
            }
        }

        public int ColorSelection(Color pixelColor)
        {
            int value;

            if (pixelColor == Color.Black)
                value = 0;  //Border
            else if (pixelColor == Color.White)
                value = 1;  //White space
            else if (pixelColor == Color.Red)
                value = 2;  //Unk
            else
                value = -1; //Disable value

            return value;
        }

        public void Draw(SpriteBatch sb, Dictionary<string, SpriteManager> tileTexs)
        {
            for (int wS = 0, w = 0; wS < scaledWidth && w < _width; wS += _scale, w++)
            {
                for (int hS = 0, h = 0; hS < scaledHeight && h < _height; hS += _scale, h++)
                {
                    if(_mapMatrix[w, h] == 0)   //If the value is 0, is a border
                        sb.Draw(tileTexs["border"]._Image, new Vector2(wS, hS), Color.White);   //Draw it!
                }
            }
        }
    }
}
