using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Rooms.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Index : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "ix_rooms_name",
                table: "rooms",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_rooms_parameters_computer_seats",
                table: "rooms",
                column: "parameters_computer_seats");

            migrationBuilder.CreateIndex(
                name: "ix_rooms_parameters_seats",
                table: "rooms",
                column: "parameters_seats");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_rooms_name",
                table: "rooms");

            migrationBuilder.DropIndex(
                name: "ix_rooms_parameters_computer_seats",
                table: "rooms");

            migrationBuilder.DropIndex(
                name: "ix_rooms_parameters_seats",
                table: "rooms");
        }
    }
}
