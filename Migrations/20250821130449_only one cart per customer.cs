using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_Commerce.Migrations
{
    /// <inheritdoc />
    public partial class onlyonecartpercustomer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_carts_customers_CustomerId",
                table: "carts");

            migrationBuilder.DropIndex(
                name: "IX_carts_CustomerId",
                table: "carts");

            migrationBuilder.AddColumn<int>(
                name: "CartId",
                table: "customers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_customers_CartId",
                table: "customers",
                column: "CartId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_customers_carts_CartId",
                table: "customers",
                column: "CartId",
                principalTable: "carts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_customers_carts_CartId",
                table: "customers");

            migrationBuilder.DropIndex(
                name: "IX_customers_CartId",
                table: "customers");

            migrationBuilder.DropColumn(
                name: "CartId",
                table: "customers");

            migrationBuilder.CreateIndex(
                name: "IX_carts_CustomerId",
                table: "carts",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_carts_customers_CustomerId",
                table: "carts",
                column: "CustomerId",
                principalTable: "customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
