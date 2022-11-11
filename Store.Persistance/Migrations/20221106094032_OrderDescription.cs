using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Store.Persistance.Migrations
{
    public partial class OrderDescription : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ZipCode",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "MoreDetail",
                table: "OrderDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "ProductBrands",
                keyColumn: "BrandId",
                keyValue: 1,
                column: "InsertTime",
                value: new DateTime(2022, 11, 6, 13, 10, 30, 610, DateTimeKind.Local).AddTicks(8022));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: 1,
                column: "InsertTime",
                value: new DateTime(2022, 11, 6, 13, 10, 30, 610, DateTimeKind.Local).AddTicks(7656));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: 2,
                column: "InsertTime",
                value: new DateTime(2022, 11, 6, 13, 10, 30, 610, DateTimeKind.Local).AddTicks(7913));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: 3,
                column: "InsertTime",
                value: new DateTime(2022, 11, 6, 13, 10, 30, 610, DateTimeKind.Local).AddTicks(7959));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ZipCode",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "MoreDetail",
                table: "OrderDetails");

            migrationBuilder.UpdateData(
                table: "ProductBrands",
                keyColumn: "BrandId",
                keyValue: 1,
                column: "InsertTime",
                value: new DateTime(2022, 11, 4, 13, 5, 59, 647, DateTimeKind.Local).AddTicks(8460));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: 1,
                column: "InsertTime",
                value: new DateTime(2022, 11, 4, 13, 5, 59, 647, DateTimeKind.Local).AddTicks(8154));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: 2,
                column: "InsertTime",
                value: new DateTime(2022, 11, 4, 13, 5, 59, 647, DateTimeKind.Local).AddTicks(8367));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: 3,
                column: "InsertTime",
                value: new DateTime(2022, 11, 4, 13, 5, 59, 647, DateTimeKind.Local).AddTicks(8406));
        }
    }
}
