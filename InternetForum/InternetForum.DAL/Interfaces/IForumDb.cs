using Microsoft.EntityFrameworkCore;
using System;

namespace InternetForum.DAL.Interfaces
{
    public interface IForumDb : IDisposable
    {
        int SaveChanges();
        DbSet<T> Set<T>() where T: class;
    }
}
