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
        
        Model _quad;
        private const float _quadside = 1.0f; // length of any side of quad (which is square)

        public BoundingSphere bounds;

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
        protected SkeetGame _game;

        public Sprite(SkeetGame game) : base(game)
        {
            this._game = game;
        }

        protected override void LoadContent()
        {
            _quad = this._game.Content.Load<Model>("Models/flatsquare");
            /*
            foreach (ModelMesh mesh in _quad.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.EnableDefaultLighting();
                    effect.PreferPerPixelLighting = true;
                }
            }
             */
        }

        
        public override void Update(GameTime gameTime)
        {
            // normalize rotation values

            while (this.rotation.X < 0f)
            {
                this.rotation.X += (2.0f * (float)Math.PI);
            }
            while (this.rotation.Y < 0f)
            {
                this.rotation.Y += (2.0f * (float)Math.PI);
            }
            while (this.rotation.Z < 0f)
            {
                this.rotation.Z += (2.0f * (float)Math.PI);
            }

            this.rotation.X = this.rotation.X % (2.0f * (float)Math.PI);
            this.rotation.Y = this.rotation.Y % (2.0f * (float)Math.PI);
            this.rotation.Z = this.rotation.Z % (2.0f * (float)Math.PI);


            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            // set rotation to face the camera

            // draw object
            foreach (ModelMesh mesh in _quad.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.Begin();

                    effect.World =
                        Matrix.CreateScale(_scale) *

                        Matrix.CreateTranslation(new Vector3(-this.ctr.X, -this.ctr.Y, 0)) *
                        
                        Matrix.CreateFromYawPitchRoll(
                        rotation.Y,
                        rotation.X,
                        rotation.Z) *

                        //Matrix.CreateTranslation(new Vector3(this.ctr.X, this.ctr.Y, 0)) *

                        Matrix.CreateTranslation(pos);

                    effect.Projection = _game.projection;
                    effect.View = _game.view;

                    effect.End();
                }
                mesh.Draw();
            }
            base.Draw(gameTime);

        }

        /* 
         * convert a 3D move vector, relative to the object's position in a
         * 2D plane (eg. according to its rotation), into a 3D vector that
         * represents movement in that direction
         */
        public Vector3 moveFwdToDir(Vector3 direction, Vector3 rotation)
        {
            Vector3 adjusted_direction = new Vector3();
            
            Matrix m = Matrix.CreateTranslation(direction);
            m = m * Matrix.CreateFromYawPitchRoll(rotation.Y, rotation.X, rotation.Z);

            adjusted_direction = m.Translation;

            return adjusted_direction;
        }
        /*
         * same as above but using object's known rotation
         */
        public Vector3 moveFwdToDir(Vector3 direction)
        {
            return moveFwdToDir(direction, this.rotation);
        }

        public BoundingSphere getBounds()
        {
            this.bounds.Center = this.pos;

            return this.bounds;
        }

        /*
         * spritey bits
         */

        // xxx for now, the texture must be square (like the quad)
        protected float texture_width
        {
            get
            {
                return _texture_width;
            }
            set
            {
                _texture_width = value;
                _scale = _texture_width / _quadside;
            }
        }
        protected float _texture_width = 0.064f;
        protected float _texture_height = 0.064f;
        
        protected float _scale = 0.064f;

        // width/height of the actual sprite (will likely be smaller than _texture_*)
        protected float width = 0.032f;
        protected float height = 0.064f;
        // center of sprite, as an offset from center of quad.
        // eg. base of sprite should be at 0 - offs.Y - (height/2f)
        protected Vector2 ctr = new Vector2(0, 0);

        public Vector3 rotation = Vector3.Zero;
        public Vector3 pos = Vector3.Zero;

    }
}
