using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessApp
{
    public class ChessApp
    {
        static void Main(string[] args)
        {
            char user = 'W';             // Set user to play 'W' or 'B'. 'W' starts the game
            int depth = 3;               // Search depth in gametree

            GameState currentState = new GameState();
            Console.WriteLine(currentState.toString());
            while (!currentState.gameIsOver())
            {
            
                Move nextMove;
                Console.WriteLine("{0}", currentState.playerToMove);

                if (currentState.playerToMove == user && !currentState.checkMate())
                { // user moves


                    int rStart, cStart, r, c;
                    rStart = cStart = r = c = 0;

                    do
                    {
                        Console.WriteLine("Enter which piece you want to move - row");
                        String input;
                        input = Console.ReadLine();
                        int temp;
                        while (!Int32.TryParse(input, out temp) || temp > 7 || temp < 0)
                        {
                            Console.WriteLine("It is simple 2D array 0 - 7 \n");
                            Console.WriteLine("Enter which piece you want to move - row");
                            input = Console.ReadLine();
                        }
                        rStart = temp;
                        Console.WriteLine("Enter second coordinate of wanted piece - colum.");
                        input = Console.ReadLine();
                        while (!Int32.TryParse(input, out temp) || temp > 7 || temp < 0)
                        {
                            Console.WriteLine("It is simple 2D array 0 - 7 \n");
                            Console.WriteLine("Enter second coordinate of wanted piece - colum.");
                            input = Console.ReadLine();
                        }
                        cStart = temp;
                        Console.WriteLine("Selected piece on board {0}", currentState.board[rStart, cStart]);
                        Console.WriteLine("Enter row coordinate where you want to move piece");
                        input = Console.ReadLine();
                        while (!Int32.TryParse(input, out temp) || temp > 7 || temp < 0)
                        {
                            Console.WriteLine("It is simple 2D array 0 - 7 \n");
                            Console.WriteLine("Enter row coordinate where you want to move piece");
                            input = Console.ReadLine();
                        }
                        r = temp;
                        Console.WriteLine("Enter second coordinate of wanted square - colum.");
                        input = Console.ReadLine();
                        while (!Int32.TryParse(input, out temp) || temp > 7 || temp < 0)
                        {
                            Console.WriteLine("It is simple 2D array 0 - 7 \n");
                            Console.WriteLine("Enter second coordinate of wanted square - colum.");
                            input = Console.ReadLine();
                        }
                        c = temp;

                    }
                    while (!(((0 <= r) && (r <= 7)) && ((0 <= c) && (c <= 7)) && currentState.legimateMove(new Move(user, rStart, cStart, r, c, '0'))));
                    nextMove = new Move(user, rStart, cStart, r, c, '0');

                }
                else
                {  // computer moves
                    Console.WriteLine("computer move");
                    currentState.AB(-(GameState.inf + 1000), GameState.inf + 1000, depth);
                    nextMove = currentState.bestMove;
                }
                currentState = currentState.makeMove(nextMove);

                Console.WriteLine(currentState.toString());
            }

            if (currentState.checkMate())
            {
                if (currentState.playerToMove == 'W')  // last piece was set by 'B' so 'W' won the game
                {
                    Console.WriteLine("Game won by Black");
                    Console.ReadKey();
                }
                else
                {
                    Console.WriteLine("Game won by White");
                    Console.ReadKey();
                }

            }
            if (currentState.gameDraw())
                Console.WriteLine("Game is draw");
        }

    }

}

