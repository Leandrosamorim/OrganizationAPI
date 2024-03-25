using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class fixingdeveloperprojectrelationship4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectDeveloper_Developer_DeveloperId",
                table: "ProjectDeveloper");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectDeveloper_Project_ProjectId",
                table: "ProjectDeveloper");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectDeveloper_Developer_DeveloperId",
                table: "ProjectDeveloper",
                column: "DeveloperId",
                principalTable: "Developer",
                principalColumn: "UId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectDeveloper_Project_ProjectId",
                table: "ProjectDeveloper",
                column: "ProjectId",
                principalTable: "Project",
                principalColumn: "UId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectDeveloper_Developer_DeveloperId",
                table: "ProjectDeveloper");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectDeveloper_Project_ProjectId",
                table: "ProjectDeveloper");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectDeveloper_Developer_DeveloperId",
                table: "ProjectDeveloper",
                column: "DeveloperId",
                principalTable: "Developer",
                principalColumn: "UId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectDeveloper_Project_ProjectId",
                table: "ProjectDeveloper",
                column: "ProjectId",
                principalTable: "Project",
                principalColumn: "UId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
