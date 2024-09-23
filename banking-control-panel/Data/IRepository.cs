using System.Linq.Expressions;

namespace banking_control_panel.Data;

/// <summary>
///   A generic repository interface that defines the basic operations that can be performed on an entity
/// </summary>
/// <typeparam name="T">
///  The type of the entity to be used
/// </typeparam>
/// <typeparam name="TKey">
///     The type of the entity's primary key
/// </typeparam>
public interface IRepository<T>
{
    Task<T?> GetByIdAsync(Guid id);
    Task<List<T>> ListAsync();
    Task<T> AddAsync(T entity);
    void DeleteAsync(T entity);

    /// <summary>
    ///     Get all entities with pagination support
    /// </summary>
    /// <param name="pageIndex">
    ///     The index of the page to be retrieved
    /// </param>
    /// <param name="pageSize">
    ///     The number of items in each page
    /// </param>
    /// <returns>
    ///    A paged result of entities wrapped <see cref="PagedResult{T}">PagedResult</see>
    /// </returns>
    Task<PagedResult<T>> GetAllPagedAsync(int pageIndex, int pageSize);

    /// <summary>
    ///   Get all entities with pagination support and sorting
    /// </summary>
    /// <param name="pageIndex">
    ///   The index of the page to be retrieved
    /// </param>
    /// <param name="pageSize">
    ///  The number of items in each page
    /// </param>
    /// <param name="orderBy">
    ///  The property to be sorted by
    /// </param>
    /// <param name="ascending">
    ///   Whether the sorting is ascending or descending
    /// </param>
    /// <returns>
    ///     A paged result of entities wrapped <see cref="PagedResult{T}">PagedResult</see>
    /// </returns>
    Task<PagedResult<T>> GetSortedByPagedAsync(int pageIndex, int pageSize, Expression<Func<T, object>> orderBy,
        bool ascending = true);

    /// <summary>
    ///   Get all entities sorted by a property
    /// </summary>
    /// <param name="orderBy">
    ///     The property to be sorted by
    /// </param>
    /// <param name="ascending">
    ///    Whether the sorting is ascending or descending
    /// </param>
    /// <returns>
    ///   A list of entities sorted by the property
    /// </returns>
    Task<IEnumerable<T>> GetSortByAsync(Expression<Func<T, object>> orderBy, bool ascending = true);


    /// <summary>
    ///    Get all entities that satisfy the condition
    /// </summary>
    /// <param name="condition">
    ///    The condition to be satisfied
    /// </param>
    /// <returns>
    ///   A list of entities that satisfy the condition
    /// </returns>
    Task<List<T>> GetAllWhereAsync(Expression<Func<T, bool>> condition);

    
    /// <summary>
    ///   Get the first entity that satisfies the condition
    /// </summary>
    /// <param name="condition">
    ///     The condition to be satisfied
    /// </param>
    /// <returns >
    ///   The first entity that satisfies the condition
    /// </returns>
    Task<T?> GetFirstWhereAsync(Expression<Func<T, bool>> condition);

    /// <summary>
    ///  Get the last entity that satisfies the condition
    /// </summary>
    /// <param name="condition">
    ///  The condition to be satisfied
    /// </param>
    /// <returns>
    ///     The last entity that satisfies the condition
    /// </returns>
    Task<T?> GetLastWhereAsync(Expression<Func<T, bool>> condition);

    /// <summary>
    ///  Count the number of entities that satisfy the condition
    /// </summary>
    /// <param name="condition">
    /// The condition to be satisfied
    /// </param>
    /// <returns>
    ///  The number of entities that satisfy the condition
    /// </returns>
    Task<int> CountWhereAsync(Expression<Func<T, bool>> condition);

    /// <summary>
    /// Check if there is any entity that satisfies the condition
    /// </summary>
    /// <param name="condition">
    /// The condition to be satisfied
    /// </param>
    /// <returns>
    /// Whether there is any entity that satisfies the condition
    /// </returns>
    Task<bool> AnyAsync(Expression<Func<T, bool>> condition);


    /// <summary>
    /// Save changes to the database
    /// </summary>
    /// <returns>
    ///     The number of entities that were saved
    /// </returns>
    Task<int> SaveChangesAsync();
}