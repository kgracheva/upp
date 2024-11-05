using AutoMapper.QueryableExtensions;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace upp.Mapper
{
    public static class MappingExtensions
    {
        public static Task<PaginatedList<TDestination>> PaginatedListAsync<TDestination>(this IQueryable<TDestination> queryable, int pageNumber, int pageSize) where TDestination : class
        => PaginatedList<TDestination>.CreateAsync(queryable.AsNoTracking(), pageNumber, pageSize);

        public static Task<List<TDestination>> ProjectToListAsync<TDestination>(this IQueryable queryable, AutoMapper.IConfigurationProvider configuration) where TDestination : class
            => queryable.ProjectTo<TDestination>(configuration).AsNoTracking().ToListAsync();

        public static Task<PaginatedList<TDestination>> MapAndPaginateToListAsync<TDestination>(this IQueryable queryable, AutoMapper.IConfigurationProvider configuration, int pageNumber, int pageSize, CancellationToken token = default) where TDestination : class
        {
            return PaginatedList<TDestination>.CreateAsync(queryable.ProjectTo<TDestination>(configuration).AsNoTracking(), pageNumber, pageSize);
        }

        public static async Task<PaginatedList<TDestination>> ToPaginateListAsync<TSource, TDestination>(this IQueryable<TSource> queryable, IMapper mapper, int pageNumber, int pageSize, CancellationToken token) where TDestination : class
        {
            var count = await queryable.CountAsync();
            var entries = await queryable.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync(token);
            var mappedEntries = mapper.Map<List<TDestination>>(entries);

            return new PaginatedList<TDestination>(mappedEntries, count, pageNumber, pageSize);
        }
    }
}
