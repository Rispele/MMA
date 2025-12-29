using System;
using System.Collections.Generic;
using Booking.Domain.Models.BookingRequests;
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
                .Annotation("Npgsql:Enum:booking_status", "approved_by_moderator,approved_in_edms,error,event_finished,initiated,new,rejected_by_moderator,rejected_in_edms,sent_for_approval_in_edms,sent_for_moderation");

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
                    room_event_coordinator = table.Column<string>(type: "jsonb", nullable: false),
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

            migrationBuilder.CreateTable(
                name: "booking_process",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    booking_request_id = table.Column<int>(type: "integer", nullable: false),
                    state = table.Column<int>(type: "integer", nullable: false),
                    rollback_attempt = table.Column<int>(type: "integer", nullable: false),
                    rollback_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_booking_process", x => x.id);
                    table.ForeignKey(
                        name: "fk_booking_process_booking_requests_booking_request_id",
                        column: x => x.booking_request_id,
                        principalTable: "booking_requests",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "booking_event_retry_context",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    attempt = table.Column<int>(type: "integer", nullable: false),
                    retry_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    state = table.Column<int>(type: "integer", nullable: false),
                    booking_process_id = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_booking_event_retry_context", x => x.id);
                    table.ForeignKey(
                        name: "fk_booking_event_retry_context_booking_process_booking_process",
                        column: x => x.booking_process_id,
                        principalTable: "booking_process",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "booking_events",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    booking_request_id = table.Column<int>(type: "integer", nullable: false),
                    payload = table.Column<string>(type: "jsonb", nullable: false),
                    rolled_back = table.Column<bool>(type: "boolean", nullable: false),
                    booking_process_id = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_booking_events", x => x.id);
                    table.ForeignKey(
                        name: "fk_booking_events_booking_process_booking_process_id",
                        column: x => x.booking_process_id,
                        principalTable: "booking_process",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_booking_events_booking_requests_booking_request_id",
                        column: x => x.booking_request_id,
                        principalTable: "booking_requests",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_booking_event_retry_context_booking_process_id",
                table: "booking_event_retry_context",
                column: "booking_process_id");

            migrationBuilder.CreateIndex(
                name: "ix_booking_events_booking_process_id",
                table: "booking_events",
                column: "booking_process_id");

            migrationBuilder.CreateIndex(
                name: "ix_booking_events_booking_request_id",
                table: "booking_events",
                column: "booking_request_id");

            migrationBuilder.CreateIndex(
                name: "ix_booking_process_booking_request_id",
                table: "booking_process",
                column: "booking_request_id",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "booking_event_retry_context");

            migrationBuilder.DropTable(
                name: "booking_events");

            migrationBuilder.DropTable(
                name: "institute_coordinators");

            migrationBuilder.DropTable(
                name: "booking_process");

            migrationBuilder.DropTable(
                name: "booking_requests");
        }
    }
}
