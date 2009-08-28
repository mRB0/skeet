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
        const int _sprite_width = 30;
        const int _sprite_height = 48;
        const int _sprite_animspeed = 10; // ticks required per change
        const float _sprite_maxfallspeed = 0.001f;
        const float _sprite_maxwalkspeed = 0.0005f;
        const float _sprite_fallaccel = 0.00001f;
        const float _sprite_walkaccel = 0.00005f;

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
        
        Texture2D[] _animation;
        int _sprite_idx;
        int _sprite_animwait;
        String _whoami;
        public bool _falling;
        Vector3 _speed = new Vector3(0,0,0);

        public Vector3 move = Vector3.Zero;
        
        public Player(SkeetGame game, String who) : base(game)
        {
            _sprite_idx = 0;
            _sprite_animwait = 0;
            
            this._whoami = who;
            
            _falling = false;
        }

        protected override void LoadContent()
        {
            base.LoadContent();

            _animation = new Texture2D[_sprite_animcount];

            for (int i = 0; i < _sprite_animcount; i++)
            {
                _animation[i] = this._game.Content.Load<Texture2D>("Sprites/" + _whoami + "/move" + i);
            }

            this.texture_width = 0.0001f * 64f;
            this.ctr = new Vector2(0f, -0.0004f);
            //this.texture = drawsprite;

            this.height = (float)_sprite_height * 0.0001f;
            this.width = (float)_sprite_width * 0.0001f;
            

            this.bounds = new BoundingSphere(Vector3.Zero, this.height/2.0f);

            
        }

        

        public override void Update(GameTime gameTime)
        {
            _sprite_animwait = (_sprite_animwait + 1) % _sprite_animspeed;

            if (0 == _sprite_animwait)
            {
                _sprite_idx = (_sprite_idx + 1) % _sprite_animcount;
            }

            this.texture = drawsprite;


            if (this.move.X > 0.00001f)
            {
                this._speed.X += _sprite_walkaccel;
                if (this._speed.X > _sprite_maxwalkspeed)
                {
                    this._speed.X = _sprite_maxwalkspeed;
                }
            }
            else if (this.move.X < -0.00001f)
            {
                this._speed.X -= _sprite_walkaccel;
                if (this._speed.X < -_sprite_maxwalkspeed)
                {
                    this._speed.X = -_sprite_maxwalkspeed;
                }
            }
            else
            {
                if (this._speed.X > 0.00001f)
                {
                    this._speed.X -= _sprite_walkaccel;
                    if (this._speed.X < 0.00001f)
                    {
                        this._speed.X = 0;
                    }

                }
                else if (this._speed.X < -0.00001f)
                {
                    this._speed.X += _sprite_walkaccel;
                    if (this._speed.X > -0.00001f)
                    {
                        this._speed.X = 0;
                    }
                }
                else
                {
                    this._speed.X = 0;
                }
            }

            Vector3 move = new Vector3(this._speed.X, 0, 0);
            
            move = moveFwdToDir(move, new Vector3(0.0f, this.rotation.Y, 0.0f));
            
            if (this._falling)
            {
                this._speed.Y -= _sprite_fallaccel;

                if (this._speed.Y < -_sprite_maxfallspeed)
                {
                    this._speed.Y = -_sprite_maxfallspeed;
                }
            }
            else
            {
                this._speed.Y = 0f;
            }
            
            move.Y += this._speed.Y;

            this.pos += move;

            
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
