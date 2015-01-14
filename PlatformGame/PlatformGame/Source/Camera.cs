using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace PlatformGame.Source
{
    public class Camera
    {
        private Vector2 _position;
        private Vector2 _dimension;
        private Matrix _view;
        private float _zoom;

        private MouseState currentMouseState;
        private MouseState originalMouseState;

        public Matrix View
        {
            get { return _view; }
        }

        public float Zoom
        {
            get { return _zoom; }
            set { _zoom = value; if (_zoom < 0.1f) _zoom = 0.1f; } // Negative zoom will flip image
        }

        public float ScreenWidth
        {
            get { return _dimension.X / _zoom; }
        }

        public float ScreenHeight
        {
            get { return _dimension.Y / _zoom; }
        }

        public Camera(Vector2 windowDimension)
        {
            _dimension = windowDimension;

            currentMouseState = Mouse.GetState();
            originalMouseState = currentMouseState;
        }

        public void Update(Vector2 playerPos)
        {
            currentMouseState = Mouse.GetState();
            if (currentMouseState.ScrollWheelValue < originalMouseState.ScrollWheelValue)
            {
                _zoom -= 0.1f;
                originalMouseState = currentMouseState;
                currentMouseState.ScrollWheelValue.Equals(0);
            }
            if (currentMouseState.ScrollWheelValue > originalMouseState.ScrollWheelValue)
            {
                _zoom += 0.1f;
                originalMouseState = currentMouseState;
                currentMouseState.ScrollWheelValue.Equals(0);
            }

            _position.X = playerPos.X - (ScreenWidth / 2);
            _position.Y = playerPos.Y - (ScreenHeight/ 2);

            if (_position.X < 0)
                _position.X = 0;
            if (_position.Y < 0)
                _position.Y = 0;

            _view = Matrix.CreateTranslation(new Vector3(-_position, 0)) * Matrix.CreateScale(new Vector3(_zoom, _zoom, 1)) * Matrix.CreateRotationZ(0.0f);
        }
    }
}
