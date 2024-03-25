using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class fixingdeveloperprojectrelationship2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ProjectDeveloper",
                table: "ProjectDeveloper");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProjectDeveloper",
                table: "ProjectDeveloper",
                column: "UId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectDeveloper_ProjectId",
                table: "ProjectDeveloper",
                column: "ProjectId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ProjectDeveloper",
                table: "ProjectDeveloper");

            migrationBuilder.DropIndex(
                name: "IX_ProjectDeveloper_ProjectId",
                table: "ProjectDeveloper");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProjectDeveloper",
                table: "ProjectDeveloper",
                columns: new[] { "ProjectId", "DeveloperId" });
        }
    }
}
