using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GOF.Domain.Interfaces.Repositories
{
    public interface IRepositoryBase<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(Guid id);
        Task<T> AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(int id);
    }
}