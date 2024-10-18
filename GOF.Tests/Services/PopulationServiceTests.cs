using System;
using System.Collections.Generic;
using GOF.Service.Services;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace GOF.Tests.Services
{
    public class PopulationServiceTests
    {
        private readonly PopulationService _populationService;
        private readonly Mock<ILogger<GameService>> _mockLogger;

        public PopulationServiceTests()
        {
            _mockLogger = new Mock<ILogger<GameService>>();
            _populationService = new PopulationService(_mockLogger.Object);
        }

        [Fact]
        public void GeneratePopulationBoardAsync_ShouldGenerateRandomBoard_WhenInitialStateIsNullOrEmpty()
        {
            // Arrange
            int squareSideSize = 3;
            List<List<int>>? initialState = null;

            // Act
            var board = _populationService.GeneratePopulationBoardAsync(squareSideSize, initialState);

            // Assert
            Assert.NotNull(board);
            Assert.Equal(squareSideSize, board.Count);
            Assert.All(board, row => Assert.Equal(squareSideSize, row.Count));
        }

        [Fact]
        public void GeneratePopulationBoardAsync_ShouldFillBoardWithInitialState()
        {
            // Arrange
            int squareSideSize = 3;
            var initialState = new List<List<int>>
            {
                new List<int> { 1, 0, 1 },
                new List<int> { 0, 1, 0 },
                new List<int> { 1, 0, 1 }
            };

            // Act
            var board = _populationService.GeneratePopulationBoardAsync(squareSideSize, initialState);

            // Assert
            Assert.NotNull(board);
            Assert.Equal(squareSideSize, board.Count);
            Assert.Equal(initialState[0], board[0]);
            Assert.Equal(initialState[1], board[1]);
            Assert.Equal(initialState[2], board[2]);
        }

        [Fact]
        public void NextGeneration_ShouldGenerateCorrectNextState()
        {
            // Arrange
            int squareSideSize = 3;
            var currentState = new List<List<int>>
            {
                new List<int> { 1, 0, 0 },
                new List<int> { 0, 1, 0 },
                new List<int> { 0, 0, 1 }
            };

            // Act
            var nextState = _populationService.NextGeneration(currentState, squareSideSize);

            // Assert
            Assert.NotNull(nextState);
            Assert.Equal(3, nextState.Count);
        }

        [Fact]
        public void NextGeneration_ShouldHandleEmptyBoard()
        {
            // Arrange
            int squareSideSize = 3;
            var emptyBoard = new List<List<int>>
            {
                new List<int> { 0, 0, 0 },
                new List<int> { 0, 0, 0 },
                new List<int> { 0, 0, 0 }
            };

            // Act
            var nextState = _populationService.NextGeneration(emptyBoard, squareSideSize);

            // Assert
            Assert.NotNull(nextState);
            Assert.Equal(3, nextState.Count);
            Assert.All(nextState, row => Assert.All(row, cell => Assert.Equal(0, cell)));
        }

        [Fact]
        public void GenerateRandomBoard_ShouldReturnRandomPopulationBoard()
        {
            // Arrange
            int squareSideSize = 3;

            // Act
            var board = _populationService.GeneratePopulationBoardAsync(squareSideSize, null);

            // Assert
            Assert.NotNull(board);
            Assert.Equal(squareSideSize, board.Count);
            Assert.All(board, row => Assert.Equal(squareSideSize, row.Count));
            Assert.Contains(board, row => row.Contains(1));
        }
    }
}
