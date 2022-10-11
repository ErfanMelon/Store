using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Store.Persistance.Migrations
{
    public partial class Update_HomePage_Banners : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Clicks",
                table: "Banners",
                newName: "BannerLocation");

            migrationBuilder.AddColumn<bool>(
                name: "DisplayOnPage",
                table: "Banners",
                type: "bit",
                nullable: false,
                defaultValue: false);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DisplayOnPage",
                table: "Banners");

            migrationBuilder.RenameColumn(
                name: "BannerLocation",
                table: "Banners",
                newName: "Clicks");

            migrationBuilder.UpdateData(
                table: "ProductBrands",
                keyColumn: "BrandId",
                keyValue: 1,
                column: "InsertTime",
                value: new DateTime(2022, 10, 3, 16, 1, 42, 53, DateTimeKind.Local).AddTicks(9252));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: 1,
                column: "InsertTime",
                value: new DateTime(2022, 10, 3, 16, 1, 42, 53, DateTimeKind.Local).AddTicks(8905));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: 2,
                column: "InsertTime",
                value: new DateTime(2022, 10, 3, 16, 1, 42, 53, DateTimeKind.Local).AddTicks(9128));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: 3,
                column: "InsertTime",
                value: new DateTime(2022, 10, 3, 16, 1, 42, 53, DateTimeKind.Local).AddTicks(9183));
        }
    }
}
