namespace banking_control_panel.Data;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;
    private readonly Dictionary<Type, object> _repositories = new();

    public UnitOfWork(AppDbContext context)
    {
        _context = context;
    }

    public IRepository<TEntity> Repository<TEntity>() where TEntity : class
    {
        var type = typeof(TEntity);
        if (!_repositories.ContainsKey(type))
        {
            var repositoryInstance = new BaseRepository<TEntity>(_context);
            _repositories.Add(type, repositoryInstance);
        }
        return (IRepository<TEntity>)_repositories[type];
    }

    public Task<int> CompleteAsync()
    {
        return _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}