using InternetForum.DAL;
using Microsoft.EntityFrameworkCore;

namespace RepositoryTesting
{
    public static class UnitTestHelper
    {
        public static ForumDbContext GetForumDbContext(string database)
        {
            var options = new DbContextOptionsBuilder<ForumDbContext>()
                .UseInMemoryDatabase(databaseName: database)
                .Options;
            
            return new ForumDbContext(options);
        }
    }
}
