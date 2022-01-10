using System.Linq.Expressions;

namespace Carguero.FeatureFlag.Abstrations;

public interface IRepository<TEntity> where TEntity:class
{
    public Task AddAsync(TEntity data);

    public void Update(TEntity data);

    public void Delete(TEntity data);

    public IAsyncEnumerable<TEntity> GetAllAsync();
    
    public IAsyncEnumerable<TEntity> GetAllAsync(Expression<Func<TEntity, bool>> expression);

    public Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> expression);

    public IQueryable<TEntity> GetQueryable();

    public Task<bool> CommitAsync();
}