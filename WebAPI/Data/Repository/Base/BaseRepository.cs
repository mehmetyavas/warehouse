using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata;
using WebAPI.Data.Dto.Pagination;
using WebAPI.Data.Entity.Base;
using WebAPI.Data.Enum;
using WebAPI.Extensions;

namespace WebAPI.Data.Repository.Base
{
    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TContext"></typeparam>
    public class BaseRepository<TEntity, TContext>
        where TEntity : class, IEntity, new()
        where TContext : DbContext
    {
        public BaseRepository(TContext context)
        {
            Context = context;
        }

        protected TContext Context { get; }

        public TEntity Add(TEntity entity)
        {
            return Context.Add(entity).Entity;
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            Context.AddRange(entities);
        }

        public TEntity Update(TEntity entity)
        {
            Context.Update(entity);
            return entity;
        }

        public void Delete(TEntity entity)
        {
            Context.Remove(entity);
        }

        private TEntity Entity()
        {
            return new TEntity();
        }


        public TEntity Get(Expression<Func<TEntity, bool>> expression)
        {
            return Context.Set<TEntity>().FirstOrDefault(expression)!;
        }


        public async Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> expression,
            CancellationToken cancellationToken = default, bool ignore = false)
        {
            return (ignore == false
                ? await Context.Set<TEntity>().AsQueryable().FirstOrDefaultAsync(expression, cancellationToken)
                : await Context.Set<TEntity>()
                    .IgnoreQueryFilters()
                    .FirstOrDefaultAsync(expression, cancellationToken))!;
        }

        public IEnumerable<TEntity> GetList(Expression<Func<TEntity, bool>>? expression = null)
        {
            return expression == null
                ? Context.Set<TEntity>().AsNoTracking()
                : Context.Set<TEntity>().Where(expression).AsNoTracking();
        }


        public async Task<IEnumerable<TEntity>> GetListAsync(CancellationToken cancellationToken = default,
            Expression<Func<TEntity, bool>>? expression = null, bool ignore = false)
        {
            if (ignore)
            {
                return expression == null
                    ? await Context.Set<TEntity>()
                        .IgnoreQueryFilters()
                        .ToListAsync(cancellationToken)
                    : await Context.Set<TEntity>()
                        .IgnoreQueryFilters()
                        .Where(expression)
                        .ToListAsync(cancellationToken);
            }

            return expression == null
                ? await Context.Set<TEntity>().ToListAsync(cancellationToken)
                : await Context.Set<TEntity>().Where(expression).ToListAsync(cancellationToken);
        }


        public async Task<IEnumerable<TSource>> GetSelectListAsync<TSource>(Expression<Func<TEntity, TSource>> select,
            CancellationToken cancellation = default,
            Expression<Func<TSource, bool>>? expression = null
            , bool ignore = false)
        {
            if (ignore)
            {
                return expression == null
                    ? await Context.Set<TEntity>()
                        .IgnoreQueryFilters()
                        .Select(select)
                        .ToListAsync(cancellation)
                    : await Context.Set<TEntity>()
                        .IgnoreQueryFilters()
                        .Select(select)
                        .Where(expression)
                        .ToListAsync(cancellation);
            }

            return expression == null
                ? await Context.Set<TEntity>().Select(select).ToListAsync(cancellation)
                : await Context.Set<TEntity>().Select(select).Where(expression).ToListAsync(cancellation);
        }

        public async Task<TSource?> GetSelectAsync<TSource>(Expression<Func<TEntity, TSource>> select,
            Expression<Func<TSource, bool>> expression,
            CancellationToken cancellation = default,
            bool ignore = false)
            where TSource : IDto
        {
            return (ignore
                ? await Context.Set<TEntity>()
                    .IgnoreQueryFilters()
                    .Select(select)
                    .AsQueryable()
                    .FirstOrDefaultAsync(expression, cancellation)!
                : await Context.Set<TEntity>()
                    .Select(select)
                    .AsQueryable()
                    .FirstOrDefaultAsync(expression, cancellation))!;
        }


        public IQueryable<TEntity> PagingList(PagingRequest request,
            Expression<Func<TEntity, bool>>? exp = null)
        {
            var entity = exp == null
                ? Context.Set<TEntity>().AsQueryable()
                : Context.Set<TEntity>().Where(exp).AsQueryable();


            var page = request.Page < 1 ? 0 : request.Page - 1;


            if (request is { Column: not null, SearchParam: not null })
            {
                var parameter = Expression.Parameter(typeof(TEntity), "x");
                var property = Expression.Property(parameter, request.Column);

                Expression body;

                if (property.Type == typeof(string))
                {
                    var constant = Expression.Constant(request.SearchParam.ToLower());
                    var toLowerMethod = typeof(string).GetMethod("ToLower", new Type[0]);

                    var callToLower = Expression.Call(property, toLowerMethod!);
                    var containsMethod = typeof(string).GetMethod("Contains", new[]
                    {
                        typeof(string)
                    });
                    body = Expression.Call(callToLower, containsMethod!, constant);
                }
                else
                {
                    var constant = Expression.Constant(int.Parse(request.SearchParam));
                    body = Expression.Equal(property, constant);
                }

                var expression = Expression.Lambda<Func<TEntity, bool>>(body, parameter);


                entity = entity.Where(expression).AsQueryable();
            }

            var order = entity.AscOrDescOrder(request.SortDirection, Entity().SortBy(request.SortBy));

            var limit = order.Skip(page * request.Limit).Take(request.Limit);


            return limit;
        }

        public async Task<(List<TEntity> List, int count)> PagingListAsync(PagingRequest request,
            IQueryable<TEntity> entity,
            CancellationToken cancellationToken = default)
        {
            var page = request.Page < 1 ? 0 : request.Page - 1;


            if (request is { Column: not null, SearchParam: not null })
            {
                var parameter = Expression.Parameter(typeof(TEntity), "x");
                var property = Expression.Property(parameter, request.Column);

                Expression body;

                if (property.Type == typeof(string))
                {
                    var constant = Expression.Constant(request.SearchParam.ToLower());
                    var toLowerMethod = typeof(string).GetMethod("ToLower", new Type[0]);

                    var callToLower = Expression.Call(property, toLowerMethod!);
                    var containsMethod = typeof(string).GetMethod("Contains", new[]
                    {
                        typeof(string)
                    });
                    body = Expression.Call(callToLower, containsMethod!, constant);
                }
                else
                {
                    var constant = Expression.Constant(int.Parse(request.SearchParam));
                    body = Expression.Equal(property, constant);
                }

                var expression = Expression.Lambda<Func<TEntity, bool>>(body, parameter);


                entity = entity.Where(expression).AsQueryable();
            }

            var count = await entity.CountAsync(cancellationToken);


            var order = entity.AscOrDescOrder(request.SortDirection, Entity().SortBy(request.SortBy));

            var limit = order.Skip(page * request.Limit).Take(request.Limit);

            var result = await limit.ToListAsync(cancellationToken);

            return (result, count);
        }

        public async Task<(List<TEntity> List, int count)> PagingListAsync(BasePagingRequest request,
            IQueryable<TEntity> entity,
            CancellationToken cancellationToken = default)
        {
            var page = request.Page < 1 ? 0 : request.Page - 1;


            var count = await entity.CountAsync(cancellationToken);


            var order = entity.AscOrDescOrder(request.SortDirection, Entity().SortBy(request.SortBy));

            var limit = order.Skip(page * request.Limit).Take(request.Limit);

            var result = await limit.ToListAsync(cancellationToken);

            return (result, count);
        }

        public IQueryable<TEntity> Query()
        {
            return Context.Set<TEntity>().AsQueryable();
        }

        public async Task<int> CountAsync(CancellationToken cancellationToken,
            Expression<Func<TEntity, bool>>? expression = null)
        {
            if (expression == null)
            {
                return await Context.Set<TEntity>().CountAsync(cancellationToken);
            }
            else
            {
                return await Context.Set<TEntity>().CountAsync(expression, cancellationToken);
            }
        }

        public int GetCount(Expression<Func<TEntity, bool>>? expression = null)
        {
            return expression == null ? Context.Set<TEntity>().Count() : Context.Set<TEntity>().Count(expression);
        }

        public async Task SoftDelete(TEntity tEntity, CancellationToken cancellationToken = default)
        {
            if (tEntity is IEntity entity)
                await HandleCascadeSoftDelete(Context.Entry(entity), cancellationToken);
        }


        private async Task HandleCascadeSoftDelete(EntityEntry<IEntity> entry,
            CancellationToken cancellationToken)
        {
            entry.State = EntityState.Modified;
            entry.Entity.RowStatus = RowStatus.Deleted;
            entry.Entity.DeletedAt = DateTime.Now;

            //TODO: Refactor With GetSkipNavigations

            await HandleNavigationsDelete(entry, cancellationToken);
        }

        private async Task HandleNavigationsDelete(EntityEntry<IEntity> entry, CancellationToken cancellationToken)
        {
            foreach (var navigationEntry in entry.Navigations.Where(x =>
                         !((INavigation)x.Metadata).IsOnDependent))
            {
                await navigationEntry.LoadAsync(cancellationToken);
                if (navigationEntry is CollectionEntry collectionEntry)
                {
                    foreach (var dependentEntry in collectionEntry.CurrentValue)
                    {
                        if (dependentEntry is IEntity abstractEntiy)
                        {
                            await HandleCascadeSoftDelete(Context.Entry(abstractEntiy), cancellationToken);
                        }
                    }
                }
                else
                {
                    var dependentEntry = navigationEntry.CurrentValue;
                    if (dependentEntry is IEntity auditEntity)
                    {
                        await HandleCascadeSoftDelete(Context.Entry(auditEntity), cancellationToken);
                    }
                }
            }
        }
    }
}