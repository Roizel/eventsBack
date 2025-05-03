using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventTrackingSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CreateGalleryTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Galleries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    PreviewPhotoPath = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Galleries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GalleryPhotos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PhotoPath = table.Column<string>(type: "TEXT", nullable: false),
                    GalleryId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GalleryPhotos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GalleryPhotos_Galleries_GalleryId",
                        column: x => x.GalleryId,
                        principalTable: "Galleries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GalleryPhotos_GalleryId",
                table: "GalleryPhotos",
                column: "GalleryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GalleryPhotos");

            migrationBuilder.DropTable(
                name: "Galleries");
        }
    }
}
