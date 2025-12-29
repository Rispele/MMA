using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Rooms.Domain.Models.Equipments;
using Rooms.Domain.Models.Rooms;
using Rooms.Domain.Propagated.Rooms;

#nullable disable

namespace Rooms.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:Enum:room_layout", "amphitheater,flat,unspecified")
                .Annotation("Npgsql:Enum:room_net_type", "none,unspecified,wired,wired_and_wireless,wireless")
                .Annotation("Npgsql:Enum:room_status", "malfunction,partially_ready,ready,unspecified")
                .Annotation("Npgsql:Enum:room_type", "computer,mixed,multimedia,special,unspecified");

            migrationBuilder.CreateTable(
                name: "equipment_types",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    parameters = table.Column<IEnumerable<EquipmentParameterDescriptor>>(type: "jsonb", nullable: false),
                    name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_equipment_types", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "operator_departments",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    contacts = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    operators = table.Column<Dictionary<string, string>>(type: "jsonb", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_operator_departments", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "equipment_schemas",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    parameter_values = table.Column<IReadOnlyDictionary<string, string>>(type: "jsonb", nullable: false),
                    name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    type_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_equipment_schemas", x => x.id);
                    table.ForeignKey(
                        name: "fk_equipment_schemas_equipment_types_type_id",
                        column: x => x.type_id,
                        principalTable: "equipment_types",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

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
                    schedule_address_schedule_room_id = table.Column<int>(type: "integer", nullable: true),
                    parameters_type = table.Column<RoomType>(type: "room_type", nullable: false),
                    parameters_layout = table.Column<RoomLayout>(type: "room_layout", nullable: false),
                    parameters_net_type = table.Column<RoomNetType>(type: "room_net_type", nullable: false),
                    parameters_seats = table.Column<int>(type: "integer", nullable: true),
                    parameters_computer_seats = table.Column<int>(type: "integer", nullable: true),
                    parameters_has_conditioning = table.Column<bool>(type: "boolean", nullable: true),
                    attachments = table.Column<RoomAttachments>(type: "jsonb", nullable: false),
                    owner = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: true),
                    fix_info_status = table.Column<RoomStatus>(type: "room_status", nullable: false),
                    fix_info_fix_deadline = table.Column<DateTime>(type: "timestamptz", nullable: true),
                    fix_info_comment = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    allow_booking = table.Column<bool>(type: "boolean", nullable: false),
                    operator_department_id = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_rooms", x => x.id);
                    table.ForeignKey(
                        name: "fk_rooms_operator_departments_operator_department_id",
                        column: x => x.operator_department_id,
                        principalTable: "operator_departments",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "equipments",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    room_id = table.Column<int>(type: "integer", nullable: false),
                    schema_id = table.Column<int>(type: "integer", nullable: false),
                    inventory_number = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    serial_number = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    network_equipment_ip = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    comment = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    status = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_equipments", x => x.id);
                    table.ForeignKey(
                        name: "fk_equipments_equipment_schemas_schema_id",
                        column: x => x.schema_id,
                        principalTable: "equipment_schemas",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_equipments_rooms_room_id",
                        column: x => x.room_id,
                        principalTable: "rooms",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_equipment_schemas_type_id",
                table: "equipment_schemas",
                column: "type_id");

            migrationBuilder.CreateIndex(
                name: "ix_equipments_room_id",
                table: "equipments",
                column: "room_id");

            migrationBuilder.CreateIndex(
                name: "ix_equipments_schema_id",
                table: "equipments",
                column: "schema_id");

            migrationBuilder.CreateIndex(
                name: "ix_rooms_name",
                table: "rooms",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_rooms_operator_department_id",
                table: "rooms",
                column: "operator_department_id");

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
            migrationBuilder.DropTable(
                name: "equipments");

            migrationBuilder.DropTable(
                name: "equipment_schemas");

            migrationBuilder.DropTable(
                name: "rooms");

            migrationBuilder.DropTable(
                name: "equipment_types");

            migrationBuilder.DropTable(
                name: "operator_departments");
        }
    }
}
