using GOF.Domain.Interfaces.Services;
using Microsoft.Extensions.Logging;

namespace GOF.Service.Services
{
    /// <summary>
    /// PopulationService class that implements IPopulationService
    /// </summary>
    /// <seealso cref="IPopulationService" />
    public class PopulationService : IPopulationService
    {
        private readonly ILogger<GameService> _logger;

        public PopulationService(ILogger<GameService> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Generates a population board.
        /// </summary>
        /// <remarks
        /// The population board is a square matrix with the same number of rows and columns.
        /// Each cell of the matrix can be either alive (1) or dead (0).
        /// </remarks>
        /// <param name="squareSideSize"></param>
        /// <param name="initialState"></param>
        /// <returns></returns>
        public List<List<int>> GeneratePopulationBoardAsync(int squareSideSize, List<List<int>>? initialState)
        {
            _logger.LogInformation("[PopulationService] Generating population board");

            if (initialState == null || initialState?.Count == 0)
            {
                _logger.LogInformation("[PopulationService] Generating random population board");

                return GenerateRandomBoard(squareSideSize);
            }

            return FillBoardWithInitialState(squareSideSize, initialState);
        }

        /// <summary>
        /// Generates the next generation of the population board.
        /// </summary>
        /// <param name="board"></param>
        /// <param name="squareSideSize"></param>
        /// <returns>
        /// The next generation of the population board.
        /// </returns>
        public List<List<int>> NextGeneration(List<List<int>> board, int squareSideSize)
        {
            List<List<int>> newBoard = CreateEmptyBoard(squareSideSize, squareSideSize);

            for (int i = 0; i < squareSideSize; i++)
            {
                for (int j = 0; j < squareSideSize; j++)
                {
                    int aliveNeighbors = CountAliveNeighbors(board, i, j);

                    // Rules of the game
                    // Any live cell with fewer than two live neighbors dies (underpopulation).
                    // Any live cell with two or three live neighbors lives on to the next generation (survival).
                    // Any live cell with more than three live neighbors dies (overpopulation).
                    // Any dead cell with exactly three live neighbors becomes a live cell (reproduction).
                    if (board[i][j] == 1)
                    {
                        newBoard[i][j] = (aliveNeighbors == 2 || aliveNeighbors == 3) ? 1 : 0;
                    }
                    else
                    {
                        newBoard[i][j] = (aliveNeighbors == 3) ? 1 : 0;
                    }
                }
            }

            return newBoard;
        }

        /// <summary>
        /// Counts the number of alive neighbors of a cell.
        /// Considering the cell at position (row, col) in the board and diagonals.
        /// </summary>
        /// <param name="board"></param>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <returns></returns>
        private int CountAliveNeighbors(List<List<int>> board, int row, int col)
        {
            int alive = 0;

            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    int neighborRow = row + i;
                    int neighborCol = col + j;

                    // Ignore the cell itself
                    if (i == 0 && j == 0)
                        continue;

                    // Verify if the neighbor is on the board and if it is alive
                    if (neighborRow >= 0 && neighborRow < board.Count && neighborCol >= 0 && neighborCol < board[0].Count)
                    {
                        alive += board[neighborRow][neighborCol];
                    }
                }
            }

            return alive;
        }

        /// <summary>
        /// Generates a random population board.
        /// </summary>
        /// <param name="SquareSideSize"></param>
        /// <returns></returns>
        private List<List<int>> GenerateRandomBoard(int SquareSideSize)
        {
            Random random = new Random();
            List<List<int>> board = CreateEmptyBoard(SquareSideSize, SquareSideSize);

            for (int i = 0; i < SquareSideSize; i++)
            {
                for (int j = 0; j < SquareSideSize; j++)
                {
                    board[i][j] = random.Next(0, 2); // Generate a random number between 0 and 1
                }
            }

            return board;
        }

        /// <summary>
        /// Creates an empty board with the specified number of rows and columns.
        /// </summary>
        /// <param name="rows"></param>
        /// <param name="columns"></param>
        /// <returns></returns>
        private List<List<int>> CreateEmptyBoard(int rows, int columns)
        {
            var board = new List<List<int>>();
            for (int i = 0; i < rows; i++)
            {
                var row = new List<int>(new int[columns]); // Cria uma linha com células mortas (0)
                board.Add(row);
            }
            return board;
        }

        /// <summary>
        /// Fills the board with the initial state.
        /// </summary>
        /// <param name="SquareSideSize"></param>
        /// <param name="initialState"></param>
        /// <returns></returns>
        private List<List<int>> FillBoardWithInitialState(int SquareSideSize, List<List<int>>? initialState)
        {
            List<List<int>> board = CreateEmptyBoard(SquareSideSize, SquareSideSize);

            for (int i = 0; i < SquareSideSize; i++)
            {
                if (i >= initialState?.Count)
                    break;

                for (int j = 0; j < SquareSideSize; j++)
                {
                    if (j >= initialState[i].Count)
                        break;

                    board[i][j] = initialState[i][j];
                }
            }

            return board;
        }
    }
}