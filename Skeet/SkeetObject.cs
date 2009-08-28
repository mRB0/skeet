using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

    class SkeetObject
    {
        protected List<string> __msgbox;
        protected Mutex __mut_msgbox;
        public Thread throbj;
        protected SkeetGame _game;

        public SkeetObject(SkeetGame game)
        {
            this.__msgbox = new List<string>();
            __mut_msgbox = new Mutex();

            this._game = game;
        }

        public void mb_send(string msg)
        {
            __mut_msgbox.WaitOne();
            __msgbox.Add(msg);
            __mut_msgbox.ReleaseMutex();
        }

        protected string mb_rcvone()
        {
            string rcv;
            __mut_msgbox.WaitOne();
            if (__msgbox.Count > 0)
            {
                rcv = __msgbox[0];
                __msgbox.RemoveAt(0);
            }
            else
            {
                rcv = null;
            }
            __mut_msgbox.ReleaseMutex();

            return rcv;
        }

        protected List<string> mb_rcvall()
        {
            List<string> rcv;

            __mut_msgbox.WaitOne();
            rcv = __msgbox;
            __msgbox = new List<string>();
            __mut_msgbox.ReleaseMutex();

            return rcv;
        }

        public virtual void start()
        {
            throbj = new Thread(new ThreadStart(this.run));
            throbj.Start();
        }

        protected virtual void run()
        {
            // override me!
        }

    }
}
