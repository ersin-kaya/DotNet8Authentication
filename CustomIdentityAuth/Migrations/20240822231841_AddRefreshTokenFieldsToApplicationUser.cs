using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CustomIdentityAuth.Migrations
{
    /// <inheritdoc />
    public partial class AddRefreshTokenFieldsToApplicationUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RefreshToken",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RefreshTokenExpiration",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8a6c17f4-3baf-4e59-b6f8-1b0e7d6d2f4b",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RefreshToken", "RefreshTokenExpiration", "SecurityStamp" },
                values: new object[] { "e43c930f-ecfc-43f3-8c0b-073de887536b", "AQAAAAIAAYagAAAAEDy7T04vDVn/saNlc1PRGA9Lfau9kp5NH+RFk7hC/XyShH+yRviAwb/33Uf+fzS+Wg==", null, null, "cff726a7-aa00-4c6f-b5c6-9d83fdd59f07" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b2f5c3e7-2a6c-4d7a-8b47-fb2b0a2d9f8c",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RefreshToken", "RefreshTokenExpiration", "SecurityStamp" },
                values: new object[] { "30ef7fa9-9b82-4ddf-a539-5de349d6bf04", "AQAAAAIAAYagAAAAEKF93jOXu/og+MOEtCgWzQH4sMWvgstVl7SUcgmYpguZGs+/DXjMRrNNVWhTl544RA==", null, null, "66e16ee0-78b2-46ed-a9f2-c4fa1bda2b42" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RefreshToken",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "RefreshTokenExpiration",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8a6c17f4-3baf-4e59-b6f8-1b0e7d6d2f4b",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "37d82d05-e016-4694-8811-99ae197bbdd7", "AQAAAAIAAYagAAAAEKrjUvYdlUdZ4t1cvarWLpTkhTnqqIyRSgd5vTI8nXMRb4yb1GJpmnHiYBa+rixa+g==", "4c00faa1-579a-4d85-9b13-b08db9571c10" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b2f5c3e7-2a6c-4d7a-8b47-fb2b0a2d9f8c",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e44840e1-79c5-4c13-8f12-d3061e5f7a58", "AQAAAAIAAYagAAAAEPCA9jif8H/VKqTc4wWCALzXKwQ40GwprblC5x6eO8V+5DKc7aC3JucO+Fbrer2Gtw==", "e68d32c4-dcef-4499-be37-5e7e2463d305" });
        }
    }
}
