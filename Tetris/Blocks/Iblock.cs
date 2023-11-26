using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris.Blocks
{
    public class Iblock : Block
    {
        private readonly Pos[][] tiles = new Pos[][]
        {
            new Pos[] { new(1,0), new(1,1), new(1,2), new(1,3) },
            new Pos[] { new(0,2), new(1,2), new(2,2), new(3,2) },
            new Pos[] { new(2,0), new(2,1), new(2,2), new(2,3) },
            new Pos[] { new(0,1), new(1,1), new(2,1), new(3,1) }
        };
        public override int id => 1;
        protected override Pos SOSet => new Pos(-1,3);
        protected override Pos[][] Tiles => tiles;
    }
}
