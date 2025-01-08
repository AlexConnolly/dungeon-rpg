using LDG;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homestead.World
{
    public static class ChunkGenerator
    {
        public static Chunk Generate(Chunk adjacent, int resolution, Direction relation)
        {
            var newChunk = new Chunk(resolution);

            FloodGrass(newChunk);

            return newChunk;
        }

        private static void FloodGrass(Chunk chunk)
        {
            for(int x = 0; x < chunk.Floor.Length; x++)
            {
                var random = Random.Shared.Next(0, 10) <= 2;

                chunk.Floor[x] = random ? FloorType.Flower : FloorType.Grass;
            }
        }
    }
}
