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
        public Vector3 camera_position = new Vector3(0, 0, 0.0225f);
        float camera_distance = 0.04f;

        Model[] cubes;

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
            //Graphics.MinimumVertexShaderProfile = ShaderProfile.VS_2_SW;


            //Graphics.ApplyChanges();

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

            cubes = new Model[8];

            for (int i = 0; i < 8; i++)
            {
                cubes[i] = this.Content.Load<Model>("Models/cubes");
            }
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

            if (Keyboard.GetState().IsKeyDown(Keys.Home))
            {
                this.player.rotation.Y = this.player.rotation.Y + ((float)Math.PI / 90f);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.End))
            {
                this.player.rotation.Y = this.player.rotation.Y - ((float)Math.PI / 90f);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.PageDown))
            {
                camera_distance = camera_distance + 0.0025f;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.PageUp))
            {
                camera_distance = camera_distance - 0.0025f;
                if (camera_distance < 0.0025f)
                {
                    camera_distance = 0.0025f;
                }
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                camera_distance = 0.04f;
                this.player.pos = Vector3.Zero;
                this.player.rotation = Vector3.Zero;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Z))
            {
                updatecount_max = 0;
            }

            player.move = new Vector3(0, 0, 0);
            // movement
            if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                player.move.Y += 0.0002f;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                player.move.Y -= 0.0002f;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                player.move.X -= 0.0002f;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                player.move.X += 0.0002f;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.OemComma))
            {
                player.move.Z -= 0.0002f;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.OemPeriod))
            {
                player.move.Z += 0.0002f;
            }


            base.Update(gameTime);

            Vector3 anglevector = player.pos - camera_position;
            
            camera_position.X = player.pos.X + (camera_distance * (float)Math.Sin(player.rotation.Y));
            camera_position.Z = player.pos.Z + (camera_distance * (float)Math.Cos(player.rotation.Y));
            camera_position.Y = this.player.pos.Y;
            
            

            //camera_position.X = this.player.pos.X;
            view = Matrix.CreateLookAt(
                camera_position,
                player.pos,
                Vector3.Up);

        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            int i;

            //GraphicsDevice.Clear(new Color(0.08f, 0.08f, 0.08f));
            //GraphicsDevice.Clear(Color.DarkSlateBlue);
            GraphicsDevice.Clear(ClearOptions.Target | ClearOptions.DepthBuffer, new Color(0.08f, 0.08f, 0.28f), 1.0f, 0);
            
            GraphicsDevice.RenderState.DepthBufferEnable = true;
            GraphicsDevice.RenderState.DepthBufferWriteEnable = true;

            for (i = 0; i < 8; i++ )
            {
                foreach (ModelMesh mesh in cubes[i].Meshes)
                {
                    foreach (BasicEffect effect in mesh.Effects)
                    {
                        effect.Begin();

                        effect.EnableDefaultLighting();

                        if (i == 0)
                        {
                            effect.DiffuseColor = new Vector3(0f, 0.7f, 0.5f);
                        }
                        else
                        {
                            effect.DiffuseColor = new Vector3(0.9f, 0.2f, 0.2f);
                        }

                        float x, y, z;
                        if (i % 2 == 0)
                        {
                            x = 0.0064f;
                        }
                        else
                        {
                            x = -0.0064f;
                        }
                        if (i % 4 < 2)
                        {
                            y = 0.0064f;
                        }
                        else
                        {
                            y = -0.0064f;
                        }
                        if (i < 4)
                        {
                            z = 0.0064f;
                        }
                        else
                        {
                            z = -0.0064f;
                        }

                        effect.World =
                            Matrix.CreateFromYawPitchRoll(
                            0, 0, 0) *

                            Matrix.CreateScale(0.1f) *

                            Matrix.CreateTranslation(new Vector3(x, y, z));

                        effect.Projection = projection;
                        effect.View = view;
                        effect.End();
                    }
                    mesh.Draw();

                }
            }

            base.Draw(gameTime);

            spriteBatch.Begin();

            if (updatecount > updatecount_max)
            {
                updatecount_max = updatecount;
            }

            List<string> strlist = new List<string>();

            strlist.Add(Graphics.GraphicsDevice.Viewport.Width + "x" + Graphics.GraphicsDevice.Viewport.Height + "; updatecount = " + updatecount + ", worst = " + updatecount_max);
            strlist.Add("camera distance = " + camera_distance + "; camera position = " + camera_position);
            strlist.Add("player.rotation = " + player.rotation + "; player.position = " + player.pos);
            
            updatecount = 0;
            i = 0;

            foreach (String output in strlist)
            {
                i++;

                spriteBatch.DrawString(this.my_font, output, new Vector2(10, (float)my_font.LineSpacing * (float)i * 1.5f), Color.White);
            }

            
            spriteBatch.End();
            
        }
    }
}
