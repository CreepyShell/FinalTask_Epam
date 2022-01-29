using InternetForum.DAL.DomainModels;
using Microsoft.EntityFrameworkCore;
using System;

namespace InternetForum.DAL.DbExtentions
{
    public static class ForumDbSeedData
    {
        public static void SeedDataForumDb(this ModelBuilder builder)
        {
            builder.Entity<User>().HasData(DataForSeeding.GetUsersValues());

            builder.Entity<Post>().HasData(DataForSeeding.GetPostsValues());

            builder.Entity<Comment>().HasData(DataForSeeding.GetCommentsValues());

            builder.Entity<PostReaction>().HasData(DataForSeeding.GetPostReactionsValues());

            builder.Entity<CommentReaction>().HasData(DataForSeeding.GetCommentReactionsValues());

            builder.Entity<Questionnaire>().HasData(DataForSeeding.GetQuestionnairesValues());

            builder.Entity<Question>().HasData(DataForSeeding.GetQuestionsValues());

            builder.Entity<Answer>().HasData(DataForSeeding.GetAnswersValues());

            builder.Entity<AnswerUser>().HasData(DataForSeeding.GetAnswerUsersValues());
        }
    }
}
