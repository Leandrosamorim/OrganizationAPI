using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class fixingprojectdeveloperrelationship6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectDeveloper_Developer_DeveloperId",
                table: "ProjectDeveloper");

            migrationBuilder.DropTable(
                name: "Developer");

            migrationBuilder.DropIndex(
                name: "IX_ProjectDeveloper_DeveloperId",
                table: "ProjectDeveloper");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Developer",
                columns: table => new
                {
                    UId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ContactId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StackName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Developer", x => x.UId);
                    table.ForeignKey(
                        name: "FK_Developer_Contact_ContactId",
                        column: x => x.ContactId,
                        principalTable: "Contact",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProjectDeveloper_DeveloperId",
                table: "ProjectDeveloper",
                column: "DeveloperId");

            migrationBuilder.CreateIndex(
                name: "IX_Developer_ContactId",
                table: "Developer",
                column: "ContactId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectDeveloper_Developer_DeveloperId",
                table: "ProjectDeveloper",
                column: "DeveloperId",
                principalTable: "Developer",
                principalColumn: "UId");
        }
    }
}
