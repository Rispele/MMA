using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

namespace Rooms.Infrastructure.EFCore.QueryHandlers.Equipments;

internal static class EquipmentsFilterFunctions
{
    [DbFunction(IsBuiltIn = false)]
    public static bool EquipmentTypeParameterFilter(int typeId, [NotParameterized] string value) => throw new NotImplementedException();

    internal static void HasEquipmentTypeParameterFilterTranslation(this ModelBuilder modelBuilder)
    {
        modelBuilder
            .HasDbFunction(typeof(EquipmentsFilterFunctions).GetMethod(nameof(EquipmentTypeParameterFilter)))
            .HasTranslation(args => new SqlFunctionExpression(
                "",
                [
                    new SqlFragmentExpression($"""
                                               EXISTS (
                                                   SELECT 1
                                                   FROM jsonb_array_elements(
                                                       (SELECT parameters FROM equipment_types WHERE id = {args[0]})
                                                   ) AS elem
                                                   WHERE elem->>'Name' ILIKE '%' || {args[1].Print()} || '%'
                                               )
                                               """)
                ],
                nullable: false,
                argumentsPropagateNullability: [false],
                type: typeof(int),
                typeMapping: null));
    }
}