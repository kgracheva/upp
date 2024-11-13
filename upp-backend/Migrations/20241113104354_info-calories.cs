using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace upp.Migrations
{
    /// <inheritdoc />
    public partial class infocalories : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CaloriesCountByDay",
                table: "AdditionalInfo",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CaloriesCountByDay",
                table: "AdditionalInfo");
        }
    }
}
