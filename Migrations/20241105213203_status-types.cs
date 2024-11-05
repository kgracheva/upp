using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace upp.Migrations
{
    /// <inheritdoc />
    public partial class statustypes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Articles_StatusType_StatusTypeId",
                table: "Articles");

            migrationBuilder.DropForeignKey(
                name: "FK_Training_StatusType_StatusTypeId",
                table: "Training");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StatusType",
                table: "StatusType");

            migrationBuilder.RenameTable(
                name: "StatusType",
                newName: "StatusTypes");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StatusTypes",
                table: "StatusTypes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Articles_StatusTypes_StatusTypeId",
                table: "Articles",
                column: "StatusTypeId",
                principalTable: "StatusTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Training_StatusTypes_StatusTypeId",
                table: "Training",
                column: "StatusTypeId",
                principalTable: "StatusTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Articles_StatusTypes_StatusTypeId",
                table: "Articles");

            migrationBuilder.DropForeignKey(
                name: "FK_Training_StatusTypes_StatusTypeId",
                table: "Training");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StatusTypes",
                table: "StatusTypes");

            migrationBuilder.RenameTable(
                name: "StatusTypes",
                newName: "StatusType");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StatusType",
                table: "StatusType",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Articles_StatusType_StatusTypeId",
                table: "Articles",
                column: "StatusTypeId",
                principalTable: "StatusType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Training_StatusType_StatusTypeId",
                table: "Training",
                column: "StatusTypeId",
                principalTable: "StatusType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
