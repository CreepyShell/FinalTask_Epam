using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace InternetForum.DAL.Migrations
{
    public partial class AddedSomeDataForTests : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "CommentReactions",
                keyColumn: "Id",
                keyValue: "1",
                column: "ReactedAt",
                value: new DateTime(2022, 2, 2, 17, 49, 27, 797, DateTimeKind.Local).AddTicks(8734));

            migrationBuilder.UpdateData(
                table: "CommentReactions",
                keyColumn: "Id",
                keyValue: "2",
                column: "ReactedAt",
                value: new DateTime(2022, 5, 5, 15, 49, 27, 797, DateTimeKind.Local).AddTicks(9621));

            migrationBuilder.UpdateData(
                table: "CommentReactions",
                keyColumn: "Id",
                keyValue: "3",
                column: "ReactedAt",
                value: new DateTime(2022, 8, 2, 15, 49, 27, 797, DateTimeKind.Local).AddTicks(9669));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: "1",
                column: "CreatedAt",
                value: new DateTime(2022, 2, 2, 16, 49, 27, 795, DateTimeKind.Local).AddTicks(437));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: "2",
                column: "CreatedAt",
                value: new DateTime(2022, 5, 4, 15, 49, 27, 795, DateTimeKind.Local).AddTicks(2772));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: "3",
                column: "CreatedAt",
                value: new DateTime(2022, 8, 2, 15, 49, 27, 795, DateTimeKind.Local).AddTicks(2847));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: "4",
                column: "CreatedAt",
                value: new DateTime(2022, 2, 2, 16, 49, 27, 795, DateTimeKind.Local).AddTicks(2858));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: "5",
                column: "CreatedAt",
                value: new DateTime(2022, 5, 4, 15, 49, 27, 795, DateTimeKind.Local).AddTicks(2867));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: "6",
                column: "CreatedAt",
                value: new DateTime(2022, 5, 4, 15, 49, 27, 795, DateTimeKind.Local).AddTicks(2874));

            migrationBuilder.UpdateData(
                table: "PostReactions",
                keyColumn: "Id",
                keyValue: "1",
                column: "ReactedAt",
                value: new DateTime(2022, 2, 2, 16, 49, 27, 796, DateTimeKind.Local).AddTicks(9218));

            migrationBuilder.UpdateData(
                table: "PostReactions",
                keyColumn: "Id",
                keyValue: "10",
                column: "ReactedAt",
                value: new DateTime(2022, 5, 4, 15, 49, 27, 797, DateTimeKind.Local).AddTicks(441));

            migrationBuilder.UpdateData(
                table: "PostReactions",
                keyColumn: "Id",
                keyValue: "2",
                column: "ReactedAt",
                value: new DateTime(2022, 2, 2, 16, 49, 27, 797, DateTimeKind.Local).AddTicks(289));

            migrationBuilder.UpdateData(
                table: "PostReactions",
                keyColumn: "Id",
                keyValue: "3",
                column: "ReactedAt",
                value: new DateTime(2022, 5, 4, 15, 49, 27, 797, DateTimeKind.Local).AddTicks(343));

            migrationBuilder.UpdateData(
                table: "PostReactions",
                keyColumn: "Id",
                keyValue: "4",
                column: "ReactedAt",
                value: new DateTime(2022, 8, 2, 15, 49, 27, 797, DateTimeKind.Local).AddTicks(359));

            migrationBuilder.UpdateData(
                table: "PostReactions",
                keyColumn: "Id",
                keyValue: "5",
                column: "ReactedAt",
                value: new DateTime(2022, 8, 2, 15, 49, 27, 797, DateTimeKind.Local).AddTicks(379));

            migrationBuilder.UpdateData(
                table: "PostReactions",
                keyColumn: "Id",
                keyValue: "6",
                column: "ReactedAt",
                value: new DateTime(2022, 4, 24, 15, 49, 27, 797, DateTimeKind.Local).AddTicks(399));

            migrationBuilder.UpdateData(
                table: "PostReactions",
                keyColumn: "Id",
                keyValue: "7",
                column: "ReactedAt",
                value: new DateTime(2022, 2, 2, 16, 49, 27, 797, DateTimeKind.Local).AddTicks(418));

            migrationBuilder.UpdateData(
                table: "PostReactions",
                keyColumn: "Id",
                keyValue: "8",
                column: "ReactedAt",
                value: new DateTime(2022, 2, 2, 16, 49, 27, 797, DateTimeKind.Local).AddTicks(428));

            migrationBuilder.UpdateData(
                table: "PostReactions",
                keyColumn: "Id",
                keyValue: "9",
                column: "ReactedAt",
                value: new DateTime(2022, 8, 2, 15, 49, 27, 797, DateTimeKind.Local).AddTicks(434));

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 2, 2, 15, 49, 27, 786, DateTimeKind.Local).AddTicks(1313), new DateTime(2022, 2, 2, 15, 59, 27, 793, DateTimeKind.Local).AddTicks(4791) });

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: "2",
                column: "CreatedAt",
                value: new DateTime(2022, 5, 3, 15, 49, 27, 793, DateTimeKind.Local).AddTicks(8077));

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: "3",
                column: "CreatedAt",
                value: new DateTime(2022, 8, 1, 15, 49, 27, 793, DateTimeKind.Local).AddTicks(8223));

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "Id", "CreatedAt", "Header", "PostTopic", "Text", "UpdatedAt", "UserId" },
                values: new object[,]
                {
                    { "5", new DateTime(2022, 8, 6, 15, 49, 27, 793, DateTimeKind.Local).AddTicks(8244), "Test post for updating", 5, "Test text", null, "2" },
                    { "4", new DateTime(2022, 8, 3, 15, 49, 27, 793, DateTimeKind.Local).AddTicks(8236), "Test post for deleting", 5, "Test text", null, "4" }
                });

            migrationBuilder.UpdateData(
                table: "Questionnaires",
                keyColumn: "Id",
                keyValue: "1",
                column: "OpenAt",
                value: new DateTime(2022, 2, 2, 15, 49, 27, 798, DateTimeKind.Local).AddTicks(6842));

            migrationBuilder.InsertData(
                table: "Questionnaires",
                columns: new[] { "Id", "AuthorId", "ClosedAt", "OpenAt", "Title" },
                values: new object[] { "2", "3", new DateTime(2022, 2, 5, 15, 49, 27, 798, DateTimeKind.Local).AddTicks(8274), new DateTime(2022, 2, 2, 15, 49, 27, 798, DateTimeKind.Local).AddTicks(8327), "Test questionnaire" });

            migrationBuilder.InsertData(
                table: "Questions",
                columns: new[] { "Id", "IsAllowedMultiple", "IsRequired", "QuestionnaireId", "Text" },
                values: new object[] { "4", false, false, "2", "Test question" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: "4");

            migrationBuilder.DeleteData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: "5");

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: "4");

            migrationBuilder.DeleteData(
                table: "Questionnaires",
                keyColumn: "Id",
                keyValue: "2");

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

            migrationBuilder.UpdateData(
                table: "Questionnaires",
                keyColumn: "Id",
                keyValue: "1",
                column: "OpenAt",
                value: new DateTime(2022, 1, 30, 18, 36, 45, 549, DateTimeKind.Local).AddTicks(8063));
        }
    }
}
