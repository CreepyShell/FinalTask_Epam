using InternetForum.DAL;
using Microsoft.EntityFrameworkCore;

namespace RepositoryTesting
{
    public static class UnitTestHelper
    {
        public static ForumDbContext GetForumDbContext()
        {
            var options = new DbContextOptionsBuilder<ForumDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            
            return new ForumDbContext(options);
        }
    }
}
