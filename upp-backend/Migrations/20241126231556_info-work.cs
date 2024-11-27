using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace upp.Migrations
{
    /// <inheritdoc />
    public partial class infowork : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsAvailableToWork",
                table: "AdditionalInfo",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAvailableToWork",
                table: "AdditionalInfo");
        }
    }
}
