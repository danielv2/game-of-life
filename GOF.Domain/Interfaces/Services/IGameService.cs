using GOF.Domain.Entities;

namespace GOF.Domain.Interfaces.Services
{
    /// <summary>
    /// IGameService interface
    /// </summary>
    public interface IGameService
    {
        Task<IEnumerable<GameEntity>> GetAllAsync();
        Task<GameEntity> GetByIdAsync(Guid id);
        Task<GameEntity> CreateAsync(GameEntity entity);
        Task<IEnumerable<GameStageEntity>> GetNextAsync(Guid gameId, int count, bool lastState);
    }
}