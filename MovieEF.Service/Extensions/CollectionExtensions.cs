using MovieEF.Domain.Commons;

namespace MovieEF.Service.Extensions;

public static class CollectionExtensions
{
    public static IEnumerable<T> ToPagedList<T>(this IEnumerable<T> source, PaginationParameters? parameters)
        where T : class
    {
        parameters ??= new PaginationParameters {PageSize = 20, PageIndex = 1};

        return parameters.PageSize >= 0 && parameters.PageIndex > 0
            ? source.Skip(parameters.PageSize * (parameters.PageIndex - 1)).Take(parameters.PageSize)
            : source;
    }

    public static IQueryable<T> ToPagedList<T>(this IQueryable<T> source, PaginationParameters? parameters)
        where T : class
    {
        parameters ??= new PaginationParameters {PageSize = 20, PageIndex = 1};

        return parameters.PageSize >= 0 && parameters.PageIndex > 0
            ? source.Skip(parameters.PageSize * (parameters.PageIndex - 1)).Take(parameters.PageSize)
            : source;

    }
}