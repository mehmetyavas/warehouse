using System.Linq.Expressions;
using System.Reflection;
using WebAPI.Data.Enum;

namespace WebAPI.Extensions;

public static class LinqExtensions
{
    public static IOrderedQueryable<TSource> AscOrDescOrder<TSource>(this IQueryable<TSource> query, SortDirection sortDirection,
        string propertyName)
    {
        var entityType = typeof(TSource);

        var propertyInfo = entityType.GetProperty(propertyName,
            BindingFlags.Instance | BindingFlags.Public | BindingFlags.IgnoreCase) ?? entityType.GetProperty("Id");

        var arg = Expression.Parameter(entityType, "x");
        var property = Expression.Property(arg, propertyInfo!.Name);
        var selector = Expression.Lambda(property, new ParameterExpression[]
        {
            arg
        });

        var enumarableType = typeof(Queryable);

        var sortType = sortDirection == SortDirection.Asc ? "OrderBy" : "OrderByDescending";

        var method = enumarableType.GetMethods()
            .Where(m => m.Name == sortType && m.IsGenericMethodDefinition)
            .Where(m =>
            {
                var parameters = m.GetParameters().ToList();
                return parameters.Count == 2;
            })
            .Single();

        var genericMethod = method.MakeGenericMethod(entityType, propertyInfo.PropertyType);

        var newQuery = (IOrderedQueryable<TSource>)genericMethod.Invoke(genericMethod, new object[]
        {
            query,
            selector
        })!;
        return newQuery;
    }
}