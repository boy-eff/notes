using System.Linq.Expressions;
using Notes.Application.Common.CQRS;

namespace Notes.Application.Common.Extensions;

public static class QueryableExtensions
{
    
    public static IQueryable<T> ApplyPagination<T>(this IQueryable<T> entities, WithPaginationParameters paginationParameters)
    {
        if (paginationParameters.LastId.HasValue)
        {
            // Курсорная пагинация
            var parameter = Expression.Parameter(typeof(T), "x");
            var property = Expression.Property(parameter, "Id");
            var constant = Expression.Constant(paginationParameters.LastId.Value);
            var comparison = Expression.GreaterThan(property, Expression.Convert(constant, property.Type));
            var lambda = Expression.Lambda<Func<T, bool>>(comparison, parameter);

            entities = entities.Where(lambda);
        }
        else if (paginationParameters.PageNumber.HasValue)
        {
            // Пагинация по номеру страницы
            entities = entities.Skip((paginationParameters.PageNumber.Value - 1) * paginationParameters.PageSize);
        }
        
        return entities.Take(paginationParameters.PageSize);
    }
}