using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

namespace Booking.Infrastructure.EFCore.QueryHandlers.BookingRequests;

internal static class BookingRequestsFilterFunctions
{
    [DbFunction(IsBuiltIn = false)]
    public static bool RoomIdsParameterFilter(int bookingRequestId, [NotParameterized] int[] values) => throw new NotImplementedException();

    internal static void HasRoomIdsParameterFilterTranslation(this ModelBuilder modelBuilder)
    {
        modelBuilder
            .HasDbFunction(typeof(BookingRequestsFilterFunctions).GetMethod(nameof(RoomIdsParameterFilter)))
            .HasTranslation(args => new SqlFunctionExpression(
                "",
                [
                    new SqlFragmentExpression($"""
                                               EXISTS (
                                                   SELECT 1
                                                   FROM jsonb_array_elements(
                                                       (SELECT booking_schedule FROM booking_requests WHERE id = {args[0]})
                                                   ) AS elem
                                                   WHERE CAST(elem->>'room_id' AS int) = ANY({args[1].Print()})
                                               )
                                               """)
                ],
                nullable: false,
                argumentsPropagateNullability: [false],
                type: typeof(int),
                typeMapping: null));
    }
}