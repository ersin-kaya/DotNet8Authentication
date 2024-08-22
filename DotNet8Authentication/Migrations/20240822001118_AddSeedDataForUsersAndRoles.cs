using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DotNet8Authentication.Migrations
{
    /// <inheritdoc />
    public partial class AddSeedDataForUsersAndRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "6b7d7e8c-5b7a-4c5d-b9f6-f2e9d7c4b1d3", null, "User", "USER" },
                    { "c8a9f7f2-4e6a-4c6d-b6b4-d3b2e2e6c7b2", null, "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FullName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "8a6c17f4-3baf-4e59-b6f8-1b0e7d6d2f4b", 0, "37d82d05-e016-4694-8811-99ae197bbdd7", "test_user@example.com", false, "Test User", false, null, "TEST_USER@EXAMPLE.COM", "TEST_USER", "AQAAAAIAAYagAAAAEKrjUvYdlUdZ4t1cvarWLpTkhTnqqIyRSgd5vTI8nXMRb4yb1GJpmnHiYBa+rixa+g==", null, false, "4c00faa1-579a-4d85-9b13-b08db9571c10", false, "test_user" },
                    { "b2f5c3e7-2a6c-4d7a-8b47-fb2b0a2d9f8c", 0, "e44840e1-79c5-4c13-8f12-d3061e5f7a58", "admin@example.com", false, "Admin", false, null, "ADMIN@EXAMPLE.COM", "ADMIN", "AQAAAAIAAYagAAAAEPCA9jif8H/VKqTc4wWCALzXKwQ40GwprblC5x6eO8V+5DKc7aC3JucO+Fbrer2Gtw==", null, false, "e68d32c4-dcef-4499-be37-5e7e2463d305", false, "admin" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "6b7d7e8c-5b7a-4c5d-b9f6-f2e9d7c4b1d3", "8a6c17f4-3baf-4e59-b6f8-1b0e7d6d2f4b" },
                    { "c8a9f7f2-4e6a-4c6d-b6b4-d3b2e2e6c7b2", "b2f5c3e7-2a6c-4d7a-8b47-fb2b0a2d9f8c" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "6b7d7e8c-5b7a-4c5d-b9f6-f2e9d7c4b1d3", "8a6c17f4-3baf-4e59-b6f8-1b0e7d6d2f4b" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "c8a9f7f2-4e6a-4c6d-b6b4-d3b2e2e6c7b2", "b2f5c3e7-2a6c-4d7a-8b47-fb2b0a2d9f8c" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6b7d7e8c-5b7a-4c5d-b9f6-f2e9d7c4b1d3");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c8a9f7f2-4e6a-4c6d-b6b4-d3b2e2e6c7b2");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8a6c17f4-3baf-4e59-b6f8-1b0e7d6d2f4b");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b2f5c3e7-2a6c-4d7a-8b47-fb2b0a2d9f8c");
        }
    }
}
