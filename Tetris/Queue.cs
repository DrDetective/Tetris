using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tetris.Blocks;

namespace Tetris
{
    public class Queue
    {
        private readonly Block[] blocks = new Block[]
        {
            new Iblock(),
            new Jblock(),
            new Lblock(),
            new Oblock(),
            new Sblock(),
            new Tblock(),
            new Zblock()
        };
        private readonly Random rng = new Random();
        public Block NBlock { get; private set; }
        public Queue()
        {
            NBlock = RNGBlock();
        }
        private Block RNGBlock()
        {
            return blocks[rng.Next(blocks.Length)];
        }
        public Block GAU()
        {
            Block bloc = NBlock;
            do { NBlock = RNGBlock(); }
            while (bloc.id == NBlock.id);
            return bloc;
        }
    }
}
