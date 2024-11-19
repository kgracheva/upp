using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace upp.Migrations
{
    /// <inheritdoc />
    public partial class specialinfo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Weight",
                table: "Calendars",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "CaloriesCountByDay",
                table: "AdditionalInfo",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<DateTime>(
                name: "BirthDay",
                table: "AdditionalInfo",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "DesiredWeight",
                table: "AdditionalInfo",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Height",
                table: "AdditionalInfo",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Sex",
                table: "AdditionalInfo",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Weight",
                table: "AdditionalInfo",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "WorkType",
                table: "AdditionalInfo",
                type: "integer",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Weight",
                table: "Calendars");

            migrationBuilder.DropColumn(
                name: "BirthDay",
                table: "AdditionalInfo");

            migrationBuilder.DropColumn(
                name: "DesiredWeight",
                table: "AdditionalInfo");

            migrationBuilder.DropColumn(
                name: "Height",
                table: "AdditionalInfo");

            migrationBuilder.DropColumn(
                name: "Sex",
                table: "AdditionalInfo");

            migrationBuilder.DropColumn(
                name: "Weight",
                table: "AdditionalInfo");

            migrationBuilder.DropColumn(
                name: "WorkType",
                table: "AdditionalInfo");

            migrationBuilder.AlterColumn<int>(
                name: "CaloriesCountByDay",
                table: "AdditionalInfo",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);
        }
    }
}
