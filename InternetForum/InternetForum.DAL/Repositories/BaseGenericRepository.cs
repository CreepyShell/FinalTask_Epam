using InternetForum.DAL.DomainModels;
using InternetForum.DAL.Interfaces;
using InternetForum.DAL.Interfaces.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InternetForum.DAL.Repositories
{
    public abstract class BaseGenericRepository<T> : IBaseRepository<T>
        where T : BaseModel
    {
        protected ForumDbContext _context;
        public BaseGenericRepository(IForumDb context)
        {
            _context = (ForumDbContext) context;
        }
        public async Task CreateAsync(T entity)
        {
            if (entity == null || string.IsNullOrEmpty(entity.Id))
                throw new ArgumentNullException("entity", "enitity is null or entity id is null");

            if (await _context.FindAsync(typeof(T), entity.Id) is T)
                throw new ArgumentException("entity with this id is already exists");

            await _context.AddAsync(entity);
        }

        public async Task<bool> DeleteAsync(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity", "enitity is null");

            T removedEntity = await _context.FindAsync(typeof(T), entity.Id) as T;
            if (removedEntity == null)
                return false;

            _context.Remove(removedEntity);
            return true;
        }

        public async Task<bool> DeleteByIdAsync(string id)
        {
            T entity = await _context.FindAsync(typeof(T), id) as T;
            if (entity == null)
                return false;

            _context.Remove(entity);
            return true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool dispose)
        {
            if(dispose)
            {
                _context.Dispose();
            }
        }
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _context.Set<T>().AsNoTracking().ToListAsync();
        }

        public async Task<T> GetByIdAsync(string id)
        {
            T entity = (await _context.FindAsync(typeof(T), id)) as T;
            return entity;

        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
