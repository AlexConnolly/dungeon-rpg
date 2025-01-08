using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homestead.World
{
    public class Chunk
    {
        public Chunk(int resolution)
        {
            Floor = new FloorType[resolution * resolution];
        }

        public FloorType[] Floor;
    }
}
