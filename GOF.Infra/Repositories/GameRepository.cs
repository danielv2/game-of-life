using GOF.Domain.Entities;
using GOF.Domain.Interfaces.Repositories;
using GOF.Infra.Context;

namespace GOF.Infra.Repositories
{
    /// <summary>
    /// GameRepository class that implements IGameRepository
    /// </summary>
    public class GameRepository : RepositoryBase<GameEntity>, IGameRepository
    {
        public GameRepository(SQLiteDbContext context) : base(context)
        {
        }
    }
}