using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace InternetForum.DAL.Migrations
{
    public partial class AddedQuestionnaires : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Questionnaires",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    AuthorId = table.Column<string>(nullable: false),
                    Title = table.Column<string>(maxLength: 50, nullable: false),
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
                    Text = table.Column<string>(maxLength: 40, nullable: false),
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
                value: new DateTime(2022, 1, 30, 20, 36, 45, 549, DateTimeKind.Local).AddTicks(4791));

            migrationBuilder.UpdateData(
                table: "CommentReactions",
                keyColumn: "Id",
                keyValue: "2",
                column: "ReactedAt",
                value: new DateTime(2022, 5, 2, 18, 36, 45, 549, DateTimeKind.Local).AddTicks(5236));

            migrationBuilder.UpdateData(
                table: "CommentReactions",
                keyColumn: "Id",
                keyValue: "3",
                column: "ReactedAt",
                value: new DateTime(2022, 7, 30, 18, 36, 45, 549, DateTimeKind.Local).AddTicks(5259));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: "1",
                column: "CreatedAt",
                value: new DateTime(2022, 1, 30, 19, 36, 45, 547, DateTimeKind.Local).AddTicks(6190));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: "2",
                column: "CreatedAt",
                value: new DateTime(2022, 5, 1, 18, 36, 45, 547, DateTimeKind.Local).AddTicks(7837));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: "3",
                column: "CreatedAt",
                value: new DateTime(2022, 7, 30, 18, 36, 45, 547, DateTimeKind.Local).AddTicks(7908));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: "4",
                column: "CreatedAt",
                value: new DateTime(2022, 1, 30, 19, 36, 45, 547, DateTimeKind.Local).AddTicks(7916));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: "5",
                column: "CreatedAt",
                value: new DateTime(2022, 5, 1, 18, 36, 45, 547, DateTimeKind.Local).AddTicks(7929));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: "6",
                column: "CreatedAt",
                value: new DateTime(2022, 5, 1, 18, 36, 45, 547, DateTimeKind.Local).AddTicks(7941));

            migrationBuilder.UpdateData(
                table: "PostReactions",
                keyColumn: "Id",
                keyValue: "1",
                column: "ReactedAt",
                value: new DateTime(2022, 1, 30, 19, 36, 45, 548, DateTimeKind.Local).AddTicks(8176));

            migrationBuilder.UpdateData(
                table: "PostReactions",
                keyColumn: "Id",
                keyValue: "10",
                column: "ReactedAt",
                value: new DateTime(2022, 5, 1, 18, 36, 45, 548, DateTimeKind.Local).AddTicks(8695));

            migrationBuilder.UpdateData(
                table: "PostReactions",
                keyColumn: "Id",
                keyValue: "2",
                column: "ReactedAt",
                value: new DateTime(2022, 1, 30, 19, 36, 45, 548, DateTimeKind.Local).AddTicks(8613));

            migrationBuilder.UpdateData(
                table: "PostReactions",
                keyColumn: "Id",
                keyValue: "3",
                column: "ReactedAt",
                value: new DateTime(2022, 5, 1, 18, 36, 45, 548, DateTimeKind.Local).AddTicks(8639));

            migrationBuilder.UpdateData(
                table: "PostReactions",
                keyColumn: "Id",
                keyValue: "4",
                column: "ReactedAt",
                value: new DateTime(2022, 7, 30, 18, 36, 45, 548, DateTimeKind.Local).AddTicks(8643));

            migrationBuilder.UpdateData(
                table: "PostReactions",
                keyColumn: "Id",
                keyValue: "5",
                column: "ReactedAt",
                value: new DateTime(2022, 7, 30, 18, 36, 45, 548, DateTimeKind.Local).AddTicks(8648));

            migrationBuilder.UpdateData(
                table: "PostReactions",
                keyColumn: "Id",
                keyValue: "6",
                column: "ReactedAt",
                value: new DateTime(2022, 4, 21, 18, 36, 45, 548, DateTimeKind.Local).AddTicks(8653));

            migrationBuilder.UpdateData(
                table: "PostReactions",
                keyColumn: "Id",
                keyValue: "7",
                column: "ReactedAt",
                value: new DateTime(2022, 1, 30, 19, 36, 45, 548, DateTimeKind.Local).AddTicks(8682));

            migrationBuilder.UpdateData(
                table: "PostReactions",
                keyColumn: "Id",
                keyValue: "8",
                column: "ReactedAt",
                value: new DateTime(2022, 1, 30, 19, 36, 45, 548, DateTimeKind.Local).AddTicks(8686));

            migrationBuilder.UpdateData(
                table: "PostReactions",
                keyColumn: "Id",
                keyValue: "9",
                column: "ReactedAt",
                value: new DateTime(2022, 7, 30, 18, 36, 45, 548, DateTimeKind.Local).AddTicks(8691));

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 1, 30, 18, 36, 45, 541, DateTimeKind.Local).AddTicks(9155), new DateTime(2022, 1, 30, 18, 46, 45, 546, DateTimeKind.Local).AddTicks(2278) });

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: "2",
                column: "CreatedAt",
                value: new DateTime(2022, 4, 30, 18, 36, 45, 546, DateTimeKind.Local).AddTicks(5429));

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: "3",
                column: "CreatedAt",
                value: new DateTime(2022, 7, 29, 18, 36, 45, 546, DateTimeKind.Local).AddTicks(5622));

            migrationBuilder.InsertData(
                table: "Questionnaires",
                columns: new[] { "Id", "AuthorId", "ClosedAt", "OpenAt", "Title" },
                values: new object[] { "1", "1", null, new DateTime(2022, 1, 30, 18, 36, 45, 549, DateTimeKind.Local).AddTicks(8063), "Best Time Of Year" });

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
                    { "1", "1" },
                    { "1", "2" },
                    { "1", "3" },
                    { "3", "1" },
                    { "4", "1" },
                    { "5", "1" }
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
                value: new DateTime(2022, 1, 30, 20, 27, 58, 787, DateTimeKind.Local).AddTicks(8125));

            migrationBuilder.UpdateData(
                table: "CommentReactions",
                keyColumn: "Id",
                keyValue: "2",
                column: "ReactedAt",
                value: new DateTime(2022, 5, 2, 18, 27, 58, 787, DateTimeKind.Local).AddTicks(8504));

            migrationBuilder.UpdateData(
                table: "CommentReactions",
                keyColumn: "Id",
                keyValue: "3",
                column: "ReactedAt",
                value: new DateTime(2022, 7, 30, 18, 27, 58, 787, DateTimeKind.Local).AddTicks(8528));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: "1",
                column: "CreatedAt",
                value: new DateTime(2022, 1, 30, 19, 27, 58, 786, DateTimeKind.Local).AddTicks(6537));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: "2",
                column: "CreatedAt",
                value: new DateTime(2022, 5, 1, 18, 27, 58, 786, DateTimeKind.Local).AddTicks(7491));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: "3",
                column: "CreatedAt",
                value: new DateTime(2022, 7, 30, 18, 27, 58, 786, DateTimeKind.Local).AddTicks(7533));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: "4",
                column: "CreatedAt",
                value: new DateTime(2022, 1, 30, 19, 27, 58, 786, DateTimeKind.Local).AddTicks(7538));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: "5",
                column: "CreatedAt",
                value: new DateTime(2022, 5, 1, 18, 27, 58, 786, DateTimeKind.Local).AddTicks(7543));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: "6",
                column: "CreatedAt",
                value: new DateTime(2022, 5, 1, 18, 27, 58, 786, DateTimeKind.Local).AddTicks(7548));

            migrationBuilder.UpdateData(
                table: "PostReactions",
                keyColumn: "Id",
                keyValue: "1",
                column: "ReactedAt",
                value: new DateTime(2022, 1, 30, 19, 27, 58, 787, DateTimeKind.Local).AddTicks(3904));

            migrationBuilder.UpdateData(
                table: "PostReactions",
                keyColumn: "Id",
                keyValue: "10",
                column: "ReactedAt",
                value: new DateTime(2022, 5, 1, 18, 27, 58, 787, DateTimeKind.Local).AddTicks(4346));

            migrationBuilder.UpdateData(
                table: "PostReactions",
                keyColumn: "Id",
                keyValue: "2",
                column: "ReactedAt",
                value: new DateTime(2022, 1, 30, 19, 27, 58, 787, DateTimeKind.Local).AddTicks(4290));

            migrationBuilder.UpdateData(
                table: "PostReactions",
                keyColumn: "Id",
                keyValue: "3",
                column: "ReactedAt",
                value: new DateTime(2022, 5, 1, 18, 27, 58, 787, DateTimeKind.Local).AddTicks(4314));

            migrationBuilder.UpdateData(
                table: "PostReactions",
                keyColumn: "Id",
                keyValue: "4",
                column: "ReactedAt",
                value: new DateTime(2022, 7, 30, 18, 27, 58, 787, DateTimeKind.Local).AddTicks(4319));

            migrationBuilder.UpdateData(
                table: "PostReactions",
                keyColumn: "Id",
                keyValue: "5",
                column: "ReactedAt",
                value: new DateTime(2022, 7, 30, 18, 27, 58, 787, DateTimeKind.Local).AddTicks(4323));

            migrationBuilder.UpdateData(
                table: "PostReactions",
                keyColumn: "Id",
                keyValue: "6",
                column: "ReactedAt",
                value: new DateTime(2022, 4, 21, 18, 27, 58, 787, DateTimeKind.Local).AddTicks(4328));

            migrationBuilder.UpdateData(
                table: "PostReactions",
                keyColumn: "Id",
                keyValue: "7",
                column: "ReactedAt",
                value: new DateTime(2022, 1, 30, 19, 27, 58, 787, DateTimeKind.Local).AddTicks(4332));

            migrationBuilder.UpdateData(
                table: "PostReactions",
                keyColumn: "Id",
                keyValue: "8",
                column: "ReactedAt",
                value: new DateTime(2022, 1, 30, 19, 27, 58, 787, DateTimeKind.Local).AddTicks(4337));

            migrationBuilder.UpdateData(
                table: "PostReactions",
                keyColumn: "Id",
                keyValue: "9",
                column: "ReactedAt",
                value: new DateTime(2022, 7, 30, 18, 27, 58, 787, DateTimeKind.Local).AddTicks(4342));

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 1, 30, 18, 27, 58, 782, DateTimeKind.Local).AddTicks(4332), new DateTime(2022, 1, 30, 18, 37, 58, 785, DateTimeKind.Local).AddTicks(8825) });

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: "2",
                column: "CreatedAt",
                value: new DateTime(2022, 4, 30, 18, 27, 58, 786, DateTimeKind.Local).AddTicks(605));

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: "3",
                column: "CreatedAt",
                value: new DateTime(2022, 7, 29, 18, 27, 58, 786, DateTimeKind.Local).AddTicks(688));
        }
    }
}
