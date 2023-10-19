using LDG.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LDG.Components.Character
{
    public class CharacterController : GameComponent
    {
        public CharacterController(GameObject gameObject) : base(gameObject)
        {
        }

        public override void Update(TimeFrame time)
        {
            bool keyPressed = false;

            var actor = GetComponent<Actor>();

            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                actor.Direction = Direction.Up;
                keyPressed = true;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                actor.Direction = Direction.Down;
                keyPressed = true;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                actor.Direction = Direction.Left;
                keyPressed = true;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                actor.Direction = Direction.Right;
                keyPressed = true;
            }

            if (!keyPressed)
            {
                actor.IsMoving = false;
            }
            else
            {
                actor.IsMoving = true;
            }

            using (var group = UIGroup.BeginGroup(new UIGroupSettings()
            {
                Position = new Rectangle(10, 10, 400, 180)
            }))
            {

            }
        }
    }
}
