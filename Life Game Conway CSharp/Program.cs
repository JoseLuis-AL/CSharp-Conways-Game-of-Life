using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Life_Game_Conway_CSharp
{
    class Program
    {
        static void Main(string[] args)
        {
            // --------------------------------------------------
            int xPosition = 1, yPosition = xPosition;
            int rowsNumber = 25, columnNumber = rowsNumber;
            int animetionSpeed = 50;

            // --------------------------------------------------
            Console.CursorVisible = false;

            // --------------------------------------------------
            // Create game board.
            GameBoard gameBoard = new GameBoard(xPosition, yPosition, rowsNumber, columnNumber, animetionSpeed);

            // Game loop.
            while (gameBoard.CurrentState != GameBoardState.GAME_OVER)
                gameBoard.Routines();
            // --------------------------------------------------


            // End.
            Console.Clear();
            Console.SetCursorPosition(0, 0);
            Console.WriteLine("Thanks for play! Created by: Aguilera Luzania José Luis.");
        }
    }
}
