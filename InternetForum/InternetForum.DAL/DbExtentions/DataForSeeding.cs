using InternetForum.DAL.DomainModels;
using System;
using System.Collections.Generic;

namespace InternetForum.DAL.DbExtentions
{
    public static class DataForSeeding
    {
        public static IEnumerable<User> GetUsersValues() => new User[5]
            {
                new User()
                {
                    Id = "1",
                    UserName = "anton_1990",
                    BirthDay = new DateTime(1990, 5, 4),
                    FirstName = "Anton",
                    LastName = "Gerashenko",
                    Bio = "Electrical Engineer"
                },
                new User()
                {
                    Id = "2",
                    UserName = "dmidro",
                    FirstName = "Dmitro",
                    Bio = "18 years"
                },
                new User()
                {
                    Id = "3",
                    UserName = "user1984",
                    BirthDay = new DateTime(2000, 10, 24)
                },
                new User()
                {
                    Id = "4",
                    UserName = "Have_A_Nice_Day",
                    FirstName = "bad",
                    Bio = "The best chef in Iceland"
                },
                new User()
                {
                    Id = "5",
                    UserName = "mike_2002",
                    BirthDay = new DateTime(1999, 8, 11)
                }
            };
        public static IEnumerable<Post> GetPostsValues() => new Post[5] {
                new Post()
                {
                    Id = "1",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now.AddMinutes(10),
                    Header = "Summer holidays",
                    PostTopic = PostTopic.Activities,
                    Text = "Tell about your best summer holidays",
                    UserId = "1"
                },
                new Post()
                {
                    Id = "2",
                    CreatedAt = DateTime.Now.AddDays(90),
                    Header = "Winter holidays",
                    PostTopic = PostTopic.Activities,
                    Text = "Tell about your best winter holidays",
                    UserId = "3"
                },
                new Post()
                {
                    Id = "3",
                    CreatedAt = DateTime.Now.AddDays(180),
                    Header = "Autumn holidays",
                    PostTopic = PostTopic.Activities,
                    Text = "Tell about your best Autumn holidays",
                    UserId = "5"
                },
                new Post()
                {
                    Id = "4",
                    CreatedAt = DateTime.Now.AddDays(182),
                    Header = "Test post for deleting",
                    PostTopic = PostTopic.Another,
                    Text = "Test text",
                    UserId = "4"
                },
                new Post()
                {
                    Id = "5",
                    CreatedAt = DateTime.Now.AddDays(185),
                    Header = "Test post for updating",
                    PostTopic = PostTopic.Another,
                    Text = "Test text",
                    UserId = "2"
                }
            };
        public static IEnumerable<Comment> GetCommentsValues() => new Comment[6] {
                new Comment()
                {
                    Id = "1",
                    UserId = "1",
                    PostId = "1",
                    CreatedAt = DateTime.Now.AddHours(1),
                    CommentText = "My last summer holidays was the best",
                    CommentId = null
                },
                new Comment()
                {
                     Id = "2",
                    UserId = "2",
                    PostId = "2",
                    CreatedAt = DateTime.Now.AddDays(91),
                    CommentText = "My last winter holidays was the best",
                    CommentId = null
                },
                new Comment()
                {
                     Id = "3",
                    UserId = "3",
                    PostId = "3",
                    CreatedAt = DateTime.Now.AddDays(181),
                    CommentText = "My last autumn holidays was the best",
                    CommentId = null
                },
               new Comment()
                {
                    Id = "4",
                    UserId = "5",
                    PostId = "1",
                    CreatedAt = DateTime.Now.AddHours(1),
                    CommentText = "My last summer holidays was the best too. Thank you!",
                    CommentId = "1"
                },
                new Comment()
                {
                    Id = "5",
                    UserId = "4",
                    PostId = "2",
                    CreatedAt = DateTime.Now.AddDays(91),
                    CommentText = "My last winter holidays was the best too. It was good time",
                    CommentId = "2"
                },
                new Comment()
                {
                    Id = "6",
                    UserId = "1",
                    PostId = "2",
                    CreatedAt = DateTime.Now.AddDays(91),
                    CommentText = "My last winter holidays was the best too. It was the best time",
                    CommentId = null
                }
            };
        public static IEnumerable<PostReaction> GetPostReactionsValues() => new PostReaction[10] {
                new PostReaction()
                {
                    Id = "1",
                    IsLiked = true,
                    PostId = "1",
                    UserId = "1",
                    ReactedAt = DateTime.Now.AddHours(1)
                },
                new PostReaction()
                {
                    Id = "2",
                    IsLiked = false,
                    PostId = "1",
                    UserId = "2",
                    ReactedAt = DateTime.Now.AddHours(1)
                },
                new PostReaction()
                {
                    Id = "3",
                    IsLiked = true,
                    PostId = "2",
                    UserId = "3",
                    ReactedAt = DateTime.Now.AddDays(91)
                },
                new PostReaction()
                {
                    Id = "4",
                    IsLiked = true,
                    PostId = "3",
                    UserId = "3",
                    ReactedAt = DateTime.Now.AddDays(181)
                },
                new PostReaction()
                {
                    Id = "5",
                    IsLiked = false,
                    PostId = "3",
                    UserId = "4",
                    ReactedAt = DateTime.Now.AddDays(181)
                },
                new PostReaction()
                {
                    Id = "6",
                    IsLiked = true,
                    PostId = "2",
                    UserId = "4",
                    ReactedAt = DateTime.Now.AddDays(81)
                },
                new PostReaction()
                {
                    Id = "7",
                    IsLiked = false,
                    PostId = "1",
                    UserId = "5",
                    ReactedAt = DateTime.Now.AddHours(1)
                },
                new PostReaction()
                {
                    Id = "8",
                    IsLiked = true,
                    PostId = "1",
                    UserId = "4",
                    ReactedAt = DateTime.Now.AddHours(1)
                },
                new PostReaction()
                {
                    Id = "9",
                    IsLiked = false,
                    PostId = "3",
                    UserId = "5",
                    ReactedAt = DateTime.Now.AddDays(181)
                },
                new PostReaction()
                {
                    Id = "10",
                    IsLiked = false,
                    PostId = "2",
                    UserId = "2",
                    ReactedAt = DateTime.Now.AddDays(91)
                },
            };
        public static IEnumerable<CommentReaction> GetCommentReactionsValues() => new CommentReaction[3] {
                new CommentReaction()
                {
                    Id = "1",
                    CommentId = "1",
                    IsLiked = true,
                    UserId = "5",
                    ReactedAt = DateTime.Now.AddHours(2)
                },
                new CommentReaction()
                {
                     Id = "2",
                    CommentId = "2",
                    IsLiked = true,
                    UserId = "4",
                    ReactedAt = DateTime.Now.AddDays(92)
                },
                new CommentReaction()
                {
                     Id = "3",
                    CommentId = "3",
                    IsLiked = false,
                    UserId = "1",
                    ReactedAt = DateTime.Now.AddDays(181)
                }
            };
        public static IEnumerable<Questionnaire> GetQuestionnairesValues() => new Questionnaire[1] {
                new Questionnaire()
                {
                    AuthorId = "1",
                    ClosedAt = null,
                    Id = "1",
                    OpenAt = DateTime.Now,
                    Title = "Best Time Of Year"
                }
            };
        public static IEnumerable<Question> GetQuestionsValues() => new Question[3]
            {
                new Question()
                {
                    Id = "1",
                    IsAllowedMultiple = false,
                    QuestionnaireId = "1",
                    Text = "Is Summer the best time of year?",
                    IsRequired = true
                },
                new Question()
                {
                    Id = "2",
                    IsAllowedMultiple = true,
                    QuestionnaireId = "1",
                    Text = "What you best time of year?",
                    IsRequired = true
                },
                new Question()
                {
                    Id = "3",
                    IsAllowedMultiple = true,
                    QuestionnaireId = "1",
                    Text = "What is your favorite activity?",
                    IsRequired = false
                }
            };
        public static IEnumerable<Answer> GetAnswersValues() => new Answer[5]
            {
                new Answer()
                {
                    Id = "1",
                    QuestionId = "1",
                    Text = "Yes"
                },
                new Answer()
                {
                    Id = "2",
                    QuestionId = "1",
                    Text = "No"
                },
                new Answer()
                {
                    Id = "3",
                    QuestionId = "2",
                    Text = "Winter"
                },
                new Answer()
                {
                     Id = "4",
                    QuestionId = "2",
                    Text = "Summer"
                },
                new Answer()
                {
                     Id = "5",
                    QuestionId = "3",
                    Text = "Sleeping"
                },
            };
        public static IEnumerable<AnswerUser> GetAnswerUsersValues() => new AnswerUser[6]
            {
                new AnswerUser()
                {
                    AnswerId = "1",
                    UserId = "1"
                },
                new AnswerUser()
                {
                    AnswerId = "1",
                    UserId = "2"
                },
                new AnswerUser()
                {
                    AnswerId = "1",
                    UserId = "3"
                },
                new AnswerUser()
                {
                    AnswerId = "3",
                    UserId = "1"
                },
                new AnswerUser()
                {
                    AnswerId = "4",
                    UserId = "1"
                },
                new AnswerUser()
                {
                     AnswerId = "5",
                    UserId = "1"
                }
            };
    }
}
