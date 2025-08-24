using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repository.Migrations
{
    public partial class AddModuleCartOnly : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // 1) Companies (yoksa oluştur)
            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    CompanyId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyName = table.Column<string>(type: "nvarchar(256)", nullable: false),
                    TaxNumber = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    RegistrationDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.CompanyId);
                });

            // 2) ModuleCarts
            migrationBuilder.CreateTable(
                name: "ModuleCarts",
                columns: table => new
                {
                    CartID = table.Column<int>(type: "int", nullable: false)
                                   .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CompanyID = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModuleCarts", x => x.CartID);

                    table.ForeignKey(
                        name: "FK_ModuleCarts_AspNetUsers_UserID",
                        column: x => x.UserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);

                    // Companies tablosunun PK adı sende "CompanyId" ise böyle bırak.
                    // Eğer "CompanyID" ise principalColumn'ı "CompanyID" yap.
                    table.ForeignKey(
                        name: "FK_ModuleCarts_Companies_CompanyID",
                        column: x => x.CompanyID,
                        principalTable: "Companies",
                        principalColumn: "CompanyId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ModuleCarts_UserID",
                table: "ModuleCarts",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_ModuleCarts_CompanyID",
                table: "ModuleCarts",
                column: "CompanyID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "ModuleCarts");
            migrationBuilder.DropTable(name: "Companies");
        }
    }
}
