﻿// <auto-generated />
using System;
using InternetForum.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace InternetForum.DAL.Migrations
{
    [DbContext(typeof(ForumDbContext))]
    [Migration("20220202134929_AddedSomeDataForTests")]
    partial class AddedSomeDataForTests
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.22")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("InternetForum.DAL.DomainModels.Answer", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("QuestionId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("nvarchar(20)")
                        .HasMaxLength(20);

                    b.HasKey("Id");

                    b.HasIndex("QuestionId");

                    b.ToTable("Answers");

                    b.HasData(
                        new
                        {
                            Id = "1",
                            QuestionId = "1",
                            Text = "Yes"
                        },
                        new
                        {
                            Id = "2",
                            QuestionId = "1",
                            Text = "No"
                        },
                        new
                        {
                            Id = "3",
                            QuestionId = "2",
                            Text = "Winter"
                        },
                        new
                        {
                            Id = "4",
                            QuestionId = "2",
                            Text = "Summer"
                        },
                        new
                        {
                            Id = "5",
                            QuestionId = "3",
                            Text = "Sleeping"
                        });
                });

            modelBuilder.Entity("InternetForum.DAL.DomainModels.AnswerUser", b =>
                {
                    b.Property<string>("AnswerId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("AnswerId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("AnswerUsers");

                    b.HasData(
                        new
                        {
                            AnswerId = "1",
                            UserId = "1"
                        },
                        new
                        {
                            AnswerId = "1",
                            UserId = "2"
                        },
                        new
                        {
                            AnswerId = "1",
                            UserId = "3"
                        },
                        new
                        {
                            AnswerId = "3",
                            UserId = "1"
                        },
                        new
                        {
                            AnswerId = "4",
                            UserId = "1"
                        },
                        new
                        {
                            AnswerId = "5",
                            UserId = "1"
                        });
                });

            modelBuilder.Entity("InternetForum.DAL.DomainModels.Comment", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CommentId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CommentText")
                        .IsRequired()
                        .HasColumnType("nvarchar(140)")
                        .HasMaxLength(140);

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("PostId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("PostId");

                    b.HasIndex("UserId");

                    b.ToTable("Comments");

                    b.HasData(
                        new
                        {
                            Id = "1",
                            CommentText = "My last summer holidays was the best",
                            CreatedAt = new DateTime(2022, 2, 2, 16, 49, 27, 795, DateTimeKind.Local).AddTicks(437),
                            PostId = "1",
                            UserId = "1"
                        },
                        new
                        {
                            Id = "2",
                            CommentText = "My last winter holidays was the best",
                            CreatedAt = new DateTime(2022, 5, 4, 15, 49, 27, 795, DateTimeKind.Local).AddTicks(2772),
                            PostId = "2",
                            UserId = "2"
                        },
                        new
                        {
                            Id = "3",
                            CommentText = "My last autumn holidays was the best",
                            CreatedAt = new DateTime(2022, 8, 2, 15, 49, 27, 795, DateTimeKind.Local).AddTicks(2847),
                            PostId = "3",
                            UserId = "3"
                        },
                        new
                        {
                            Id = "4",
                            CommentId = "1",
                            CommentText = "My last summer holidays was the best too. Thank you!",
                            CreatedAt = new DateTime(2022, 2, 2, 16, 49, 27, 795, DateTimeKind.Local).AddTicks(2858),
                            PostId = "1",
                            UserId = "5"
                        },
                        new
                        {
                            Id = "5",
                            CommentId = "2",
                            CommentText = "My last winter holidays was the best too. It was good time",
                            CreatedAt = new DateTime(2022, 5, 4, 15, 49, 27, 795, DateTimeKind.Local).AddTicks(2867),
                            PostId = "2",
                            UserId = "4"
                        },
                        new
                        {
                            Id = "6",
                            CommentText = "My last winter holidays was the best too. It was the best time",
                            CreatedAt = new DateTime(2022, 5, 4, 15, 49, 27, 795, DateTimeKind.Local).AddTicks(2874),
                            PostId = "2",
                            UserId = "1"
                        });
                });

            modelBuilder.Entity("InternetForum.DAL.DomainModels.CommentReaction", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CommentId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("IsLiked")
                        .HasColumnType("bit");

                    b.Property<DateTime>("ReactedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("CommentId");

                    b.HasIndex("UserId");

                    b.ToTable("CommentReactions");

                    b.HasData(
                        new
                        {
                            Id = "1",
                            CommentId = "1",
                            IsLiked = true,
                            ReactedAt = new DateTime(2022, 2, 2, 17, 49, 27, 797, DateTimeKind.Local).AddTicks(8734),
                            UserId = "5"
                        },
                        new
                        {
                            Id = "2",
                            CommentId = "2",
                            IsLiked = true,
                            ReactedAt = new DateTime(2022, 5, 5, 15, 49, 27, 797, DateTimeKind.Local).AddTicks(9621),
                            UserId = "4"
                        },
                        new
                        {
                            Id = "3",
                            CommentId = "3",
                            IsLiked = false,
                            ReactedAt = new DateTime(2022, 8, 2, 15, 49, 27, 797, DateTimeKind.Local).AddTicks(9669),
                            UserId = "1"
                        });
                });

            modelBuilder.Entity("InternetForum.DAL.DomainModels.Post", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Header")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<int>("PostTopic")
                        .HasColumnType("int");

                    b.Property<string>("Text")
                        .HasColumnType("nvarchar(150)")
                        .HasMaxLength(150);

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Posts");

                    b.HasData(
                        new
                        {
                            Id = "1",
                            CreatedAt = new DateTime(2022, 2, 2, 15, 49, 27, 786, DateTimeKind.Local).AddTicks(1313),
                            Header = "Summer holidays",
                            PostTopic = 4,
                            Text = "Tell about your best summer holidays",
                            UpdatedAt = new DateTime(2022, 2, 2, 15, 59, 27, 793, DateTimeKind.Local).AddTicks(4791),
                            UserId = "1"
                        },
                        new
                        {
                            Id = "2",
                            CreatedAt = new DateTime(2022, 5, 3, 15, 49, 27, 793, DateTimeKind.Local).AddTicks(8077),
                            Header = "Winter holidays",
                            PostTopic = 4,
                            Text = "Tell about your best winter holidays",
                            UserId = "3"
                        },
                        new
                        {
                            Id = "3",
                            CreatedAt = new DateTime(2022, 8, 1, 15, 49, 27, 793, DateTimeKind.Local).AddTicks(8223),
                            Header = "Autumn holidays",
                            PostTopic = 4,
                            Text = "Tell about your best Autumn holidays",
                            UserId = "5"
                        },
                        new
                        {
                            Id = "4",
                            CreatedAt = new DateTime(2022, 8, 3, 15, 49, 27, 793, DateTimeKind.Local).AddTicks(8236),
                            Header = "Test post for deleting",
                            PostTopic = 5,
                            Text = "Test text",
                            UserId = "4"
                        },
                        new
                        {
                            Id = "5",
                            CreatedAt = new DateTime(2022, 8, 6, 15, 49, 27, 793, DateTimeKind.Local).AddTicks(8244),
                            Header = "Test post for updating",
                            PostTopic = 5,
                            Text = "Test text",
                            UserId = "2"
                        });
                });

            modelBuilder.Entity("InternetForum.DAL.DomainModels.PostReaction", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("IsLiked")
                        .HasColumnType("bit");

                    b.Property<string>("PostId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("ReactedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("PostId");

                    b.HasIndex("UserId");

                    b.ToTable("PostReactions");

                    b.HasData(
                        new
                        {
                            Id = "1",
                            IsLiked = true,
                            PostId = "1",
                            ReactedAt = new DateTime(2022, 2, 2, 16, 49, 27, 796, DateTimeKind.Local).AddTicks(9218),
                            UserId = "1"
                        },
                        new
                        {
                            Id = "2",
                            IsLiked = false,
                            PostId = "1",
                            ReactedAt = new DateTime(2022, 2, 2, 16, 49, 27, 797, DateTimeKind.Local).AddTicks(289),
                            UserId = "2"
                        },
                        new
                        {
                            Id = "3",
                            IsLiked = true,
                            PostId = "2",
                            ReactedAt = new DateTime(2022, 5, 4, 15, 49, 27, 797, DateTimeKind.Local).AddTicks(343),
                            UserId = "3"
                        },
                        new
                        {
                            Id = "4",
                            IsLiked = true,
                            PostId = "3",
                            ReactedAt = new DateTime(2022, 8, 2, 15, 49, 27, 797, DateTimeKind.Local).AddTicks(359),
                            UserId = "3"
                        },
                        new
                        {
                            Id = "5",
                            IsLiked = false,
                            PostId = "3",
                            ReactedAt = new DateTime(2022, 8, 2, 15, 49, 27, 797, DateTimeKind.Local).AddTicks(379),
                            UserId = "4"
                        },
                        new
                        {
                            Id = "6",
                            IsLiked = true,
                            PostId = "2",
                            ReactedAt = new DateTime(2022, 4, 24, 15, 49, 27, 797, DateTimeKind.Local).AddTicks(399),
                            UserId = "4"
                        },
                        new
                        {
                            Id = "7",
                            IsLiked = false,
                            PostId = "1",
                            ReactedAt = new DateTime(2022, 2, 2, 16, 49, 27, 797, DateTimeKind.Local).AddTicks(418),
                            UserId = "5"
                        },
                        new
                        {
                            Id = "8",
                            IsLiked = true,
                            PostId = "1",
                            ReactedAt = new DateTime(2022, 2, 2, 16, 49, 27, 797, DateTimeKind.Local).AddTicks(428),
                            UserId = "4"
                        },
                        new
                        {
                            Id = "9",
                            IsLiked = false,
                            PostId = "3",
                            ReactedAt = new DateTime(2022, 8, 2, 15, 49, 27, 797, DateTimeKind.Local).AddTicks(434),
                            UserId = "5"
                        },
                        new
                        {
                            Id = "10",
                            IsLiked = false,
                            PostId = "2",
                            ReactedAt = new DateTime(2022, 5, 4, 15, 49, 27, 797, DateTimeKind.Local).AddTicks(441),
                            UserId = "2"
                        });
                });

            modelBuilder.Entity("InternetForum.DAL.DomainModels.Question", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("IsAllowedMultiple")
                        .HasColumnType("bit");

                    b.Property<bool>("IsRequired")
                        .HasColumnType("bit");

                    b.Property<string>("QuestionnaireId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("nvarchar(40)")
                        .HasMaxLength(40);

                    b.HasKey("Id");

                    b.HasIndex("QuestionnaireId");

                    b.ToTable("Questions");

                    b.HasData(
                        new
                        {
                            Id = "1",
                            IsAllowedMultiple = false,
                            IsRequired = true,
                            QuestionnaireId = "1",
                            Text = "Is Summer the best time of year?"
                        },
                        new
                        {
                            Id = "2",
                            IsAllowedMultiple = true,
                            IsRequired = true,
                            QuestionnaireId = "1",
                            Text = "What you best time of year?"
                        },
                        new
                        {
                            Id = "3",
                            IsAllowedMultiple = true,
                            IsRequired = false,
                            QuestionnaireId = "1",
                            Text = "What is your favorite activity?"
                        },
                        new
                        {
                            Id = "4",
                            IsAllowedMultiple = false,
                            IsRequired = false,
                            QuestionnaireId = "2",
                            Text = "Test question"
                        });
                });

            modelBuilder.Entity("InternetForum.DAL.DomainModels.Questionnaire", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("AuthorId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime?>("ClosedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("OpenAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.ToTable("Questionnaires");

                    b.HasData(
                        new
                        {
                            Id = "1",
                            AuthorId = "1",
                            OpenAt = new DateTime(2022, 2, 2, 15, 49, 27, 798, DateTimeKind.Local).AddTicks(6842),
                            Title = "Best Time Of Year"
                        },
                        new
                        {
                            Id = "2",
                            AuthorId = "3",
                            ClosedAt = new DateTime(2022, 2, 5, 15, 49, 27, 798, DateTimeKind.Local).AddTicks(8274),
                            OpenAt = new DateTime(2022, 2, 2, 15, 49, 27, 798, DateTimeKind.Local).AddTicks(8327),
                            Title = "Test questionnaire"
                        });
                });

            modelBuilder.Entity("InternetForum.DAL.DomainModels.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int?>("Age")
                        .HasColumnType("int");

                    b.Property<string>("Avatar")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Bio")
                        .HasColumnType("nvarchar(200)")
                        .HasMaxLength(200);

                    b.Property<DateTime?>("BirthDay")
                        .HasColumnType("datetime2");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(35)")
                        .HasMaxLength(35);

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(35)")
                        .HasMaxLength(35);

                    b.Property<DateTime>("RegisteredAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(30)")
                        .HasMaxLength(30);

                    b.HasKey("Id");

                    b.HasIndex("UserName")
                        .IsUnique();

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = "1",
                            Bio = "Electrical Engineer",
                            BirthDay = new DateTime(1990, 5, 4, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            FirstName = "Anton",
                            LastName = "Gerashenko",
                            RegisteredAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            UserName = "anton_1990"
                        },
                        new
                        {
                            Id = "2",
                            Bio = "18 years",
                            FirstName = "Dmitro",
                            RegisteredAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            UserName = "dmidro"
                        },
                        new
                        {
                            Id = "3",
                            BirthDay = new DateTime(2000, 10, 24, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            RegisteredAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            UserName = "user1984"
                        },
                        new
                        {
                            Id = "4",
                            Bio = "The best chef in Iceland",
                            FirstName = "bad",
                            RegisteredAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            UserName = "Have_A_Nice_Day"
                        },
                        new
                        {
                            Id = "5",
                            BirthDay = new DateTime(1999, 8, 11, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            RegisteredAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            UserName = "mike_2002"
                        });
                });

            modelBuilder.Entity("InternetForum.DAL.DomainModels.Answer", b =>
                {
                    b.HasOne("InternetForum.DAL.DomainModels.Question", "Question")
                        .WithMany("Answers")
                        .HasForeignKey("QuestionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("InternetForum.DAL.DomainModels.AnswerUser", b =>
                {
                    b.HasOne("InternetForum.DAL.DomainModels.Answer", "Answer")
                        .WithMany("Users")
                        .HasForeignKey("AnswerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("InternetForum.DAL.DomainModels.User", "User")
                        .WithMany("Answers")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("InternetForum.DAL.DomainModels.Comment", b =>
                {
                    b.HasOne("InternetForum.DAL.DomainModels.Post", "Post")
                        .WithMany("Comments")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("InternetForum.DAL.DomainModels.User", "User")
                        .WithMany("Comments")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();
                });

            modelBuilder.Entity("InternetForum.DAL.DomainModels.CommentReaction", b =>
                {
                    b.HasOne("InternetForum.DAL.DomainModels.Comment", "Comment")
                        .WithMany("Reactions")
                        .HasForeignKey("CommentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("InternetForum.DAL.DomainModels.User", "User")
                        .WithMany("CommentReactions")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();
                });

            modelBuilder.Entity("InternetForum.DAL.DomainModels.Post", b =>
                {
                    b.HasOne("InternetForum.DAL.DomainModels.User", "User")
                        .WithMany("Posts")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();
                });

            modelBuilder.Entity("InternetForum.DAL.DomainModels.PostReaction", b =>
                {
                    b.HasOne("InternetForum.DAL.DomainModels.Post", "Post")
                        .WithMany("Reactions")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("InternetForum.DAL.DomainModels.User", "User")
                        .WithMany("PostReactions")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();
                });

            modelBuilder.Entity("InternetForum.DAL.DomainModels.Question", b =>
                {
                    b.HasOne("InternetForum.DAL.DomainModels.Questionnaire", "Questionnaire")
                        .WithMany("Questions")
                        .HasForeignKey("QuestionnaireId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("InternetForum.DAL.DomainModels.Questionnaire", b =>
                {
                    b.HasOne("InternetForum.DAL.DomainModels.User", "Author")
                        .WithMany("Questionnaires")
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
