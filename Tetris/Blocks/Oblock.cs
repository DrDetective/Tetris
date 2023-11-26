using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris.Blocks
{
    public class Oblock : Block
    {
        private readonly Pos[][] tiles = new Pos[][]
        {
            new Pos[] {new(0,0), new(0,1), new(1,0), new(1,1) }
        };
        public override int id => 4;
        protected override Pos SOSet => new Pos(0,4);
        protected override Pos[][] Tiles => tiles;
    }
}
