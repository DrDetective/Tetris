using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    public class GState
    {
        private Block currentBlock;
        public Block CurrentBlock
        {
            get => currentBlock;
            private set
            {
                currentBlock = value;
                currentBlock.Res();
                for (int i = 0; i < 2; i++)
                { currentBlock.Move(1,0);
                    if (!FitBlock())
                    { currentBlock.Move(-1,0); }
                }
            }
        }
        public Grid Grid { get; }
        public Queue BloQueue { get; }
        public bool GO { get; private set; }
        public int Score { get; private set; }
        public Block Hold { get; private set; }
        public bool CanHold { get; private set; }

        public GState()
        {
            Grid = new Grid(22, 10);
            BloQueue = new Queue();
            CurrentBlock = BloQueue.GAU();
            CanHold = true;
        }

        private bool FitBlock()
        {
            foreach (Pos p in CurrentBlock.TilePos())
            {
                if (!Grid.isEm(p.Row, p.Column))
                {
                    return false;
                }
            }
            return true;
        }
        public void HoldBloc()
        {
            if (!CanHold) { return; }
            if (Hold == null)
            {
                Hold = CurrentBlock;
                CurrentBlock = BloQueue.GAU();
            }
            else
            {
                Block tmp = CurrentBlock;
                CurrentBlock = Hold;
                Hold = tmp;
            }
            CanHold = false;
        }
        public void RBCW()
        {
            CurrentBlock.RotCW();
            if (!FitBlock()) { CurrentBlock.RotCCW(); }
        }
        public void RBCCW()
        {
            CurrentBlock.RotCCW();
            if (!FitBlock()) { CurrentBlock.RotCW(); }
        }
        public void MBLeft()
        {
            CurrentBlock.Move(0,-1);
            if (!FitBlock()) { CurrentBlock.Move(0,1); }
        }
        public void MBRight()
        {
            CurrentBlock.Move(0, 1);
            if (!FitBlock()) { CurrentBlock.Move(0,-1); }
        }
        private bool IsGO()
        {
            return !(Grid.isREm(0) && Grid.isREm(1));
        }
        private void PlaceBlock()
        {
            foreach (Pos p in CurrentBlock.TilePos())
            {
                Grid[p.Row, p.Column] = CurrentBlock.id;
            }
            Score += Grid.CFRows();
            if (IsGO()) { GO = true; }
            else { CurrentBlock = BloQueue.GAU(); CanHold = true; }
        }
        public void MBDown()
        {
            CurrentBlock.Move(1, 0);
            if (!FitBlock())
            {
                CurrentBlock.Move(-1, 0);
                PlaceBlock();
            }
        }
        private int TileDropDis(Pos p)
        {
            int drop = 0;
            while (Grid.isEm(p.Row + drop + 1, p.Column))
            { drop++; }
            return drop;
        }
        public int BlockDropDis()
        {
            int drop = Grid.row;
            foreach (Pos p in CurrentBlock.TilePos())
            { drop = Math.Min(drop, TileDropDis(p)); }
            return drop;
        }
        public void DropBlock()
        {
            CurrentBlock.Move(BlockDropDis(), 0);
            PlaceBlock();

        }
    }
}
