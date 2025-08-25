using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RestaurantOrdering.Migrations
{
    /// <inheritdoc />
    public partial class AddSupplierAndTaxRateTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Supplier",
                table: "Stocks");

            migrationBuilder.AddColumn<int>(
                name: "SupplierId",
                table: "Stocks",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TaxRateId",
                table: "Stocks",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Suppliers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ContactPerson = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Suppliers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TaxRates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Rate = table.Column<decimal>(type: "decimal(5,2)", precision: 5, scale: 2, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaxRates", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Suppliers",
                columns: new[] { "Id", "Address", "ContactPerson", "CreatedAt", "Email", "IsActive", "Name", "Phone" },
                values: new object[,]
                {
                    { 1, "İstanbul, Türkiye", "Ahmet Yılmaz", new DateTime(2025, 8, 21, 18, 0, 0, 0, DateTimeKind.Utc), "info@metro.com", true, "Metro Market", "0212 555 0001" },
                    { 2, "Ankara, Türkiye", "Fatma Kaya", new DateTime(2025, 8, 21, 18, 0, 0, 0, DateTimeKind.Utc), "tedarik@carrefour.com", true, "Carrefour", "0312 555 0002" },
                    { 3, "İzmir, Türkiye", "Mehmet Öz", new DateTime(2025, 8, 21, 18, 0, 0, 0, DateTimeKind.Utc), "satinalma@bim.com", true, "BİM", "0232 555 0003" },
                    { 4, "Bursa, Türkiye", "Ayşe Demir", new DateTime(2025, 8, 21, 18, 0, 0, 0, DateTimeKind.Utc), "tedarikci@migros.com", true, "Migros", "0224 555 0004" },
                    { 5, "Antalya, Türkiye", "Ali Vural", new DateTime(2025, 8, 21, 18, 0, 0, 0, DateTimeKind.Utc), "info@yerelmarket.com", true, "Yerel Market", "0242 555 0005" }
                });

            migrationBuilder.InsertData(
                table: "TaxRates",
                columns: new[] { "Id", "CreatedAt", "Description", "IsActive", "Name", "Rate" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 8, 21, 18, 0, 0, 0, DateTimeKind.Utc), "Temel gıda maddeleri için KDV oranı", true, "KDV %1", 1.00m },
                    { 2, new DateTime(2025, 8, 21, 18, 0, 0, 0, DateTimeKind.Utc), "Bazı gıda maddeleri için KDV oranı", true, "KDV %8", 8.00m },
                    { 3, new DateTime(2025, 8, 21, 18, 0, 0, 0, DateTimeKind.Utc), "İndirimli KDV oranı", true, "KDV %10", 10.00m },
                    { 4, new DateTime(2025, 8, 21, 18, 0, 0, 0, DateTimeKind.Utc), "Genel KDV oranı", true, "KDV %18", 18.00m },
                    { 5, new DateTime(2025, 8, 21, 18, 0, 0, 0, DateTimeKind.Utc), "Lüks ürünler için KDV oranı", true, "KDV %20", 20.00m }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Stocks_SupplierId",
                table: "Stocks",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_Stocks_TaxRateId",
                table: "Stocks",
                column: "TaxRateId");

            migrationBuilder.AddForeignKey(
                name: "FK_Stocks_Suppliers_SupplierId",
                table: "Stocks",
                column: "SupplierId",
                principalTable: "Suppliers",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Stocks_TaxRates_TaxRateId",
                table: "Stocks",
                column: "TaxRateId",
                principalTable: "TaxRates",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stocks_Suppliers_SupplierId",
                table: "Stocks");

            migrationBuilder.DropForeignKey(
                name: "FK_Stocks_TaxRates_TaxRateId",
                table: "Stocks");

            migrationBuilder.DropTable(
                name: "Suppliers");

            migrationBuilder.DropTable(
                name: "TaxRates");

            migrationBuilder.DropIndex(
                name: "IX_Stocks_SupplierId",
                table: "Stocks");

            migrationBuilder.DropIndex(
                name: "IX_Stocks_TaxRateId",
                table: "Stocks");

            migrationBuilder.DropColumn(
                name: "SupplierId",
                table: "Stocks");

            migrationBuilder.DropColumn(
                name: "TaxRateId",
                table: "Stocks");

            migrationBuilder.AddColumn<string>(
                name: "Supplier",
                table: "Stocks",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);
        }
    }
}
