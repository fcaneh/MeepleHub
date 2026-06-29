using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MeepleHub.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RefactorTradeItemRequestedByUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TradeItems_Users_FromUserId",
                table: "TradeItems");

            migrationBuilder.DropForeignKey(
                name: "FK_TradeItems_Users_ToUserId",
                table: "TradeItems");

            migrationBuilder.DropIndex(
                name: "IX_TradeItems_FromUserId",
                table: "TradeItems");

            migrationBuilder.DropColumn(
                name: "FromUserId",
                table: "TradeItems");

            migrationBuilder.RenameColumn(
                name: "ToUserId",
                table: "TradeItems",
                newName: "RequestedByUserId");

            migrationBuilder.RenameIndex(
                name: "IX_TradeItems_ToUserId",
                table: "TradeItems",
                newName: "IX_TradeItems_RequestedByUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_TradeItems_Users_RequestedByUserId",
                table: "TradeItems",
                column: "RequestedByUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TradeItems_Users_RequestedByUserId",
                table: "TradeItems");

            migrationBuilder.RenameColumn(
                name: "RequestedByUserId",
                table: "TradeItems",
                newName: "ToUserId");

            migrationBuilder.RenameIndex(
                name: "IX_TradeItems_RequestedByUserId",
                table: "TradeItems",
                newName: "IX_TradeItems_ToUserId");

            migrationBuilder.AddColumn<int>(
                name: "FromUserId",
                table: "TradeItems",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_TradeItems_FromUserId",
                table: "TradeItems",
                column: "FromUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_TradeItems_Users_FromUserId",
                table: "TradeItems",
                column: "FromUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TradeItems_Users_ToUserId",
                table: "TradeItems",
                column: "ToUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
