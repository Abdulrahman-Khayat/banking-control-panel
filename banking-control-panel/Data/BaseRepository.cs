using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace banking_control_panel.Data;


/// <summary>
///  A base repository class that implements the <see cref="IRepository{T}">IRepository</see> interface
/// </summary>
/// <param name="context">
///  The database context to be used
/// </param>
/// <inheritdoc cref="IRepository{T}"/>
public class BaseRepository<T>(AppDbContext context) : IRepository<T>
    where T : class
{
    /// <inheritdoc />
    public async Task<T?> GetByIdAsync(Guid id)
    {
        return await context.Set<T>().FindAsync(id);
    }
    
    /// <inheritdoc />
    public async Task<List<T>> ListAsync()
    {
        return await context.Set<T>().ToListAsync();
    }

    /// <inheritdoc />
    public async Task<T> AddAsync(T entity)
    {
        await context.Set<T>().AddAsync(entity);
        return entity;
    }

    /// <inheritdoc />
    public void DeleteAsync(T entity)
    {
        context.Set<T>().Remove(entity);
    }

    /// <inheritdoc />
    public async Task<PagedResult<T>> GetAllPagedAsync(int pageIndex, int pageSize)
    {
        var items = await context.Set<T>().Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
        var count = await context.Set<T>().CountAsync();
        return new PagedResult<T>(items, count, pageIndex, pageSize);
    }

    /// <inheritdoc />
    public async Task<PagedResult<T>> GetSortedByPagedAsync(int pageIndex, int pageSize,
        Expression<Func<T, object>> orderBy, bool ascending = true)
    {
        var items = ascending
            ? await context.Set<T>().OrderBy(orderBy).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync()
            : await context.Set<T>().OrderByDescending(orderBy).Skip((pageIndex - 1) * pageSize).Take(pageSize)
                .ToListAsync();
        var count = items.Count;
        return new PagedResult<T>(items, count, pageIndex, pageSize);
    }

    /// <inheritdoc />
    public async Task<IEnumerable<T>> GetSortByAsync(Expression<Func<T, object>> orderBy, bool ascending = true)
    {
        return ascending
            ? await context.Set<T>().OrderBy(orderBy).ToListAsync()
            : await context.Set<T>().OrderByDescending(orderBy).ToListAsync();
    }


    /// <inheritdoc />
    public async Task<List<T>> GetAllWhereAsync(Expression<Func<T, bool>> condition)
    {
        var items = await context.Set<T>().Where(condition).ToListAsync();
        return items;
    }

    /// <inheritdoc />
    public async Task<T?> GetFirstWhereAsync(Expression<Func<T, bool>> condition)
    {
        var item = await context.Set<T>().FirstOrDefaultAsync(condition);
        return item;
    }

    /// <inheritdoc />
    public async Task<T?> GetLastWhereAsync(Expression<Func<T, bool>> condition)
    {
        var item = await context.Set<T>().LastOrDefaultAsync(condition);
        return item;
    }

    /// <inheritdoc />
    public async Task<int> CountWhereAsync(Expression<Func<T, bool>> condition)
    {
        var count = await context.Set<T>().CountAsync(condition);
        return count;
    }

    /// <inheritdoc />
    public async Task<bool> AnyAsync(Expression<Func<T, bool>> condition)
    {
        var any = await context.Set<T>().AnyAsync(condition);
        return any;
    }
    
    /// <inheritdoc />
    public async Task<int> SaveChangesAsync()
    {
        return await context.SaveChangesAsync();
    }
}