using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace LDG.Input
{
    public static class KeyboardHelper
    {
        private static List<Keys> keysDown = new List<Keys>();
        private static List<Keys> keysPressed = new List<Keys>();

        public static bool WasKeyPressed(Keys key)
        {
            return KeyboardHelper.keysPressed.Contains(key);
        }
        
        public static void Update(TimeFrame time)
        {
            // A key press will be reset every frame
            KeyboardHelper.keysPressed = new List<Keys>();

            var keyboardState = Microsoft.Xna.Framework.Input.Keyboard.GetState();

            var keysPressed = keyboardState.GetPressedKeys().ToList();

            // Add all the new keys to keys down
            foreach(var key in keysPressed)
            {
                if(!keysDown.Contains(key))
                {
                    keysDown.Add(key);
                }
            }

            List<Keys> toRemove = new List<Keys>();

            // Any keys that were released should be pressed
            foreach(var key in keysDown)
            {
                if(!keysPressed.Contains(key))
                {
                    KeyboardHelper.keysPressed.Add(key);
                    toRemove.Add(key);
                }
            }

            foreach(var key in toRemove)
            {
                KeyboardHelper.keysDown.Remove(key);
            }
        }
    }
}
