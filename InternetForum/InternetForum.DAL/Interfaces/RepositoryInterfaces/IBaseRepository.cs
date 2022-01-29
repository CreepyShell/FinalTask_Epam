using InternetForum.DAL.DomainModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InternetForum.DAL.Interfaces.RepositoryInterfaces
{
    public interface IBaseRepository<T> : IDisposable
        where T : BaseModel
    {
        Task CreateAsync(T entity);
        Task<bool> DeleteByIdAsync(string id);
        Task<bool> DeleteAsync(T entity);
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(string id);
        Task<int> SaveChangesAsync();
    }
}
