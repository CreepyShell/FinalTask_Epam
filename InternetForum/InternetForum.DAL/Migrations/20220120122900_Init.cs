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
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Avatar = table.Column<string>(nullable: true),
                    UserName = table.Column<string>(maxLength: 20, nullable: false),
                    Email = table.Column<string>(nullable: false),
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
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(nullable: false),
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
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CommentText = table.Column<string>(maxLength: 100, nullable: false),
                    PostId = table.Column<int>(nullable: false),
                    CommentId = table.Column<int>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UserId = table.Column<int>(nullable: false)
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
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PostId = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false),
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
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CommentId = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false),
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
                columns: new[] { "Id", "Avatar", "BirthDay", "Email", "RegisteredAt", "UserName" },
                values: new object[,]
                {
                    { 1, null, new DateTime(1990, 5, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "anton@gmail.com", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "anton_1990" },
                    { 2, null, null, "dmitro_kovalcuk@gmail.com", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "dmidro" },
                    { 3, null, new DateTime(2000, 10, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), "My_mail84@gmail.com", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "user1984" },
                    { 4, null, null, "GoodLuck11@gmail.com", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Have_A_Nice_Day" },
                    { 5, null, new DateTime(1999, 8, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "t_mike2002_11@gmail.com", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "mike_2002" }
                });

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "Id", "CreatedAt", "Header", "PostTopic", "Text", "UpdatedAt", "UserId" },
                values: new object[] { 1, new DateTime(2022, 1, 20, 14, 28, 59, 676, DateTimeKind.Local).AddTicks(3811), "Summer holidays", 3, "Tell about your best summer holidays", new DateTime(2022, 1, 20, 14, 38, 59, 683, DateTimeKind.Local).AddTicks(1060), 1 });

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "Id", "CreatedAt", "Header", "PostTopic", "Text", "UpdatedAt", "UserId" },
                values: new object[] { 2, new DateTime(2022, 4, 20, 14, 28, 59, 683, DateTimeKind.Local).AddTicks(5292), "Winter holidays", 3, "Tell about your best winter holidays", null, 3 });

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "Id", "CreatedAt", "Header", "PostTopic", "Text", "UpdatedAt", "UserId" },
                values: new object[] { 3, new DateTime(2022, 7, 19, 14, 28, 59, 683, DateTimeKind.Local).AddTicks(5502), "Autumn holidays", 3, "Tell about your best Autumn holidays", null, 5 });

            migrationBuilder.InsertData(
                table: "Comments",
                columns: new[] { "Id", "CommentId", "CommentText", "CreatedAt", "PostId", "UserId" },
                values: new object[,]
                {
                    { 1, null, "My last summer holidays was the best", new DateTime(2022, 1, 20, 15, 28, 59, 683, DateTimeKind.Local).AddTicks(8783), 1, 1 },
                    { 4, 1, "My last summer holidays was the best too. Thank you!", new DateTime(2022, 1, 20, 15, 28, 59, 684, DateTimeKind.Local).AddTicks(1297), 1, 5 },
                    { 2, null, "My last winter holidays was the best", new DateTime(2022, 4, 21, 14, 28, 59, 684, DateTimeKind.Local).AddTicks(1201), 2, 2 },
                    { 5, 2, "My last winter holidays was the best too. It was good time", new DateTime(2022, 4, 21, 14, 28, 59, 684, DateTimeKind.Local).AddTicks(1308), 2, 4 },
                    { 3, null, "My last autumn holidays was the best", new DateTime(2022, 7, 20, 14, 28, 59, 684, DateTimeKind.Local).AddTicks(1285), 3, 3 }
                });

            migrationBuilder.InsertData(
                table: "PostReactions",
                columns: new[] { "Id", "IsLiked", "PostId", "ReactedAt", "UserId" },
                values: new object[,]
                {
                    { 1, true, 1, new DateTime(2022, 1, 20, 15, 28, 59, 684, DateTimeKind.Local).AddTicks(4709), 1 },
                    { 2, false, 1, new DateTime(2022, 1, 20, 15, 28, 59, 684, DateTimeKind.Local).AddTicks(5584), 2 },
                    { 7, false, 1, new DateTime(2022, 1, 20, 15, 28, 59, 684, DateTimeKind.Local).AddTicks(5660), 5 },
                    { 8, true, 1, new DateTime(2022, 1, 20, 15, 28, 59, 684, DateTimeKind.Local).AddTicks(5666), 4 },
                    { 3, true, 2, new DateTime(2022, 4, 21, 14, 28, 59, 684, DateTimeKind.Local).AddTicks(5629), 3 },
                    { 6, true, 2, new DateTime(2022, 4, 11, 14, 28, 59, 684, DateTimeKind.Local).AddTicks(5652), 4 },
                    { 10, false, 2, new DateTime(2022, 4, 21, 14, 28, 59, 684, DateTimeKind.Local).AddTicks(5681), 2 },
                    { 4, true, 3, new DateTime(2022, 7, 20, 14, 28, 59, 684, DateTimeKind.Local).AddTicks(5638), 3 },
                    { 5, false, 3, new DateTime(2022, 7, 20, 14, 28, 59, 684, DateTimeKind.Local).AddTicks(5645), 4 },
                    { 9, false, 3, new DateTime(2022, 7, 20, 14, 28, 59, 684, DateTimeKind.Local).AddTicks(5673), 5 }
                });

            migrationBuilder.InsertData(
                table: "CommentReactions",
                columns: new[] { "Id", "CommentId", "IsLiked", "ReactedAt", "UserId" },
                values: new object[] { 1, 1, true, new DateTime(2022, 1, 20, 16, 28, 59, 684, DateTimeKind.Local).AddTicks(9035), 5 });

            migrationBuilder.InsertData(
                table: "CommentReactions",
                columns: new[] { "Id", "CommentId", "IsLiked", "ReactedAt", "UserId" },
                values: new object[] { 2, 2, true, new DateTime(2022, 4, 22, 14, 28, 59, 684, DateTimeKind.Local).AddTicks(9975), 4 });

            migrationBuilder.InsertData(
                table: "CommentReactions",
                columns: new[] { "Id", "CommentId", "IsLiked", "ReactedAt", "UserId" },
                values: new object[] { 3, 3, false, new DateTime(2022, 7, 20, 14, 28, 59, 685, DateTimeKind.Local).AddTicks(20), 1 });

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
