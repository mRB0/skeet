using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Skeet
{
    public class Player : Sprite
    {
        /*
         * CONSTANTS
         */
        const int _sprite_animcount = 4;
        const int _sprite_width = 64;
        const int _sprite_height = 64;
        const int _sprite_animspeed = 10; // ticks required per change

        /*
         * VARIABLES
         */
        public Texture2D drawsprite
        {
            get
            {
                return _animation[_sprite_idx];
            }
        }
        public Vector2 drawloc
        {
            get
            {
                return _sprite_loc;
            }
        }

        Texture2D[] _animation;
        int _sprite_idx;
        int _sprite_animwait;
        Vector2 _sprite_loc;
        String _whoami;
        SkeetGame _game;

        
        public Player(SkeetGame game, String who) : base(game)
        {
            this._game = game;
            _sprite_idx = 0;
            _sprite_animwait = 0;
            _sprite_loc.X = 0f - _sprite_width;
            _sprite_loc.Y = (this._game.GraphicsDevice.Viewport.Height / 2) - (_sprite_height/2);

            this._whoami = who;
        }

        protected override void LoadContent()
        {
            base.LoadContent();

            _animation = new Texture2D[_sprite_animcount];

            for (int i = 0; i < _sprite_animcount; i++)
            {
                _animation[i] = this._game.Content.Load<Texture2D>("Sprites/" + _whoami + "/move" + i);
            }

            //this.texture = drawsprite;

        }

        public override void Update(GameTime gameTime)
        {
            _sprite_loc.X = _sprite_loc.X + 1.5f;
            if (_sprite_loc.X > this._game.Graphics.GraphicsDevice.Viewport.Width)
            {
                _sprite_loc.X = 0f - _sprite_width;
            }

            _sprite_animwait = (_sprite_animwait + 1) % _sprite_animspeed;

            if (0 == _sprite_animwait)
            {
                _sprite_idx = (_sprite_idx + 1) % _sprite_animcount;
            }

            this.texture = drawsprite;

            //this.pos = this.pos + new Vector3(0.001f, 0f, 0f);

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            //_animation_sprite.texture = _animation[_sprite_idx];// drawsprite;
            //_animation_sprite.Draw(gameTime);

            base.Draw(gameTime);
        }

    }
}
