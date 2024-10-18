using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GOF.Domain.Entities;
using Xunit;

namespace GOF.Tests.Entities
{
    public class GameStageEntityTests
    {
        [Fact]
        public void GameStageEntity_Id_ShouldBeInitialized()
        {
            // Arrange & Act
            var gameStageEntity = new GameStageEntity();

            // Assert
            Assert.NotEqual(Guid.Empty, gameStageEntity.Id);
        }

        [Fact]
        public void GameStageEntity_Generation_Should_SetAndGetCorrectValue()
        {
            // Arrange
            var gameStageEntity = new GameStageEntity();
            int expectedGeneration = 10;

            // Act
            gameStageEntity.Generation = expectedGeneration;
            int actualGeneration = gameStageEntity.Generation;

            // Assert
            Assert.Equal(expectedGeneration, actualGeneration);
        }

        [Fact]
        public void GameStageEntity_Population_Should_SetAndGetCorrectValue()
        {
            // Arrange
            var gameStageEntity = new GameStageEntity();
            List<List<int>> expectedPopulation = new List<List<int>> { new List<int> { 1, 0, 1 }, new List<int> { 0, 1, 0 }, new List<int> { 1, 0, 1 } };

            // Act
            gameStageEntity.Population = expectedPopulation;
            List<List<int>>? actualPopulation = gameStageEntity.Population;

            // Assert
            Assert.Equal(expectedPopulation, actualPopulation);
        }

        [Fact]
        public void GameStageEntity_Game_Should_SetAndGetCorrectValue()
        {
            // Arrange
            var gameStageEntity = new GameStageEntity();
            var gameEntity = new GameEntity();

            // Act
            gameStageEntity.Game = gameEntity;
            var actualGame = gameStageEntity.Game;

            // Assert
            Assert.Equal(gameEntity, actualGame);
        }
    }
}