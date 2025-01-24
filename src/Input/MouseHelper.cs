using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LDG.Input
{
    public static class MouseHelper
    {
        public enum ScrollDirection
        {
            Up,
            Down,
            None
        }

        private static MouseState previousState;
        private static MouseState currentState;

        public static ScrollDirection GetScrollDirection()
        {
            if(previousState.ScrollWheelValue < currentState.ScrollWheelValue)
            {
                return ScrollDirection.Up;
            } else if(previousState.ScrollWheelValue > currentState.ScrollWheelValue)
            {
                return ScrollDirection.Down;
            } else
            {
                return ScrollDirection.None;
            }
        }

        public static void Update(TimeFrame time)
        {
            previousState = currentState;
            currentState = Mouse.GetState();
        }
    }
}
