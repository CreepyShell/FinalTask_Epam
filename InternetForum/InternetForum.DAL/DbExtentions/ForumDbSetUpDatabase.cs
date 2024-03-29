﻿using InternetForum.DAL.DomainModels;
using Microsoft.EntityFrameworkCore;

namespace InternetForum.DAL.DbExtentions
{
    public static class ForumDbSetUpDatabase
    {
        public static void SetUpDataBase(this ModelBuilder builder)
        {
            builder.Entity<AnswerUser>()
                .HasKey(au => new
                {
                    A = au.AnswerId,
                    U = au.UserId
                });

            builder.Entity<User>()
                .HasIndex(u => u.UserName)
                .IsUnique(true);

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

            builder.Entity<User>()
                .HasMany(u => u.Answers)
                .WithOne(c => c.User)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<User>()
                .HasMany(u => u.Questionnaires)
                .WithOne(c => c.Author)
                .HasForeignKey(c => c.AuthorId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<User>()
               .HasMany(u => u.Posts)
               .WithOne(p => p.User)
               .HasForeignKey(p => p.UserId)
               .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<User>()
              .HasMany(u => u.PostReactions)
              .WithOne(p => p.User)
              .HasForeignKey(p => p.UserId)
              .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Comment>()
                 .HasOne(c => c.Post)
                 .WithMany(p => p.Comments)
                 .HasForeignKey(c => c.PostId)
                 .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<PostReaction>()
                .HasOne(p => p.Post)
                .WithMany(p => p.Reactions)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<CommentReaction>()
               .HasOne(c => c.Comment)
               .WithMany(c => c.Reactions)
               .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<CommentReaction>()
                .HasOne(c => c.User)
                .WithMany(u => u.CommentReactions)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Answer>()
               .HasMany(a => a.Users)
               .WithOne(a => a.Answer)
               .HasForeignKey(c => c.AnswerId)
               .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Question>()
                .HasMany(q => q.Answers)
                .WithOne(a => a.Question)
                .HasForeignKey(a => a.QuestionId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Questionnaire>()
                .HasMany(q => q.Questions)
                .WithOne(q => q.Questionnaire)
                .HasForeignKey(q => q.QuestionnaireId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
