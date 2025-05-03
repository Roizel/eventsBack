using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventTrackingSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdatetblEventEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Preview",
                table: "Events",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PreviewPhoto",
                table: "Events",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Preview",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "PreviewPhoto",
                table: "Events");
        }
    }
}
