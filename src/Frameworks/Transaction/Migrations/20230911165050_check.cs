using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Transaction.Framework.Migrations
{
    public partial class check : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "AccountSummary",
                schema: "dbo",
                columns: table => new
                {
                    AccountNumber = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Balance = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Currency = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountSummary", x => x.AccountNumber);
                });

            migrationBuilder.CreateTable(
                name: "AccountTransaction",
                schema: "dbo",
                columns: table => new
                {
                    TransactionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountNumber = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TransactionType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountTransaction", x => x.TransactionId);
                    table.ForeignKey(
                        name: "FK_AccountTransaction_AccountSummary_AccountNumber",
                        column: x => x.AccountNumber,
                        principalSchema: "dbo",
                        principalTable: "AccountSummary",
                        principalColumn: "AccountNumber");
                });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "AccountSummary",
                columns: new[] { "AccountNumber", "Balance", "Currency" },
                values: new object[] { "1234567890", 1000000m, "IDR" });

            migrationBuilder.CreateIndex(
                name: "IX_AccountTransaction_AccountNumber",
                schema: "dbo",
                table: "AccountTransaction",
                column: "AccountNumber");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccountTransaction",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "AccountSummary",
                schema: "dbo");
        }
    }
}
