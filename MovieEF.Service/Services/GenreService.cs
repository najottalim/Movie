using System.Linq.Expressions;
using MovieEF.Data.IRepositories;
using MovieEF.Domain.Commons;
using MovieEF.Domain.Entities;
using MovieEF.Service.Exceptions;
using MovieEF.Service.Extensions;
using MovieEF.Service.Interfaces;
using AutoMapper;
using MovieEF.Data.Contexts;
using MovieEF.Data.Repositories;
using MovieEF.Service.Mappers;

namespace MovieEF.Service.Services;

public class GenreService : IGenreService
{
    private readonly IMapper _mapper;
    private readonly IGenericRepository<Genre> _genreRepository;
    private readonly MovieEFDbContext _dbContext;

    public GenreService()
    {
        _dbContext = new MovieEFDbContext();
        _genreRepository = new GenericRepository<Genre>(_dbContext);
        _mapper = new Mapper(new MapperConfiguration(
            c => c.AddProfile<MapperProfile>()));
    }

    public async Task<Genre> CreateAsync(Genre model)
    {
        // Logic goes here...
        
        var result = (await _genreRepository.CreateAsync(model)).Entity;
        await _dbContext.SaveChangesAsync();

        return result;
    }

    public async Task<Genre> UpdateAsync(int id, Genre model)
    {
        var existGenre = await _genreRepository.GetAsync(genre => genre.Id == id);
        
        if (existGenre is null)
        {
            throw new HttpStatusCodeException(400, "Genre not found!");
        }
        
        // Updates goes here
        // model -> existGenre
        _genreRepository.Update(existGenre);
        await _dbContext.SaveChangesAsync();

        return existGenre;
    }

    public async Task<bool> DeleteAsync(Expression<Func<Genre, bool>> expression)
    {
        var genres = _genreRepository.Where(expression);
        
        if (!genres.Any())
        {
            throw new HttpStatusCodeException(400, "Genre not found!");
        }

        _genreRepository.DeleteRange(genres);
        await _dbContext.SaveChangesAsync();
        
        return true;
    }

    public Task<Genre?> GetAsync(Expression<Func<Genre, bool>> expression)
        => _genreRepository.GetAsync(expression);

    public Task<IEnumerable<Genre>> GetAllAsync(Expression<Func<Genre, bool>>? expression = null,
        PaginationParameters? parameters = null)
        => Task.FromResult<IEnumerable<Genre>>(_genreRepository.Where(expression).ToPagedList(parameters));
}
