using System.Linq.Expressions;
using MovieEF.Domain.Commons;
using MovieEF.Domain.Entities;

namespace MovieEF.Service.Interfaces;

public interface IActorService
{
    Task<Actor> CreateAsync(Actor model);
    
    Task<Actor> UpdateAsync(int id, Actor model);
    
    Task<bool> DeleteAsync(Expression<Func<Actor, bool>> expression);
    
    Task<Actor?> GetAsync(Expression<Func<Actor, bool>> expression);

    Task<IEnumerable<Actor>> GetAllAsync(Expression<Func<Actor, bool>>? expression = null,
        PaginationParameters? parameters = null);
}