using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace InternetForum.Administration.DAL.Migrations
{
    public partial class AddedRefreshToken : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "AspNetUserTokens",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetRoles",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "Discriminator" },
                values: new object[] { "b4e7af9c-2807-4a5b-affa-7c59fcf3b173", "IdentityRole" });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "Discriminator" },
                values: new object[] { "8e9a50d2-165d-462e-ba6a-dba7334c79cb", "IdentityRole" });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3",
                columns: new[] { "ConcurrencyStamp", "Discriminator" },
                values: new object[] { "5948b03c-d04f-493a-8ed5-24fffa93e29c", "IdentityRole" });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4",
                columns: new[] { "ConcurrencyStamp", "Discriminator" },
                values: new object[] { "b69776a1-c3f2-4caf-916f-f5623a74c1b6", "IdentityRole" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "2d0265a0-184b-43bf-971e-c8d2ac1a0fa3", "b31079ba-97eb-456d-a19b-eeab926b7843" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "6d5cb195-144a-45b7-a18c-8771c63b1ab6", "1cb5cd70-5cc7-44f7-9cfc-a1fca7c47927" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "29dca4bc-672d-45d5-8aed-edb0103fd167", "45e18131-2e55-4488-91a7-834d7cea85c2" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "4",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "1f5feb18-2979-4f35-a2b2-ac5aaeee7394", "697fe423-eb93-43d7-93f0-066d7699f81a" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "5",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "343b5ecd-806e-4285-81a8-01ae6be58b9b", "00fbf031-8adc-4dab-8bae-a1cef38b959f" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "AspNetUserTokens");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetRoles");

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
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "b40843b8-40a9-49cd-8a2f-5db10046f5f3", "6d56b440-5ed7-4be2-87c1-ab45180fe8af" });

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
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "e3bbf73b-afad-49d8-aeeb-41fa33b60ed1", "10f5c4ea-0c49-4191-ac47-4b171803fc44" });

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
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "8c23bd64-c5b0-4d3b-9eda-0bb903d094ee", "904d3c03-2bbd-4cad-b82f-1b56498ae872" });
        }
    }
}
