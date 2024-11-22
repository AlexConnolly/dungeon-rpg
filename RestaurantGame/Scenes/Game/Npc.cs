using LDG;
using LDG.Components.Actor;
using LDG.Components.Pathfinding;
using LDG.Components.Sprite;
using LDG.Sprite;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using GameComponent = LDG.GameComponent;

namespace RestaurantGame.Scenes.Game
{
    public class Npc : GameComponent
    {
        private ActorComponent _actor;
        private Texture2D _texture;

        private float chanceOfPurchase = Random.Shared.Next(20, 70);
        private bool hasPurchased = false;
        private bool willPurchase = false;

        private float maximumWaitTime = Random.Shared.Next(5, 20);

        private PathfinderComponent _pathfinder;

        private Vector2 previousPosition = Vector2.Zero;

        private Microsoft.Xna.Framework.Color[] Colors = new Color[] { Color.White, Color.Blue, Color.Red, Color.Green, Color.Yellow };
        private static Color CreateTint(Color color, float intensity)
        {
            intensity = MathHelper.Clamp(intensity, 0f, 1f);

            // Blend with white
            int r = (int)(color.R * intensity + 255 * (1 - intensity));
            int g = (int)(color.G * intensity + 255 * (1 - intensity));
            int b = (int)(color.B * intensity + 255 * (1 - intensity));
            int a = color.A; // Preserve alpha

            return new Color(r, g, b, a);
        }

        public override void Initialize()
        {
            _actor = GameObject.AddComponent<ActorComponent>();
            _actor.Size = new Vector2(24, 24);

            _actor.SpriteAnimator = GameObject.AddComponent<SpriteMovementAnimator>();
            var renderer = _actor.GameObject.AddComponent<SpriteRenderer>();

            renderer.Color = CreateTint(Colors[Random.Shared.Next(0, Colors.Length)], 0.3f);

            _actor.SpriteAnimator.Sheet = Spritesheet.FromAnimatedSheet(_texture, false, 0, 0, 0, 1, new Microsoft.Xna.Framework.Point(24, 24));

            if (chanceOfPurchase >= 60)
                willPurchase = true;

            _pathfinder = GameObject.AddComponent<PathfinderComponent>();

            if (willPurchase)
                _pathfinder.SetDestination(new Vector2(30, 30));
            else
                _pathfinder.SetDestination(new Vector2(this.Transform.Position.X, this.Transform.Position.Y - 300));
        }

        public override void Load(ContentManager contentManager, GraphicsDevice graphicsDevice)
        {
            _texture = Texture2D.FromFile(graphicsDevice, "Scenes/Game/Artwork/Npc.png");
        }

        public override void Update(TimeFrame time)
        {
            // We haven't moved
            if(Transform.Position == previousPosition)
            {
                // Here we need to determine if we are actually purchasing or not
                maximumWaitTime -= time.Delta;

                if(maximumWaitTime <= 0)
                {
                    // We abandon if we haven't already made an order
                    if(willPurchase)
                    {
                        willPurchase = false;
                        chanceOfPurchase = 0;

                        _pathfinder.SetDestination(new Vector2(0, 0));
                    }
                }
            }

            previousPosition = Transform.Position;
        }
    }
}
