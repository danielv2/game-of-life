using GOF.Domain.Interfaces.Services;
using Microsoft.Extensions.Logging;

namespace GOF.Service.Services
{
    public class PopulationService : IPopulationService
    {
        private readonly ILogger<GameService> _logger;

        public PopulationService(ILogger<GameService> logger)
        {
            _logger = logger;
        }

        public List<List<int>> GeneratePopulationBoardAsync(int squareSideSize, List<List<int>>? initialState)
        {
            _logger.LogInformation("[PopulationService] Generating population board");

            if (initialState == null)
            {
                _logger.LogInformation("[PopulationService] Generating random population board");
                return GenerateRandomBoard(squareSideSize);
            }

            return FillBoardWithInitialState(squareSideSize, initialState);
        }

        public List<List<int>> NextGeneration(List<List<int>> board, int squareSideSize)
        {
            List<List<int>> newBoard = CreateEmptyBoard(squareSideSize, squareSideSize);

            for (int i = 0; i < squareSideSize; i++)
            {
                for (int j = 0; j < squareSideSize; j++)
                {
                    int aliveNeighbors = CountAliveNeighbors(board, i, j);

                    // Regras do jogo
                    if (board[i][j] == 1)
                    {
                        // Célula viva continua viva se tiver 2 ou 3 vizinhos vivos
                        newBoard[i][j] = (aliveNeighbors == 2 || aliveNeighbors == 3) ? 1 : 0;
                    }
                    else
                    {
                        // Célula morta torna-se viva se tiver exatamente 3 vizinhos vivos
                        newBoard[i][j] = (aliveNeighbors == 3) ? 1 : 0;
                    }
                }
            }

            return newBoard;
        }

        private int CountAliveNeighbors(List<List<int>> board, int row, int col)
        {
            int alive = 0;

            int[] directions = { -1, 0, 1 };

            foreach (int dx in directions)
            {
                foreach (int dy in directions)
                {
                    if (dx == 0 && dy == 0)
                        continue;

                    int newRow = row + dx;
                    int newCol = col + dy;

                    if (newRow >= 0 && newRow < board.Count && newCol >= 0 && newCol < board[0].Count)
                    {
                        alive += board[newRow][newCol];
                    }
                }
            }

            return alive;
        }

        // Método para gerar um estado inicial aleatório
        private List<List<int>> GenerateRandomBoard(int SquareSideSize)
        {
            Random random = new Random();
            List<List<int>> board = CreateEmptyBoard(SquareSideSize, SquareSideSize);

            for (int i = 0; i < SquareSideSize; i++)
            {
                for (int j = 0; j < SquareSideSize; j++)
                {
                    board[i][j] = random.Next(0, 2); // Gera 0 ou 1 aleatoriamente
                }
            }

            return board;
        }

        // Método auxiliar para criar uma nova lista aninhada (tabuleiro vazio)
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

        private List<List<int>> FillBoardWithInitialState(int SquareSideSize, List<List<int>>? initialState)
        {
            List<List<int>> board = CreateEmptyBoard(SquareSideSize, SquareSideSize);

            for (int i = 0; i < SquareSideSize; i++)
            {
                if (i >= initialState.Count)
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