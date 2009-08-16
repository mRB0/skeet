using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Skeet
{
    public class Player : DrawableGameComponent
    {
        /*
         * CONSTANTS
         */
        const int _sprite_animcount = 4;
        const int _sprite_width = 60;
        const int _sprite_height = 60;
        const int _sprite_animspeed = 18; // ticks required per change

        /*
         * VARIABLES
         */
        public Texture2D drawsprite
        {
            get
            {
                return _animation;
            }
        }
        public Rectangle drawrect
        {
            get
            {
                return new Rectangle(
                    0, 
                    _sprite_height * _sprite_idx, 
                    _sprite_width, 
                    _sprite_height
                    );
            }
        }
        public Vector2 drawloc
        {
            get
            {
                return _sprite_loc;
            }
        }

        Texture2D _animation;
        Sprite _animation_sprite;
        int _sprite_idx;
        int _sprite_animwait;
        Vector2 _sprite_loc;
        ScreenBits _screen;

        public float _dbg_newx = 0f, _dbg_newy = 0f, _dbg_newz = 0f;
        public float _dbg_scale = 1.0f;

        public Player(Game game, ScreenBits screen, String who) : base(game)
        {
            _screen = screen;
            _sprite_idx = 0;
            _sprite_animwait = 0;
            _sprite_loc.X = 10f;
            _sprite_loc.Y = 80f;

            _animation = _screen.content.Load<Texture2D>("Sprites/" + who + "/move");
            _animation_sprite = new Sprite(_screen, _animation);
        }
        
        public override void Update(GameTime gameTime)
        {
            _sprite_loc.X = _sprite_loc.X + 1;

            _sprite_animwait = (_sprite_animwait + 1) % _sprite_animspeed;

            if (0 == _sprite_animwait)
            {
                _sprite_idx = (_sprite_idx + 1) % _sprite_animcount;
            }

            _dbg_newx = _dbg_newx + 0.001f;
            _dbg_newy = _dbg_newy + 0.001f;
            if (_dbg_newx > 0.016f)
            {
                _dbg_newx = -0.016f;
            }
            if (_dbg_newy > 0.016f)
            {
                _dbg_newy = -0.016f;
            }

            _dbg_newz = _dbg_newz - 0.00125f;
            
            if (_dbg_newz < -0.2f)
            {
                _dbg_newz = 0.0f;
            }
            
            _dbg_scale = _dbg_scale + 0.025f;
            if (_dbg_scale > 3.0f)
            {
                _dbg_scale = 0.25f;
            }

            // ignore above, reset back to normal values
            _dbg_newx = 0;
            _dbg_newy = 0;
            //_dbg_newz = -0.001f;
            _dbg_scale = 1.0f;

            _animation_sprite.test_translation = new Vector3(_dbg_newx, _dbg_newy, _dbg_newz);
            _animation_sprite.test_scale = _dbg_scale;

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            _animation_sprite.texture = _animation;
            _animation_sprite.Draw(gameTime);

            base.Draw(gameTime);
        }

    }
}
