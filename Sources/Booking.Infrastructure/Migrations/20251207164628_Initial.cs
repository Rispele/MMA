using System;
using System.Collections.Generic;
using Booking.Domain.Models.BookingRequests;
using Booking.Domain.Models.BookingRequests.RoomEventCoordinator;
using Booking.Domain.Models.InstituteCoordinators;
using Booking.Domain.Propagated.BookingRequests;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Booking.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:Enum:booking_frequency", "everyday,undefined,weekly")
                .Annotation("Npgsql:Enum:booking_schedule_status", "booked,booking_approved,booking_cancel_error,booking_cancelled,not_sent")
                .Annotation("Npgsql:Enum:booking_status", "approved,error,event_finished,in_approve,new,rejected,sed_rejected,under_moderation");

            migrationBuilder.CreateTable(
                name: "booking_requests",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    creator_full_name = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    creator_email = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    creator_phone_number = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    reason = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    participants_count = table.Column<int>(type: "integer", nullable: false),
                    tech_employee_required = table.Column<bool>(type: "boolean", nullable: false),
                    event_host_full_name = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    room_event_coordinator = table.Column<IRoomEventCoordinator>(type: "jsonb", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamptz", nullable: false),
                    event_name = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    status = table.Column<BookingStatus>(type: "booking_status", nullable: false),
                    moderator_comment = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    booking_schedule_status = table.Column<BookingScheduleStatus>(type: "booking_schedule_status", nullable: true),
                    room_ids = table.Column<int[]>(type: "integer[]", nullable: false),
                    booking_schedule = table.Column<IEnumerable<BookingTime>>(type: "jsonb", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_booking_requests", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "institute_coordinators",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    institute = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    coordinators = table.Column<List<Coordinator>>(type: "jsonb", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_institute_coordinators", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "booking_requests");

            migrationBuilder.DropTable(
                name: "institute_coordinators");
        }
    }
}
