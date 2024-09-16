using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CustomIdentityAuth.Migrations
{
    /// <inheritdoc />
    public partial class UpdateApplicationUserAndSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FullName",
                table: "AspNetUsers",
                newName: "LastName");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedDate",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastActivity",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDate",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8a6c17f4-3baf-4e59-b6f8-1b0e7d6d2f4b",
                columns: new[] { "ConcurrencyStamp", "CreatedDate", "DeletedDate", "Email", "FirstName", "IsActive", "LastActivity", "LastName", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "SecurityStamp", "UpdatedDate", "UserName" },
                values: new object[] { "25be9bc5-13e7-4496-9906-c88178c69ec4", new DateTime(2024, 9, 16, 20, 8, 45, 870, DateTimeKind.Utc).AddTicks(2750), null, "user@example.com", "User", true, new DateTime(2024, 9, 16, 20, 8, 45, 870, DateTimeKind.Utc).AddTicks(2740), "", "USER@EXAMPLE.COM", "USER", "AQAAAAIAAYagAAAAEErYV5nd8CAkQbWHYYQqFcw/blff6dQJJSmQpoIgNS1wHbU+k4OzmVFtZ+i6tN1geg==", "27a38d94-d1ae-47e6-afdd-97047bca0678", null, "user" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b2f5c3e7-2a6c-4d7a-8b47-fb2b0a2d9f8c",
                columns: new[] { "ConcurrencyStamp", "CreatedDate", "DeletedDate", "FirstName", "IsActive", "LastActivity", "LastName", "PasswordHash", "SecurityStamp", "UpdatedDate" },
                values: new object[] { "a9bb3e06-f0d0-4b37-b6e4-87ba0caae010", new DateTime(2024, 9, 16, 20, 8, 45, 830, DateTimeKind.Utc).AddTicks(7190), null, "Admin", true, new DateTime(2024, 9, 16, 20, 8, 45, 830, DateTimeKind.Utc).AddTicks(7160), "", "AQAAAAIAAYagAAAAEKmfTxjhPlryJ3fYt6Ky792st0hf9/2TSPpROdrycwWVt1ZGBhZCGg6ymQi0eHe3/g==", "647b645b-2a37-45ef-b60c-b20d203d34a4", null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "DeletedDate",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LastActivity",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "AspNetUsers",
                newName: "FullName");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8a6c17f4-3baf-4e59-b6f8-1b0e7d6d2f4b",
                columns: new[] { "ConcurrencyStamp", "Email", "FullName", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "SecurityStamp", "UserName" },
                values: new object[] { "e43c930f-ecfc-43f3-8c0b-073de887536b", "test_user@example.com", "Test User", "TEST_USER@EXAMPLE.COM", "TEST_USER", "AQAAAAIAAYagAAAAEDy7T04vDVn/saNlc1PRGA9Lfau9kp5NH+RFk7hC/XyShH+yRviAwb/33Uf+fzS+Wg==", "cff726a7-aa00-4c6f-b5c6-9d83fdd59f07", "test_user" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b2f5c3e7-2a6c-4d7a-8b47-fb2b0a2d9f8c",
                columns: new[] { "ConcurrencyStamp", "FullName", "PasswordHash", "SecurityStamp" },
                values: new object[] { "30ef7fa9-9b82-4ddf-a539-5de349d6bf04", "Admin", "AQAAAAIAAYagAAAAEKF93jOXu/og+MOEtCgWzQH4sMWvgstVl7SUcgmYpguZGs+/DXjMRrNNVWhTl544RA==", "66e16ee0-78b2-46ed-a9f2-c4fa1bda2b42" });
        }
    }
}
