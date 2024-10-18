using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GOF.Domain.Entities;
using Xunit;

namespace GOF.Tests.Entities
{
    public class GameEntityTest
    {
        [Fact]
        public void SquareSideSize_Should_SetAndGetCorrectValue()
        {
            // Arrange
            var gameEntity = new GameEntity();
            int expectedSize = 10;

            // Act
            gameEntity.SquareSideSize = expectedSize;
            int actualSize = gameEntity.SquareSideSize;

            // Assert
            Assert.Equal(expectedSize, actualSize);
        }

        [Fact]
        public void InitialState_Should_SetAndGetCorrectValue()
        {
            // Arrange
            var gameEntity = new GameEntity();
            List<List<int>> expectedState = new List<List<int>> { new List<int> { 1, 0, 1 }, new List<int> { 0, 1, 0 }, new List<int> { 1, 0, 1 } };

            // Act
            gameEntity.InitialState = expectedState;
            List<List<int>>? actualState = gameEntity.InitialState;

            // Assert
            Assert.Equal(expectedState, actualState);
        }

        [Fact]
        public void MaxGenerations_Should_SetAndGetCorrectValue()
        {
            // Arrange
            var gameEntity = new GameEntity();
            int expectedGenerations = 10;

            // Act
            gameEntity.MaxGenerations = expectedGenerations;
            int actualGenerations = gameEntity.MaxGenerations;

            // Assert
            Assert.Equal(expectedGenerations, actualGenerations);
        }
    }
}