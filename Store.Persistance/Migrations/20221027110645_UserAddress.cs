using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Store.Persistance.Migrations
{
    public partial class UserAddress : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<long>(
                name: "AddressId",
                table: "Orders",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeliveredDate",
                table: "Orders",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserAddress",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "DeliveredDate",
                table: "OrderDetails",
                type: "datetime2",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "ProductBrands",
                keyColumn: "BrandId",
                keyValue: 1,
                column: "InsertTime",
                value: new DateTime(2022, 10, 27, 14, 36, 43, 934, DateTimeKind.Local).AddTicks(1277));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: 1,
                column: "InsertTime",
                value: new DateTime(2022, 10, 27, 14, 36, 43, 934, DateTimeKind.Local).AddTicks(621));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: 2,
                column: "InsertTime",
                value: new DateTime(2022, 10, 27, 14, 36, 43, 934, DateTimeKind.Local).AddTicks(906));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: 3,
                column: "InsertTime",
                value: new DateTime(2022, 10, 27, 14, 36, 43, 934, DateTimeKind.Local).AddTicks(998));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "AddressId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "DeliveredDate",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "UserAddress",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "DeliveredDate",
                table: "OrderDetails");

            migrationBuilder.UpdateData(
                table: "ProductBrands",
                keyColumn: "BrandId",
                keyValue: 1,
                column: "InsertTime",
                value: new DateTime(2022, 10, 25, 11, 53, 2, 953, DateTimeKind.Local).AddTicks(4322));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: 1,
                column: "InsertTime",
                value: new DateTime(2022, 10, 25, 11, 53, 2, 953, DateTimeKind.Local).AddTicks(4003));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: 2,
                column: "InsertTime",
                value: new DateTime(2022, 10, 25, 11, 53, 2, 953, DateTimeKind.Local).AddTicks(4209));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: 3,
                column: "InsertTime",
                value: new DateTime(2022, 10, 25, 11, 53, 2, 953, DateTimeKind.Local).AddTicks(4257));
        }
    }
}
