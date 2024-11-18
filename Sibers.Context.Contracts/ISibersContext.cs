using Microsoft.EntityFrameworkCore;
using Sibers.Context.Contracts.Models;

namespace Sibers.Context.Contracts
{
    /// <summary>
    /// Контекст работы с сущностями
    /// </summary>
    public interface ISibersContext
    {
        /// <summary>Список <inheritdoc cref="Employee"/></summary>
        DbSet<Employee> Employees { get; }

        /// <summary>Список <inheritdoc cref="Project"/></summary>
        DbSet<Project> Projects { get; }

        /// <summary>Список <inheritdoc cref="Company"/></summary>
        DbSet<Company> Companies { get; }
    }
}
