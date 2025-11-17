using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Rooms.Domain.Models.BookingRequests;

#nullable disable

namespace Rooms.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class BookingRequest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:Enum:room_layout", "amphitheater,flat,unspecified")
                .Annotation("Npgsql:Enum:room_net_type", "none,unspecified,wired,wired_and_wireless,wireless")
                .Annotation("Npgsql:Enum:room_status", "not_ready,partially_ready,ready,unspecified")
                .Annotation("Npgsql:Enum:room_type", "computer,mixed,multimedia,special,unspecified")
                .Annotation("Npgsql:PostgresExtension:hstore", ",,")
                .OldAnnotation("Npgsql:Enum:room_layout", "amphitheater,flat,unspecified")
                .OldAnnotation("Npgsql:Enum:room_net_type", "none,unspecified,wired,wired_and_wireless,wireless")
                .OldAnnotation("Npgsql:Enum:room_status", "not_ready,partially_ready,ready,unspecified")
                .OldAnnotation("Npgsql:Enum:room_type", "computer,mixed,multimedia,special,unspecified");

            migrationBuilder.AddColumn<int>(
                name: "booking_request_id",
                table: "rooms",
                type: "integer",
                nullable: true);

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
                    event_type = table.Column<int>(type: "integer", nullable: false),
                    coordinator_institute = table.Column<string>(type: "text", nullable: true),
                    coordinator_full_name = table.Column<string>(type: "text", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamptz", nullable: false),
                    event_name = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    booking_schedule = table.Column<IEnumerable<BookingTime>>(type: "jsonb", nullable: false),
                    status = table.Column<int>(type: "integer", nullable: false),
                    moderator_comment = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    booking_schedule_status = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_booking_requests", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "institute_responsible",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    institute = table.Column<string>(type: "text", nullable: false),
                    responsible = table.Column<Dictionary<string, string>>(type: "hstore", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_institute_responsible", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "ix_rooms_booking_request_id",
                table: "rooms",
                column: "booking_request_id");

            migrationBuilder.AddForeignKey(
                name: "fk_rooms_booking_requests_booking_request_id",
                table: "rooms",
                column: "booking_request_id",
                principalTable: "booking_requests",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_rooms_booking_requests_booking_request_id",
                table: "rooms");

            migrationBuilder.DropTable(
                name: "booking_requests");

            migrationBuilder.DropTable(
                name: "institute_responsible");

            migrationBuilder.DropIndex(
                name: "ix_rooms_booking_request_id",
                table: "rooms");

            migrationBuilder.DropColumn(
                name: "booking_request_id",
                table: "rooms");

            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:Enum:room_layout", "amphitheater,flat,unspecified")
                .Annotation("Npgsql:Enum:room_net_type", "none,unspecified,wired,wired_and_wireless,wireless")
                .Annotation("Npgsql:Enum:room_status", "not_ready,partially_ready,ready,unspecified")
                .Annotation("Npgsql:Enum:room_type", "computer,mixed,multimedia,special,unspecified")
                .OldAnnotation("Npgsql:Enum:room_layout", "amphitheater,flat,unspecified")
                .OldAnnotation("Npgsql:Enum:room_net_type", "none,unspecified,wired,wired_and_wireless,wireless")
                .OldAnnotation("Npgsql:Enum:room_status", "not_ready,partially_ready,ready,unspecified")
                .OldAnnotation("Npgsql:Enum:room_type", "computer,mixed,multimedia,special,unspecified")
                .OldAnnotation("Npgsql:PostgresExtension:hstore", ",,");
        }
    }
}
