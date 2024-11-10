using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace upp.Migrations
{
    /// <inheritdoc />
    public partial class trainings2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Training_AspNetUsers_CreatorId",
                table: "Training");

            migrationBuilder.DropForeignKey(
                name: "FK_Training_StatusTypes_StatusTypeId",
                table: "Training");

            migrationBuilder.DropForeignKey(
                name: "FK_TrainingBlocks_Training_TrainingId",
                table: "TrainingBlocks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Training",
                table: "Training");

            migrationBuilder.RenameTable(
                name: "Training",
                newName: "Trainings");

            migrationBuilder.RenameIndex(
                name: "IX_Training_StatusTypeId",
                table: "Trainings",
                newName: "IX_Trainings_StatusTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Training_CreatorId",
                table: "Trainings",
                newName: "IX_Trainings_CreatorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Trainings",
                table: "Trainings",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TrainingBlocks_Trainings_TrainingId",
                table: "TrainingBlocks",
                column: "TrainingId",
                principalTable: "Trainings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Trainings_AspNetUsers_CreatorId",
                table: "Trainings",
                column: "CreatorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Trainings_StatusTypes_StatusTypeId",
                table: "Trainings",
                column: "StatusTypeId",
                principalTable: "StatusTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TrainingBlocks_Trainings_TrainingId",
                table: "TrainingBlocks");

            migrationBuilder.DropForeignKey(
                name: "FK_Trainings_AspNetUsers_CreatorId",
                table: "Trainings");

            migrationBuilder.DropForeignKey(
                name: "FK_Trainings_StatusTypes_StatusTypeId",
                table: "Trainings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Trainings",
                table: "Trainings");

            migrationBuilder.RenameTable(
                name: "Trainings",
                newName: "Training");

            migrationBuilder.RenameIndex(
                name: "IX_Trainings_StatusTypeId",
                table: "Training",
                newName: "IX_Training_StatusTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Trainings_CreatorId",
                table: "Training",
                newName: "IX_Training_CreatorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Training",
                table: "Training",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Training_AspNetUsers_CreatorId",
                table: "Training",
                column: "CreatorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Training_StatusTypes_StatusTypeId",
                table: "Training",
                column: "StatusTypeId",
                principalTable: "StatusTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TrainingBlocks_Training_TrainingId",
                table: "TrainingBlocks",
                column: "TrainingId",
                principalTable: "Training",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
