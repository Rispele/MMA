using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Domain.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "rooms",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    description = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    schedule_address_room_number = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: true),
                    schedule_address_address = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: true),
                    parameters_type = table.Column<int>(type: "integer", nullable: false),
                    parameters_layout = table.Column<int>(type: "integer", nullable: false),
                    parameters_net_type = table.Column<int>(type: "integer", nullable: false),
                    parameters_seats = table.Column<int>(type: "integer", nullable: true),
                    parameters_computer_seats = table.Column<int>(type: "integer", nullable: true),
                    parameters_has_conditioning = table.Column<bool>(type: "boolean", nullable: true),
                    owner = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: true),
                    fix_info_status = table.Column<int>(type: "integer", nullable: false),
                    fix_info_fix_deadline = table.Column<DateTime>(type: "timestamptz", nullable: true),
                    fix_info_comment = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    allow_booking = table.Column<bool>(type: "boolean", nullable: false),
                    attachments = table.Column<string>(type: "jsonb", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_rooms", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "rooms");
        }
    }
}
