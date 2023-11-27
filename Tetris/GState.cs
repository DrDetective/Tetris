﻿using System;
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
        public Grid GGrid { get; }
        public Queue BloQueue { get; }
        public bool GO { get; private set; }

        public GState()
        {
            GGrid = new Grid(22, 10);
            BloQueue = new Queue();
            CurrentBlock = BloQueue.GAU();
        }

        private bool FitBlock()
        {
            foreach (Pos p in CurrentBlock.TilePos())
            {
                if (!GGrid.isEm(p.Row, p.Column))
                {
                    return false;
                }
            }
            return true;
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
            CurrentBlock.Move(1, 0);
            if (!FitBlock()) { CurrentBlock.Move(-1,0); }
        }
        private bool IsGO()
        {
            return !(GGrid.isREm(0) && GGrid.isREm(1));
        }
        private void PlaceBlock()
        {
            foreach (Pos p in CurrentBlock.TilePos())
            {
                GGrid[p.Row, p.Column] = CurrentBlock.id;
            }
            GGrid.CFRows();
            if (IsGO()) { GO = true; }
            else { CurrentBlock = BloQueue.GAU(); }
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
    }
}
