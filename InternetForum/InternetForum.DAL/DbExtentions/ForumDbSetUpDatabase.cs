using InternetForum.DAL.DomainModels;
using Microsoft.EntityFrameworkCore;

namespace InternetForum.DAL.DbExtentions
{
    public static class ForumDbSetUpDatabase
    {
        public static void SetUpDataBase(this ModelBuilder builder)
        {

            builder.Entity<Comment>()
                 .HasOne(c => c.Post)
                 .WithMany(p => p.Comments)
                 .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Post>()
                .HasOne(p => p.User)
                .WithMany(u => u.Posts)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<PostReaction>()
                .HasOne(p => p.Post)
                .WithMany(p => p.Reactions)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<PostReaction>()
                .HasOne(pr => pr.User)
                .WithMany(u => u.PostReactions)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<CommentReaction>()
               .HasOne(c => c.Comment)
               .WithMany(c => c.Reactions)
               .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<CommentReaction>()
                .HasOne(c => c.User)
                .WithMany(u => u.CommentReactions)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<User>()
                .HasIndex(u => u.UserName)
                .IsUnique();

            builder.Entity<User>()
                .HasMany(u => u.Comments)
                .WithOne(c => c.User)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<User>()
                .HasMany(u => u.CommentReactions)
                .WithOne(c => c.User)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.NoAction);

        }
    }
}
