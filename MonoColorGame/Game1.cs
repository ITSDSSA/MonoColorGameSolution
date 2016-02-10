using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.Collections;

namespace MonoColorGame
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        colorObject[] _playerColors;
        colorObject _computerColor;
        bool gameOver = false;
        Cursor _cursor;
        KeyboardState previousKeyState;

        Color[] _colorchoices = new Color[] { Color.Red, Color.Green, Color.Blue };
        enum colors { Red, Green, Blue };
        int _currentChoice = 0;        
        private string msg;
        SpriteFont messages;
                 
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
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
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            IsMouseVisible = true;
            spriteBatch = new SpriteBatch(GraphicsDevice);
            messages = Content.Load<SpriteFont>("message");    
            _playerColors = new colorObject[3];
            Vector2 startPosition = new Vector2(100,100);
            Texture2D tx = Content.Load<Texture2D>("colorTexture");
            Random r = new Random();

            for (int i = 0; i < _playerColors.Length; i++)
            {
                _playerColors[i] =
                    new colorObject(messages,startPosition,
                        tx, _colorchoices[i],r.Next(1,10));
                startPosition += new Vector2(tx.Width + 10, 0);
            }
            // TODO: use this.Content to load your game content here
            Texture2D cursorTx = Content.Load<Texture2D>("arrow");

            // Set up the arrow and point it at the first choice
            
            _cursor = new Cursor(cursorTx, _playerColors[_currentChoice].CenterPos - new Vector2(0,cursorTx.Height));
            
            reset();
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            KeyboardState currentKeyState = Keyboard.GetState();
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                this.Exit();
            if (Keyboard.GetState().IsKeyDown(Keys.P))
                reset();
            if(currentKeyState.IsKeyUp(Keys.Right) && previousKeyState.IsKeyDown(Keys.Right))
            {
                _currentChoice = _currentChoice++ < _playerColors.Length - 1 ? _currentChoice : 0;
            }

            if (currentKeyState.IsKeyUp(Keys.Left) && previousKeyState.IsKeyDown(Keys.Left))
            {
                _currentChoice = _currentChoice-- > 0 ? _currentChoice : _playerColors.Length - 1;
            }
            _cursor.Position = _playerColors[_currentChoice].CenterPos - new Vector2(0,_cursor.Tx.Height);
            if (currentKeyState.IsKeyUp(Keys.Enter) && previousKeyState.IsKeyDown(Keys.Enter))
                _playerColors[_currentChoice].Chosen = true;

                if (!gameOver)
            {
                foreach (colorObject c in _playerColors)
                    c.update(gameTime);
                checkChosen();
                // TODO: Add your update logic here
            }
            previousKeyState = currentKeyState;
            base.Update(gameTime);
        }

        private void checkChosen()
        {
            // object to grab chosen object
            colorObject chosen = null;
            foreach (colorObject c in _playerColors)
                if (c.Chosen)
                {
                    chosen = c;
                    break;
                }
            if (chosen != null)
            {
                Random r = new Random();
                _computerColor = new colorObject(messages,new Vector2(200, 200),
                    Content.Load<Texture2D>("colorTexture"), 
                    _colorchoices[r.Next(0, _colorchoices.Length)],0);
                if (_computerColor.MyColor == chosen.MyColor)
                    msg = "You chose right you win ";
                else msg = "You chose wrong you loose ";
                msg += " Press Esc to quit. Press P to play again ";
                gameOver = true;
            }
        }

        private void reset()
        {
            _computerColor = null;
            foreach (colorObject c in _playerColors)
                c.Chosen = false;
            msg = "choose a color by clicking on the color of your choice";
            gameOver = false;
        }


        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            foreach (colorObject c in _playerColors)
                c.Draw(spriteBatch);
            if (_computerColor != null)
                _computerColor.Draw(spriteBatch);
            if (_cursor != null)
                _cursor.Draw(spriteBatch);
            spriteBatch.Begin();
            spriteBatch.DrawString(messages, msg, new Vector2(100,400), Color.White);
            spriteBatch.End();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
