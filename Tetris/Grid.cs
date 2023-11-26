using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;

namespace Tetris
{
    public class Grid
    {
        private readonly int[,] grid;
        public int row { get; }
        public int column { get; }
        public int this[int r, int c] 
        {
            get => grid[r, c];
            set => grid[r, c] = value;
        }
        public Grid(int Row, int Column)
        {
            row = Row;
            column = Column;
            grid = new int[Row, Column];
        }
        public bool isIn (int r, int c)
        {
            return r >= 0 && r < row && c >= 0 && c < column; 
        }
        public bool isEm(int r, int c) 
        {
            return isIn(r, c) && grid[r, c] == 0;
        }
        public bool isRFull(int r) 
        {
            for (int c = 0; c < column; c++)
            {
                if (grid[r, c] == 0)
                {
                    return false;
                }
            }
            return true;
        }
        public bool isREm(int r) 
        {
            for (int c = 0; c < column; c++)
            {
                if (grid[r, c] != 0)
                {
                    return false;
                }
            }
            return true;
        }
        private void RClear(int r) 
        {
            for (int c = 0; c < column; c++)
            {
                grid[r, c] = 0;
            }
        }
        private void MDRows(int r, int NRows) 
        {
            for (int c = 0; c < column; c++)
            {
                grid[r + NRows, c] = grid[r, c];
                grid[r, c] = 0;
            }
        }
        public int CFRows() 
        {
            int clear = 0;
            for (int r = row-1; r >= 0; r--)
            {
                if (isRFull(r)) { RClear(r); clear++; }
                else if (clear > 0) { MDRows(r, clear); }
            }
            return clear;
        }
    }
}
