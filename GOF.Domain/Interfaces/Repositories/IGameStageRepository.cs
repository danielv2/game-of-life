using GOF.Domain.Entities;

namespace GOF.Domain.Interfaces.Repositories
{
    public interface IGameStageRepository : IRepositoryBase<GameStageEntity>
    {
        Task<GameStageEntity?> GetLatestStageByGameIdAsync(Guid gameId);
    }
}