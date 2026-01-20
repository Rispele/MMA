using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

namespace Rooms.Infrastructure.EFCore.QueryHandlers.OperatorDepartments;

internal static class OperatorDepartmentsFilterFunctions
{
    [DbFunction(IsBuiltIn = false)]
    public static bool RoomNameParameterFilter(int operatorDepartmentId, [NotParameterized] string value) => throw new NotImplementedException();

    [DbFunction(IsBuiltIn = false)]
    public static bool OperatorNameParameterFilter(int operatorDepartmentId, [NotParameterized] string value) => throw new NotImplementedException();

    internal static void HasRoomNameParameterFilterTranslation(this ModelBuilder modelBuilder)
    {
        modelBuilder
            .HasDbFunction(typeof(OperatorDepartmentsFilterFunctions).GetMethod(nameof(RoomNameParameterFilter)))
            .HasTranslation(args => new SqlFunctionExpression(
                "",
                [
                    new SqlFragmentExpression($"""
                                               EXISTS (
                                                   SELECT 1
                                                   FROM (SELECT name FROM rooms WHERE operator_department_id = {args[0].Print()}) AS elem
                                                   WHERE LOWER(elem.name) ILIKE '%' || LOWER({args[1].Print()}) || '%'
                                               )
                                               """)
                ],
                nullable: false,
                argumentsPropagateNullability: [false],
                type: typeof(int),
                typeMapping: null));
    }

    internal static void HasOperatorNameParameterFilterTranslation(this ModelBuilder modelBuilder)
    {
        modelBuilder
            .HasDbFunction(typeof(OperatorDepartmentsFilterFunctions).GetMethod(nameof(OperatorNameParameterFilter)))
            .HasTranslation(args => new SqlFunctionExpression(
                "",
                [
                    new SqlFragmentExpression($"""
                                               EXISTS (
                                                   SELECT 1
                                                   FROM (
                                                       (SELECT (
                                                           SELECT operators FROM operator_departments WHERE id = {args[0]}
                                                       )::text AS t)
                                                   ) AS elem
                                                   WHERE LOWER(elem.t) ILIKE '%' || {args[1].Print()} || '%'
                                               )
                                               """)
                ],
                nullable: false,
                argumentsPropagateNullability: [false],
                type: typeof(int),
                typeMapping: null));
    }
}