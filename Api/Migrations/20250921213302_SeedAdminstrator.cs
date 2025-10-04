using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AutoTrackMinimalApi.Migrations
{
    /// <inheritdoc />
    public partial class SeedAdminstrator : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Administrators",
                columns: new[] { "Id", "Email", "Password", "Profile" },
                values: new object[] { 1, "admin@teste.com", "12345678", "admin" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Administrators",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
