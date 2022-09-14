using System.Linq.Expressions;
using MovieEF.Domain.Commons;
using MovieEF.Domain.Entities;
using MovieEF.Service.DTOs.Movies;

namespace MovieEF.Service.Interfaces;

public interface IMovieService
{
    Task<MovieViewDto> CreateAsync(MovieForCreationDto model);
    
    Task<Movie> UpdateAsync(int id, Movie model);
    
    Task<bool> DeleteAsync(Expression<Func<Movie, bool>> expression);
    
    Task<Movie?> GetAsync(Expression<Func<Movie, bool>> expression);

    Task<IEnumerable<Movie>> GetAllAsync(Expression<Func<Movie, bool>>? expression = null,
        PaginationParameters? parameters = null);
}