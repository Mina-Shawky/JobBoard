using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobBoard.Migrations
{
    /// <inheritdoc />
    public partial class init3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Jobs_AspNetUsers_UserId",
                table: "Jobs");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Jobs",
                newName: "RecruiterId");

            migrationBuilder.RenameIndex(
                name: "IX_Jobs_UserId",
                table: "Jobs",
                newName: "IX_Jobs_RecruiterId");

            migrationBuilder.AddForeignKey(
                name: "FK_Jobs_AspNetUsers_RecruiterId",
                table: "Jobs",
                column: "RecruiterId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Jobs_AspNetUsers_RecruiterId",
                table: "Jobs");

            migrationBuilder.RenameColumn(
                name: "RecruiterId",
                table: "Jobs",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Jobs_RecruiterId",
                table: "Jobs",
                newName: "IX_Jobs_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Jobs_AspNetUsers_UserId",
                table: "Jobs",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
