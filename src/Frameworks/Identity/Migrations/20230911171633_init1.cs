using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Identity.Framework.Migrations
{
    public partial class init1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "UserCredential",
                schema: "dbo",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserCredential", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "UserBankAccount",
                schema: "dbo",
                columns: table => new
                {
                    BankAccountId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Currency = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserBankAccount", x => x.BankAccountId);
                    table.ForeignKey(
                        name: "FK_UserBankAccount_UserCredential_UserId",
                        column: x => x.UserId,
                        principalSchema: "dbo",
                        principalTable: "UserCredential",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserProfile",
                schema: "dbo",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Dob = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProfile", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_UserProfile_UserCredential_UserId",
                        column: x => x.UserId,
                        principalSchema: "dbo",
                        principalTable: "UserCredential",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "UserCredential",
                columns: new[] { "UserId", "Password", "UserName" },
                values: new object[] { 1, "test", "test" });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "UserBankAccount",
                columns: new[] { "BankAccountId", "AccountNumber", "Currency", "UserId" },
                values: new object[] { 1, "1234567890", "IDR", 1 });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "UserProfile",
                columns: new[] { "UserId", "Dob", "FullName" },
                values: new object[] { 1, new DateTime(1992, 4, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "fullname test" });

            migrationBuilder.CreateIndex(
                name: "IX_UserBankAccount_UserId",
                schema: "dbo",
                table: "UserBankAccount",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserCredential_UserName",
                schema: "dbo",
                table: "UserCredential",
                column: "UserName",
                unique: true,
                filter: "[UserName] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserBankAccount",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "UserProfile",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "UserCredential",
                schema: "dbo");
        }
    }
}
