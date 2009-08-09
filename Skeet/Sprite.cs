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
    class Sprite
    {
        Quad _quad;
        public Texture2D texture
        {
            get
            {
                return _texture;
            }
            set
            {
                _texture = value;
                _quadEffect.Texture = _texture;
            }
        }

        Texture2D _texture;

        BasicEffect _quadEffect;
        ScreenBits _screen;
        VertexDeclaration _quadVertexDecl;

        public Sprite(ScreenBits screen, Texture2D texture)
        {
            _screen = screen;
            _quad = new Quad(new Vector3(0, 0, 0), Vector3.Backward, Vector3.Up, texture.Width, texture.Height);
            this._texture = texture;

            _quadEffect = new BasicEffect(screen.graphics.GraphicsDevice, null);
            _quadEffect.EnableDefaultLighting();

            _quadEffect.World = Matrix.Identity;
            _quadEffect.View = screen.View;
            _quadEffect.Projection = screen.Projection;
            _quadEffect.TextureEnabled = true;
            _quadEffect.Texture = this._texture;

            _quadVertexDecl = new VertexDeclaration(
                _screen.graphics.GraphicsDevice,
                VertexPositionNormalTexture.VertexElements
                );
        }

        public void Draw(GameTime gameTime)
        {
            _screen.graphics.GraphicsDevice.VertexDeclaration = _quadVertexDecl;

            _quadEffect.View = _screen.View;
            _quadEffect.Projection = _screen.Projection;

            _screen.test = _screen.test + 1;

            _quadEffect.Begin();
            foreach (EffectPass pass in _quadEffect.CurrentTechnique.Passes)
            {
                pass.Begin();

                _screen.graphics.GraphicsDevice.DrawUserIndexedPrimitives
                    <VertexPositionNormalTexture>(
                    PrimitiveType.TriangleList,
                    _quad.Vertices, 0, 4,
                    _quad.Indexes, 0, 2);

                pass.End();
            }
            _quadEffect.End();
        }
    }
}
