using System.Windows.Forms;

namespace Life
{
    class LifeGrid
    {
        #region Variables
        //Tables that holds the current generation and the last generation
        int[,] currentGeneration, lastGeneration;

        //Integers that holds information about height, width and generation count
        int generationCount, height, width;
        #endregion

        #region Constructor
        /// <summary>
        /// Sets the currentGeneration to the grid from the constructor call. And makes lastGeneration to be
        /// an empty array using the width and height of the currentGeneration.
        /// </summary>
        /// <param name="newGrid">The new grid.</param>
        public LifeGrid(int[,] newGrid)
        {
            currentGeneration = (int[,])newGrid.Clone();
            generationCount = 1;
            width = currentGeneration.GetLength(1);
            height = currentGeneration.GetLength(0);
            lastGeneration = new int[height, width];
        }
        #endregion

        #region Methods
        /// <summary>
        /// Checking all the coordinates around a cell and adding to the counter
        /// if the neigbouring cell is alive.
        /// </summary>
        /// <param name="x">The x coordinate of the cell.</param>
        /// <param name="y">The y coordinate of the cell.</param>
        /// <returns>Number of neigbours to the cell.</returns>
        private int Neighbours(int x, int y)
        {
            int count = 0;

            //check for x-1, y-1
            if (x > 0 && y > 0)
            {
                if (currentGeneration[y - 1, x - 1] == 1)
                    count++;
            }
            //check for x, y-1
            if (y > 0)
            {
                if (currentGeneration[y - 1, x] == 1)
                    count++;
            }
            //check for x+1, y-1
            if (x < width - 1 && y > 0)
            {
                if(currentGeneration[y-1,x+1]==1)
                    count++;
            }
            //check for x-1, y
            if (x > 0)
            {
                if(currentGeneration[y,x-1]==1)
                    count++;
            }
            //check for x+1, y
            if (x < width - 1)
            {
                if (currentGeneration[y, x + 1] == 1)
                    count++;
            }
            //check for x-1, y+1
            if (x > 0 && y < height - 1)
            {
                if (currentGeneration[y + 1, x - 1] == 1)
                    count++;
            }
            //check for x, y+1
            if (y < height - 1)
            {
                if (currentGeneration[y + 1, x] == 1)
                    count++;
            }
            //check for x+1, y+1
            if (x < width - 1 && y < height - 1)
            {
                if (currentGeneration[y + 1, x + 1] == 1)
                    count++;
            }
            return count;
        }

        /// <summary>
        /// Simply writes the number of neigbours each cell has in a LifeGrid
        /// </summary>
        public void writeNeighbours()
        {
            for (int y = 0; y < height; y++)
                for (int x = 0; x < width; x++)
                    MessageBox.Show("{0}",Neighbours(x,y).ToString());
        }

        /// <summary>
        /// Checking all cells in the current Generation and using the laws of Conway's Game of Life
        /// Then making a new generation based on those rules. Sets the lastGeneration to the
        /// old one, and the currentGeneration to the new one.
        /// </summary>
        public void processGeneration()
        {
            int[,] nextGeneration = new int[height, width];
            lastGeneration = (int[,])currentGeneration.Clone();
            generationCount++;

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    if (Neighbours(x, y) < 2)

                        nextGeneration[y, x] = 0;
                    else if (currentGeneration[y, x] == 0 && Neighbours(x, y) == 3)
                        nextGeneration[y, x] = 1;

                    else if (currentGeneration[y, x] == 1 && (Neighbours(x, y) == 2 || Neighbours(x, y) == 3))
                        nextGeneration[y, x] = 1;

                    else
                        nextGeneration[y, x] = 0;
                }
            }
            currentGeneration = (int[,])nextGeneration.Clone();
        }

        /// <summary>
        /// Goes through all the cells and counts the cells that are alive and return that number.
        /// </summary>
        /// <returns>Number of alive cells in the life.</returns>
        public int aliveCells()
        {
            int count = 0;
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    if (currentGeneration[y, x] == 1)
                        count++;
                }
            }
            return count;
        }
        #endregion

        #region Properties
        public int[,] Cells
        {
            get { return currentGeneration; }
        }

        public int GenerationCount
        {
            get { return generationCount; }
        }

        public int Height
        {
            get { return height; }
        }

        public int Width
        {
            get { return width; }
        }
        #endregion
    }
}
