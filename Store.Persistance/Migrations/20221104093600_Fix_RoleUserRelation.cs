using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Store.Persistance.Migrations
{
    public partial class Fix_RoleUserRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Users_RoleId",
                table: "Users");

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

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Users_RoleId",
                table: "Users");

            migrationBuilder.UpdateData(
                table: "ProductBrands",
                keyColumn: "BrandId",
                keyValue: 1,
                column: "InsertTime",
                value: new DateTime(2022, 11, 3, 13, 15, 40, 530, DateTimeKind.Local).AddTicks(8646));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: 1,
                column: "InsertTime",
                value: new DateTime(2022, 11, 3, 13, 15, 40, 530, DateTimeKind.Local).AddTicks(8277));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: 2,
                column: "InsertTime",
                value: new DateTime(2022, 11, 3, 13, 15, 40, 530, DateTimeKind.Local).AddTicks(8479));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: 3,
                column: "InsertTime",
                value: new DateTime(2022, 11, 3, 13, 15, 40, 530, DateTimeKind.Local).AddTicks(8517));

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId",
                unique: true);
        }
    }
}
