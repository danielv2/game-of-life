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
    public class GameRepositoryTest
    {
        private readonly GameRepository _gameRepository;
        private readonly SQLiteDbContext _context;

        public GameRepositoryTest()
        {
            var options = new DbContextOptionsBuilder<SQLiteDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            _context = new SQLiteDbContext(options);
            _gameRepository = new GameRepository(_context);

            // Preencha o banco de dados com dados de teste, se necessário
            SeedDatabase();
        }

        private void SeedDatabase()
        {
            _context.GameEntity.Add(new GameEntity
            {
                SquareSideSize = 10,
                InitialState = new List<List<int>> { new List<int> { 1, 0, 1 }, new List<int> { 0, 1, 0 }, new List<int> { 1, 0, 1 } },
                MaxGenerations = 10
            });

            _context.GameEntity.Add(new GameEntity
            {
                SquareSideSize = 5,
                InitialState = new List<List<int>> { new List<int> { 0, 1, 0 }, new List<int> { 1, 0, 1 }, new List<int> { 0, 1, 0 } },
                MaxGenerations = 20
            });

            _context.SaveChanges();
        }

        [Fact]
        public void GameRepository_Constructor_ShouldInitialize()
        {
            // Arrange & Act
            var repository = new GameRepository(_context);

            // Assert
            Assert.NotNull(repository);
        }

        [Fact]
        public async Task GameRepository_Add_ShouldAddEntity()
        {
            // Arrange
            var gameEntity = new GameEntity
            {
                SquareSideSize = 10,
                InitialState = new List<List<int>> { new List<int> { 1, 0, 1 }, new List<int> { 0, 1, 0 }, new List<int> { 1, 0, 1 } },
                MaxGenerations = 15
            };

            // Act
            await _gameRepository.AddAsync(gameEntity);
            await _context.SaveChangesAsync();

            // Assert
            var addedEntity = await _context.GameEntity.FindAsync(gameEntity.Id);
            Assert.NotNull(addedEntity);
            Assert.Equal(10, addedEntity.SquareSideSize);
        }

        [Fact]
        public async Task GameRepository_GetById_ShouldReturnEntity()
        {
            // Arrange
            var existingGame = _context.GameEntity.First();

            // Act
            var result = await _gameRepository.GetByIdAsync(existingGame.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(existingGame.Id, result.Id);
        }

        [Fact]
        public async Task GameRepository_GetAll_ShouldReturnEntities()
        {
            // Act
            var result = await _gameRepository.GetAllAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(9, result.Count());
        }
    }
}
