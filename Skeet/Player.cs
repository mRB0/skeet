using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Skeet
{
    class Player
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

        public Player(ScreenBits screen, String who)
        {
            _screen = screen;
            _sprite_idx = 0;
            _sprite_animwait = 0;
            _sprite_loc.X = 10f;
            _sprite_loc.Y = 80f;

            _animation = _screen.content.Load<Texture2D>("Sprites/" + who + "/move");
            _animation_sprite = new Sprite(_screen, _animation);
        }
        
        public void Update(GameTime gameTime)
        {
            _sprite_loc.X = _sprite_loc.X + 1;

            _sprite_animwait = (_sprite_animwait + 1) % _sprite_animspeed;

            if (0 == _sprite_animwait)
            {
                _sprite_idx = (_sprite_idx + 1) % _sprite_animcount;
            }
        }

        public void Draw(GameTime gameTime)
        {
            _animation_sprite.texture = _animation;
            _animation_sprite.Draw(gameTime);
        }

    }
}
