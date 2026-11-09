using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobBoard.Migrations
{
    /// <inheritdoc />
    public partial class init2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
            table: "AspNetRoles",
            columns: new[] { "Id", "Name", "NormalizedName" },
            values: new object[,]
            {
                  { "11111111-1111-1111-1111-111111111111", "Admin", "ADMIN" },
                  { "22222222-2222-2222-2222-222222222222", "User", "USER" },
                  { "33333333-3333-3333-3333-333333333333", "Recruiter", "RECRUITER" }
            });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
            table: "AspNetRoles",
            keyColumn: "Id",
            keyValues: new object[]
            {
                  "11111111-1111-1111-1111-111111111111",
                  "22222222-2222-2222-2222-222222222222",
                  "33333333-3333-3333-3333-333333333333"
            });
        }
    }
}
