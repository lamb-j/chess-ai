using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessApp
{
    class MoveGeneration
    {
        internal char[,] board = new char[8, 8];

        public List<Move> gen_wmv(int r, int c, GameState s)
        {
            board = s.board;
            switch (board[r, c])
            {
                case 'r': return wmv_rook(r, c);
                case 'n': return wmv_knight(r, c);
                case 'b': return wmv_bishop(r, c);
                case 'q': return wmv_queen(r, c);
                case 'k': return wmv_king(r, c);
                case 'p': return wmv_pawn(r, c);
                default: return new List<Move>();
                //default: return null;
            }

        } // exit gen_bmv


        /////////////////////////////////////
        // functions dealing with the white pieces

        // copy/paste once you finish wmoves

        public List<Move> gen_white(GameState s)
        {
            //Console.WriteLine("got into generate method white");
            List<Move> ret = new List<Move>();
            for (int i = 0; i <= 7; i++)
            {
                for (int j = 0; j <= 7; j++)
                {
                    if (Char.IsLower(s.board[i, j]))
                    {
                        ret.AddRange(gen_wmv(i, j, s));

                    }
                }
            }
            return ret;
        }

        // Function to generate all of the moves possible for a black piece
        // from the given square

        public List<Move> gen_bmv(int r, int c, GameState s)
        {
            board = s.board;
            switch (board[r, c])
            {
                case 'R': return bmv_rook(r, c);
                case 'N': return bmv_knight(r, c);
                case 'B': return bmv_bishop(r, c);
                case 'Q': return bmv_queen(r, c);
                case 'K': return bmv_king(r, c);
                case 'P': return bmv_pawn(r, c);
                default: return new List<Move>();
            }

        } // exit gen_bmv


        /////////////////////////////////////
        // functions dealing with the white pieces

        // copy/paste once you finish bmoves

        public List<Move> gen_black(GameState s)
        {
            //Console.WriteLine("got into generate method black");
            List<Move> ret = new List<Move>();
            for (int i = 0; i <= 7; i++)
            {
                for (int j = 0; j <= 7; j++)
                {
                    if (Char.IsUpper(s.board[i, j]))
                    {
                        ret.AddRange(gen_bmv(i, j, s));


                    }
                }
            }
            return ret;
        }


        public List<Move> bmv_rook(int r, int c)
        {
            List<Move> rv = new List<Move>();
            char tmp, tmp2;

            int tr, tc;

            tr = r; tc = c;
            tmp2 = board[r,c];

            // north

            // first create moves for the blank spaces
            while (tr > 0 && board[tr - 1, c] == '-')
            {
                tr--;

                tmp = board[tr, tc];
                board[tr, tc] = 'R';
                board[r, c] = '-';

                if (bking_safe() == 1) rv.Add(new Move('B', r, c, tr, tc, tmp));

                board[tr, tc] = tmp;
                board[r, c] = tmp2;

            }

            // Now we have hit the edge of the board or another piece
            // If it's an enemy piece, capture and record move
            if (tr > 0 && Char.IsLower(board[tr - 1, c]))
            {
                tr--;

                tmp = board[tr, tc];
                board[tr, tc] = 'R';
                board[r, c] = '-';

                if (bking_safe() == 1) rv.Add(new Move('B', r, c, tr, tc, tmp));

                board[tr, tc] = tmp;
                board[r, c] = tmp2;
            }

            // south
            tr = r; tc = c;

            while (tr < 7 && board[tr + 1, c] == '-')
            {
                tr++;

                tmp = board[tr, tc];
                board[tr, tc] = 'R';
                board[r, c] = '-';

                if (bking_safe() == 1) rv.Add(new Move('B', r, c, tr, tc, tmp));

                board[tr, tc] = tmp;
                board[r, c] = tmp2;
            }

            if (tr < 7 && Char.IsLower(board[tr + 1, c]))
            {
                tr++;

                tmp = board[tr, tc];
                board[tr, tc] = 'R';
                board[r, c] = '-';

                if (bking_safe() == 1) rv.Add(new Move('B', r, c, tr, tc, tmp));

                board[tr, tc] = tmp;
                board[r, c] = tmp2;
            }

            // west
            tr = r; tc = c;

            while (tc > 0 && board[r, tc - 1] == '-')
            {
                tc--;

                tmp = board[tr, tc];
                board[tr, tc] = 'R';
                board[r, c] = '-';

                if (bking_safe() == 1) rv.Add(new Move('B', r, c, tr, tc, tmp));

                board[tr, tc] = tmp;
                board[r, c] = tmp2;
            }

            if (tc > 0 && Char.IsLower(board[r, tc - 1]))
            {
                tc--;

                tmp = board[tr, tc];
                board[tr, tc] = 'R';
                board[r, c] = '-';

                if (bking_safe() == 1) rv.Add(new Move('B', r, c, tr, tc, tmp));

                board[tr, tc] = tmp;
                board[r, c] = tmp2;
            }

            // east
            tr = r; tc = c;

            while (tc < 7 && board[r, tc + 1] == '-')
            {
                tc++;

                tmp = board[tr, tc];
                board[tr, tc] = 'R';
                board[r, c] = '-';

                if (bking_safe() == 1) rv.Add(new Move('B', r, c, tr, tc, tmp));

                board[tr, tc] = tmp;
                board[r, c] = tmp2;
            }

            if (tc < 7 && Char.IsLower(board[r, tc + 1]))
            {
                tc++;

                tmp = board[tr, tc];
                board[tr, tc] = 'R';
                board[r, c] = '-';

                if (bking_safe() == 1) rv.Add(new Move('B', r, c, tr, tc, tmp));

                board[tr, tc] = tmp;
                board[r, c] = tmp2;
            }

            return rv;
        }

        public List<Move> bmv_knight(int r, int c)
        {
            List<Move> rv = new List<Move>();
            char tmp;

            /*  -1-2-
                8---3
                --N--
                7---4
                -6-5-  */

            // 1
            // If the move is on the board and empty or enemy,
            if (r - 2 >= 0 && c - 1 >= 0 && (board[r - 2, c - 1] == '-'
                  || Char.IsLower(board[r - 2, c - 1])))
            {

                tmp = board[r - 2, c - 1];
                board[r - 2, c - 1] = 'N';
                board[r, c] = '-';

                if (bking_safe() == 1) rv.Add(new Move('B', r, c, r - 2, c - 1, tmp));

                board[r - 2, c - 1] = tmp;
                board[r, c] = 'N';
            }

            // System.//Console.Write("K2!\n");
            // 2
            if (r - 2 >= 0 && c + 1 < 8 && (board[r - 2, c + 1] == '-'
                  || Char.IsLower(board[r - 2, c + 1])))
            {

                tmp = board[r - 2, c + 1];
                board[r - 2, c + 1] = 'N';
                board[r, c] = '-';

                if (bking_safe() == 1) rv.Add(new Move('B', r, c, r - 2, c + 1, tmp));

                board[r - 2, c + 1] = tmp;
                board[r, c] = 'N';

            }

            // System.//Console.Write("K3!\n");
            // 3
            if (r - 1 >= 0 && c + 2 < 8 && (board[r - 1, c + 2] == '-'
                  || Char.IsLower(board[r - 1, c + 2])))
            {
                tmp = board[r - 1, c + 2];
                board[r - 1, c + 2] = 'N';
                board[r, c] = '-';

                if (bking_safe() == 1) rv.Add(new Move('B', r, c, r - 1, c + 2, tmp));

                board[r - 1, c + 2] = tmp;
                board[r, c] = 'N';
            }

            // System.//Console.Write("K4!\n");
            // 4
            if (r + 1 < 8 && c + 2 < 8 && (board[r + 1, c + 2] == '-'
                  || Char.IsLower(board[r + 1, c + 2])))
            {

                tmp = board[r + 1, c + 2];
                board[r + 1, c + 2] = 'N';
                board[r, c] = '-';

                if (bking_safe() == 1) rv.Add(new Move('B', r, c, r + 1, c + 2, tmp));

                board[r + 1, c + 2] = tmp;
                board[r, c] = 'N';

            }

            // System.//Console.Write("K5!\n");
            // 5
            if (r + 2 < 8 && c + 1 < 8 && (board[r + 2, c + 1] == '-'
                  || Char.IsLower(board[r + 2, c + 1])))
            {

                tmp = board[r + 2, c + 1];
                board[r + 2, c + 1] = 'N';
                board[r, c] = '-';

                if (bking_safe() == 1) rv.Add(new Move('B', r, c, r + 2, c + 1, tmp));

                board[r + 2, c + 1] = tmp;
                board[r, c] = 'N';

            }

            // System.//Console.Write("K6!\n");
            // 6
            if (r + 2 < 8 && c - 1 >= 0 && (board[r + 2, c - 1] == '-'
                  || Char.IsLower(board[r + 2, c - 1])))
            {

                tmp = board[r + 2, c - 1];
                board[r + 2, c - 1] = 'N';
                board[r, c] = '-';

                if (bking_safe() == 1) rv.Add(new Move('B', r, c, r + 2, c - 1, tmp));

                board[r + 2, c - 1] = tmp;
                board[r, c] = 'N';

            }

            // System.//Console.Write("K7!\n");
            // 7
            if (r + 1 < 8 && c - 2 >= 0 && (board[r + 1, c - 2] == '-'
                  || Char.IsLower(board[r + 1, c - 2])))
            {

                tmp = board[r + 1, c - 2];
                board[r + 1, c - 2] = 'N';
                board[r, c] = '-';

                if (bking_safe() == 1) rv.Add(new Move('B', r, c, r + 1, c - 2, tmp));

                board[r + 1, c - 2] = tmp;
                board[r, c] = 'N';

            }

            // System.//Console.Write("K8!\n");
            // 8
            if (r - 1 >= 0 && c - 2 >= 0 && (board[r - 1, c - 2] == '-'
                  || Char.IsLower(board[r - 1, c - 2])))
            {

                tmp = board[r - 1, c - 2];
                board[r - 1, c - 2] = 'N';
                board[r, c] = '-';

                if (bking_safe() == 1) rv.Add(new Move('B', r, c, r - 1, c - 2, tmp));

                board[r - 1, c - 2] = tmp;
                board[r, c] = 'N';

            }


            return rv;
        }

        public List<Move> bmv_bishop(int r, int c)
        {
            
            List<Move> rv = new List<Move>();
            char tmp, tmp2;
            tmp2 = board[r, c];
            int tr, tc;

            // north west
            tr = r; tc = c;

            // First record moves to empty spaces
            while (tr > 0 && tc > 0 && board[tr - 1, tc - 1] == '-')
            {

                tr--; tc--;
                tmp = board[tr, tc];
                board[tr, tc] = 'B';
                board[r, c] = '-';

                if (bking_safe() == 1) rv.Add(new Move('B', r, c, tr, tc, tmp));

                board[tr, tc] = tmp;
                board[r, c] = tmp2;

            }

            // Now we have hit the edge of the board or another piece
            // If it's an enemy piece, capture and record move
            if (tr > 0 && tc > 0 && Char.IsLower(board[tr - 1, tc - 1]))
            {

                tr--; tc--;
                tmp = board[tr, tc];
                board[tr, tc] = 'B';
                board[r, c] = '-';

                if (bking_safe() == 1) rv.Add(new Move('B', r, c, tr, tc, tmp));

                board[tr, tc] = tmp;
                board[r, c] = tmp2;

            }

            // System.//Console.Write("B2?\n");
            // north east
            tr = r; tc = c;

            while (tr > 0 && tc < 7 && board[tr - 1, tc + 1] == '-')
            {
                tr--; tc++;
                tmp = board[tr, tc];
                board[tr, tc] = 'B';
                board[r, c] = '-';

                if (bking_safe() == 1) rv.Add(new Move('B', r, c, tr, tc, tmp));

                board[tr, tc] = tmp;
                board[r, c] = tmp2;

            }

            if (tr > 0 && tc < 7 && Char.IsLower(board[tr - 1, tc + 1]))
            {

                tr--; tc++;
                tmp = board[tr, tc];
                board[tr, tc] = 'B';
                board[r, c] = '-';

                if (bking_safe() == 1) rv.Add(new Move('B', r, c, tr, tc, tmp));

                board[tr, tc] = tmp;
                board[r, c] = tmp2;

            }

            // System.//Console.Write("B3?\n");
            // south east
            tr = r; tc = c;

            while (tr < 7 && tc < 7 && board[tr + 1, tc + 1] == '-')
            {

                tr++; tc++;
                tmp = board[tr, tc];
                board[tr, tc] = 'B';
                board[r, c] = '-';

                if (bking_safe() == 1) rv.Add(new Move('B', r, c, tr, tc, tmp));

                board[tr, tc] = tmp;
                board[r, c] = tmp2;

            }

            if (tr < 7 && tc < 7 && Char.IsLower(board[tr + 1, tc + 1]))
            {

                tr++; tc++;
                tmp = board[tr, tc];
                board[tr, tc] = 'B';
                board[r, c] = '-';

                if (bking_safe() == 1) rv.Add(new Move('B', r, c, tr, tc, tmp));

                board[tr, tc] = tmp;
                board[r, c] = tmp2;

            }

            // System.//Console.Write("B4?\n");
            // south west
            tr = r; tc = c;

            while (tr < 7 && tc > 0 && (board[tr + 1, tc - 1] == '-'))
            {

                tr++; tc--;
                tmp = board[tr, tc];
                board[tr, tc] = 'B';
                board[r, c] = '-';

                if (bking_safe() == 1) rv.Add(new Move('B', r, c, tr, tc, tmp));

                board[tr, tc] = tmp;
                board[r, c] = tmp2;

            }

            if (tr < 7 && tc > 0 && Char.IsLower(board[tr + 1, tc - 1]))
            {

                tr++; tc--;
                tmp = board[tr, tc];
                board[tr, tc] = 'B';
                board[r, c] = '-';

                if (bking_safe() == 1) rv.Add(new Move('B', r, c, tr, tc, tmp));

                board[tr, tc] = tmp;
                board[r, c] = tmp2;

            }

            // System.//Console.Write("B5?\n");

            return rv;
        }

        public List<Move> bmv_queen(int r, int c)
        {

            List<Move> rv = new List<Move>();
            rv.AddRange(bmv_rook(r, c));
            rv.AddRange(bmv_bishop(r, c));

            return rv;
        }

        public List<Move> bmv_king(int r, int c)
        {
            List<Move> rv = new List<Move>();
            char tmp;

            // north
            if (r > 0 && (board[r - 1, c] == '-' || Char.IsLower(board[r - 1, c])))
            {

                tmp = board[r - 1, c];
                board[r - 1, c] = 'K';
                board[r, c] = '-';

                if (bking_safe() == 1) rv.Add(new Move('B', r, c, r - 1, c, tmp));

                board[r - 1, c] = tmp;
                board[r, c] = 'K';

            }

            // east
            if (c < 7 && (board[r, c + 1] == '-' || Char.IsLower(board[r, c + 1])))
            {

                tmp = board[r, c + 1];
                board[r, c + 1] = 'K';
                board[r, c] = '-';

                if (bking_safe() == 1) rv.Add(new Move('B', r, c, r, c + 1, tmp));

                board[r, c + 1] = tmp;
                board[r, c] = 'K';

            }

            // south
            if (r < 7 && (board[r + 1, c] == '-' || Char.IsLower(board[r + 1, c])))
            {

                tmp = board[r + 1, c];
                board[r + 1, c] = 'K';
                board[r, c] = '-';

                if (bking_safe() == 1) rv.Add(new Move('B', r, c, r + 1, c, tmp));

                board[r + 1, c] = tmp;
                board[r, c] = 'K';

            }

            // west
            if (c > 0 && (board[r, c - 1] == '-' || Char.IsLower(board[r, c - 1])))
            {

                tmp = board[r, c - 1];
                board[r, c - 1] = 'K';
                board[r, c] = '-';

                if (bking_safe() == 1) rv.Add(new Move('B', r, c, r, c - 1, tmp));

                board[r, c - 1] = tmp;
                board[r, c] = 'K';

            }

            // north east
            if (r > 0 && c < 7 && (board[r - 1, c + 1] == '-'
                  || Char.IsLower(board[r - 1, c + 1])))
            {

                tmp = board[r - 1, c + 1];
                board[r - 1, c + 1] = 'K';
                board[r, c] = '-';

                if (bking_safe() == 1) rv.Add(new Move('B', r, c, r - 1, c + 1, tmp));

                board[r - 1, c + 1] = tmp;
                board[r, c] = 'K';

            }

            // south east
            if (r < 7 && c < 7 && (board[r + 1, c + 1] == '-'
                  || Char.IsLower(board[r + 1, c + 1])))
            {

                tmp = board[r + 1, c + 1];
                board[r + 1, c + 1] = 'K';
                board[r, c] = '-';

                if (bking_safe() == 1) rv.Add(new Move('B', r, c, r + 1, c + 1, tmp));

                board[r + 1, c + 1] = tmp;
                board[r, c] = 'K';
            }

            // south west
            if (r < 7 && c > 0 && (board[r + 1, c - 1] == '-'
                  || Char.IsLower(board[r + 1, c - 1])))
            {

                tmp = board[r + 1, c - 1];
                board[r + 1, c - 1] = 'K';
                board[r, c] = '-';

                if (bking_safe() == 1) rv.Add(new Move('B', r, c, r + 1, c - 1, tmp));

                board[r + 1, c - 1] = tmp;
                board[r, c] = 'K';

            }

            // north west
            if (r > 0 && c > 0 && (board[r - 1, c - 1] == '-'
                  || Char.IsLower(board[r - 1, c - 1])))
            {
                tmp = board[r - 1, c - 1];
                board[r - 1, c - 1] = 'K';
                board[r, c] = '-';

                if (bking_safe() == 1) rv.Add(new Move('B', r, c, r - 1, c - 1, tmp));

                board[r - 1, c - 1] = tmp;
                board[r, c] = 'K';
            }


            return rv;
        }

        public List<Move> bmv_pawn(int r, int c)
        {
            List<Move> rv = new List<Move>();
            char tmp;

            // south 1
            if (r < 7 && board[r + 1, c] == '-')
            {

                tmp = board[r + 1, c];
                board[r + 1, c] = 'P';
                board[r, c] = '-';

                if (bking_safe() == 1) rv.Add(new Move('B', r, c, r + 1, c, tmp));

                board[r + 1, c] = tmp;
                board[r, c] = 'P';

            }

            // south 2
            if (r == 1 && board[r + 1, c] == '-' && board[r + 2, c] == '-')
            {

                tmp = board[r + 2, c];
                board[r + 2, c] = 'P';
                board[r, c] = '-';

                if (bking_safe() == 1) rv.Add(new Move('B', r, c, r + 2, c, tmp));

                board[r + 2, c] = tmp;
                board[r, c] = 'P';

            }

            // capture left
            if (c > 0 && r < 7 && Char.IsLower(board[r + 1, c - 1]))
            {

                tmp = board[r + 1, c - 1];
                board[r + 1, c - 1] = 'P';
                board[r, c] = '-';

                if (bking_safe() == 1) rv.Add(new Move('B', r, c, r + 1, c - 1, tmp));

                board[r + 1, c - 1] = tmp;
                board[r, c] = 'P';

            }

            // capture right
            if (c < 7 && r < 7 && Char.IsLower(board[r + 1, c + 1]))
            {

                tmp = board[r + 1, c + 1];
                board[r + 1, c + 1] = 'P';
                board[r, c] = '-';

                if (bking_safe() == 1) rv.Add(new Move('B', r, c, r + 1, c + 1, tmp));

                board[r + 1, c + 1] = tmp;
                board[r, c] = 'P';
            }
            return rv;
        }

        public int bking_safe()
        {
            int r = 0, c = 0;
            int tr, tc;
            int i, j;

            // find the black king
            for (i = 0; i < 7; i++)
            {
                for (j = 0; j < 7; j++)
                {
                    if (board[i, j] == 'K')
                    {
                        r = i;
                        c = j;
                    }
                }
            }

            // check for kings
            if (r - 1 >= 0 && c - 1 >= 0 && board[r - 1, c - 1] == 'k') return 0; // ne 
            if (r - 1 >= 0 && c + 1 <= 7 && board[r - 1, c + 1] == 'k') return 0; // nw
            if (r + 1 <= 7 && c - 1 >= 0 && board[r + 1, c - 1] == 'k') return 0; // se
            if (r + 1 <= 7 && c + 1 <= 7 && board[r + 1, c + 1] == 'k') return 0; // sw

            if (r + 1 <= 7 && board[r + 1, c] == 'k') return 0; // n
            if (r - 1 >= 0 && board[r - 1, c] == 'k') return 0; // s
            if (c + 1 <= 7 && board[r, c + 1] == 'k') return 0; // e
            if (c - 1 >= 0 && board[r, c - 1] == 'k') return 0; // w

            // check for knights
            if (r - 2 >= 0 && c - 1 >= 0 && board[r - 2, c - 1] == 'n') return 0; // 1
            if (r - 2 >= 0 && c + 1 <= 7 && board[r - 2, c + 1] == 'n') return 0; // 2
            if (r - 1 >= 0 && c + 2 <= 7 && board[r - 1, c + 2] == 'n') return 0; // 3
            if (r + 1 <= 7 && c + 2 <= 7 && board[r + 1, c + 2] == 'n') return 0; // 4
            if (r + 2 <= 7 && c + 1 <= 7 && board[r + 2, c + 1] == 'n') return 0; // 5
            if (r + 2 <= 7 && c - 1 >= 0 && board[r + 2, c - 1] == 'n') return 0; // 6
            if (r + 1 <= 7 && c - 2 >= 0 && board[r + 1, c - 2] == 'n') return 0; // 7
            if (r - 1 >= 0 && c - 2 >= 0 && board[r - 1, c - 2] == 'n') return 0; // 8

            // check for pawns
            if (r + 1 <= 7 && c + 1 <= 7 && board[r + 1, c + 1] == 'p') return 0;
            if (r + 1 <= 7 && c - 1 >= 0 && board[r + 1, c - 1] == 'p') return 0;

            // check for rooks and queens

            // north
            tr = r; tc = c;

            while (tr > 0 && board[tr - 1, c] == '-') tr--;
            if (tr > 0 && (board[tr - 1, c] == 'r' || board[tr - 1, c] == 'q'))
            {
                return 0;
            }

            // south
            tr = r; tc = c;

            while (tr < 7 && board[tr + 1, c] == '-') tr++;
            if (tr < 7 && (board[tr + 1, c] == 'r' || board[tr + 1, c] == 'q'))
            {
                return 0;
            }

            // west
            tr = r; tc = c;

            while (tc > 0 && board[r, tc - 1] == '-') tc--;
            if (tc > 0 && (board[r, tc - 1] == 'r' || board[r, tc - 1] == 'q'))
            {
                return 0;
            }

            // east
            tr = r; tc = c;

            while (tc < 7 && board[r, tc + 1] == '-') tc++;
            if (tc < 7 && (board[r, tc + 1] == 'r' || board[r, tc + 1] == 'q'))
            {
                return 0;
            }

            // check for bishops and queens

            // north west
            tr = r; tc = c;

            while (tr > 0 && tc > 0 && board[tr - 1, tc - 1] == '-')
            {
                tr--; tc--;
            }
            if (tr > 0 && tc > 0 &&
                (board[tr - 1, tc - 1] == 'b' || board[tr - 1, tc - 1] == 'q')) return 0;

            // north east
            tr = r; tc = c;

            while (tr > 0 && tc < 7 && board[tr - 1, tc + 1] == '-')
            {
                tr--; tc++;
            }
            if (tr > 0 && tc < 7 &&
                (board[tr - 1, tc + 1] == 'b' || board[tr - 1, tc + 1] == 'q')) return 0;

            // south east
            tr = r; tc = c;

            while (tr < 7 && tc < 7 && board[tr + 1, tc + 1] == '-')
            {
                tr++; tc++;
            }
            if (tr < 7 && tc < 7 &&
                (board[tr + 1, tc + 1] == 'b' || board[tr + 1, tc + 1] == 'q')) return 0;

            // south west
            tr = r; tc = c;

            while (tr < 7 && tc > 0 && board[tr + 1, tc - 1] == '-')
            {
                tr++; tc--;
            }
            if (tr < 7 && tc > 0 &&
                (board[tr + 1, tc - 1] == 'b' || board[tr + 1, tc - 1] == 'q')) return 0;


            return 1;
        }

        public List<Move> wmv_rook(int r, int c)
        {
            List<Move> rv = new List<Move>();
            char tmp, tmp2;
            tmp2 = board[r, c];

            int tr, tc;

            tr = r; tc = c;

            // north

            // first create moves for the blank spaces
            while (tr > 0 && board[tr - 1, c] == '-')
            {
                tr--;

                tmp = board[tr, tc];
                board[tr, tc] = 'r';
                board[r, c] = '-';

                if (wking_safe() == 1) rv.Add(new Move('W', r, c, tr, tc, tmp));

                board[tr, tc] = tmp;
                board[r, c] = tmp2;

            }

            // Now we have hit the edge of the board or another piece
            // If it's an enemy piece, capture and record move
            if (tr > 0 && Char.IsUpper(board[tr - 1, c]))
            {
                tr--;

                tmp = board[tr, tc];
                board[tr, tc] = 'r';
                board[r, c] = '-';

                if (wking_safe() == 1) rv.Add(new Move('W', r, c, tr, tc, tmp));

                board[tr, tc] = tmp;
                board[r, c] = tmp2;
            }

            // south
            tr = r; tc = c;

            while (tr < 7 && board[tr + 1, c] == '-')
            {
                tr++;

                tmp = board[tr, tc];
                board[tr, tc] = 'r';
                board[r, c] = '-';

                if (wking_safe() == 1) rv.Add(new Move('W', r, c, tr, tc, tmp));

                board[tr, tc] = tmp;
                board[r, c] = tmp2;
            }

            if (tr < 7 && Char.IsUpper(board[tr + 1, c]))
            {
                tr++;

                tmp = board[tr, tc];
                board[tr, tc] = 'r';
                board[r, c] = '-';

                if (wking_safe() == 1) rv.Add(new Move('W', r, c, tr, tc, tmp));

                board[tr, tc] = tmp;
                board[r, c] = tmp2;
            }

            // west
            tr = r; tc = c;

            while (tc > 0 && board[r, tc - 1] == '-')
            {
                tc--;

                tmp = board[tr, tc];
                board[tr, tc] = 'r';
                board[r, c] = '-';

                if (wking_safe() == 1) rv.Add(new Move('W', r, c, tr, tc, tmp));

                board[tr, tc] = tmp;
                board[r, c] = tmp2;
            }

            if (tc > 0 && Char.IsUpper(board[r, tc - 1]))
            {
                tc--;

                tmp = board[tr, tc];
                board[tr, tc] = 'r';
                board[r, c] = '-';

                if (wking_safe() == 1) rv.Add(new Move('W', r, c, tr, tc, tmp));

                board[tr, tc] = tmp;
                board[r, c] = tmp2;
            }

            // east
            tr = r; tc = c;

            while (tc < 7 && board[r, tc + 1] == '-')
            {
                tc++;

                tmp = board[tr, tc];
                board[tr, tc] = 'r';
                board[r, c] = '-';

                if (wking_safe() == 1) rv.Add(new Move('W', r, c, tr, tc, tmp));

                board[tr, tc] = tmp;
                board[r, c] = tmp2;
            }

            if (tc < 7 && Char.IsUpper(board[r, tc + 1]))
            {
                tc++;

                tmp = board[tr, tc];
                board[tr, tc] = 'r';
                board[r, c] = '-';

                if (wking_safe() == 1) rv.Add(new Move('W', r, c, tr, tc, tmp));

                board[tr, tc] = tmp;
                board[r, c] = tmp2;
            }

            return rv;
        }





        public List<Move> wmv_knight(int r, int c)
        {
            List<Move> rv = new List<Move>();
            char tmp;

            /*  -1-2-
                8---3
                --n--
                7---4
                -6-5-  */

            // 1
            // If the move is on the board and empty or enemy,
            if (r - 2 >= 0 && c - 1 >= 0 && (board[r - 2, c - 1] == '-'
                  || Char.IsUpper(board[r - 2, c - 1])))
            {

                tmp = board[r - 2, c - 1];
                board[r - 2, c - 1] = 'n';
                board[r, c] = '-';

                if (wking_safe() == 1) rv.Add(new Move('W', r, c, r - 2, c - 1, tmp));

                board[r - 2, c - 1] = tmp;
                board[r, c] = 'n';
            }

            // System.//Console.Write("K2!\n");
            // 2
            if (r - 2 >= 0 && c + 1 < 8 && (board[r - 2, c + 1] == '-'
                  || Char.IsUpper(board[r - 2, c + 1])))
            {

                tmp = board[r - 2, c + 1];
                board[r - 2, c + 1] = 'n';
                board[r, c] = '-';

                if (wking_safe() == 1) rv.Add(new Move('W', r, c, r - 2, c + 1, tmp));

                board[r - 2, c + 1] = tmp;
                board[r, c] = 'n';

            }

            // System.//Console.Write("K3!\n");
            // 3
            if (r - 1 >= 0 && c + 2 < 8 && (board[r - 1, c + 2] == '-'
                  || Char.IsUpper(board[r - 1, c + 2])))
            {
                tmp = board[r - 1, c + 2];
                board[r - 1, c + 2] = 'n';
                board[r, c] = '-';

                if (wking_safe() == 1) rv.Add(new Move('W', r, c, r - 1, c + 2, tmp));

                board[r - 1, c + 2] = tmp;
                board[r, c] = 'n';
            }

            // System.//Console.Write("K4!\n");
            // 4
            if (r + 1 < 8 && c + 2 < 8 && (board[r + 1, c + 2] == '-'
                  || Char.IsUpper(board[r + 1, c + 2])))
            {

                tmp = board[r + 1, c + 2];
                board[r + 1, c + 2] = 'n';
                board[r, c] = '-';

                if (wking_safe() == 1) rv.Add(new Move('W', r, c, r + 1, c + 2, tmp));

                board[r + 1, c + 2] = tmp;
                board[r, c] = 'n';

            }

            // System.//Console.Write("K5!\n");
            // 5
            if (r + 2 < 8 && c + 1 < 8 && (board[r + 2, c + 1] == '-'
                  || Char.IsUpper(board[r + 2, c + 1])))
            {

                tmp = board[r + 2, c + 1];
                board[r + 2, c + 1] = 'n';
                board[r, c] = '-';

                if (wking_safe() == 1) rv.Add(new Move('W', r, c, r + 2, c + 1, tmp));

                board[r + 2, c + 1] = tmp;
                board[r, c] = 'n';

            }

            // System.//Console.Write("K6!\n");
            // 6
            if (r + 2 < 8 && c - 1 >= 0 && (board[r + 2, c - 1] == '-'
                  || Char.IsUpper(board[r + 2, c - 1])))
            {

                tmp = board[r + 2, c - 1];
                board[r + 2, c - 1] = 'n';
                board[r, c] = '-';

                if (wking_safe() == 1) rv.Add(new Move('W', r, c, r + 2, c - 1, tmp));

                board[r + 2, c - 1] = tmp;
                board[r, c] = 'n';

            }

            // System.//Console.Write("K7!\n");
            // 7
            if (r + 1 < 8 && c - 2 >= 0 && (board[r + 1, c - 2] == '-'
                  || Char.IsUpper(board[r + 1, c - 2])))
            {

                tmp = board[r + 1, c - 2];
                board[r + 1, c - 2] = 'n';
                board[r, c] = '-';

                if (wking_safe() == 1) rv.Add(new Move('W', r, c, r + 1, c - 2, tmp));

                board[r + 1, c - 2] = tmp;
                board[r, c] = 'n';

            }

            // System.//Console.Write("K8!\n");
            // 8
            if (r - 1 >= 0 && c - 2 >= 0 && (board[r - 1, c - 2] == '-'
                  || Char.IsUpper(board[r - 1, c - 2])))
            {

                tmp = board[r - 1, c - 2];
                board[r - 1, c - 2] = 'n';
                board[r, c] = '-';

                if (wking_safe() == 1) rv.Add(new Move('W', r, c, r - 1, c - 2, tmp));

                board[r - 1, c - 2] = tmp;
                board[r, c] = 'n';

            }


            return rv;
        }




        public List<Move> wmv_bishop(int r, int c)
        {

            List<Move> rv = new List<Move>();
            char tmp, tmp2;
            tmp2 = board[r, c];
            int tr, tc;

            // north west
            tr = r; tc = c;

            // First record moves to empty spaces
            while (tr > 0 && tc > 0 && board[tr - 1, tc - 1] == '-')
            {

                tr--; tc--;
                tmp = board[tr, tc];
                board[tr, tc] = 'b';
                board[r, c] = '-';

                if (wking_safe() == 1) rv.Add(new Move('W', r, c, tr, tc, tmp));

                board[tr, tc] = tmp;
                board[r, c] = tmp2;

            }

            // Now we have hit the edge of the board or another piece
            // If it's an enemy piece, capture and record move
            if (tr > 0 && tc > 0 && Char.IsUpper(board[tr - 1, tc - 1]))
            {

                tr--; tc--;
                tmp = board[tr, tc];
                board[tr, tc] = 'b';
                board[r, c] = '-';

                if (wking_safe() == 1) rv.Add(new Move('W', r, c, tr, tc, tmp));

                board[tr, tc] = tmp;
                board[r, c] = tmp2;

            }

            // System.//Console.Write("b2?\n");
            // north east
            tr = r; tc = c;

            while (tr > 0 && tc < 7 && board[tr - 1, tc + 1] == '-')
            {
                tr--; tc++;
                tmp = board[tr, tc];
                board[tr, tc] = 'b';
                board[r, c] = '-';

                if (wking_safe() == 1) rv.Add(new Move('W', r, c, tr, tc, tmp));

                board[tr, tc] = tmp;
                board[r, c] = tmp2;

            }

            if (tr > 0 && tc < 7 && Char.IsUpper(board[tr - 1, tc + 1]))
            {

                tr--; tc++;
                tmp = board[tr, tc];
                board[tr, tc] = 'b';
                board[r, c] = '-';

                if (wking_safe() == 1) rv.Add(new Move('W', r, c, tr, tc, tmp));

                board[tr, tc] = tmp;
                board[r, c] = tmp2;

            }

            // System.//Console.Write("b3?\n");
            // south east
            tr = r; tc = c;

            while (tr < 7 && tc < 7 && board[tr + 1, tc + 1] == '-')
            {

                tr++; tc++;
                tmp = board[tr, tc];
                board[tr, tc] = 'b';
                board[r, c] = '-';

                if (wking_safe() == 1) rv.Add(new Move('W', r, c, tr, tc, tmp));

                board[tr, tc] = tmp;
                board[r, c] = tmp2;

            }

            if (tr < 7 && tc < 7 && Char.IsUpper(board[tr + 1, tc + 1]))
            {

                tr++; tc++;
                tmp = board[tr, tc];
                board[tr, tc] = 'b';
                board[r, c] = '-';

                if (wking_safe() == 1) rv.Add(new Move('W', r, c, tr, tc, tmp));

                board[tr, tc] = tmp;
                board[r, c] = tmp2;

            }

            // System.//Console.Write("b4?\n");
            // south west
            tr = r; tc = c;

            while (tr < 7 && tc > 0 && (board[tr + 1, tc - 1] == '-'))
            {

                tr++; tc--;
                tmp = board[tr, tc];
                board[tr, tc] = 'b';
                board[r, c] = '-';

                if (wking_safe() == 1) rv.Add(new Move('W', r, c, tr, tc, tmp));

                board[tr, tc] = tmp;
                board[r, c] = tmp2;

            }

            if (tr < 7 && tc > 0 && Char.IsUpper(board[tr + 1, tc - 1]))
            {

                tr++; tc--;
                tmp = board[tr, tc];
                board[tr, tc] = 'b';
                board[r, c] = '-';

                if (wking_safe() == 1) rv.Add(new Move('W', r, c, tr, tc, tmp));

                board[tr, tc] = tmp;
                board[r, c] = tmp2;

            }

            return rv;
        }

        public List<Move> wmv_queen(int r, int c)
        {
            //Console.WriteLine("Please wait, generating white queens moves");
            List<Move> rv = new List<Move>();
            rv.AddRange(wmv_rook(r, c));
            rv.AddRange(wmv_bishop(r, c));

            return rv;
        }

        public List<Move> wmv_king(int r, int c)
        {
            List<Move> rv = new List<Move>();
            char tmp;

            // north
            if (r > 0 && (board[r - 1, c] == '-' || Char.IsUpper(board[r - 1, c])))
            {

                tmp = board[r - 1, c];
                board[r - 1, c] = 'k';
                board[r, c] = '-';

                if (wking_safe() == 1) rv.Add(new Move('W', r, c, r - 1, c, tmp));

                board[r - 1, c] = tmp;
                board[r, c] = 'k';

            }

            // east
            if (c < 7 && (board[r, c + 1] == '-' || Char.IsUpper(board[r, c + 1])))
            {

                tmp = board[r, c + 1];
                board[r, c + 1] = 'k';
                board[r, c] = '-';

                if (wking_safe() == 1) rv.Add(new Move('W', r, c, r, c + 1, tmp));

                board[r, c + 1] = tmp;
                board[r, c] = 'k';

            }

            // south
            if (r < 7 && (board[r + 1, c] == '-' || Char.IsUpper(board[r + 1, c])))
            {

                tmp = board[r + 1, c];
                board[r + 1, c] = 'k';
                board[r, c] = '-';

                if (wking_safe() == 1) rv.Add(new Move('W', r, c, r + 1, c, tmp));

                board[r + 1, c] = tmp;
                board[r, c] = 'k';

            }

            // west
            if (c > 0 && (board[r, c - 1] == '-' || Char.IsUpper(board[r, c - 1])))
            {

                tmp = board[r, c - 1];
                board[r, c - 1] = 'k';
                board[r, c] = '-';

                if (wking_safe() == 1) rv.Add(new Move('W', r, c, r, c - 1, tmp));

                board[r, c - 1] = tmp;
                board[r, c] = 'k';

            }

            // north east
            if (r > 0 && c < 7 && (board[r - 1, c + 1] == '-'
                  || Char.IsUpper(board[r - 1, c + 1])))
            {

                tmp = board[r - 1, c + 1];
                board[r - 1, c + 1] = 'k';
                board[r, c] = '-';

                if (wking_safe() == 1) rv.Add(new Move('W', r, c, r - 1, c + 1, tmp));

                board[r - 1, c + 1] = tmp;
                board[r, c] = 'k';

            }

            // south east
            if (r < 7 && c < 7 && (board[r + 1, c + 1] == '-'
                  || Char.IsUpper(board[r + 1, c + 1])))
            {

                tmp = board[r + 1, c + 1];
                board[r + 1, c + 1] = 'k';
                board[r, c] = '-';

                if (wking_safe() == 1) rv.Add(new Move('W', r, c, r + 1, c + 1, tmp));

                board[r + 1, c + 1] = tmp;
                board[r, c] = 'k';
            }

            // south west
            if (r < 7 && c > 0 && (board[r + 1, c - 1] == '-'
                  || Char.IsUpper(board[r + 1, c - 1])))
            {

                tmp = board[r + 1, c - 1];
                board[r + 1, c - 1] = 'k';
                board[r, c] = '-';

                if (wking_safe() == 1) rv.Add(new Move('W', r, c, r + 1, c - 1, tmp));

                board[r + 1, c - 1] = tmp;
                board[r, c] = 'k';

            }

            // north west
            if (r > 0 && c > 0 && (board[r - 1, c - 1] == '-'
                  || Char.IsUpper(board[r - 1, c - 1])))
            {
                tmp = board[r - 1, c - 1];
                board[r - 1, c - 1] = 'k';
                board[r, c] = '-';

                if (wking_safe() == 1) rv.Add(new Move('W', r, c, r - 1, c - 1, tmp));

                board[r - 1, c - 1] = tmp;
                board[r, c] = 'k';
            }


            return rv;
        }




        public List<Move> wmv_pawn(int r, int c)
        {
            List<Move> rv = new List<Move>();
            char tmp;

            // south 1
            if (r > 0 && board[r - 1, c] == '-')
            {

                tmp = board[r - 1, c];
                board[r - 1, c] = 'p';
                board[r, c] = '-';

                if (wking_safe() == 1) rv.Add(new Move('W', r, c, r - 1, c, tmp));

                board[r - 1, c] = tmp;
                board[r, c] = 'p';

            }

            // south 2
            if (r == 6 && board[r - 1, c] == '-' && board[r - 2, c] == '-')
            {

                tmp = board[r - 2, c];
                board[r - 2, c] = 'p';
                board[r, c] = '-';

                if (wking_safe() == 1) rv.Add(new Move('W', r, c, r - 2, c, tmp));

                board[r - 2, c] = tmp;
                board[r, c] = 'p';

            }

            // capture left
            if (c > 0 && r > 0 && Char.IsUpper(board[r - 1, c - 1]))
            {

                tmp = board[r - 1, c - 1];
                board[r - 1, c - 1] = 'p';
                board[r, c] = '-';

                if (wking_safe() == 1) rv.Add(new Move('W', r, c, r - 1, c - 1, tmp));

                board[r - 1, c - 1] = tmp;
                board[r, c] = 'p';

            }

            // capture right
            if (c < 7 && r > 0 && Char.IsUpper(board[r - 1, c + 1]))
            {

                tmp = board[r - 1, c + 1];
                board[r - 1, c + 1] = 'p';
                board[r, c] = '-';

                if (wking_safe() == 1) rv.Add(new Move('W', r, c, r - 1, c + 1, tmp));

                board[r - 1, c + 1] = tmp;
                board[r, c] = 'p';
            }
            return rv;
        }


        public int wking_safe()
        {
            int r = 0, c = 0;
            int tr, tc;
            int i, j;

            // find the white king
            for (i = 0; i < 7; i++)
            {
                for (j = 0; j < 7; j++)
                {
                    if (board[i, j] == 'k')
                    {
                        r = i;
                        c = j;
                    }
                }
            }

            // check for king
            if (r - 1 >= 0 && c - 1 >= 0 && board[r - 1, c - 1] == 'K') return 0; // ne 
            if (r - 1 >= 0 && c + 1 <= 7 && board[r - 1, c + 1] == 'K') return 0; // nw
            if (r + 1 <= 7 && c - 1 >= 0 && board[r + 1, c - 1] == 'K') return 0; // se
            if (r + 1 <= 7 && c + 1 <= 7 && board[r + 1, c + 1] == 'K') return 0; // sw

            if (r + 1 <= 7 && board[r + 1, c] == 'K') return 0; // n
            if (r - 1 >= 0 && board[r - 1, c] == 'K') return 0; // s
            if (c + 1 <= 7 && board[r, c + 1] == 'K') return 0; // e
            if (c - 1 >= 0 && board[r, c - 1] == 'K') return 0; // w

            // check for knights
            if (r - 2 >= 0 && c - 1 >= 0 && board[r - 2, c - 1] == 'N') return 0; // 1
            if (r - 2 >= 0 && c + 1 <= 7 && board[r - 2, c + 1] == 'N') return 0; // 2
            if (r - 1 >= 0 && c + 2 <= 7 && board[r - 1, c + 2] == 'N') return 0; // 3
            if (r + 1 <= 7 && c + 2 <= 7 && board[r + 1, c + 2] == 'N') return 0; // 4
            if (r + 2 <= 7 && c + 1 <= 7 && board[r + 2, c + 1] == 'N') return 0; // 5
            if (r + 2 <= 7 && c - 1 >= 0 && board[r + 2, c - 1] == 'N') return 0; // 6
            if (r + 1 <= 7 && c - 2 >= 0 && board[r + 1, c - 2] == 'N') return 0; // 7
            if (r - 1 >= 0 && c - 2 >= 0 && board[r - 1, c - 2] == 'N') return 0; // 8

            // check for pawns
            if (r - 1 >= 0 && c + 1 <= 7 && board[r - 1, c + 1] == 'P') return 0;
            if (r - 1 >= 0 && c - 1 >= 0 && board[r - 1, c - 1] == 'P') return 0;

            // check for rooks and queens

            // north
            tr = r; tc = c;

            while (tr > 0 && board[tr - 1, c] == '-') tr--;
            if (tr > 0 && (board[tr - 1, c] == 'R' || board[tr - 1, c] == 'Q'))
            {
                return 0;
            }

            // south
            tr = r; tc = c;

            while (tr < 7 && board[tr + 1, c] == '-') tr++;
            if (tr < 7 && (board[tr + 1, c] == 'R' ||  board[tr + 1, c] == 'Q'))
            {
                return 0;
            }

            // west
            tr = r; tc = c;

            while (tc > 0 && board[r, tc - 1] == '-') tc--;
            if (tc > 0 && (board[r, tc - 1] == 'R' || board[r, tc - 1] == 'Q'))
            {
                return 0;
            }

            // east
            tr = r; tc = c;

            while (tc < 7 && board[r, tc + 1] == '-') tc++;
            if (tc < 7 && (board[r, tc + 1] == 'R' || board[r, tc + 1] == 'Q'))
            {
                return 0;
            }

            // check for bishops and queens

            // north west
            tr = r; tc = c;

            while (tr > 0 && tc > 0 && board[tr - 1, tc - 1] == '-')
            {
                tr--; tc--;
            }
            if (tr > 0 && tc > 0 &&
                (board[tr - 1, tc - 1] == 'B' || board[tr - 1, tc - 1] == 'Q')) return 0;

            // north east
            tr = r; tc = c;

            while (tr > 0 && tc < 7 && board[tr - 1, tc + 1] == '-')
            {
                tr--; tc++;
            }
            if (tr > 0 && tc < 7 &&
                (board[tr - 1, tc + 1] == 'B' || board[tr - 1, tc + 1] == 'Q')) return 0;

            // south east
            tr = r; tc = c;

            while (tr < 7 && tc < 7 && board[tr + 1, tc + 1] == '-')
            {
                tr++; tc++;
            }
            if (tr < 7 && tc < 7 &&
                (board[tr + 1, tc + 1] == 'B' || board[tr + 1, tc + 1] == 'Q')) return 0;

            // south west
            tr = r; tc = c;

            while (tr < 7 && tc > 0 && board[tr + 1, tc - 1] == '-')
            {
                tr++; tc--;
            }
            if (tr < 7 && tc > 0 &&
                (board[tr + 1, tc - 1] == 'B' || board[tr + 1, tc - 1] == 'Q')) return 0;


            return 1;
        }

    }
}
