using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SFrost.API.Migrations
{
    /// <inheritdoc />
    public partial class ChangeImportTimeType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
    name: "ImportTime",
    table: "ExcelData",
    type: "timestamp",
    nullable: false,
    oldClrType: typeof(DateTime),
    oldType: "timestamp with time zone");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
