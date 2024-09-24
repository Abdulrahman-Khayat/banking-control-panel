namespace banking_control_panel.Data;

public interface IUnitOfWork : IDisposable
{
    IRepository<TEntity> Repository<TEntity>() where TEntity : class;
    Task<int> CompleteAsync();
}