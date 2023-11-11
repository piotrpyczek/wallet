using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Wallet.API.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Wallets",
                columns: table => new
                {
                    WalletId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wallets", x => x.WalletId);
                });

            migrationBuilder.CreateTable(
                name: "CurrencyBuckets",
                columns: table => new
                {
                    CurrencyBucketId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CurrencyCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 0m),
                    WalletId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CurrencyBuckets", x => x.CurrencyBucketId);
                    table.ForeignKey(
                        name: "FK_CurrencyBuckets_Wallets_WalletId",
                        column: x => x.WalletId,
                        principalTable: "Wallets",
                        principalColumn: "WalletId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    TransactionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    ReferencedTransactionId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CurrencyBucketId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.TransactionId);
                    table.ForeignKey(
                        name: "FK_Transactions_CurrencyBuckets_CurrencyBucketId",
                        column: x => x.CurrencyBucketId,
                        principalTable: "CurrencyBuckets",
                        principalColumn: "CurrencyBucketId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Transactions_Transactions_ReferencedTransactionId",
                        column: x => x.ReferencedTransactionId,
                        principalTable: "Transactions",
                        principalColumn: "TransactionId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_CurrencyBuckets_WalletId",
                table: "CurrencyBuckets",
                column: "WalletId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_CurrencyBucketId",
                table: "Transactions",
                column: "CurrencyBucketId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_ReferencedTransactionId",
                table: "Transactions",
                column: "ReferencedTransactionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropTable(
                name: "CurrencyBuckets");

            migrationBuilder.DropTable(
                name: "Wallets");
        }
    }
}
