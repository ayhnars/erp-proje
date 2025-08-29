using Microsoft.EntityFrameworkCore.Migrations;
using Entities.Models;


#nullable disable

namespace Repository.Migrations
{
    /// <inheritdoc />
    public partial class SyncModuleCart_Model : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ModuleCarts_AspNetUsers_UserId",
                table: "ModuleCarts");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "ModuleCarts",
                newName: "UserID");

            migrationBuilder.RenameIndex(
                name: "IX_ModuleCarts_UserId",
                table: "ModuleCarts",
                newName: "IX_ModuleCarts_UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_ModuleCarts_AspNetUsers_UserID",
                table: "ModuleCarts",
                column: "UserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ModuleCarts_AspNetUsers_UserID",
                table: "ModuleCarts");

            migrationBuilder.RenameColumn(
                name: "UserID",
                table: "ModuleCarts",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_ModuleCarts_UserID",
                table: "ModuleCarts",
                newName: "IX_ModuleCarts_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ModuleCarts_AspNetUsers_UserId",
                table: "ModuleCarts",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
