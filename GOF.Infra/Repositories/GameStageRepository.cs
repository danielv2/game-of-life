using GOF.Domain.Entities;
using GOF.Domain.Interfaces.Repositories;
using GOF.Infra.Context;
using Microsoft.EntityFrameworkCore;

namespace GOF.Infra.Repositories
{
    public class GameStageRepository : RepositoryBase<GameStageEntity>, IGameStageRepository
    {
        public GameStageRepository(SQLiteDbContext context) : base(context)
        {
        }

        public async Task<GameStageEntity?> GetLatestStageByGameIdAsync(Guid gameId)
        {
            return await _context.GameStages
                                 .Where(gs => gs.GameId == gameId)
                                 .OrderByDescending(gs => gs.Generation)
                                 .FirstOrDefaultAsync();
        }
    }
}