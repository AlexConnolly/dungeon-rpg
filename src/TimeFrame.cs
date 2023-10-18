using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LDG
{
    public class TimeFrame
    {
        private readonly GameTime _gameTime;

        public TimeFrame(GameTime time)
        {
            this._gameTime = time;
        }

        public float Delta
        {
            get
            {
                return this._gameTime.ElapsedGameTime.Milliseconds * 0.001f;
            }
        }
    }
}
