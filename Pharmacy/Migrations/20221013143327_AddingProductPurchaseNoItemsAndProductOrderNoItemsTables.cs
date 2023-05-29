using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pharmacy.Migrations
{
    public partial class AddingProductPurchaseNoItemsAndProductOrderNoItemsTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Time",
                table: "Purchases",
                newName: "PurchaseTime");

            migrationBuilder.RenameColumn(
                name: "NoItems",
                table: "Purchases",
                newName: "TotalNoProducts");

            migrationBuilder.RenameColumn(
                name: "NoProducts",
                table: "Products",
                newName: "ProductNoItems");

            migrationBuilder.RenameColumn(
                name: "age",
                table: "Pharmacists",
                newName: "Age");

            migrationBuilder.RenameColumn(
                name: "NoItems",
                table: "Orders",
                newName: "TotalNoProducts");

            migrationBuilder.RenameColumn(
                name: "gender",
                table: "Customers",
                newName: "Gender");

            migrationBuilder.RenameColumn(
                name: "debts",
                table: "Customers",
                newName: "Debts");

            migrationBuilder.RenameColumn(
                name: "age",
                table: "Customers",
                newName: "Age");

            migrationBuilder.CreateTable(
                name: "ProductsOrdersNoItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    NoItems = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductsOrdersNoItems", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductsPurchasesNoItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PurchaseId = table.Column<int>(type: "int", nullable: false),
                    NoItems = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductsPurchasesNoItems", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductsOrdersNoItems");

            migrationBuilder.DropTable(
                name: "ProductsPurchasesNoItems");

            migrationBuilder.RenameColumn(
                name: "TotalNoProducts",
                table: "Purchases",
                newName: "NoItems");

            migrationBuilder.RenameColumn(
                name: "PurchaseTime",
                table: "Purchases",
                newName: "Time");

            migrationBuilder.RenameColumn(
                name: "ProductNoItems",
                table: "Products",
                newName: "NoProducts");

            migrationBuilder.RenameColumn(
                name: "Age",
                table: "Pharmacists",
                newName: "age");

            migrationBuilder.RenameColumn(
                name: "TotalNoProducts",
                table: "Orders",
                newName: "NoItems");

            migrationBuilder.RenameColumn(
                name: "Gender",
                table: "Customers",
                newName: "gender");

            migrationBuilder.RenameColumn(
                name: "Debts",
                table: "Customers",
                newName: "debts");

            migrationBuilder.RenameColumn(
                name: "Age",
                table: "Customers",
                newName: "age");
        }
    }
}
