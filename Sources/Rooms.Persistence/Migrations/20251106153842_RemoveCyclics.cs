using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Rooms.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class RemoveCyclics : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_equipment_schemas_equipment_types_equipment_type_id",
                table: "equipment_schemas");

            migrationBuilder.RenameColumn(
                name: "equipment_type_id",
                table: "equipment_schemas",
                newName: "type_id");

            migrationBuilder.RenameIndex(
                name: "ix_equipment_schemas_equipment_type_id",
                table: "equipment_schemas",
                newName: "ix_equipment_schemas_type_id");

            migrationBuilder.AddForeignKey(
                name: "fk_equipment_schemas_equipment_types_type_id",
                table: "equipment_schemas",
                column: "type_id",
                principalTable: "equipment_types",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_equipment_schemas_equipment_types_type_id",
                table: "equipment_schemas");

            migrationBuilder.RenameColumn(
                name: "type_id",
                table: "equipment_schemas",
                newName: "equipment_type_id");

            migrationBuilder.RenameIndex(
                name: "ix_equipment_schemas_type_id",
                table: "equipment_schemas",
                newName: "ix_equipment_schemas_equipment_type_id");

            migrationBuilder.AddForeignKey(
                name: "fk_equipment_schemas_equipment_types_equipment_type_id",
                table: "equipment_schemas",
                column: "equipment_type_id",
                principalTable: "equipment_types",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
