using System.Linq.Expressions;
using MovieEF.Domain.Commons;
using MovieEF.Domain.Entities;

namespace MovieEF.Service.Interfaces;

public interface IGenreService
{
    Task<Genre> CreateAsync(Genre model);
    
    Task<Genre> UpdateAsync(int id, Genre model);
    
    Task<bool> DeleteAsync(Expression<Func<Genre, bool>> expression);
    
    Task<Genre?> GetAsync(Expression<Func<Genre, bool>> expression);

    Task<IEnumerable<Genre>> GetAllAsync(Expression<Func<Genre, bool>>? expression = null,
        PaginationParameters? parameters = null);
}