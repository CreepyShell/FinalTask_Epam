using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace InternetForum.Administration.DAL.Migrations
{
    public partial class AddedSaltAndOwnerRole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "salt",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "NormalizedName" },
                values: new object[] { "da6e20d5-db08-4055-a7d4-198d8c84103b", "ADMINISTRATOR" });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "NormalizedName" },
                values: new object[] { "51a5eb5d-a1da-4a37-9be9-f6f35a587cda", "USER" });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3",
                columns: new[] { "ConcurrencyStamp", "NormalizedName" },
                values: new object[] { "02bded0a-8439-4f99-aea7-c9189aed81c2", "BANNEDUSER" });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4",
                columns: new[] { "ConcurrencyStamp", "NormalizedName" },
                values: new object[] { "7d966810-83dd-4a66-8326-fd2a5c5dce4d", "PREMIUMUSER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Discriminator", "Name", "NormalizedName" },
                values: new object[] { "5", "bc0b6a8a-2e28-423e-be34-993bb62a4c25", "IdentityRole", "Owner", "OWNER" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "SecurityStamp", "salt" },
                values: new object[] { "29a88a00-cc4e-4f50-975f-a9bfd824627f", "ANTON@GMAIL.COM", "ANTON_1990", "7bi7gJ3/LNEKOkqCvCF8T5MJWjI23GBrxYiPMTmGV1U=", "bc14db7c-f529-4dcb-9120-9ca6be23c181", new byte[] { 239, 40, 24, 28, 201, 108, 9, 164, 218, 44, 230, 103, 243, 210, 40, 227, 4, 31, 245, 49, 239, 38, 228, 71, 73, 50, 162, 38, 204, 224, 12, 172 } });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "SecurityStamp", "salt" },
                values: new object[] { "23665b11-e2df-4e13-9237-7d39767e83b9", "DMITRO_KOVALCUK@GMAIL.COM", "DMIDRO", "5FaVRUYDNlmA5fspxYnObyeP8PlfTXdDHH08ILn9XfU=", "b3806388-b109-4ef0-a6d2-1b60c45d0074", new byte[] { 114, 53, 226, 149, 70, 167, 84, 79, 175, 151, 201, 46, 230, 111, 48, 5, 170, 194, 54, 112, 192, 33, 145, 121, 76, 43, 95, 99, 113, 52, 187, 6 } });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3",
                columns: new[] { "ConcurrencyStamp", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "SecurityStamp", "salt" },
                values: new object[] { "133b5cb8-58b2-4d51-a53e-a3ce03f301ed", "MY_MAIL84@GMAIL.COM", "USER1984", "BnGrZd3DgF/LpNAVRLy/yMEQF60jJotJgioc/k36Aaw=", "f631a080-1f4d-48fc-8a8a-82e750da943a", new byte[] { 238, 249, 69, 57, 26, 38, 36, 56, 111, 152, 200, 122, 157, 33, 7, 187, 79, 132, 66, 145, 225, 112, 22, 65, 222, 187, 208, 220, 145, 109, 237, 157 } });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "4",
                columns: new[] { "ConcurrencyStamp", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "SecurityStamp", "salt" },
                values: new object[] { "c4a78659-e69c-4758-8078-555f6c701dfc", "GOODLUCK11@GMAIL.COM", "HAVE_A_NICE_DAY", "+fw/IThmwaKuhDduV2zTNzLD/yyS+wQZZDnLZW6KQLg=", "68d62387-d258-4a9e-9cb7-dfd0c7d15e76", new byte[] { 195, 93, 49, 152, 232, 160, 32, 109, 243, 76, 18, 149, 26, 4, 139, 52, 84, 79, 198, 3, 6, 227, 166, 182, 188, 240, 113, 119, 97, 44, 48, 173 } });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "5",
                columns: new[] { "ConcurrencyStamp", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "SecurityStamp", "salt" },
                values: new object[] { "f0efcbfd-6ab5-4a6c-8fc8-74b4663b3d09", "t_mike2002_11@gmail.com", "MIKE_2002", "fXBqIKok+nPPn+/PvFD6q0sdO/Hr63iULr0G+PmLwJE=", "918a8b7b-edcf-46a0-99ea-b3a2cb783111", new byte[] { 133, 85, 124, 102, 178, 36, 61, 50, 113, 19, 82, 239, 224, 85, 140, 225, 22, 23, 246, 82, 136, 253, 172, 216, 87, 49, 237, 170, 17, 88, 44, 123 } });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "CodeWords", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName", "salt" },
                values: new object[] { "6", 0, "owner of this project", "0c52977a-cb5a-4448-ba54-e070885124fd", "danil.t404@gmail.com", true, false, null, "danil.t404@gmail.com", "DANIL_OWNER", "D6E8Lon6PMsdI+nk2+V9rKxbEVRfWJsxvesYrt0Pl2U=", null, false, "5b483070-2b92-441e-ac98-b15be56de7c3", false, "danil_owner", new byte[] { 43, 150, 1, 40, 49, 238, 134, 154, 12, 128, 168, 217, 148, 47, 195, 35, 183, 162, 119, 56, 123, 155, 4, 166, 231, 121, 244, 194, 21, 104, 247, 2 } });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "UserId", "RoleId" },
                values: new object[] { "6", "1" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "UserId", "RoleId" },
                values: new object[] { "6", "2" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "UserId", "RoleId" },
                values: new object[] { "6", "5" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "UserId", "RoleId" },
                keyValues: new object[] { "6", "1" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "UserId", "RoleId" },
                keyValues: new object[] { "6", "2" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "UserId", "RoleId" },
                keyValues: new object[] { "6", "5" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6");

            migrationBuilder.DropColumn(
                name: "salt",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "NormalizedName" },
                values: new object[] { "b4e7af9c-2807-4a5b-affa-7c59fcf3b173", null });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "NormalizedName" },
                values: new object[] { "8e9a50d2-165d-462e-ba6a-dba7334c79cb", null });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3",
                columns: new[] { "ConcurrencyStamp", "NormalizedName" },
                values: new object[] { "5948b03c-d04f-493a-8ed5-24fffa93e29c", null });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4",
                columns: new[] { "ConcurrencyStamp", "NormalizedName" },
                values: new object[] { "b69776a1-c3f2-4caf-916f-f5623a74c1b6", null });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2d0265a0-184b-43bf-971e-c8d2ac1a0fa3", null, null, "1111", "b31079ba-97eb-456d-a19b-eeab926b7843" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6d5cb195-144a-45b7-a18c-8771c63b1ab6", null, null, "1111", "1cb5cd70-5cc7-44f7-9cfc-a1fca7c47927" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3",
                columns: new[] { "ConcurrencyStamp", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "SecurityStamp" },
                values: new object[] { "29dca4bc-672d-45d5-8aed-edb0103fd167", null, null, "1111", "45e18131-2e55-4488-91a7-834d7cea85c2" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "4",
                columns: new[] { "ConcurrencyStamp", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1f5feb18-2979-4f35-a2b2-ac5aaeee7394", null, null, "1111", "697fe423-eb93-43d7-93f0-066d7699f81a" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "5",
                columns: new[] { "ConcurrencyStamp", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "SecurityStamp" },
                values: new object[] { "343b5ecd-806e-4285-81a8-01ae6be58b9b", null, null, "1111", "00fbf031-8adc-4dab-8bae-a1cef38b959f" });
        }
    }
}
