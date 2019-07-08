using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/// <summary>
/// Enumeration that represents the life stages of a bug.
/// </summary>
enum BugLifeStage { DEAD, ALIVE };


namespace Life_Game_Conway_CSharp
{
    class Bug : Entity
    {
        // PRIVATE ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        private char lifeSimbol = '■';          // Symbol that will be shown when the bug is alive.
        private char deadSimbol = '≡';          // Symbol that will be shown when the bug is dead.
        private int currentNeighbors;           // Number of current neighbors
        private BugLifeStage currentLifeStage;  // Current life stage of the Bug.

        // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~


        /// <summary>
        /// Constructor of Bug entity.
        /// </summary>
        /// <param name="_xPosition">Position on the X axis.</param>
        /// <param name="_yPosition">Position on the Y axis.</param>
        /// <param name="_currentLifeStage">Current stage of life.</param>
        // Bug Constructor ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        public Bug(int _xPosition, int _yPosition, BugLifeStage _currentLifeStage)
        {
            xPosition = _xPosition;
            yPosition = _yPosition;

            currentLifeStage = _currentLifeStage;
            currentNeighbors = 0;
        }
        // End Bug Constructor ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~


        /// <summary>
        /// Symbol that will be shown when the bug is alive.
        /// </summary>
        // Property LifeSimbol ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        public char LifeSimbol { get => lifeSimbol; set => lifeSimbol = value; }
        // End Property LifeSimbol ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~


        /// <summary> 
        /// Symbol that will be shown when the bug is dead. 
        /// </summary>
        // Property DeadSimbol ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        public char DeadSimbol { get => deadSimbol; set => deadSimbol = value; }
        // End Property DeadSimbol ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~


        /// <summary>
        /// Number of current neighbors
        /// </summary>
        // Property CurrentNeightbors ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        public int CurrentNeightbors { get => currentNeighbors; set => currentNeighbors = value; }
        // End Property CurrentNeightbors ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~


        /// <summary>
        /// Current life stage of the Bug.
        /// </summary>
        // Property CurrentLifeStage ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        public BugLifeStage CurrentLifeStage { get => currentLifeStage; set => currentLifeStage = value; }
        // End Property CurrentLifeStage ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~


        /// <summary>
        /// Function that allows to draw the object in the console.
        /// </summary>
        // Function Draw ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        public override void Draw()
        {
            Console.SetCursorPosition(xPosition, yPosition);
            Console.Write((currentLifeStage == BugLifeStage.ALIVE ? lifeSimbol : deadSimbol));
        }
        // End Function Draw ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~


        /// <summary>
        /// Function that check the stage of life of the bug. If the bug has 2 or three neighbors, the bug lives, 
        /// otherwise it dies. And if he is dead and has exactly three neighbors, he lives.
        /// </summary>
        // Function CheckLifeStage ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        public void CheckLifeStage()
        {
            if (currentLifeStage == BugLifeStage.ALIVE)
                if (currentNeighbors < 2 || currentNeighbors > 3)
                    currentLifeStage = BugLifeStage.DEAD;

            if (currentLifeStage == BugLifeStage.DEAD && currentNeighbors == 3)
                currentLifeStage = BugLifeStage.ALIVE;
        }
        // End Function CheckLifeStage ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    }
}
