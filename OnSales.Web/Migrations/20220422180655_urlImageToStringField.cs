using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OnSales.Web.Migrations
{
    public partial class urlImageToStringField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageId",
                table: "Countries");

            migrationBuilder.AddColumn<string>(
                name: "UrlImage",
                table: "Countries",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UrlImage",
                table: "Countries");

            migrationBuilder.AddColumn<Guid>(
                name: "ImageId",
                table: "Countries",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }
    }
}
