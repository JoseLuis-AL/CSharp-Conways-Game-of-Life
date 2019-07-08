using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


enum GameBoardState { GAME_OVER, PLAYING, PAUSE };


namespace Life_Game_Conway_CSharp
{
    class GameBoard : Entity
    {
        // PRIVATE ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        private int rowsNumber;              // Number of rows in the game board.
        private int columnsNumber;           // Number of columns in the game board.
        private int generation;              // Number of the current generation of bugs.
        private int animationSpeed;          // Speed ​​in milliseconds with which the changes in the game board will be shown.
        private GameBoardState currentState; // Current state of game board.
        private Bug[,] bugInCell;            // Bugs matrix that contains all the bugs on the game board.

        // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~


        /// <summary>
        /// Constructor of game board entity.
        /// </summary>
        /// <param name="_xPosition">Position on the X axis.</param>
        /// <param name="_yPosition">Position on the Y axis.</param>
        /// <param name="_rows">Number of rows in the game board.</param>
        /// <param name="_columns">Number of columns in the game board.</param>
        /// <param name="_speedAnimation">Speed ​​in milliseconds with which the changes in the game board will be shown.</param>
        // GameBoard Constructor ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        public GameBoard(int _xPosition = 1, int _yPosition = 1, int _rows = 5, int _columns = 5, int _speedAnimation = 250)
        {
            xPosition = _xPosition;
            yPosition = _yPosition;

            if (_rows > 5) rowsNumber = _rows;
            else rowsNumber = 5;

            if (_columns > 5) columnsNumber = _columns;
            else columnsNumber = 5;

            animationSpeed = _speedAnimation;
            currentState = GameBoardState.PLAYING;

            bugInCell = new Bug[rowsNumber, columnsNumber];
            InitializeGameBoard();

        }
        // End GameBoard Constructor ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~


        /// <summary>
        /// Number of rows in the game board.
        /// </summary>
        // Property RowsNumber ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        public int RowsNumber { get => rowsNumber; }
        // End Property RowsNumber ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

        /// <summary>
        /// Number of columns in the game board.
        /// </summary>
        // Property ColumnsNumber ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        public int ColumnsNumber { get => columnsNumber; }
        // End Property ColumnsNumber ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

        /// <summary>
        /// Number of the current generation of bugs.
        /// </summary>
        // Property Generation ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        public int Generation { get => generation; }
        // End Property Generation ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~


        /// <summary>
        /// Speed ​​in milliseconds with which the changes in the game board will be shown.
        /// </summary>
        // Property AnimationSpeed ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        public int AnimationSpeed { get => animationSpeed; set => animationSpeed = value; }
        // End Property AnimationSpeed ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

        /// <summary>
        /// Current state of game board.
        /// </summary>
        // Property CurrentState ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        public GameBoardState CurrentState { get => currentState; }
        // End Property CurrentState ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~


        /// <summary>
        /// Function responsible for capturing keyboard inputs and perform the corresponding actions. 
        /// [SPACEBAR] to pause the game and [ESCAPE] to exit.
        /// </summary>
        // Function CheckInputs ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        private void CheckInputs()
        {
            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo input = Console.ReadKey(true);

                if (input.Key == ConsoleKey.Spacebar)
                {
                    if (currentState == GameBoardState.PLAYING) currentState = GameBoardState.PAUSE;
                    else currentState = GameBoardState.PLAYING;
                }

                if (input.Key == ConsoleKey.Escape)
                    currentState = GameBoardState.GAME_OVER;

                if (input.Key == ConsoleKey.R)
                    RestartGame();
            }
        }
        // End Function CheckInputs ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~


        /// <summary>
        /// Function that distributes alive bugs of random form in the cells of the game board.
        /// </summary>
        // Function RandomBugsAlive ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        private void RandomBugsAlive()
        {
            int randomRow, randomColumn, numberOfIterations;

            // Create a random number generator.
            Random random = new Random();

            // Assign alive to a random bugs.
            numberOfIterations = (rowsNumber * columnsNumber) / 2;

            for (int i = 0; i < numberOfIterations; ++i)
            {
                randomRow = random.Next(0, rowsNumber - 1);       // Assign random row.
                randomColumn = random.Next(0, columnsNumber - 1); // Assign random column.

                bugInCell[randomRow, randomColumn].CurrentLifeStage = BugLifeStage.ALIVE;
            }
        }
        // End Function RandomBugsAlive ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~


        /// <summary>
        /// Function that initializes the position of the critters on the board, as well as their stage of life.
        /// </summary>
        // Function InitializeBugsInBoard ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        private void InitializeGameBoard()
        {
            // Initialize the position and life stage of all bugs in the game board.
            for (int row = 0; row < rowsNumber; ++row)
                for (int column = 0; column < columnsNumber; ++column)
                    bugInCell[row, column] = new Bug((xPosition + column * 2), (yPosition + row), BugLifeStage.DEAD);


            RandomBugsAlive();     
        }
        // End Function InitializeBugsInBoard ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~


        /// <summary>
        /// Function that allows to restart the game board.
        /// </summary>
        // Function ResetGame ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        private void RestartGame()
        {
            foreach (Bug bug in bugInCell)
                bug.CurrentLifeStage = BugLifeStage.DEAD;

            RandomBugsAlive();
            generation = 0;
            currentState = GameBoardState.PAUSE;
        }
        // End Function ResetGame ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~


        /// <summary>
        /// Function that checks the states of the neighbors, all the surrounding bugs (if they exist) and 
        /// assigns a number of neighbors to the current bug depending on the living neighbors.
        /// </summary>
        /// <param name="_row">Row of the current bug.</param>
        /// <param name="_column">Column of the current bug.</param>
        /// <param name="_currentBug">Reference to the current bug.</param>
        // Function CheckNeighbors ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        private void CheckNeighbors(int _row, int _column, ref Bug _currentBug)
        {
            _currentBug.CurrentNeightbors = 0;

            // -----------------------------------------------------------------------------------------
            if (_row - 1 >= 0)
            {
                if (bugInCell[(_row - 1), _column].CurrentLifeStage == BugLifeStage.ALIVE)            // Neighbor, up
                    ++_currentBug.CurrentNeightbors;

                if (_column - 1 >= 0)
                    if (bugInCell[(_row - 1), (_column - 1)].CurrentLifeStage == BugLifeStage.ALIVE)  // Neighbor, upper left corner
                        ++_currentBug.CurrentNeightbors;

                if (_column + 1 < columnsNumber - 1)
                    if (bugInCell[(_row - 1), (_column + 1)].CurrentLifeStage == BugLifeStage.ALIVE)  // Neighbor, upper right corner
                        ++_currentBug.CurrentNeightbors;
            }

            // -----------------------------------------------------------------------------------------
            if (_row + 1 < rowsNumber - 1)
            {
                if (bugInCell[(_row + 1), _column].CurrentLifeStage == BugLifeStage.ALIVE)            // Neighbor, below
                    ++_currentBug.CurrentNeightbors;

                if (_column - 1 >= 0)
                    if (bugInCell[(_row + 1), (_column - 1)].CurrentLifeStage == BugLifeStage.ALIVE)  // Neighbor, lower left corner.
                        ++_currentBug.CurrentNeightbors;

                if (_column + 1 < columnsNumber - 1)
                    if (bugInCell[(_row + 1), (_column + 1)].CurrentLifeStage == BugLifeStage.ALIVE)  // Neighbor, lower right corner.
                        ++_currentBug.CurrentNeightbors;
            }

            // -----------------------------------------------------------------------------------------
            if (_column - 1 >= 0)
                if (bugInCell[_row, _column - 1].CurrentLifeStage == BugLifeStage.ALIVE)              // Neighbor, left.
                    ++_currentBug.CurrentNeightbors;

            if (_column + 1 < columnsNumber - 1)
                if (bugInCell[_row, _column + 1].CurrentLifeStage == BugLifeStage.ALIVE)              // Neighbor, right.
                    ++_currentBug.CurrentNeightbors;

        }
        // End Function CheckNeighbors ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~


        /// <summary>
        /// Function that checks the states of all the critters of the board, its neighbors and its stage of life.
        /// </summary>
        // Function CheckGeneration ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        private void CheckGeneration()
        {
            for (int row = 0; row < rowsNumber; ++row)
                for (int column = 0; column < columnsNumber; ++column)
                    CheckNeighbors(row, column, ref bugInCell[row, column]);

            foreach (Bug bug in bugInCell)
                bug.CheckLifeStage();

            ++generation;

            if (generation == 50) CheckInputs();
        }
        // End Function CheckGeneration ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~


        /// <summary>
        /// Function that allows to draw the game board in the console.
        /// </summary>
        // Function Draw ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        public override void Draw()
        {
            foreach(Bug bug in bugInCell)
                bug.Draw();


            Console.SetCursorPosition(1, (yPosition + rowsNumber + 1));
            Console.Write("Generation:                     ", generation);
            Console.SetCursorPosition(1, (yPosition + rowsNumber + 1));
            Console.Write("Generation: {0}.", generation);

            Console.SetCursorPosition(1, (yPosition + rowsNumber + 2));
            Console.Write("Current state:                  ");
            Console.SetCursorPosition(1, (yPosition + rowsNumber + 2));
            Console.Write("Currente state: " + currentState.ToString());

            Console.SetCursorPosition(1, (yPosition + rowsNumber + 3));
            Console.Write("[SPACEBAR] = PAUSE        [ESC] = Exit       [R] = Restart");
        }
        // End Function Draw ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~


        /// <summary>
        /// Function that manages all the routines of the game board. Functions like draw and check generation.
        /// </summary>
        // Function Routines ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        public void Routines()
        {
            CheckInputs();
            Draw();

            if (currentState == GameBoardState.PLAYING)
                CheckGeneration();

            System.Threading.Thread.Sleep(animationSpeed);
        }
        // End Function Routines ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    }
}
