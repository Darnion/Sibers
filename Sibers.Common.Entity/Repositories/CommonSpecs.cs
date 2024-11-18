using Microsoft.EntityFrameworkCore;
using Sibers.Common.Entity.EntityInterface;
using System.Collections.ObjectModel;

namespace Sibers.Common.Entity.Repositories
{
    /// <summary>
    /// Общие спецификации чтения
    /// </summary>
    public static class CommonSpecs
    {
        /// <summary>
        /// По идентификатору 
        /// </summary>
        public static IQueryable<TEntity> ById<TEntity>(this IQueryable<TEntity> query, Guid id) where TEntity : class, IEntityWithId
            => query.Where(x => x.Id == id);

        /// <summary>
        /// По идентификаторам
        /// </summary>
        public static IQueryable<TEntity> ByIds<TEntity>(this IQueryable<TEntity> query, IEnumerable<Guid> ids) where TEntity : class, IEntityWithId
        {
            var cnt = ids.Count();
            switch (cnt)
            {
                case 0:
                    return query.Where(x => false);
                case 1:
                    return query.ById(ids.First());
                default:
                    return query.Where(x => ids.Contains(x.Id));
            }
        }

        /// <summary>
        /// Не удаленные записи
        /// </summary>
        public static IQueryable<TEntity> NotDeletedAt<TEntity>(this IQueryable<TEntity> query) where TEntity : class, IEntityAuditDeleted
            => query.Where(x => x.DeletedAt == null);

        /// <summary>
        /// Возвращает <see cref="IReadOnlyCollection{TEntity}"/>
        /// </summary>
        public static Task<IReadOnlyCollection<TEntity>> ToReadOnlyCollectionAsync<TEntity>(this IQueryable<TEntity> query,
            CancellationToken cancellationToken)
            => query.ToListAsync(cancellationToken)
                .ContinueWith(x => new ReadOnlyCollection<TEntity>(x.Result) as IReadOnlyCollection<TEntity>,
                    cancellationToken);
    }
}