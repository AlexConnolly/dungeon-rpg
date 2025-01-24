using Homestead.Abilities.Crafting;
using Homestead.Abilities.Woodcutting;
using LDG;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homestead.World
{
    public class ChunkGenerator : LDG.GameComponent
    {
        private bool everAdded = false;

        public Chunk Generate(int resolution, WorldManager worldManager, Point location)
        {
            var newChunk = new Chunk(resolution, worldManager, location);

            FloodGrass(newChunk);

            GenerateTrees(newChunk);

            var crafting = GameObject.Scene.AddGameObject();

            var bench = crafting.AddComponent<CraftingBench>();

            newChunk.AddWorldObject(bench, new Point(2, 1));

            return newChunk;
        }

        private void GenerateTrees(Chunk chunk)
        {
            for(int x = 0; x < chunk.Resolution; x++)
            {
                for(int y = 0; y < chunk.Resolution; y++)
                {
                    var generateTree = Random.Shared.Next(0, 10) == 1;

                    if (generateTree)
                    {
                        var newTree = GameObject.Scene.AddGameObject();

                        newTree.DrawPriority = 0;

                        var tree = newTree.AddComponent<TreeComponent>();
                        chunk.AddWorldObject(tree, new Microsoft.Xna.Framework.Point(x, y));
                    }
                }
            }
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
