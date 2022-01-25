﻿using InternetForum.DAL.DomainModels;
using Microsoft.EntityFrameworkCore;
using System;

namespace InternetForum.DAL.DbExtentions
{
    public static class ForumDbSeedData
    {
        public static void SeedDataForumDb(this ModelBuilder builder)
        {
            builder.Entity<User>().HasData(new User[5]
            {
                new User()
                {
                    Id = "1s",
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
            });

            builder.Entity<Post>().HasData(new Post[3] {
                new Post()
                {
                    Id = "1",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now.AddMinutes(10),
                    Header = "Summer holidays",
                    PostTopic = PostTopic.Activities,
                    Text = "Tell about your best summer holidays",
                    UserId = "1s"
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
                }
            });

            builder.Entity<Comment>().HasData(new Comment[5] {
                new Comment()
                {
                    Id = "1",
                    UserId = "1s",
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
                }
            });

            builder.Entity<PostReaction>().HasData(new PostReaction[10] {
                new PostReaction()
                {
                    Id = "1",
                    IsLiked = true,
                    PostId = "1",
                    UserId = "1s",
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
            }) ;

            builder.Entity<CommentReaction>().HasData(new CommentReaction[3] {
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
                    UserId = "1s",
                    ReactedAt = DateTime.Now.AddDays(181)
                }
            });
        }
    }
}