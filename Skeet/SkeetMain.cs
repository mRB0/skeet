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
        public GraphicsDeviceManager Graphics;
        public Matrix view, projection;
        public SpriteBatch spriteBatch;

        SpriteFont my_font;
        Player player;

        int updatecount = 0, updatecount_max = 0;
        public Vector3 camera_position = new Vector3(0, 0, 0.10f);

        public SkeetGame()
        {
            Graphics = new GraphicsDeviceManager(this);
            
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
            Graphics.MinimumVertexShaderProfile = ShaderProfile.VS_2_SW;
            
            Graphics.ApplyChanges();

            view = Matrix.CreateLookAt(
                new Vector3(0, -110, 54),
                Vector3.Zero,
                Vector3.Up);
            projection = Matrix.CreatePerspectiveFieldOfView(
                MathHelper.ToRadians(45.0f),
                Graphics.GraphicsDevice.Viewport.AspectRatio,
                0.001f,
                1.0f);
                /*Matrix.CreatePerspectiveFieldOfView(
                MathHelper.PiOver4,
                screen.graphics.GraphicsDevice.Viewport.AspectRatio,
                1,
                500);*/

            player = new Player(this, "Celes");
            Components.Add(player);
            
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
            
            // use this.Content to load your game content here
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

            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                this.Exit();
            }

            /*
             * Update logic.
             */
            updatecount++;

            if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                camera_position.Y = camera_position.Y + 0.01f;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                camera_position.Y = camera_position.Y - 0.01f;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                camera_position.X = camera_position.X - 0.01f;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                camera_position.X = camera_position.X + 0.01f;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                camera_position.X = 0;
                camera_position.Y = 0;
            }

            view = Matrix.CreateLookAt(
                camera_position,
                Vector3.Zero,
                Vector3.Up);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            
            GraphicsDevice.Clear(new Color(0.08f, 0.08f, 0.08f));

            base.Draw(gameTime);

            spriteBatch.Begin();

            if (updatecount > updatecount_max)
            {
                updatecount_max = updatecount;
            }

            List<string> strlist = new List<string>();

            strlist.Add(Graphics.GraphicsDevice.Viewport.Width + "x" + Graphics.GraphicsDevice.Viewport.Height);
            strlist.Add("GameTime.ElapsedGameTime = " + gameTime.ElapsedGameTime);
            strlist.Add("GameTime.ElapsedRealTime = " + gameTime.ElapsedRealTime);
            strlist.Add("GameTime.TotalGameTime = " + gameTime.TotalGameTime);
            strlist.Add("GameTime.TotalRealTime = " + gameTime.TotalRealTime);
            strlist.Add("");
            strlist.Add("updatecount = " + updatecount + ", worst = " + updatecount_max);
            strlist.Add("cameraposition.x,y,z = " + camera_position.X + ", " + camera_position.Y + ", " + camera_position.Z);
            strlist.Add("rotation.Y = " + MathHelper.ToDegrees(player.rotation.Y));
            
            updatecount = 0;
            int i = 0;

            foreach (String output in strlist)
            {
                i++;

                spriteBatch.DrawString(this.my_font, output, new Vector2(10, (float)my_font.LineSpacing * (float)i * 1.5f), Color.White);
            }

            //screen.spriteBatch.Draw(player.drawsprite, player.drawloc, player.drawrect, Color.White);
            //spriteBatch.Draw(player.drawsprite, player.drawloc, new Rectangle(0, 0, 64, 64), Color.White);

            spriteBatch.End();
           
            //player.Draw(gameTime);
            
        }
    }
}
