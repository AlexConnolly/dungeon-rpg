using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homestead.World
{
    public class ChunkView
    {
        public Chunk Left;
        public Chunk Right;
        public Chunk Center;

        public Chunk TopLeft;
        public Chunk TopRight;
        public Chunk TopCenter;

        public Chunk BottomLeft;
        public Chunk BottomRight;
        public Chunk BottomCenter;
    }
}
