using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    public class Sprite : DrawableGameComponent
    {
        public Vector3 test_translation = Vector3.Zero;
        public Vector3 test_rotation = Vector3.Zero;
        public float test_scale = 1.0f;

        Model _quad;

        public Texture2D texture
        {
            get
            {
                return _texture;
            }
            set
            {
                _texture = value;

                foreach (ModelMesh mesh in _quad.Meshes)
                {
                    foreach (BasicEffect effect in mesh.Effects)
                    {
                        effect.TextureEnabled = true;
                        effect.Texture = _texture;
                    }
                }
               
            }
        }

        Texture2D _texture;
        SkeetGame _game;

        public Sprite(SkeetGame game) : base(game)
        {
            this._game = game;
        }

        protected override void LoadContent()
        {
            _quad = this._game.Content.Load<Model>("Models/flatsquare");

            foreach (ModelMesh mesh in _quad.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.EnableDefaultLighting();
                    effect.PreferPerPixelLighting = true;
                }
            }
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {

            // draw object
            foreach (ModelMesh mesh in _quad.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.World = Matrix.CreateFromYawPitchRoll(
                        test_rotation.Y,
                        test_rotation.X,
                        test_rotation.Z) *

                        Matrix.CreateScale(test_scale) *

                        Matrix.CreateTranslation(test_translation);

                        Matrix.CreateTranslation(Vector3.Zero);

                    effect.Projection = _game.projection;
                    effect.View = _game.view;
                }
                mesh.Draw();
            }
            base.Draw(gameTime);

        }
    }
}
