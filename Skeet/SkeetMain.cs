using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;

namespace Skeet
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class SkeetGame : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteFont my_font;
        int updatecount = 0;

        public SkeetGame()
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
            spriteBatch = new SpriteBatch(GraphicsDevice);
            
            // TODO: use this.Content to load your game content here
            my_font = this.Content.Load<SpriteFont>("SpriteFont1");

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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            
            // TODO: Add your update logic here
            updatecount++;

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            
            GraphicsDevice.Clear(new Color(0.08f, 0.08f, 0.08f));

            spriteBatch.Begin();

            // TODO: Add your drawing code here
            List<string> strlist = new List<string>();

            strlist.Add("GameTime.ElapsedGameTime = " + gameTime.ElapsedGameTime);
            strlist.Add("GameTime.ElapsedRealTime = " + gameTime.ElapsedRealTime);
            strlist.Add("GameTime.TotalGameTime = " + gameTime.TotalGameTime);
            strlist.Add("GameTime.TotalRealTime = " + gameTime.TotalRealTime);
            strlist.Add("");
            strlist.Add("updatecount = " + updatecount);

            updatecount = 0;
            int i = 0;

            foreach (String output in strlist)
            {
                i++;

                spriteBatch.DrawString(this.my_font, output, new Vector2(10, (float)my_font.LineSpacing * (float)i * 1.5f), Color.White);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
