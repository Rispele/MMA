using Booking.Domain.Propagated.BookingRequests;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Booking.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class tt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<BookingScheduleStatus>(
                name: "booking_schedule_status",
                table: "booking_requests",
                type: "booking_schedule_status",
                nullable: false,
                defaultValue: (BookingScheduleStatus)0,
                oldClrType: typeof(BookingScheduleStatus),
                oldType: "booking_schedule_status",
                oldNullable: true);

            migrationBuilder.AddColumn<int[]>(
                name: "room_ids",
                table: "booking_requests",
                type: "integer[]",
                nullable: false,
                defaultValue: new int[0]);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "room_ids",
                table: "booking_requests");

            migrationBuilder.AlterColumn<BookingScheduleStatus>(
                name: "booking_schedule_status",
                table: "booking_requests",
                type: "booking_schedule_status",
                nullable: true,
                oldClrType: typeof(BookingScheduleStatus),
                oldType: "booking_schedule_status");
        }
    }
}
