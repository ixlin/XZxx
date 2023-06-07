using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SFrost.API.Migrations
{
    /// <inheritdoc />
    public partial class UpdateExcelData20 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Age",
                table: "ExcelData");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "ExcelData",
                newName: "TaskOwner");

            migrationBuilder.AddColumn<DateTime>(
                name: "EndTime",
                table: "ExcelData",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "ModuleName",
                table: "ExcelData",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "StartTime",
                table: "ExcelData",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "TaskItem",
                table: "ExcelData",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TaskNotes",
                table: "ExcelData",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndTime",
                table: "ExcelData");

            migrationBuilder.DropColumn(
                name: "ModuleName",
                table: "ExcelData");

            migrationBuilder.DropColumn(
                name: "StartTime",
                table: "ExcelData");

            migrationBuilder.DropColumn(
                name: "TaskItem",
                table: "ExcelData");

            migrationBuilder.DropColumn(
                name: "TaskNotes",
                table: "ExcelData");

            migrationBuilder.RenameColumn(
                name: "TaskOwner",
                table: "ExcelData",
                newName: "Name");

            migrationBuilder.AddColumn<int>(
                name: "Age",
                table: "ExcelData",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
