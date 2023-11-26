using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris.Blocks
{
    public class Tblock : Block
    {
        private readonly Pos[][] tiles = new Pos[][]
        {
            new Pos[] { new(0,1), new(1,0), new(1,1), new(1,2) },
            new Pos[] { new(0,1), new(1,1), new(1,2), new(2,1) },
            new Pos[] { new(1,0), new(1,1), new(1,2), new(2,1) },
            new Pos[] { new(0,1), new(1,0), new(1,1), new(2,1) }
        };
        public override int id => 6;
        protected override Pos SOSet => new Pos(0,3);
        protected override Pos[][] Tiles => tiles;
    }
}
