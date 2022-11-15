using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bookzilla.API.DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class DefValueCover : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "CollectionId",
                table: "Series",
                type: "INTEGER",
                nullable: false,
                defaultValue: 1,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<int>(
                name: "ReadingStatus",
                table: "Albums",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddColumn<string>(
                name: "CoverArtPath",
                table: "Albums",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "Collections",
                columns: new[] { "Id", "ImageArtPath", "Name" },
                values: new object[] { 1, "homes\\yflechel\\Livre\\CollectionArt\\1.jpg", "Autres" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Collections",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DropColumn(
                name: "CoverArtPath",
                table: "Albums");

            migrationBuilder.AlterColumn<int>(
                name: "CollectionId",
                table: "Series",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldDefaultValue: 1);

            migrationBuilder.AlterColumn<int>(
                name: "ReadingStatus",
                table: "Albums",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldDefaultValue: 0);
        }
    }
}
