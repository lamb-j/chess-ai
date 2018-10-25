using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessApp
{
    class Evaluation
    {
        int P = 100;
        int N = 320;
        int B = 330;
        int R = 500;
        int Q = 900;
        int K = 10000;
        int Pa = 70;
        int Na = 150;
        int Ba = 200;
        int Ra = 300;
        int Qa = 500;
        int Ka = 350;

        int[,] pawnTable = new int[,]{
            {0,  0,  0,  0,  0,  0,  0,  0},
            {50, 50, 50, 50, 50, 50, 50, 50},
            {10, 10, 20, 30, 30, 20, 10, 10},
            {5,  5, 10, 25, 25, 10,  5,  5},
            {0,  0,  0, 20, 20,  0,  0,  0},
            {5, -5,-10,  0,  0,-10, -5,  5},
            {5, 10, 10,-20,-20, 10, 10,  5},
            {0,  0,  0,  0,  0,  0,  0,  0}};

        int[,] knightTable = new int[,]{
            {-50,-40,-30,-30,-30,-30,-40,-50},
            {-40,-20,  0,  0,  0,  0,-20,-40},
            {-30,  0, 10, 15, 15, 10,  0,-30},
            {-30,  5, 15, 20, 20, 15,  5,-30},
            {-30,  0, 15, 20, 20, 15,  0,-30},
            {-30,  5, 10, 15, 15, 10,  5,-30},
            {-40,-20,  0,  5,  5,  0,-20,-40},
            {-50,-40,-30,-30,-30,-30,-40,-50}};

        int[,] bishopTable = new int[,]{
                   { -20,-10,-10,-10,-10,-10,-10,-20},
{-10,  0,  0,  0,  0,  0,  0,-10},
{-10,  0,  5, 10, 10,  5,  0,-10},
{-10,  5,  5, 10, 10,  5,  5,-10},
{-10,  0, 10, 10, 10, 10,  0,-10},
{-10, 10, 10, 10, 10, 10, 10,-10},
{-10,  5,  0,  0,  0,  0,  5,-10},
{-20,-10,-10,-10,-10,-10,-10,-20}};

        int[,] rookTable = new int[,]{
                   { 0,  0,  0,  0,  0,  0,  0,  0},
 { 5, 10, 10, 10, 10, 10, 10,  5},
 {-5,  0,  0,  0,  0,  0,  0, -5},
 {-5,  0,  0,  0,  0,  0,  0, -5},
 {-5,  0,  0,  0,  0,  0,  0, -5},
 {-5,  0,  0,  0,  0,  0,  0, -5},
 {-5,  0,  0,  0,  0,  0,  0, -5},
 { 0,  0,  0,  5,  5,  0,  0,  0}};

        int[,] queenTable = new int[,]{
                   { -20,-10,-10, -5, -5,-10,-10,-20},
{-10,  0,  0,  0,  0,  0,  0,-10},
{-10,  0,  5,  5,  5,  5,  0,-10},
{ -5,  0,  5,  5,  5,  5,  0, -5},
{  0,  0,  5,  5,  5,  5,  0, -5},
{-10,  5,  5,  5,  5,  5,  0,-10},
{-10,  0,  5,  0,  0,  0,  0,-10},
{-20,-10,-10, -5, -5,-10,-10,-20}};


        int[,] kingMTable = new int[,]{
            {-30,-40,-40,-50,-50,-40,-40,-30},
            {-30,-40,-40,-50,-50,-40,-40,-30},
            {-30,-40,-40,-50,-50,-40,-40,-30},
            {-30,-40,-40,-50,-50,-40,-40,-30},
            {-20,-30,-30,-40,-40,-30,-30,-20},
            {-10,-20,-20,-20,-20,-20,-20,-10},
            { 20, 20,  0,  0,  0,  0, 20, 20},
            { 20, 30, 10,  0,  0, 10, 30, 20}};

        int[,] kingETable = new int[,]{
                  {  -50,-40,-30,-20,-20,-30,-40,-50},
{-30,-20,-10,  0,  0,-10,-20,-30},
{-30,-10, 20, 30, 30, 20,-10,-30},
{-30,-10, 30, 40, 40, 30,-10,-30},
{-30,-10, 30, 40, 40, 30,-10,-30},
{-30,-10, 20, 30, 30, 20,-10,-30},
{-30,-30,  0,  0,  0,  0,-30,-30},
{-50,-30,-30,-30,-30,-30,-30,-50}};



        public int[,] invert(int[,] table)
        {
            int[,] ret = new int[8, 8];
            for (int i = 0; i <= 7; i++)
            {
                for (int j = 0; j <= 7; j++)
                {
                    ret[i, j] = table[(7 - i), j];
                }
            }
            return ret;
        }


        public char[,] invert(char[,] table)
        {
            char[,] ret = new char[8, 8];
            for (int i = 0; i <= 7; i++)
            {
                for (int j = 0; j <= 7; j++)
                {
                    ret[i, j] = table[(7 - i), j];
                }
            }
            return ret;
        }

        int[,] pawnTableI;
        int[,] rookTableI;
        int[,] knightTableI;
        int[,] bishopTableI;
        int[,] queenTableI;
        int[,] kingMTableI;
        int[,] kingETableI;

        public Evaluation()
        {
            pawnTableI = invert(pawnTable);
            rookTableI = invert(rookTable);
            knightTableI = invert(knightTable);
            bishopTableI = invert(bishopTable);
            queenTableI = invert(queenTable);
            kingMTableI = invert(kingMTable);
            kingETableI = invert(kingETable);




        //    //Console.WriteLine("started evaluation");
            char[,] initial1 = new char[8, 8]
                        {             
                         {'R',  'N',  'B',  'Q',  'K',  'B',  'N',  'R'},
                        {'P',  'P',  'P',  'P',  'P',  'P',  'P',  'P'},
                         {'-','-','-','-','-','-','-','-'},
                         {'-','-','-','-','-','-','-','-'},
                         {'-','-','-','-','-','-','-','-'},
                         {'-','-','-','-','-','-','-','-'},
                         {'p','p','p','p','p','p','p','p'},
                        {'r', 'n','b','q','k','b','n','r'},
            };


            char[,] initial2 = new char[8, 8]
            {{'P','-','B','-','-','-','-','-'},
             {'N','-','P','-','-','-','-','-'},//blacks caps
             {'-','-','-','-','-','-','-','-'},
             {'-','-','-','-','-','-','-','-'},
             {'-','-','-','-','-','-','-','-'},
             {'-','-','-','-','-','-','-','-'},
             {'-','-','-','-','-','-','-','-'},//whites lower case
             {'-','-','-','-','-','-','-','-'},};
            //evaluateBoard(initial1);
        }

        internal String toString(char[,] board)
        {
            String result = "";

            for (int r = 0; r < 8; r++)
            {
                for (int c = 0; c < 8; c++)
                {
                    result += (string.Format("{0} ", board[r, c]));
                }
                result += "\n";
            }
            return result;
        }

        public double evaluateBoard(char[,] board, char capture)
        {
            //Console.WriteLine(toString(board));

            //Console.WriteLine(toString(invert(board)));

            //Console.WriteLine("Evaluation started");
            //to swtich whites and blacks
            //board = invert(board);
            int bScore = 0;

            for (int i = 0; i <= 7; i++)
            {
                for (int j = 0; j <= 7; j++)
                {
                    bScore += findScore(board, new Point(i, j));
                    //Console.WriteLine("pos " + j + " " + i + " score " + bScore);
                }

            }

            //if (capture != '-')
            //{
            //    bScore = bScore + checkCapture(capture);
            //}

          //  Console.WriteLine("final score {0}", bScore);

            return bScore;
        }

        


        public int findScore(char[,] board, Point pos)
        {
            int ret = 0;
            char name = board[pos.X, pos.Y];
            //Console.WriteLine(name);
            //whites
            //pawn
            if (name.Equals('p'))
            {
                ret -= pawnAttack(board, pos, 0);
                //initial value
                ret -= P;
                //table value
                ret -= pawnTableI[pos.X, pos.Y];
                return ret;
            }
            else if (name.Equals('r'))
            {
                ret -= rookAttack(board, pos, 0);
                //initial value
                ret -= R;
                //table value
                ret -= rookTableI[pos.X, pos.Y];
                return ret;
            }
            else if (name.Equals('n'))
            {
                ret -= knightAttack(board, pos, 0);       
                //initial value
                ret -= N;
                //table value
                ret -= knightTableI[pos.X, pos.Y];
                return ret;
            }
            else if (name.Equals('b'))
            {
                ret -= bishopAttack(board, pos, 0);
                //initial value
                ret -= B;
                //table value
                ret -= bishopTableI[pos.X, pos.Y];
                return ret;
            }
            else if (name.Equals('q'))
            {
                ret -= queenAttack(board, pos, 0);
                //initial value
                ret -= Q;
                //table value
                ret -= queenTableI[pos.X, pos.Y];
                return ret;
            }
            else if (name.Equals('k'))
            {
                ret -= kingAttack(board, pos, 0);
                //initial value
                ret -= K;
                //table value
                ret -= kingMTableI[pos.X, pos.Y];
                return ret;
            }


//blacks

            else if (name.Equals('P'))
            {
                //maybe invert the board;

                ret += pawnAttack(invert(board), new Point(7-pos.X, pos.Y), 1);
               // ret += pawnAttack(board, pos, 1);

                //initial value
                ret += P;
                //table value
                ret += pawnTable[pos.X, pos.Y];

                return ret;
            }
            else if (name.Equals('R'))
            {
                ret += rookAttack(board, pos, 1);
                //initial value
                ret += R;
                //table value
                ret += rookTable[pos.X, pos.Y];
                return ret;
            }
            else if (name.Equals('N'))
            {
                ret += knightAttack(board, pos, 1);
                //initial value
                ret += N;
                //table value
                ret += knightTable[pos.X, pos.Y];
                return ret;
            }
            else if (name.Equals('B'))
            {
                ret += bishopAttack(board, pos, 1);
                //initial value
                ret += B;
                //table value
                ret += bishopTable[pos.X, pos.Y];
                return ret;
            }
            else if (name.Equals('Q'))
            {

                ret += queenAttack(board, pos, 1);
                //initial value
                ret += Q;
                //table value
                ret += queenTable[pos.X, pos.Y];
                return ret;
            }
            else if (name.Equals('K'))
            {
                ret += kingAttack(board, pos, 1);
                //initial value
                ret += K;
                //table value
                ret += kingMTable[pos.X, pos.Y];
                //ret += isAttackingL(board, pos);
                return ret;
            }
            return 0;
        }


        public Tuple<int, bool> checkMove(int ret, int x, int y, int color, Point pos, char[,] board)
        {
            bool stop = false;
            char temp = ' ';
            if (pos.X + x >= 0 && pos.X + x <= 7 && pos.Y + y >= 0 && pos.Y + y <= 7)
            {
                temp = board[pos.X + x, pos.Y + y];
                if (!temp.Equals('-'))
                {
                    stop = true;
                    switch (color)
                    {
                        case 0:
                            if (ret < attValue(temp))
                                ret = attValue(temp);
                            break;
                        case 1:
                            if (ret < attValueL(temp))
                                ret = attValueL(temp);
                            break;
                    }
                }
            }
            return new Tuple<int, bool>(ret, stop);
        }

        public int queenAttack(char[,] board, Point pos, int color)
        {
            int ret = 0;

            Tuple<int, bool> temp;
            List<Point> list = new List<Point>(){
new Point(1, -1),
new Point(-1, -1),
new Point(1, 1),
new Point(-1, 1)};


            foreach (Point p in list)
            {
                for (int i = 1; i <= 7; i++)
                {
                    temp = checkMove(ret, p.X * i, p.Y * i, color, pos, board);
                    ret = temp.Item1;
                    if (temp.Item2)
                    {
                        break;
                    }
                }
            }
         //   //Console.WriteLine("attack score " + ret);

            return ret;
        }

        public int bishopAttack(char[,] board, Point pos, int color)
        {
            int ret = 0;

            Tuple<int, bool> temp;
            List<Point> list = new List<Point>(){
new Point(1, -1),
new Point(-1, -1),
new Point(1, 1),
new Point(-1, 1),
new Point(0, -1),
new Point(0, 1),
new Point(1, 0),
new Point(-1, 0)};


            foreach (Point p in list)
            {
                for (int i = 1; i <= 7; i++)
                {
                    temp = checkMove(ret, p.X * i, p.Y * i, color, pos, board);
                    ret = temp.Item1;
                    if (temp.Item2)
                    {
                        break;
                    }
                }
            }
            return ret;
        }

        public int rookAttack(char[,] board, Point pos, int color)
        {
            int ret = 0;

            Tuple<int, bool> temp;
            List<Point> list = new List<Point>(){
new Point(0, -1),
new Point(0, 1),
new Point(1, 0),
new Point(-1, 0)};
            foreach (Point p in list)
            {
                for (int i = 1; i <= 7; i++)
                {
                    temp = checkMove(ret, p.X*i, p.Y*i, color, pos, board);
                    ret = temp.Item1;
                    if (temp.Item2)
                    {
                        break;
                    }
                }
            }
   return ret;
        }

        public int knightAttack(char[,] board, Point pos, int color)
        {
            int ret = 0;
            Tuple<int, bool> temp;
            List<Point> list = new List<Point>(){
new Point(-1, -2),
new Point(1, -2),
new Point(-1, 2),
new Point(1, 2),
new Point(-2, 1),
new Point(-2, -1),
new Point(2, 1),
new Point(2, -1)};

            foreach (Point p in list)
            {
                temp = checkMove(ret, p.X, p.Y, color, pos, board);
                ret = temp.Item1;
            }
          
            return ret;
        }

        public int attValue(char c)
        {
            if (c.Equals('P'))
            {
                return Pa;
            }

            if (c.Equals('R'))
            {
                return Ra;
            }
            if (c.Equals('N'))
            {
                return Na;
            }

            if (c.Equals('B'))
            {
                return Ba;
            }

            if (c.Equals('Q'))
            {
                return Qa;
            }
            if (c.Equals('K'))
            {
                return Ka;
            }
            return 0;
        }

        public int attValueL(char c)
        {
            if (c.Equals('p'))
            {
                return Pa;
            }

            if (c.Equals('r'))
            {
                return Ra;
            }
            if (c.Equals('n'))
            {
                return Na;
            }

            if (c.Equals('b'))
            {
                return Ba;
            }

            if (c.Equals('q'))
            {
                return Qa;
            }
            if (c.Equals('k'))
            {
                return Ka;
            }
            return 0;
        }

        public int pawnAttack(char[,] board, Point pos, int color)
        {
            int ret = 0;
            Tuple<int, bool> temp;
            List<Point> list = new List<Point>(){
new Point(-1, -1),
new Point(-1, 1)
            };

            foreach (Point p in list)
            {
                temp = checkMove(ret, p.X, p.Y, color, pos, board);
                ret = temp.Item1;
            }
           // //Console.WriteLine("attack score " + ret);
            return ret;
        }

        public int kingAttack(char[,] board, Point pos, int color)
        {
            int ret = 0;
            Tuple<int, bool> temp;
            List<Point> list = new List<Point>(){
new Point(0, -1),
new Point(0, 1),
new Point(1,0),
new Point(-1,0),
new Point(1,-1),
new Point(-1,-1),
new Point(1,1),
new Point(-1,1)
            };
            foreach (Point p in list)
            {
                temp = checkMove(ret, p.X, p.Y, color, pos, board);
                ret = temp.Item1;
            }           
            return ret;
        }

        public int checkCapture(char capture)
        {
            if (Char.IsUpper(capture))
            {
                return attValue(capture);
            }
            else
                return attValueL(capture);
        }
    }
}
