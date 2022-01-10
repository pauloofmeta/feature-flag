using System.Linq.Expressions;
using Carguero.FeatureFlag.Abstrations;
using Microsoft.EntityFrameworkCore;

namespace Carguero.FeatureFlag.Data.Repositories;

public class BaseRepository<TEntity> : IRepository<TEntity> 
    where TEntity: class
{
    private readonly MyDbContext _dbContext;
    private DbSet<TEntity> Set => _dbContext.Set<TEntity>();

    public BaseRepository(MyDbContext dbContext) => _dbContext = dbContext;

    public async Task AddAsync(TEntity data) => await Set.AddAsync(data);

    public void Update(TEntity data) => Set.Update(data);

    public void Delete(TEntity data) => Set.Remove(data);

    public IAsyncEnumerable<TEntity> GetAllAsync() => Set.AsAsyncEnumerable();
    
    public IAsyncEnumerable<TEntity> GetAllAsync(Expression<Func<TEntity, bool>> expression) => 
        Set.Where(expression).AsAsyncEnumerable();

    public Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> expression) => 
        Set.SingleOrDefaultAsync(expression);
    
    public IQueryable<TEntity> GetQueryable() => 
        Set.AsQueryable();

    public async Task<bool> CommitAsync() => await _dbContext.SaveChangesAsync() > 0;
}