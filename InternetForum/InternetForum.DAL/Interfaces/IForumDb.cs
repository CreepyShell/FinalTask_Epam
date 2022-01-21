using Microsoft.EntityFrameworkCore;

namespace InternetForum.DAL.Interfaces
{
    public interface IForumDb
    {
        int SaveChanges();
        DbSet<T> Set<T>() where T: class;
    }
}
