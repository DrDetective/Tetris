using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    public abstract class Block
    {
        protected abstract Pos[][] Tiles { get; }
        protected abstract Pos SOSet { get; }
        public abstract int id { get; }
        private int RotState;
        private Pos OfSet;

        public Block()
        {
            OfSet = new Pos(SOSet.Row, SOSet.Column);
        }
        public IEnumerable<Pos> TilePos()
        {
            foreach (Pos p in Tiles[RotState])
            {
                yield return new Pos(p.Row + OfSet.Row, p.Column + OfSet.Column); 
            }
        }
        public void RotCW()
        {
            RotState = (RotState + 1) % Tiles.Length;
        }
        public void RotCCW()
        {
            if (RotState == 0)
            { RotState = Tiles.Length - 1; }
            else { RotState--; }
        }
        public void Move(int row, int column) 
        {
            OfSet.Row += row;
            OfSet.Column += column;
        }
        public void Res()
        {
            RotState = 0;
            OfSet.Row = SOSet.Row;
            OfSet.Column = SOSet.Column;
        }
    }
}
