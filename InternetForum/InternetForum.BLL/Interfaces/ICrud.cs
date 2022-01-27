using System.Collections.Generic;
using System.Threading.Tasks;

namespace InternetForum.BLL.Interfaces
{
    public interface ICrud<T>
    {
        Task<T> AddEntityAsync(T entity);
        Task<T> UpdateAsync(T newEntity);
        Task<T> GetByIdAsync(string id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<bool> DeleteAsync(string id);
    }
}
