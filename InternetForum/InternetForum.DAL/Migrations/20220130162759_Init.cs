using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace InternetForum.DAL.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Avatar = table.Column<string>(nullable: true),
                    Age = table.Column<int>(nullable: true),
                    UserName = table.Column<string>(maxLength: 30, nullable: false),
                    Bio = table.Column<string>(maxLength: 200, nullable: true),
                    FirstName = table.Column<string>(maxLength: 35, nullable: true),
                    LastName = table.Column<string>(maxLength: 35, nullable: true),
                    RegisteredAt = table.Column<DateTime>(nullable: false),
                    BirthDay = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Posts",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserId = table.Column<string>(nullable: false),
                    Header = table.Column<string>(maxLength: 50, nullable: false),
                    Text = table.Column<string>(maxLength: 150, nullable: true),
                    PostTopic = table.Column<int>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Posts_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    CommentText = table.Column<string>(maxLength: 140, nullable: false),
                    PostId = table.Column<string>(nullable: false),
                    CommentId = table.Column<string>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comments_Posts_PostId",
                        column: x => x.PostId,
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Comments_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PostReactions",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    PostId = table.Column<string>(nullable: false),
                    UserId = table.Column<string>(nullable: false),
                    IsLiked = table.Column<bool>(nullable: false),
                    ReactedAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostReactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PostReactions_Posts_PostId",
                        column: x => x.PostId,
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PostReactions_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CommentReactions",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    CommentId = table.Column<string>(nullable: false),
                    UserId = table.Column<string>(nullable: false),
                    IsLiked = table.Column<bool>(nullable: false),
                    ReactedAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommentReactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CommentReactions_Comments_CommentId",
                        column: x => x.CommentId,
                        principalTable: "Comments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CommentReactions_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Age", "Avatar", "Bio", "BirthDay", "FirstName", "LastName", "RegisteredAt", "UserName" },
                values: new object[,]
                {
                    { "1", null, null, "Electrical Engineer", new DateTime(1990, 5, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "Anton", "Gerashenko", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "anton_1990" },
                    { "2", null, null, "18 years", null, "Dmitro", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "dmidro" },
                    { "3", null, null, null, new DateTime(2000, 10, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "user1984" },
                    { "4", null, null, "The best chef in Iceland", null, "bad", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Have_A_Nice_Day" },
                    { "5", null, null, null, new DateTime(1999, 8, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "mike_2002" }
                });

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "Id", "CreatedAt", "Header", "PostTopic", "Text", "UpdatedAt", "UserId" },
                values: new object[] { "1", new DateTime(2022, 1, 30, 18, 27, 58, 782, DateTimeKind.Local).AddTicks(4332), "Summer holidays", 4, "Tell about your best summer holidays", new DateTime(2022, 1, 30, 18, 37, 58, 785, DateTimeKind.Local).AddTicks(8825), "1" });

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "Id", "CreatedAt", "Header", "PostTopic", "Text", "UpdatedAt", "UserId" },
                values: new object[] { "2", new DateTime(2022, 4, 30, 18, 27, 58, 786, DateTimeKind.Local).AddTicks(605), "Winter holidays", 4, "Tell about your best winter holidays", null, "3" });

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "Id", "CreatedAt", "Header", "PostTopic", "Text", "UpdatedAt", "UserId" },
                values: new object[] { "3", new DateTime(2022, 7, 29, 18, 27, 58, 786, DateTimeKind.Local).AddTicks(688), "Autumn holidays", 4, "Tell about your best Autumn holidays", null, "5" });

            migrationBuilder.InsertData(
                table: "Comments",
                columns: new[] { "Id", "CommentId", "CommentText", "CreatedAt", "PostId", "UserId" },
                values: new object[,]
                {
                    { "1", null, "My last summer holidays was the best", new DateTime(2022, 1, 30, 19, 27, 58, 786, DateTimeKind.Local).AddTicks(6537), "1", "1" },
                    { "4", "1", "My last summer holidays was the best too. Thank you!", new DateTime(2022, 1, 30, 19, 27, 58, 786, DateTimeKind.Local).AddTicks(7538), "1", "5" },
                    { "2", null, "My last winter holidays was the best", new DateTime(2022, 5, 1, 18, 27, 58, 786, DateTimeKind.Local).AddTicks(7491), "2", "2" },
                    { "5", "2", "My last winter holidays was the best too. It was good time", new DateTime(2022, 5, 1, 18, 27, 58, 786, DateTimeKind.Local).AddTicks(7543), "2", "4" },
                    { "6", null, "My last winter holidays was the best too. It was the best time", new DateTime(2022, 5, 1, 18, 27, 58, 786, DateTimeKind.Local).AddTicks(7548), "2", "1" },
                    { "3", null, "My last autumn holidays was the best", new DateTime(2022, 7, 30, 18, 27, 58, 786, DateTimeKind.Local).AddTicks(7533), "3", "3" }
                });

            migrationBuilder.InsertData(
                table: "PostReactions",
                columns: new[] { "Id", "IsLiked", "PostId", "ReactedAt", "UserId" },
                values: new object[,]
                {
                    { "1", true, "1", new DateTime(2022, 1, 30, 19, 27, 58, 787, DateTimeKind.Local).AddTicks(3904), "1" },
                    { "2", false, "1", new DateTime(2022, 1, 30, 19, 27, 58, 787, DateTimeKind.Local).AddTicks(4290), "2" },
                    { "7", false, "1", new DateTime(2022, 1, 30, 19, 27, 58, 787, DateTimeKind.Local).AddTicks(4332), "5" },
                    { "8", true, "1", new DateTime(2022, 1, 30, 19, 27, 58, 787, DateTimeKind.Local).AddTicks(4337), "4" },
                    { "3", true, "2", new DateTime(2022, 5, 1, 18, 27, 58, 787, DateTimeKind.Local).AddTicks(4314), "3" },
                    { "6", true, "2", new DateTime(2022, 4, 21, 18, 27, 58, 787, DateTimeKind.Local).AddTicks(4328), "4" },
                    { "10", false, "2", new DateTime(2022, 5, 1, 18, 27, 58, 787, DateTimeKind.Local).AddTicks(4346), "2" },
                    { "4", true, "3", new DateTime(2022, 7, 30, 18, 27, 58, 787, DateTimeKind.Local).AddTicks(4319), "3" },
                    { "5", false, "3", new DateTime(2022, 7, 30, 18, 27, 58, 787, DateTimeKind.Local).AddTicks(4323), "4" },
                    { "9", false, "3", new DateTime(2022, 7, 30, 18, 27, 58, 787, DateTimeKind.Local).AddTicks(4342), "5" }
                });

            migrationBuilder.InsertData(
                table: "CommentReactions",
                columns: new[] { "Id", "CommentId", "IsLiked", "ReactedAt", "UserId" },
                values: new object[] { "1", "1", true, new DateTime(2022, 1, 30, 20, 27, 58, 787, DateTimeKind.Local).AddTicks(8125), "5" });

            migrationBuilder.InsertData(
                table: "CommentReactions",
                columns: new[] { "Id", "CommentId", "IsLiked", "ReactedAt", "UserId" },
                values: new object[] { "2", "2", true, new DateTime(2022, 5, 2, 18, 27, 58, 787, DateTimeKind.Local).AddTicks(8504), "4" });

            migrationBuilder.InsertData(
                table: "CommentReactions",
                columns: new[] { "Id", "CommentId", "IsLiked", "ReactedAt", "UserId" },
                values: new object[] { "3", "3", false, new DateTime(2022, 7, 30, 18, 27, 58, 787, DateTimeKind.Local).AddTicks(8528), "1" });

            migrationBuilder.CreateIndex(
                name: "IX_CommentReactions_CommentId",
                table: "CommentReactions",
                column: "CommentId");

            migrationBuilder.CreateIndex(
                name: "IX_CommentReactions_UserId",
                table: "CommentReactions",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_PostId",
                table: "Comments",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_UserId",
                table: "Comments",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_PostReactions_PostId",
                table: "PostReactions",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_PostReactions_UserId",
                table: "PostReactions",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_UserId",
                table: "Posts",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_UserName",
                table: "Users",
                column: "UserName",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CommentReactions");

            migrationBuilder.DropTable(
                name: "PostReactions");

            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "Posts");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
