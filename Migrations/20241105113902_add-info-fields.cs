using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace upp.Migrations
{
    /// <inheritdoc />
    public partial class addinfofields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Patronymic",
                table: "AdditionalInfo");

            migrationBuilder.RenameColumn(
                name: "Surname",
                table: "AdditionalInfo",
                newName: "Lastname");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Lastname",
                table: "AdditionalInfo",
                newName: "Surname");

            migrationBuilder.AddColumn<string>(
                name: "Patronymic",
                table: "AdditionalInfo",
                type: "text",
                nullable: true);
        }
    }
}
