using GOF.Domain.Entities;

namespace GOF.Domain.Interfaces.Services
{
    public interface IGameService
    {
        Task<IEnumerable<GameEntity>> GetAllAsync();
        Task<GameEntity> CreateAsync(GameEntity entity);
        Task<GameStageEntity> GetNextAsync(Guid gameId, int count);
    }
}