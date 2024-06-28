using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlaceRentalApp.API.Persistence.migrations
{
    /// <inheritdoc />
    public partial class fixPlaceBooks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlaceBooks_Places_PlaceId",
                table: "PlaceBooks");

            migrationBuilder.DropIndex(
                name: "IX_PlaceBooks_PlaceId",
                table: "PlaceBooks");

            migrationBuilder.DropColumn(
                name: "PlaceId",
                table: "PlaceBooks");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PlaceId",
                table: "PlaceBooks",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_PlaceBooks_PlaceId",
                table: "PlaceBooks",
                column: "PlaceId");

            migrationBuilder.AddForeignKey(
                name: "FK_PlaceBooks_Places_PlaceId",
                table: "PlaceBooks",
                column: "PlaceId",
                principalTable: "Places",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
