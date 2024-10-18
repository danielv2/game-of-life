using GOF.Domain.Entities;

namespace GOF.Domain.Interfaces.Repositories
{
    /// <summary>
    /// IGameStageRepository interface
    /// </summary>
    public interface IGameStageRepository : IRepositoryBase<GameStageEntity>
    {
        Task<GameStageEntity?> GetLatestStageByGameIdAsync(Guid gameId);
        Task<ICollection<GameStageEntity>> GetByGameIdAsync(Guid gameId);
    }
}