using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GOF.Domain.Entities;
using GOF.Domain.Interfaces.Repositories;
using GOF.Domain.Interfaces.Services;
using GOF.Infra.Repositories;
using Moq;
using Microsoft.Extensions.Logging;
using Xunit;
using GOF.Service.Services;

namespace GOF.Tests.Services
{
    public class GameServiceTests
    {
        private readonly Mock<IGameRepository> _mockGameRepo;
        private readonly Mock<IGameStageRepository> _mockStageRepo;
        private readonly Mock<IPopulationService> _mockPopulationService;
        private readonly Mock<ILogger<GameService>> _mockLogger;
        private readonly GameService _service;

        public GameServiceTests()
        {
            _mockGameRepo = new Mock<IGameRepository>();
            _mockStageRepo = new Mock<IGameStageRepository>();
            _mockPopulationService = new Mock<IPopulationService>();
            _mockLogger = new Mock<ILogger<GameService>>();

            _service = new GameService(
                _mockLogger.Object,
                _mockGameRepo.Object,
                _mockStageRepo.Object,
                _mockPopulationService.Object);
        }

        [Fact]
        public async Task GetNextAsync_ShouldReturnNextStages_WhenCountBelowMaxGenerations_LastStateFalse()
        {
            // Arrange
            var gameId = Guid.NewGuid();
            var game = new GameEntity { Id = gameId, MaxGenerations = 10, SquareSideSize = 10, InitialState = new List<List<int>>() };
            _mockGameRepo.Setup(repo => repo.GetByIdAsync(gameId)).ReturnsAsync(game);
            _mockStageRepo.Setup(repo => repo.GetLatestStageByGameIdAsync(gameId))
                .ReturnsAsync(new GameStageEntity { Generation = 5 });

            // Act
            var result = await _service.GetNextAsync(gameId, 2, false);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task GetNextAsync_ShouldThrowException_WhenCountExceedsMaxGenerations_LastStateFalse()
        {
            // Arrange
            var gameId = Guid.NewGuid();
            var game = new GameEntity { Id = gameId, MaxGenerations = 5, SquareSideSize = 10, InitialState = new List<List<int>>() };
            _mockGameRepo.Setup(repo => repo.GetByIdAsync(gameId)).ReturnsAsync(game);
            _mockStageRepo.Setup(repo => repo.GetLatestStageByGameIdAsync(gameId))
                .ReturnsAsync(new GameStageEntity { Generation = 5 });

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(async () => await _service.GetNextAsync(gameId, 6, false));
        }

        [Fact]
        public async Task GetNextAsync_ShouldThrowException_WhenLastStateTrueAndSquareSideSizeGreaterThan20()
        {
            // Arrange
            var gameId = Guid.NewGuid();
            var game = new GameEntity { Id = gameId, MaxGenerations = 100, SquareSideSize = 21, InitialState = new List<List<int>>() };
            _mockGameRepo.Setup(repo => repo.GetByIdAsync(gameId)).ReturnsAsync(game);
            _mockStageRepo.Setup(repo => repo.GetLatestStageByGameIdAsync(gameId))
                .ReturnsAsync(new GameStageEntity { Generation = 5 });

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(async () => await _service.GetNextAsync(gameId, 2, true));
        }

        [Fact]
        public async Task GetNextAsync_ShouldThrowException_WhenGameCannotReachFinalState()
        {
            // Arrange
            var gameId = Guid.NewGuid();
            var game = new GameEntity { Id = gameId, MaxGenerations = 50, SquareSideSize = 4, InitialState = new List<List<int>>() };

            _mockGameRepo.Setup(repo => repo.GetByIdAsync(gameId)).ReturnsAsync(game);
            _mockStageRepo.Setup(repo => repo.GetLatestStageByGameIdAsync(gameId))
                .ReturnsAsync(new GameStageEntity { Generation = 49 });

            // Act & Assert
            var exception = await Assert.ThrowsAsync<Exception>(async () => await _service.GetNextAsync(gameId, 50, true));

            Assert.Equal("The game was trying to reach the last state, but it was not possible", exception.Message);
        }

        [Fact]
        public async Task GetNextAsync_ShouldThrowException_WhenGameIdDoesNotExist()
        {
            // Arrange
            var gameId = Guid.NewGuid();
            _mockGameRepo.Setup(repo => repo.GetByIdAsync(gameId)).ReturnsAsync((GameEntity)null);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<Exception>(async () => await _service.GetNextAsync(gameId, 2, false));

            Assert.Equal("Game not found", exception.Message);
        }

        [Fact]
        public async Task GetNextAsync_ShouldThrowException_WhenGameHasReachedMaxGenerations()
        {
            // Arrange
            var gameId = Guid.NewGuid();
            var game = new GameEntity { Id = gameId, MaxGenerations = 10, SquareSideSize = 10, InitialState = new List<List<int>>() };
            _mockGameRepo.Setup(repo => repo.GetByIdAsync(gameId)).ReturnsAsync(game);
            _mockStageRepo.Setup(repo => repo.GetLatestStageByGameIdAsync(gameId))
                .ReturnsAsync(new GameStageEntity { Generation = 10 });

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(async () => await _service.GetNextAsync(gameId, 2, false));
        }

        [Fact]
        public async Task GetGameById_ShouldReturnGame_WhenGameExists()
        {
            // Arrange
            var gameId = Guid.NewGuid();
            var game = new GameEntity { Id = gameId, MaxGenerations = 10, SquareSideSize = 10, InitialState = new List<List<int>>() };
            _mockGameRepo.Setup(repo => repo.GetByIdAsync(gameId)).ReturnsAsync(game);

            // Act
            var result = await _service.GetByIdAsync(gameId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(gameId, result.Id);
        }

        [Fact]
        public async Task GetGameById_ShouldReturnNull_WhenGameDoesNotExist()
        {
            // Arrange
            var gameId = Guid.NewGuid();
            _mockGameRepo.Setup(repo => repo.GetByIdAsync(gameId)).ReturnsAsync((GameEntity)null);

            // Act
            var exception = await Assert.ThrowsAsync<Exception>(async () => await _service.GetByIdAsync(gameId));

            // Assert
            Assert.Equal("Game not found", exception.Message);
        }

        [Fact]
        public async Task AddGameAsync_ShouldAddGameAndReturnGameId()
        {
            // Arrange
            var gameId = Guid.NewGuid();
            var game = new GameEntity { Id = gameId, MaxGenerations = 10, SquareSideSize = 10, InitialState = new List<List<int>>() };

            _mockGameRepo.Setup(repo => repo.AddAsync(It.IsAny<GameEntity>())).ReturnsAsync(game);

            // Act
            var result = await _service.CreateAsync(game);

            // Assert
            Assert.Equal(gameId, result.Id);
        }
    }
}
