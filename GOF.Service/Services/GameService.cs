using GOF.Domain.Entities;
using GOF.Domain.Interfaces.Repositories;
using GOF.Domain.Interfaces.Services;
using Microsoft.Extensions.Logging;

namespace GOF.Service.Services
{
    public class GameService : IGameService
    {
        private readonly ILogger<GameService> _logger;
        private readonly IGameRepository _gameRepository;
        private readonly IGameStageRepository _gameStageRepository;
        private readonly IPopulationService _populationService;
        public GameService(ILogger<GameService> logger, IGameRepository gameRepository, IGameStageRepository gameStageRepository, IPopulationService populationService)
        {
            _logger = logger;
            _gameRepository = gameRepository;
            _gameStageRepository = gameStageRepository;
            _populationService = populationService;
        }
        public async Task<IEnumerable<GameEntity>> GetAllAsync()
        {
            _logger.LogInformation("[GameService] Getting all games");
            return await _gameRepository.GetAllAsync();
        }

        public async Task<GameEntity> CreateAsync(GameEntity entity)
        {
            _logger.LogInformation("[GameService] Creating a new game");

            entity.InitialState = _populationService.GeneratePopulationBoardAsync(entity.SquareSideSize, entity.InitialState);

            return await _gameRepository.AddAsync(entity);
        }

        public async Task<GameStageEntity> GetNextAsync(Guid gameId, int count)
        {
            _logger.LogInformation("[GameService] Getting next game stage");
            var game = await _gameRepository.GetByIdAsync(gameId);

            if (game == null)
            {
                _logger.LogWarning("[GameService] Game not found");
                return null;
            }

            var lastStage = await _gameStageRepository.GetLatestStageByGameIdAsync(gameId);

            if (lastStage?.Generation + 1 >= game.maxGenerations)
            {
                _logger.LogWarning("[GameService] Game has reached the maximum number of generations");
                throw new Exception("Game has reached the maximum number of generations");
            }

            for (int i = 0; i < count; i++)
            {
                var newStage = new GameStageEntity
                {
                    GameId = gameId,
                    Generation = lastStage?.Generation != null ? lastStage.Generation + 1 : 1,
                    Population = _populationService.NextGeneration(lastStage?.Population ?? game.InitialState, game.SquareSideSize)
                };

                lastStage = await _gameStageRepository.AddAsync(newStage);
            }

            return lastStage;
        }
    }
}