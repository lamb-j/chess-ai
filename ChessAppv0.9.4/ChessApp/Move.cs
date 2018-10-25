using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessApp
{
    public class Move
    {
        internal char player, captured; 
        internal int rowStart, colStart, rowEnd, colEnd;

        public Move(char player, int rowStart, int colStart, int rowEnd, int colEnd, char captured)
        {
            this.player = player;
            this.rowStart = rowStart;
            this.colStart = colStart;
            this.rowEnd = rowEnd;
            this.colEnd = colEnd;
            this.captured = captured;
        }


        public bool equalTo(Move m) {

            if (player == m.player && rowStart == m.rowStart && colStart == m.colStart && rowEnd == m.rowEnd && colEnd == m.colEnd)
            {
                return true;
            }
            return false;
        }


        public String toString()
        {
            return player + " from " + "(" + rowStart + ", " + colStart + ")" + " to "  + "(" + rowEnd + ", " + colEnd+ ")" + " piece taken " + captured;
        }

    }
}
