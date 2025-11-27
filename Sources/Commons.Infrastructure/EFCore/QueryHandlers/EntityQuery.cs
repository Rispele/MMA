using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Commons.Infrastructure.EFCore.QueryHandlers;

public record EntityQuery<TContext, TQuery, TResponse>(TQuery Query, TContext Context) : IRequest<TResponse>
    where TContext : DbContext;