using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LDG.Components.Camera
{
    public class MainCameraFollow : GameComponent
    {
        public MainCameraFollow(GameObject gameObject) : base(gameObject)
        {
        }

        public override void Update(TimeFrame time)
        {
            LDG.Camera.Position = this.Transform.Position;
        }
    }
}
