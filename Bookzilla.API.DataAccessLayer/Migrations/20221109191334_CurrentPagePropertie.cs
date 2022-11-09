using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bookzilla.API.DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class CurrentPagePropertie : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CurrentPage",
                table: "Albums",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrentPage",
                table: "Albums");
        }
    }
}
