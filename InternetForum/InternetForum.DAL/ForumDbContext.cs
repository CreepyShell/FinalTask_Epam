using InternetForum.DAL.DbExtentions;
using InternetForum.DAL.DomainModels;
using InternetForum.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InternetForum.DAL
{
    public class ForumDbContext : DbContext, IForumDb
    {
        public ForumDbContext(DbContextOptions<ForumDbContext> options) : base(options)
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<PostReaction> PostReactions { get; set; }
        public DbSet<CommentReaction> CommentReactions { get; set; }
        public DbSet<Questionnaire> Questionnaires { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<AnswerUser> AnswerUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.SeedDataForumDb();
            builder.SetUpDataBase();
        }
    }
}
