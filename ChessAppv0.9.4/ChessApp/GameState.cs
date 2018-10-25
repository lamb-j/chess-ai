using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessApp
{

    public class GameState
    {

        MoveGeneration movegen = new MoveGeneration();
        Evaluation evaluator = new Evaluation();
        internal Move bestMove;
        internal char playerToMove = 'W'; // W - white, B - black move 
        internal static int inf = 100000;
        internal char[,] board = new char[8, 8];
        public Move lastMove;
        //public Move previousMove = new Move('B',0,0,0,0,'S');
        public GameState()
        {         
             // Default constructor creates initial position
            //board = new char[8, 8]
            //  {{'R',  'N',  'B', 'Q', 'K', 'B',  'N',  'R'},
            //   {'P',  'P',  'P', 'P', 'P', 'P',  'P',  'P'}, 
            //   {'-',  '-',  '-', '-', '-', '-',  '-',  '-'},
            //   {'-',  '-',  '-', '-', '-', '-',  '-',  '-'},
            //   {'-',  '-',  '-', '-', '-', '-',  '-',  '-'},
            //   {'-',  '-',  '-', '-', '-', '-',  '-',  '-'},
            //   {'p',  'p',  'p', 'p', 'p', 'p',  'p',  'p'},
            //   {'r',  'n',  'b', 'q', 'k', 'b',  'n',  'r'}};
            board = new char[8, 8]
              {{'R',  'N',  'B', '-', 'K', 'B',  'N',  'R'},
               {'P',  'P',  'P', 'P', '-', 'P',  'P',  'P'}, 
               {'-',  '-',  '-', '-', 'P', '-',  '-',  '-'},
               {'-',  '-',  '-', '-', '-', '-',  '-',  '-'},
               {'-',  '-',  '-', '-', '-', '-',  'p',  'Q'},
               {'-',  '-',  '-', '-', '-', 'p',  '-',  '-'},
               {'p',  'p',  'p', 'p', 'p', '-',  '-',  'p'},
               {'r',  'n',  'b', 'q', 'k', 'b',  'n',  'r'}};
        }

        public GameState(GameState s)                // Copy constructor
        {
            lastMove = s.lastMove;
            playerToMove = s.playerToMove;         
            for (int r = 0; r < 8; r++)
            {
                for (int c = 0; c < 8; c++)
                {
                    board[r, c] = s.board[r, c];              
                }
            }
        }


        internal double AB(double a, double b, int d)
        {
        
            if (checkMate())
            {       // End of game
                if (playerToMove == 'W')  // last piece was set by 'B' so Min won the game
                    return inf + (d*1000);      // subtract d to win in as few moves as possible
                else                    // Max(W) won
                    return -(inf + (d*1000));
            }
            if (gameDraw()) return 0;  // game is draw
     
            if (d == 0)  return evaluator.evaluateBoard(this.board,this.lastMove.captured);  // this is a leaf node, d counts down
           
            if (playerToMove == 'B')
            {
                // this is a Maximizer node             
                List<Move> movelist = movegen.gen_black(this);
                         int i = 0;
                while ((a < b) && (i < movelist.Count()))
                {         
                    GameState si = makeMove(movelist[i]);
                    double v = si.AB(a, b, d - 1);
                    if (v > a)
                    {
                        a = v;
                        bestMove = movelist[i];
                    }
                    i++;
                }
                return a;
            }
            else
            { 
                // this is a Minimizer node
                List<Move> movelist = movegen.gen_white(this);

                int i = 0;
                while ((a < b) && (i < movelist.Count))
                {
                    GameState si = makeMove(movelist[i]);
                    double v = si.AB(a, b, d - 1);
                    if (v < b)
                    {
                        b = v;
                        bestMove = movelist[i];
                    }
                    i++;
                }
                return b;
            }
        }


        internal bool checkMate()
        {
            List<Move> list = new List<Move>();
            if (this.playerToMove == 'W')
            {
                list = movegen.gen_white(this);

            }
            else
            {
                list = movegen.gen_black(this);
            }
            foreach (Move move in list)
            {
                Console.WriteLine(move.toString());

            }
            if (list.Count == 0) { return true; }
            return false;

        }


        internal bool gameDraw()
        {
            return false; //need cases where game would be played draw Justas
        }



        internal GameState makeMove(Move m)
        {
            
            lastMove = m;
           //Console.WriteLine("last move {0}", lastMove);
            GameState newState = new GameState(this);  // copy this state
            
            newState.board[m.rowEnd, m.colEnd] = newState.board[m.rowStart, m.colStart];
            newState.board[m.rowStart, m.colStart] = '-'; 
            if (playerToMove == 'W')
            {
                newState.playerToMove = 'B';
            }
            else
            {
                newState.playerToMove = 'W';        
            }
            return newState;
        }

        internal bool gameIsOver()
        {
            return (checkMate() || gameDraw());
        }

        internal bool legimateMove(Move s) 
        {
            List<Move> legitWhiteMoves = new List<Move>();
            legitWhiteMoves = movegen.gen_white(this);
            foreach (Move g in legitWhiteMoves) {
                if (g.equalTo(s))
                {
                    Console.WriteLine("Move is legimate and recorded. {0}", s.toString());
                    return true;
                }
            }
            Console.WriteLine("Move is not legimate, please try again");
            return false;
        }



        internal String toString()
        {
            String result="";

            for (int r = 0; r < 8; r++)
            {
                for (int c = 0; c < 8; c++)
                {
                   result+=(string.Format("{0} ", board[r, c]));
                }
                result += "\n";

            }
            return result;
        }




    }
}
