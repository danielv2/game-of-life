using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using GOF.Domain.Interfaces.Services;
using GOF.Domain.Models.GameModel.Request;
using GOF.Domain.Models.GameModel.Response;
using GOF.Domain.Models.GameStageModel.Response;
using GOF.Host.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace GOF.Tests.Controllers
{
    public class GameControllerTest
    {
        private readonly Mock<ILogger<GameController>> _loggerMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<IGameService> _serviceMock;
        private readonly GameController _controller;

        public GameControllerTest()
        {
            _loggerMock = new Mock<ILogger<GameController>>();
            _mapperMock = new Mock<IMapper>();
            _serviceMock = new Mock<IGameService>();
            _controller = new GameController(_loggerMock.Object, _mapperMock.Object, _serviceMock.Object);
        }

        [Fact]
        public async Task GetAll_ReturnsOkResult_WithListOfGames()
        {
            // Arrange
            var games = new List<Domain.Entities.GameEntity> { new Domain.Entities.GameEntity() };
            _serviceMock.Setup(s => s.GetAllAsync()).ReturnsAsync(games);
            _mapperMock.Setup(m => m.Map<IEnumerable<GetGameResponse>>(It.IsAny<IEnumerable<Domain.Entities.GameEntity>>()))
                       .Returns(new List<GetGameResponse> { new GetGameResponse() });

            // Act
            var result = await _controller.GetAll();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<List<GetGameResponse>>(okResult.Value);
            Assert.Single(returnValue);
        }

        [Fact]
        public async Task GetById_ReturnsOkResult_WithGame()
        {
            // Arrange
            var gameId = Guid.NewGuid();
            var game = new Domain.Entities.GameEntity();
            _serviceMock.Setup(s => s.GetByIdAsync(gameId)).ReturnsAsync(game);
            _mapperMock.Setup(m => m.Map<GetGameResponse>(It.IsAny<Domain.Entities.GameEntity>()))
                       .Returns(new GetGameResponse());

            // Act
            var result = await _controller.GetById(gameId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<GetGameResponse>(okResult.Value);
        }

        [Fact]
        public async Task PostCreate_ReturnsOkResult_WithCreatedGame()
        {
            // Arrange
            var gameRequest = new PostGameRequest();
            var gameEntity = new Domain.Entities.GameEntity();
            var gameResponse = new PostGameResponse();
            _mapperMock.Setup(m => m.Map<Domain.Entities.GameEntity>(gameRequest)).Returns(gameEntity);
            _serviceMock.Setup(s => s.CreateAsync(gameEntity)).ReturnsAsync(gameEntity);
            _mapperMock.Setup(m => m.Map<PostGameResponse>(gameEntity)).Returns(gameResponse);

            // Act
            var result = await _controller.PostCreate(gameRequest);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<PostGameResponse>(okResult.Value);
        }

        [Fact]
        public async Task GetNext_ReturnsOkResult_WithGameStages()
        {
            // Arrange
            var gameId = Guid.NewGuid();
            var gameStages = new List<Domain.Entities.GameStageEntity> { new Domain.Entities.GameStageEntity() };
            _serviceMock.Setup(s => s.GetNextAsync(gameId, 1, false)).ReturnsAsync(gameStages);
            _mapperMock.Setup(m => m.Map<IEnumerable<GetGameStageResponse>>(It.IsAny<IEnumerable<Domain.Entities.GameStageEntity>>()))
                       .Returns(new List<GetGameStageResponse> { new GetGameStageResponse() });

            // Act
            var result = await _controller.GetNext(gameId, 1, false);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<List<GetGameStageResponse>>(okResult.Value);
            Assert.Single(returnValue);
        }

        [Fact]
        public async Task GetNext_ReturnsOkResult_WithGameStages_WhenLastStateIsTrue()
        {
            // Arrange
            var gameId = Guid.NewGuid();
            var gameStages = new List<Domain.Entities.GameStageEntity> { new Domain.Entities.GameStageEntity() };
            _serviceMock.Setup(s => s.GetNextAsync(gameId, 1, true)).ReturnsAsync(gameStages);
            _mapperMock.Setup(m => m.Map<IEnumerable<GetGameStageResponse>>(It.IsAny<IEnumerable<Domain.Entities.GameStageEntity>>()))
                       .Returns(new List<GetGameStageResponse> { new GetGameStageResponse() });

            // Act
            var result = await _controller.GetNext(gameId, 1, true);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<List<GetGameStageResponse>>(okResult.Value);
            Assert.Single(returnValue);
        }

        [Fact]
        public async Task PostCreate_ReturnsBadRequestResult_WhenModelStateIsInvalid()
        {
            // Arrange
            _controller.ModelState.AddModelError("SquareSideSize", "SquareSideSize must be greater than 4");

            // Act
            var result = await _controller.PostCreate(new PostGameRequest());

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.IsType<SerializableError>(badRequestResult.Value);
        }
    }
}