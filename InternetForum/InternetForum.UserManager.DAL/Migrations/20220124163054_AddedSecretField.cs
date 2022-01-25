using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace InternetForum.Administration.DAL.Migrations
{
    public partial class AddedSecretField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Birthday",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "CodeWords",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "f8d777bf-7bbf-40c1-9d79-013de0d77f55");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "0ad2019b-68ad-4eb8-ba74-2c509e0d2e0a");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3",
                column: "ConcurrencyStamp",
                value: "6edc0587-d584-4846-86fc-1d2f825f8abe");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4",
                column: "ConcurrencyStamp",
                value: "f6a524a5-7a07-4585-a911-716ad293a220");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "CodeWords", "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "good_summer_hollidays", "b40843b8-40a9-49cd-8a2f-5db10046f5f3", "6d56b440-5ed7-4be2-87c1-ab45180fe8af" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "88130f5b-a4b6-4463-962f-86e8dd7e1b32", "7f570f29-e6c3-47ff-8bf8-9bb0a6667fb1" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3",
                columns: new[] { "CodeWords", "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "Whiski", "e3bbf73b-afad-49d8-aeeb-41fa33b60ed1", "10f5c4ea-0c49-4191-ac47-4b171803fc44" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "4",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "2f4ab56f-ab0a-4267-8a00-f8ddcb51e0cb", "761c65c9-8c5f-4d67-8798-d540d13a5427" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "5",
                columns: new[] { "CodeWords", "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "Veni, vidi, vici", "8c23bd64-c5b0-4d3b-9eda-0bb903d094ee", "904d3c03-2bbd-4cad-b82f-1b56498ae872" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CodeWords",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<DateTime>(
                name: "Birthday",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "0f04778b-1526-4c8c-bd88-8d051e74f354");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "c6cd8926-7ae1-4f02-b39c-c1bffa49d05f");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3",
                column: "ConcurrencyStamp",
                value: "bbbb682a-dfa1-490b-95b8-a5f06da6c252");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4",
                column: "ConcurrencyStamp",
                value: "f528b8d1-b5f6-459a-b9e6-a74efcc3a6b8");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "Birthday", "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { new DateTime(1990, 5, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "b66b076c-2640-43db-a200-824de7b9660b", "1b1761a6-eecc-4986-b84f-b3f9adb9c7fd" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "9c7c8c07-7050-4d9f-9d52-c9afe9dd3cf0", "9d3f8801-00a2-42b1-b2f8-5086f73e9649" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3",
                columns: new[] { "Birthday", "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { new DateTime(2000, 10, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), "2e34b2d8-be00-4d77-84cc-345c976aa9aa", "6cf3026d-6124-45ef-961e-9911a0891fe7" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "4",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "a6d56fe1-26a7-4643-80e6-9c184cab8ae2", "5bd7179d-a6ac-4ce0-ba23-a9ce1b16ca0f" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "5",
                columns: new[] { "Birthday", "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { new DateTime(1999, 8, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "b20b8e85-244a-42b2-881e-2a89f3908a3e", "c9b1be8e-5bed-4f3c-bf24-458808a1bbde" });
        }
    }
}
