using Microsoft.EntityFrameworkCore;
using Sibers.Common.Entity.InterfaceDB;
using Sibers.Context.Configuration;
using Sibers.Context.Contracts;
using Sibers.Context.Contracts.Models;

namespace Sibers.Context
{
    /// <summary>
    /// Контекст работы с БД
    /// </summary>
    /// <remarks>
    /// 1) dotnet tool install --global dotnet-ef
    /// 2) dotnet tool update --global dotnet-ef
    /// 3) dotnet ef migrations add [name] --project Sibers.Context\Sibers.Context.csproj
    /// 4) dotnet ef database update --project Sibers.Context\Sibers.Context.csproj
    /// 5) dotnet ef database update [targetMigrationName] --Sibers.Context\Sibers.Context.csproj
    /// </remarks>
    public class SibersContext : DbContext,
        ISibersContext,
        IDbRead,
        IDbWriter,
        IUnitOfWork
    {

        public DbSet<Employee> Employees { get; set; }

        public DbSet<Project> Projects { get; set; }

        public DbSet<Company> Companies { get; set; }

        /// <summary>
        /// Инициализация контекста
        /// </summary>
        public SibersContext(DbContextOptions<SibersContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(IContextConfigurationAnchor).Assembly);
        }

        IQueryable<TEntity> IDbRead.Read<TEntity>()
            => base.Set<TEntity>()
                .AsNoTracking()
                .AsQueryable();

        void IDbWriter.Add<TEntities>(TEntities entity)
            => base.Entry(entity).State = EntityState.Added;

        void IDbWriter.Update<TEntities>(TEntities entity)
              => base.Entry(entity).State = EntityState.Modified;

        void IDbWriter.Delete<TEntities>(TEntities entity)
              => base.Entry(entity).State = EntityState.Deleted;


        async Task<int> IUnitOfWork.SaveChangesAsync(CancellationToken cancellationToken)
        {
            var count = await base.SaveChangesAsync(cancellationToken);
            SkipTracker();
            return count;
        }

        public void SkipTracker()
        {
            foreach (var entry in base.ChangeTracker.Entries().ToArray())
            {
                entry.State = EntityState.Detached;
            }
        }
    }
}
