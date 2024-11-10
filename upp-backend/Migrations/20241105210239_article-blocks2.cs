using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace upp.Migrations
{
    /// <inheritdoc />
    public partial class articleblocks2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ArticleBlock_Articles_ArticleId",
                table: "ArticleBlock");

            migrationBuilder.DropForeignKey(
                name: "FK_ArticleBlock_Block_BlockId",
                table: "ArticleBlock");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ArticleBlock",
                table: "ArticleBlock");

            migrationBuilder.RenameTable(
                name: "ArticleBlock",
                newName: "ArticleBlocks");

            migrationBuilder.RenameIndex(
                name: "IX_ArticleBlock_BlockId",
                table: "ArticleBlocks",
                newName: "IX_ArticleBlocks_BlockId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ArticleBlocks",
                table: "ArticleBlocks",
                columns: new[] { "ArticleId", "BlockId" });

            migrationBuilder.AddForeignKey(
                name: "FK_ArticleBlocks_Articles_ArticleId",
                table: "ArticleBlocks",
                column: "ArticleId",
                principalTable: "Articles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ArticleBlocks_Block_BlockId",
                table: "ArticleBlocks",
                column: "BlockId",
                principalTable: "Block",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ArticleBlocks_Articles_ArticleId",
                table: "ArticleBlocks");

            migrationBuilder.DropForeignKey(
                name: "FK_ArticleBlocks_Block_BlockId",
                table: "ArticleBlocks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ArticleBlocks",
                table: "ArticleBlocks");

            migrationBuilder.RenameTable(
                name: "ArticleBlocks",
                newName: "ArticleBlock");

            migrationBuilder.RenameIndex(
                name: "IX_ArticleBlocks_BlockId",
                table: "ArticleBlock",
                newName: "IX_ArticleBlock_BlockId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ArticleBlock",
                table: "ArticleBlock",
                columns: new[] { "ArticleId", "BlockId" });

            migrationBuilder.AddForeignKey(
                name: "FK_ArticleBlock_Articles_ArticleId",
                table: "ArticleBlock",
                column: "ArticleId",
                principalTable: "Articles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ArticleBlock_Block_BlockId",
                table: "ArticleBlock",
                column: "BlockId",
                principalTable: "Block",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
