using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventTrackingSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixEventNotificationLogstbl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "ChatId",
                table: "EventNotificationLogs",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0L);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ChatId",
                table: "EventNotificationLogs");
        }
    }
}
