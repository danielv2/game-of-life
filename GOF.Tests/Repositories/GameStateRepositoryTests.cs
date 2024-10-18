using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GOF.Domain.Entities;
using GOF.Infra.Context;
using GOF.Infra.Repositories;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace GOF.Tests.Repositories
{
    public class GameStageRepositoryTests
    {
        private readonly GameStageRepository _gameStageRepository;
        private readonly SQLiteDbContext _context;

        public GameStageRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<SQLiteDbContext>()
                .UseInMemoryDatabase(databaseName: "TestGameStageDatabase")
                .Options;
            _context = new SQLiteDbContext(options);
            _gameStageRepository = new GameStageRepository(_context);

            SeedDatabase();
        }

        private void SeedDatabase()
        {
            var gameId = Guid.NewGuid();

            var gameStages = new List<GameStageEntity>
            {
                new GameStageEntity { GameId = gameId, Generation = 1, Population = new List<List<int>> { new List<int> { 1, 0 }, new List<int> { 0, 1 } } },
                new GameStageEntity { GameId = gameId, Generation = 2, Population = new List<List<int>> { new List<int> { 0, 1 }, new List<int> { 1, 0 } } },
                new GameStageEntity { GameId = gameId, Generation = 3, Population = new List<List<int>> { new List<int> { 1, 1 }, new List<int> { 1, 1 } } }
            };

            _context.GameStages.AddRange(gameStages);
            _context.SaveChanges();
        }

        [Fact]
        public async Task GetLatestStageByGameIdAsync_ShouldReturnLatestStage()
        {
            // Arrange
            var gameId = _context.GameStages.First().GameId;

            // Act
            var latestStage = await _gameStageRepository.GetLatestStageByGameIdAsync(gameId);

            // Assert
            Assert.NotNull(latestStage);
            Assert.Equal(3, latestStage.Generation);
        }

        [Fact]
        public async Task GetByGameIdAsync_ShouldReturnAllStagesForGame()
        {
            // Arrange
            var gameId = _context.GameStages.First().GameId;

            // Act
            var gameStages = await _gameStageRepository.GetByGameIdAsync(gameId);

            // Assert
            Assert.NotNull(gameStages);
            Assert.Equal(3, gameStages.Count);
        }

        [Fact]
        public async Task AddAsync_ShouldAddNewStage()
        {
            // Arrange
            var gameId = Guid.NewGuid();
            var newStage = new GameStageEntity
            {
                GameId = gameId,
                Generation = 1,
                Population = new List<List<int>> { new List<int> { 1, 0 }, new List<int> { 0, 1 } }
            };

            // Act
            await _gameStageRepository.AddAsync(newStage);
            var addedStage = await _context.GameStages.FindAsync(newStage.Id);

            // Assert
            Assert.NotNull(addedStage);
            Assert.Equal(1, addedStage.Generation);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnStageById()
        {
            // Arrange
            var existingStage = _context.GameStages.First();

            // Act
            var stage = await _gameStageRepository.GetByIdAsync(existingStage.Id);

            // Assert
            Assert.NotNull(stage);
            Assert.Equal(existingStage.Id, stage.Id);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnAllStages()
        {
            // Act
            var allStages = await _gameStageRepository.GetAllAsync();

            // Assert
            Assert.NotNull(allStages);
            Assert.Equal(16, allStages.Count());
        }
    }
}
