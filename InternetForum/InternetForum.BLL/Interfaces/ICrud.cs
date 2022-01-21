using System.Collections.Generic;

namespace InternetForum.BLL.Interfaces
{
    public interface ICrud<T>
    {
        void AddEntityAsync(T entity);
        T UpdateAsync(T newEntity);
        T GetByIdAsync(int id);
        IEnumerable<T> GetAllAsync();
        bool DeleteAsync(int id);
    }
}
