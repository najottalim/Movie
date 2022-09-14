using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using MovieEF.Data.Contexts;
using MovieEF.Data.IRepositories;

namespace MovieEF.Data.Repositories;

public class GenericRepository<TSource> : IGenericRepository<TSource> where TSource : class
{
    private readonly MovieEFDbContext _dbContext;
    private readonly DbSet<TSource> _dbSet;
    
    public GenericRepository(MovieEFDbContext dbContext)
    {
        _dbContext = dbContext;
        _dbSet = dbContext.Set<TSource>();
    }

    public Task CreateRangeAsync(IEnumerable<TSource> sources) 
        => _dbSet.AddRangeAsync(sources);
    
    public ValueTask<EntityEntry<TSource>> CreateAsync(TSource source)
        => _dbSet.AddAsync(source);

    public TSource Update(TSource source)
        => _dbSet.Update(source).Entity;

    public Task<TSource?> GetAsync(Expression<Func<TSource, bool>> expression)
        => _dbSet.FirstOrDefaultAsync(expression);

    public Task<bool> AnyAsync(Expression<Func<TSource, bool>> expression) => _dbSet.AnyAsync(expression);

    public void DeleteRange(IEnumerable<TSource> sources) => _dbSet.RemoveRange(sources);

    public IQueryable<TSource> Where(Expression<Func<TSource, bool>>? expression = null)
        => expression is null ? _dbSet : _dbSet.Where(expression);
}