using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_Commerce.Migrations
{
    /// <inheritdoc />
    public partial class EditOrderTableandAddcouponsrelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "InvoiceValue",
                table: "Order",
                newName: "TotalPriceBeforeDiscount");

            migrationBuilder.AddColumn<decimal>(
                name: "TotalPriceAfterDiscount",
                table: "Order",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

           

            migrationBuilder.AddColumn<int>(
                name: "OrderId",
                table: "coupons",
                type: "int",
                nullable: true);

           

            migrationBuilder.CreateIndex(
                name: "IX_coupons_OrderId",
                table: "coupons",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_coupons_Order_OrderId",
                table: "coupons",
                column: "OrderId",
                principalTable: "Order",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_coupons_Order_OrderId",
                table: "coupons");

            migrationBuilder.DropIndex(
                name: "IX_coupons_OrderId",
                table: "coupons");

            migrationBuilder.DropColumn(
                name: "TotalPriceAfterDiscount",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "coupons");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "coupons");

            migrationBuilder.RenameColumn(
                name: "TotalPriceBeforeDiscount",
                table: "Order",
                newName: "InvoiceValue");

          
        }
    }
}
