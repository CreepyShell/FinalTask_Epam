using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace InternetForum.DAL.Migrations
{
    public partial class AddedQuestionnaire : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Questionnaires",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    AuthorId = table.Column<string>(nullable: false),
                    Title = table.Column<string>(maxLength: 30, nullable: false),
                    OpenAt = table.Column<DateTime>(nullable: false),
                    ClosedAt = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questionnaires", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Questionnaires_Users_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Questions",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    QuestionnaireId = table.Column<string>(nullable: false),
                    Text = table.Column<string>(maxLength: 50, nullable: false),
                    IsAllowedMultiple = table.Column<bool>(nullable: false),
                    IsRequired = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Questions_Questionnaires_QuestionnaireId",
                        column: x => x.QuestionnaireId,
                        principalTable: "Questionnaires",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Answers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    QuestionId = table.Column<string>(nullable: false),
                    Text = table.Column<string>(maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Answers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Answers_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Questions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AnswerUsers",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    AnswerId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnswerUsers", x => new { x.AnswerId, x.UserId });
                    table.ForeignKey(
                        name: "FK_AnswerUsers_Answers_AnswerId",
                        column: x => x.AnswerId,
                        principalTable: "Answers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AnswerUsers_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "CommentReactions",
                keyColumn: "Id",
                keyValue: "1",
                column: "ReactedAt",
                value: new DateTime(2022, 1, 25, 23, 39, 21, 216, DateTimeKind.Local).AddTicks(5508));

            migrationBuilder.UpdateData(
                table: "CommentReactions",
                keyColumn: "Id",
                keyValue: "2",
                column: "ReactedAt",
                value: new DateTime(2022, 4, 27, 21, 39, 21, 216, DateTimeKind.Local).AddTicks(6070));

            migrationBuilder.UpdateData(
                table: "CommentReactions",
                keyColumn: "Id",
                keyValue: "3",
                column: "ReactedAt",
                value: new DateTime(2022, 7, 25, 21, 39, 21, 216, DateTimeKind.Local).AddTicks(6108));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: "1",
                column: "CreatedAt",
                value: new DateTime(2022, 1, 25, 22, 39, 21, 215, DateTimeKind.Local).AddTicks(9118));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: "2",
                column: "CreatedAt",
                value: new DateTime(2022, 4, 26, 21, 39, 21, 216, DateTimeKind.Local).AddTicks(639));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: "3",
                column: "CreatedAt",
                value: new DateTime(2022, 7, 25, 21, 39, 21, 216, DateTimeKind.Local).AddTicks(723));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: "4",
                column: "CreatedAt",
                value: new DateTime(2022, 1, 25, 22, 39, 21, 216, DateTimeKind.Local).AddTicks(731));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: "5",
                column: "CreatedAt",
                value: new DateTime(2022, 4, 26, 21, 39, 21, 216, DateTimeKind.Local).AddTicks(739));

            migrationBuilder.UpdateData(
                table: "PostReactions",
                keyColumn: "Id",
                keyValue: "1",
                column: "ReactedAt",
                value: new DateTime(2022, 1, 25, 22, 39, 21, 216, DateTimeKind.Local).AddTicks(2770));

            migrationBuilder.UpdateData(
                table: "PostReactions",
                keyColumn: "Id",
                keyValue: "10",
                column: "ReactedAt",
                value: new DateTime(2022, 4, 26, 21, 39, 21, 216, DateTimeKind.Local).AddTicks(3387));

            migrationBuilder.UpdateData(
                table: "PostReactions",
                keyColumn: "Id",
                keyValue: "2",
                column: "ReactedAt",
                value: new DateTime(2022, 1, 25, 22, 39, 21, 216, DateTimeKind.Local).AddTicks(3313));

            migrationBuilder.UpdateData(
                table: "PostReactions",
                keyColumn: "Id",
                keyValue: "3",
                column: "ReactedAt",
                value: new DateTime(2022, 4, 26, 21, 39, 21, 216, DateTimeKind.Local).AddTicks(3346));

            migrationBuilder.UpdateData(
                table: "PostReactions",
                keyColumn: "Id",
                keyValue: "4",
                column: "ReactedAt",
                value: new DateTime(2022, 7, 25, 21, 39, 21, 216, DateTimeKind.Local).AddTicks(3353));

            migrationBuilder.UpdateData(
                table: "PostReactions",
                keyColumn: "Id",
                keyValue: "5",
                column: "ReactedAt",
                value: new DateTime(2022, 7, 25, 21, 39, 21, 216, DateTimeKind.Local).AddTicks(3359));

            migrationBuilder.UpdateData(
                table: "PostReactions",
                keyColumn: "Id",
                keyValue: "6",
                column: "ReactedAt",
                value: new DateTime(2022, 4, 16, 21, 39, 21, 216, DateTimeKind.Local).AddTicks(3364));

            migrationBuilder.UpdateData(
                table: "PostReactions",
                keyColumn: "Id",
                keyValue: "7",
                column: "ReactedAt",
                value: new DateTime(2022, 1, 25, 22, 39, 21, 216, DateTimeKind.Local).AddTicks(3370));

            migrationBuilder.UpdateData(
                table: "PostReactions",
                keyColumn: "Id",
                keyValue: "8",
                column: "ReactedAt",
                value: new DateTime(2022, 1, 25, 22, 39, 21, 216, DateTimeKind.Local).AddTicks(3375));

            migrationBuilder.UpdateData(
                table: "PostReactions",
                keyColumn: "Id",
                keyValue: "9",
                column: "ReactedAt",
                value: new DateTime(2022, 7, 25, 21, 39, 21, 216, DateTimeKind.Local).AddTicks(3381));

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 1, 25, 21, 39, 21, 209, DateTimeKind.Local).AddTicks(9002), new DateTime(2022, 1, 25, 21, 49, 21, 215, DateTimeKind.Local).AddTicks(4160) });

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: "2",
                column: "CreatedAt",
                value: new DateTime(2022, 4, 25, 21, 39, 21, 215, DateTimeKind.Local).AddTicks(6780));

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: "3",
                column: "CreatedAt",
                value: new DateTime(2022, 7, 24, 21, 39, 21, 215, DateTimeKind.Local).AddTicks(6934));

            migrationBuilder.InsertData(
                table: "Questionnaires",
                columns: new[] { "Id", "AuthorId", "ClosedAt", "OpenAt", "Title" },
                values: new object[] { "1", "1s", null, new DateTime(2022, 1, 25, 21, 39, 21, 216, DateTimeKind.Local).AddTicks(8178), "Best Time Of Year" });

            migrationBuilder.InsertData(
                table: "Questions",
                columns: new[] { "Id", "IsAllowedMultiple", "IsRequired", "QuestionnaireId", "Text" },
                values: new object[] { "1", false, true, "1", "Is Summer the best time of year?" });

            migrationBuilder.InsertData(
                table: "Questions",
                columns: new[] { "Id", "IsAllowedMultiple", "IsRequired", "QuestionnaireId", "Text" },
                values: new object[] { "2", true, true, "1", "What you best time of year?" });

            migrationBuilder.InsertData(
                table: "Questions",
                columns: new[] { "Id", "IsAllowedMultiple", "IsRequired", "QuestionnaireId", "Text" },
                values: new object[] { "3", true, false, "1", "What is your favorite activity?" });

            migrationBuilder.InsertData(
                table: "Answers",
                columns: new[] { "Id", "QuestionId", "Text" },
                values: new object[,]
                {
                    { "1", "1", "Yes" },
                    { "2", "1", "No" },
                    { "3", "2", "Winter" },
                    { "4", "2", "Summer" },
                    { "5", "3", "Sleeping" }
                });

            migrationBuilder.InsertData(
                table: "AnswerUsers",
                columns: new[] { "AnswerId", "UserId" },
                values: new object[,]
                {
                    { "1", "1s" },
                    { "3", "1s" },
                    { "4", "1s" },
                    { "5", "1s" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Answers_QuestionId",
                table: "Answers",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_AnswerUsers_UserId",
                table: "AnswerUsers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Questionnaires_AuthorId",
                table: "Questionnaires",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Questions_QuestionnaireId",
                table: "Questions",
                column: "QuestionnaireId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnswerUsers");

            migrationBuilder.DropTable(
                name: "Answers");

            migrationBuilder.DropTable(
                name: "Questions");

            migrationBuilder.DropTable(
                name: "Questionnaires");

            migrationBuilder.UpdateData(
                table: "CommentReactions",
                keyColumn: "Id",
                keyValue: "1",
                column: "ReactedAt",
                value: new DateTime(2022, 1, 25, 0, 6, 50, 227, DateTimeKind.Local).AddTicks(3110));

            migrationBuilder.UpdateData(
                table: "CommentReactions",
                keyColumn: "Id",
                keyValue: "2",
                column: "ReactedAt",
                value: new DateTime(2022, 4, 26, 22, 6, 50, 227, DateTimeKind.Local).AddTicks(3452));

            migrationBuilder.UpdateData(
                table: "CommentReactions",
                keyColumn: "Id",
                keyValue: "3",
                column: "ReactedAt",
                value: new DateTime(2022, 7, 24, 22, 6, 50, 227, DateTimeKind.Local).AddTicks(3476));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: "1",
                column: "CreatedAt",
                value: new DateTime(2022, 1, 24, 23, 6, 50, 226, DateTimeKind.Local).AddTicks(8995));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: "2",
                column: "CreatedAt",
                value: new DateTime(2022, 4, 25, 22, 6, 50, 226, DateTimeKind.Local).AddTicks(9840));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: "3",
                column: "CreatedAt",
                value: new DateTime(2022, 7, 24, 22, 6, 50, 226, DateTimeKind.Local).AddTicks(9884));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: "4",
                column: "CreatedAt",
                value: new DateTime(2022, 1, 24, 23, 6, 50, 226, DateTimeKind.Local).AddTicks(9889));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: "5",
                column: "CreatedAt",
                value: new DateTime(2022, 4, 25, 22, 6, 50, 226, DateTimeKind.Local).AddTicks(9893));

            migrationBuilder.UpdateData(
                table: "PostReactions",
                keyColumn: "Id",
                keyValue: "1",
                column: "ReactedAt",
                value: new DateTime(2022, 1, 24, 23, 6, 50, 227, DateTimeKind.Local).AddTicks(1242));

            migrationBuilder.UpdateData(
                table: "PostReactions",
                keyColumn: "Id",
                keyValue: "10",
                column: "ReactedAt",
                value: new DateTime(2022, 4, 25, 22, 6, 50, 227, DateTimeKind.Local).AddTicks(1740));

            migrationBuilder.UpdateData(
                table: "PostReactions",
                keyColumn: "Id",
                keyValue: "2",
                column: "ReactedAt",
                value: new DateTime(2022, 1, 24, 23, 6, 50, 227, DateTimeKind.Local).AddTicks(1607));

            migrationBuilder.UpdateData(
                table: "PostReactions",
                keyColumn: "Id",
                keyValue: "3",
                column: "ReactedAt",
                value: new DateTime(2022, 4, 25, 22, 6, 50, 227, DateTimeKind.Local).AddTicks(1628));

            migrationBuilder.UpdateData(
                table: "PostReactions",
                keyColumn: "Id",
                keyValue: "4",
                column: "ReactedAt",
                value: new DateTime(2022, 7, 24, 22, 6, 50, 227, DateTimeKind.Local).AddTicks(1632));

            migrationBuilder.UpdateData(
                table: "PostReactions",
                keyColumn: "Id",
                keyValue: "5",
                column: "ReactedAt",
                value: new DateTime(2022, 7, 24, 22, 6, 50, 227, DateTimeKind.Local).AddTicks(1718));

            migrationBuilder.UpdateData(
                table: "PostReactions",
                keyColumn: "Id",
                keyValue: "6",
                column: "ReactedAt",
                value: new DateTime(2022, 4, 15, 22, 6, 50, 227, DateTimeKind.Local).AddTicks(1724));

            migrationBuilder.UpdateData(
                table: "PostReactions",
                keyColumn: "Id",
                keyValue: "7",
                column: "ReactedAt",
                value: new DateTime(2022, 1, 24, 23, 6, 50, 227, DateTimeKind.Local).AddTicks(1728));

            migrationBuilder.UpdateData(
                table: "PostReactions",
                keyColumn: "Id",
                keyValue: "8",
                column: "ReactedAt",
                value: new DateTime(2022, 1, 24, 23, 6, 50, 227, DateTimeKind.Local).AddTicks(1732));

            migrationBuilder.UpdateData(
                table: "PostReactions",
                keyColumn: "Id",
                keyValue: "9",
                column: "ReactedAt",
                value: new DateTime(2022, 7, 24, 22, 6, 50, 227, DateTimeKind.Local).AddTicks(1736));

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 1, 24, 22, 6, 50, 223, DateTimeKind.Local).AddTicks(5139), new DateTime(2022, 1, 24, 22, 16, 50, 226, DateTimeKind.Local).AddTicks(5685) });

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: "2",
                column: "CreatedAt",
                value: new DateTime(2022, 4, 24, 22, 6, 50, 226, DateTimeKind.Local).AddTicks(7412));

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: "3",
                column: "CreatedAt",
                value: new DateTime(2022, 7, 23, 22, 6, 50, 226, DateTimeKind.Local).AddTicks(7506));
        }
    }
}
