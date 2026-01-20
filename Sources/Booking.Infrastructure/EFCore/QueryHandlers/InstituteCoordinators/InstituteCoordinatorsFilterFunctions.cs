using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

namespace Booking.Infrastructure.EFCore.QueryHandlers.InstituteCoordinators;

internal static class InstituteCoordinatorsFilterFunctions
{
    [DbFunction(IsBuiltIn = false)]
    public static bool CoordinatorParameterFilter(int coordinatorId, [NotParameterized] string value) => throw new NotImplementedException();

    internal static void HasCoordinatorParameterFilterTranslation(this ModelBuilder modelBuilder)
    {
        modelBuilder
            .HasDbFunction(typeof(InstituteCoordinatorsFilterFunctions).GetMethod(nameof(CoordinatorParameterFilter)))
            .HasTranslation(args => new SqlFunctionExpression(
                "",
                [
                    new SqlFragmentExpression($"""
                                               EXISTS (
                                                   SELECT 1
                                                   FROM jsonb_array_elements(
                                                       (SELECT coordinators FROM institute_coordinators WHERE id = {args[0]})
                                                   ) AS elem
                                                   WHERE LOWER(elem->>'FullName') LIKE '%' || LOWER({args[1].Print()}) || '%'
                                               )
                                               """)
                ],
                nullable: false,
                argumentsPropagateNullability: [false],
                type: typeof(int),
                typeMapping: null));
    }
}