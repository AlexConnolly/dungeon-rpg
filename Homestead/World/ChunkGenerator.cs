using Homestead.Abilities.Woodcutting;
using LDG;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homestead.World
{
    public class ChunkGenerator : GameComponent
    {
        public Chunk Generate(Chunk adjacent, int resolution, Direction relation)
        {
            var newChunk = new Chunk(resolution);

            FloodGrass(newChunk);

            var newTree = GameObject.Scene.AddGameObject();

            newTree.DrawPriority = -1;

            var tree = newTree.AddComponent<TreeComponent>();

            newChunk.AddWorldObject(tree, new Microsoft.Xna.Framework.Point(0, 0));

            return newChunk;
        }

        private static void FloodGrass(Chunk chunk)
        {
            for(int x = 0; x < chunk.Floor.Length; x++)
            {
                var random = Random.Shared.Next(0, 10);

                if(random == 5)
                {
                    chunk.Floor[x] = FloorType.GrassRock;
                } else if (random <= 2)
                {
                    chunk.Floor[x] = FloorType.Flower;
                } else
                {
                    chunk.Floor[x] = FloorType.Grass;
                }
            }
        }
    }
}
