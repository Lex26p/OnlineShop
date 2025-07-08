using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ozon.Db.Migrations
{
    /// <inheritdoc />
    public partial class ProductImage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImagePath",
                table: "Products",
                newName: "ImagesPath");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("3438f2c3-754d-4926-a8c5-ef89244b600c"),
                column: "ImagesPath",
                value: "[]");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("3cbf160f-7057-4342-8998-c807ab3bd17b"),
                column: "ImagesPath",
                value: "[]");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("4e21a164-f1f5-4000-855a-8bb0fe6d1660"),
                column: "ImagesPath",
                value: "[]");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("6e4446ac-a7b2-4e33-b9da-9b9528176826"),
                column: "ImagesPath",
                value: "[]");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("c5d72e67-8122-418f-8e99-04510cabf5c8"),
                column: "ImagesPath",
                value: "[]");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("de472045-c37b-4ec4-a1b1-fe92b8949585"),
                column: "ImagesPath",
                value: "[]");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImagesPath",
                table: "Products",
                newName: "ImagePath");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("3438f2c3-754d-4926-a8c5-ef89244b600c"),
                column: "ImagePath",
                value: "");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("3cbf160f-7057-4342-8998-c807ab3bd17b"),
                column: "ImagePath",
                value: "");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("4e21a164-f1f5-4000-855a-8bb0fe6d1660"),
                column: "ImagePath",
                value: "");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("6e4446ac-a7b2-4e33-b9da-9b9528176826"),
                column: "ImagePath",
                value: "");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("c5d72e67-8122-418f-8e99-04510cabf5c8"),
                column: "ImagePath",
                value: "");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("de472045-c37b-4ec4-a1b1-fe92b8949585"),
                column: "ImagePath",
                value: "");
        }
    }
}
