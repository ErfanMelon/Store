using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Store.Persistance.Migrations
{
    public partial class Comments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductLikes");

            migrationBuilder.AlterColumn<string>(
                name: "CategoryTitle",
                table: "Categories",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    CommentId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    ProductId = table.Column<long>(type: "bigint", nullable: false),
                    Score = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserComment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Pros = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cons = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InsertTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsRemoved = table.Column<bool>(type: "bit", nullable: false),
                    RemoveTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.CommentId);
                    table.ForeignKey(
                        name: "FK_Comments_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Comments_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "ProductBrands",
                keyColumn: "BrandId",
                keyValue: 1,
                column: "InsertTime",
                value: new DateTime(2022, 11, 7, 17, 50, 42, 415, DateTimeKind.Local).AddTicks(6998));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: 1,
                column: "InsertTime",
                value: new DateTime(2022, 11, 7, 17, 50, 42, 415, DateTimeKind.Local).AddTicks(6725));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: 2,
                column: "InsertTime",
                value: new DateTime(2022, 11, 7, 17, 50, 42, 415, DateTimeKind.Local).AddTicks(6913));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: 3,
                column: "InsertTime",
                value: new DateTime(2022, 11, 7, 17, 50, 42, 415, DateTimeKind.Local).AddTicks(6952));

            migrationBuilder.CreateIndex(
                name: "IX_Comments_ProductId",
                table: "Comments",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_UserId",
                table: "Comments",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.AlterColumn<string>(
                name: "CategoryTitle",
                table: "Categories",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateTable(
                name: "ProductLikes",
                columns: table => new
                {
                    LikeId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<long>(type: "bigint", nullable: false),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    InsertTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsRemoved = table.Column<bool>(type: "bit", nullable: false),
                    RemoveTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Score = table.Column<int>(type: "int", nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductLikes", x => x.LikeId);
                    table.ForeignKey(
                        name: "FK_ProductLikes_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductLikes_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_ProductLikes_ProductId",
                table: "ProductLikes",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductLikes_UserId",
                table: "ProductLikes",
                column: "UserId");
        }
    }
}
