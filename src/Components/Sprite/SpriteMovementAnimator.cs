using LDG.Sprite;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LDG.Components.Sprite
{
    internal class SpriteMovementAnimator : GameComponent
    {
        private readonly Spritesheet _sheet;

        public SpriteMovementAnimator(GameObject gameObject, Spritesheet sheet) : base(gameObject)
        {
            this._sheet = sheet;

            this.LeftFrames = sheet.GetByStartsWith("LEFT");
            this.RightFrames = sheet.GetByStartsWith("RIGHT");
            this.UpFrames = sheet.GetByStartsWith("UP");
            this.DownFrames = sheet.GetByStartsWith("DOWN");
        }

        public float FramesPerSecond { get; set; }

        private List<SpriteFrame> LeftFrames { get; set; }

        private List<SpriteFrame> RightFrames { get; set; }
        private List<SpriteFrame> UpFrames { get; set; }

        private List<SpriteFrame> DownFrames { get; set; }

        private float TimeSinceLastFrame = 0;
        private int currentFrameIndex = 0;

        private int GetNextIndex(int current, List<SpriteFrame> frames)
        {
            int next = current + 1;

            if(next > (frames.Count - 1))
            {
                next = 0;
            }

            return next;
        }

        public override void Update(TimeFrame time)
        {
            var actor = GetComponent<Actor>();
            var spriteRenderer = GetComponent<SpriteRenderer>();

            SpriteFrame frame = null;

            if(!actor.IsMoving)
            {
                TimeSinceLastFrame = 1.0f / FramesPerSecond;
                currentFrameIndex = 0;
                
                // Set the frame to the default frame for the direction
                switch(actor.Direction)
                {
                    case Direction.Up:
                        frame = UpFrames[0];
                        break;

                    case Direction.Down:
                        frame = DownFrames[0];
                        break;

                    case Direction.Left:
                        frame = LeftFrames[0];
                        break;

                    case Direction.Right:
                        frame = RightFrames[0];
                        break;
                }
            } else
            {
                // Character is moving, do we need to change frame?
                TimeSinceLastFrame -= time.Delta;

                // We need to change frame
                List<SpriteFrame> frames = new List<SpriteFrame>();

                switch (actor.Direction)
                {
                    case Direction.Up:
                        frames = UpFrames;
                        break;

                    case Direction.Down:
                        frames = DownFrames;
                        break;

                    case Direction.Left:
                        frames = LeftFrames;
                        break;


                    case Direction.Right:
                        frames = RightFrames;
                        break;
                }

                if (TimeSinceLastFrame <= 0)
                {
                    currentFrameIndex = GetNextIndex(currentFrameIndex, frames);
                    TimeSinceLastFrame = 1.0f / FramesPerSecond;
                }

                frame = frames[currentFrameIndex];
            }

            // Finally set the frame
            spriteRenderer.Frame = frame;
        }
    }
}
