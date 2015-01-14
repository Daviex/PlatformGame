using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

using PlatformGame.Engine;
using PlatformGame.Source;

namespace PlatformGame
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Main : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        private Player franco;
        private Camera _camera;
        private Texture2D _Line;
        private SpriteManager _tileBorder;

        private Dictionary<string, SpriteManager> tileTextures; 

        private Map _map;

        private SpriteFont _debugFont;

        private Vector2 _dimensionWindow;

        public Main()
        {
            graphics = new GraphicsDeviceManager(this);
            IsMouseVisible = true;
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            _dimensionWindow.X = 1280;
            _dimensionWindow.Y = 720;
            graphics.PreferredBackBufferWidth = (int)_dimensionWindow.X;
            graphics.PreferredBackBufferHeight = (int)_dimensionWindow.Y;
            graphics.PreferMultiSampling = true;
            graphics.ApplyChanges();

            tileTextures = new Dictionary<string, SpriteManager>();

            _camera = new Camera(_dimensionWindow);
            _camera.Zoom = 1.0f;

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            franco = new Player(new Vector2(50,50), Content.Load<Texture2D>("franco"), new Vector2(3, 3));

            _debugFont = Content.Load<SpriteFont>("DebugFont");

            _tileBorder = new SpriteManager(new Vector2(300, 500), Content.Load<Texture2D>("TestTile"));

            tileTextures.Add("border", _tileBorder);

            _map = new Map(Content.Load<Texture2D>("mapImage"), 32);

            _Line = new Texture2D(GraphicsDevice, 1, 1);
            _Line.SetData<Color>(new Color[] { Color.Red });
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {

        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            franco.Update(gameTime);
            _camera.Update(franco.Center);

            foreach (var tile in _map.TileBounds.Where(x => x.title == "border"))
            {
                
                if (franco.BoundingBox.Intersects(tile.bound) && !(franco.BoundingBox.Top > tile.bound.Top))
                {
                    Console.WriteLine(franco.BoundingBox.X + ":" + franco.BoundingBox.Y);
                    //franco.Velocity.Y = 0;
                }
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            if (gameTime.ElapsedGameTime.TotalSeconds > 0)
            {
                float FPS = 1/(float) gameTime.ElapsedGameTime.TotalSeconds;
                Window.Title = "PlatformGame      FPS: " + Convert.ToInt32(FPS);
            }

            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, null, null, null, _camera.View);

            _map.Draw(spriteBatch, tileTextures);

            franco.Draw(spriteBatch);

            DrawBounds(spriteBatch, franco);


            spriteBatch.End();

            base.Draw(gameTime);
        }

        public void DrawBounds(SpriteBatch sb, SpriteManager instance)
        {
            List<Rectangle> bounds = new List<Rectangle>()
            {                
                //Top line
                new Rectangle(instance.BoundingBox.Left, instance.BoundingBox.Top, instance.BoundingBox.Width, 1),
                //Bottom line
                new Rectangle(instance.BoundingBox.Left, instance.BoundingBox.Bottom - 1, instance.BoundingBox.Width, 1),
                //Left line
                new Rectangle(instance.BoundingBox.Left, instance.BoundingBox.Top, 1, instance.BoundingBox.Height),
                //Right line
                new Rectangle(instance.BoundingBox.Right - 1, instance.BoundingBox.Top, 1, instance.BoundingBox.Height)
            };

            foreach(var bound in bounds)
                sb.Draw(_Line, bound, Color.Red);

            sb.Draw(_Line, instance.Center, Color.White);
        }
    }
}
