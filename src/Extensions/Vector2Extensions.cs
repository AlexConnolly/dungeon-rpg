using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LDG.Extensions
{
    public static class Vector2Extensions
    {
        public static Vector2 FromDirection(Direction direction)
        {
            switch(direction)
            {
                case Direction.Up:
                    return new Vector2(0, -1);

                case Direction.Down:
                    return new Vector2(0, 1);

                case Direction.Left:
                    return new Vector2(-1, 0);

                case Direction.Right:
                    return new Vector2(1, 0);
            }

            return new Vector2();
        }
    }
}
