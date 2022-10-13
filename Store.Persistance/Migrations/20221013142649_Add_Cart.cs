using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Store.Persistance.Migrations
{
    public partial class Add_Cart : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Carts",
                columns: table => new
                {
                    CartId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<long>(type: "bigint", nullable: true),
                    BrowserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CurrentCart = table.Column<bool>(type: "bit", nullable: false),
                    InsertTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsRemoved = table.Column<bool>(type: "bit", nullable: false),
                    RemoveTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carts", x => x.CartId);
                    table.ForeignKey(
                        name: "FK_Carts_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "ProductsInCarts",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<long>(type: "bigint", nullable: false),
                    CartId = table.Column<long>(type: "bigint", nullable: false),
                    ProductCount = table.Column<short>(type: "smallint", nullable: false),
                    InsertTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsRemoved = table.Column<bool>(type: "bit", nullable: false),
                    RemoveTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductsInCarts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductsInCarts_Carts_CartId",
                        column: x => x.CartId,
                        principalTable: "Carts",
                        principalColumn: "CartId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductsInCarts_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "ProductBrands",
                keyColumn: "BrandId",
                keyValue: 1,
                column: "InsertTime",
                value: new DateTime(2022, 10, 13, 17, 56, 48, 286, DateTimeKind.Local).AddTicks(1235));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: 1,
                column: "InsertTime",
                value: new DateTime(2022, 10, 13, 17, 56, 48, 286, DateTimeKind.Local).AddTicks(886));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: 2,
                column: "InsertTime",
                value: new DateTime(2022, 10, 13, 17, 56, 48, 286, DateTimeKind.Local).AddTicks(1118));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: 3,
                column: "InsertTime",
                value: new DateTime(2022, 10, 13, 17, 56, 48, 286, DateTimeKind.Local).AddTicks(1175));

            migrationBuilder.CreateIndex(
                name: "IX_Carts_UserId",
                table: "Carts",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductsInCarts_CartId",
                table: "ProductsInCarts",
                column: "CartId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductsInCarts_ProductId",
                table: "ProductsInCarts",
                column: "ProductId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductsInCarts");

            migrationBuilder.DropTable(
                name: "Carts");

            migrationBuilder.UpdateData(
                table: "ProductBrands",
                keyColumn: "BrandId",
                keyValue: 1,
                column: "InsertTime",
                value: new DateTime(2022, 10, 5, 16, 51, 48, 278, DateTimeKind.Local).AddTicks(2335));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: 1,
                column: "InsertTime",
                value: new DateTime(2022, 10, 5, 16, 51, 48, 278, DateTimeKind.Local).AddTicks(2064));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: 2,
                column: "InsertTime",
                value: new DateTime(2022, 10, 5, 16, 51, 48, 278, DateTimeKind.Local).AddTicks(2234));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: 3,
                column: "InsertTime",
                value: new DateTime(2022, 10, 5, 16, 51, 48, 278, DateTimeKind.Local).AddTicks(2274));
        }
    }
}
