using GOF.Domain.Entities;
using GOF.Domain.Interfaces.Repositories;
using GOF.Infra.Context;
using Microsoft.EntityFrameworkCore;

namespace GOF.Infra.Repositories
{
    /// <summary>
    /// GameStageRepository class that implements IGameStageRepository
    /// </summary>
    public class GameStageRepository : RepositoryBase<GameStageEntity>, IGameStageRepository
    {
        public GameStageRepository(SQLiteDbContext context) : base(context)
        {
        }

        /// <summary>
        /// Get the latest stage by game id
        /// </summary>
        /// <param name="gameId"></param>
        /// <returns></returns>
        public async Task<GameStageEntity?> GetLatestStageByGameIdAsync(Guid gameId)
        {
            return await _context.GameStages
                                 .Where(gs => gs.GameId == gameId)
                                 .OrderByDescending(gs => gs.Generation)
                                 .FirstOrDefaultAsync();
        }

        /// <summary>
        /// Get all stages by game id
        /// </summary>
        /// <param name="gameId"></param>
        /// <returns></returns>
        public async Task<ICollection<GameStageEntity>> GetByGameIdAsync(Guid gameId)
        {
            return await _context.GameStages
                                 .Where(gs => gs.GameId == gameId)
                                 .ToListAsync();
        }
    }
}