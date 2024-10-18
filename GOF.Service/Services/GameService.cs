using GOF.Domain.Entities;
using GOF.Domain.Interfaces.Repositories;
using GOF.Domain.Interfaces.Services;
using Microsoft.Extensions.Logging;

namespace GOF.Service.Services
{
    /// <summary>
    /// GameService class that implements IGameService
    /// </summary>
    /// <seealso cref="IGameService" />
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

        /// <summary>
        /// Gets all games.
        /// </summary>
        /// <returns>
        /// A list of all games.
        /// </returns>
        public async Task<IEnumerable<GameEntity>> GetAllAsync()
        {
            _logger.LogInformation("[GameService] Getting all games");
            return await _gameRepository.GetAllAsync();
        }

        /// <summary>
        /// Gets a specific game.
        /// </summary>
        /// <param name="id">The game identifier.</param>
        /// <returns>
        /// The game entity.
        /// </returns>
        public async Task<GameEntity> GetByIdAsync(Guid id)
        {
            _logger.LogInformation("[GameService] Getting speficic game");
            var game = await _gameRepository.GetByIdAsync(id);
            if (game == null)
            {
                _logger.LogWarning("[GameService] Game not found");
                throw new Exception("Game not found");
            }
            game.Stages = await _gameStageRepository.GetByGameIdAsync(id);

            return game;
        }

        /// <summary>
        /// Creates a new game.
        /// </summary>
        /// <param name="entity">The game entity.</param>
        /// <returns>
        /// The created game entity.
        /// </returns>
        public async Task<GameEntity> CreateAsync(GameEntity entity)
        {
            _logger.LogInformation("[GameService] Creating a new game");

            entity.InitialState = _populationService.GeneratePopulationBoardAsync(entity.SquareSideSize, entity.InitialState);

            return await _gameRepository.AddAsync(entity);
        }

        /// <summary>
        /// Updates a game.
        /// </summary>
        /// <param name="gameId">The game Id.</param>
        /// <param name="count">The quantity to reach interactions.</param>
        /// <param name="lastState">The flag to reach the last state.</param>
        /// <returns>
        /// The list of game steps considering the criteria.
        /// </returns>
        public async Task<IEnumerable<GameStageEntity>> GetNextAsync(Guid gameId, int count, bool lastState)
        {
            _logger.LogInformation("[GameService] Getting next game stage");

            var game = await GetGameAsync(gameId);
            var lastStage = await GetLastStageAsync(gameId);

            if (HasReachedMaxGenerations(game, lastStage))
            {
                _logger.LogWarning("[GameService] Game has reached the maximum number of generations");
                throw new Exception("Game has reached the maximum number of generations");
            }

            List<GameStageEntity> newStages = new List<GameStageEntity>();

            if (count > game.MaxGenerations)
            {
                count = game.MaxGenerations;
            }

            for (int i = 0; i < count; i++)
            {
                var newStage = await CreateNextStageAsync(game, gameId, lastStage);
                lastStage = newStage;

                // If the last state is enabled, verify if the last state is reached, the loop is interrupted
                if (lastState && HasReachedLastState(newStage, newStages.LastOrDefault()))
                {
                    return new List<GameStageEntity> { lastStage };
                }

                newStages.Add(lastStage);
            }

            // Criteria - Gets final state for board. If board doesn't go to conclusion after x number of attempts, returns error
            if (lastState)
            {
                throw new Exception("The game was trying to reach the last state, but it was not possible");
            }

            return newStages;
        }

        /// <summary>
        /// Gets the game entity by id.
        /// </summary>
        /// <param name="gameId"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        private async Task<GameEntity> GetGameAsync(Guid gameId)
        {
            var game = await _gameRepository.GetByIdAsync(gameId);
            if (game == null)
            {
                _logger.LogWarning("[GameService] Game not found");
                throw new Exception("Game not found");
            }
            return game;
        }

        /// <summary>
        /// Gets the last stage by game id.
        /// </summary>
        /// <param name="gameId"></param>
        private async Task<GameStageEntity> GetLastStageAsync(Guid gameId)
        {
            return await _gameStageRepository.GetLatestStageByGameIdAsync(gameId);
        }

        /// <summary>
        /// Verifies if the game has reached the maximum number of generations.
        /// </summary>
        /// <param name="game"></param>
        /// <param name="lastStage"></param>
        private bool HasReachedMaxGenerations(GameEntity game, GameStageEntity lastStage)
        {
            return lastStage?.Generation >= game.MaxGenerations;
        }


        /// <summary>
        /// Creates the next stage.
        /// </summary>
        /// <param name="game"></param>
        /// <param name="gameId"></param>
        /// <param name="lastStage"></param>
        /// <returns>
        /// The new stage entity.
        /// </returns>
        private async Task<GameStageEntity> CreateNextStageAsync(GameEntity game, Guid gameId, GameStageEntity lastStage)
        {
            var newStage = new GameStageEntity
            {
                GameId = gameId,
                Generation = lastStage?.Generation != null ? lastStage.Generation + 1 : 1,
                Population = _populationService.NextGeneration(lastStage?.Population ?? game.InitialState, game.SquareSideSize)
            };

            return await _gameStageRepository.AddAsync(newStage);
        }

        /// <summary>
        /// Verifies if the last state has been reached.
        /// </summary>
        /// <param name="newStage"></param>
        /// <param name="lastStage"></param>
        /// <returns>bool</returns>
        private bool HasReachedLastState(GameStageEntity newStage, GameStageEntity lastStage)
        {
            return lastStage != null && newStage.Population == lastStage.Population;
        }
    }
}