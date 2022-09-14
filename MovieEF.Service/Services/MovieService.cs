using System.Linq.Expressions;
using MovieEF.Data.IRepositories;
using MovieEF.Domain.Commons;
using MovieEF.Domain.Entities;
using MovieEF.Service.Exceptions;
using MovieEF.Service.Extensions;
using MovieEF.Service.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MovieEF.Data.Contexts;
using MovieEF.Data.Repositories;
using MovieEF.Service.DTOs.Movies;
using MovieEF.Service.Mappers;

namespace MovieEF.Service.Services;

public class MovieService : IMovieService
{
    private readonly IMapper _mapper;
    private readonly IGenericRepository<Movie> _movieRepository;
    private readonly IGenericRepository<Actor> _actorRepository;
    private readonly IGenericRepository<MovieActor> _movieActorRepository;
    
    private readonly MovieEFDbContext _dbContext;

    public MovieService()
    {
        _dbContext = new MovieEFDbContext();
        _movieRepository = new GenericRepository<Movie>(_dbContext);
        _actorRepository = new GenericRepository<Actor>(_dbContext);
        _movieActorRepository = new GenericRepository<MovieActor>(_dbContext);
        
        _mapper = new Mapper(new MapperConfiguration(
            c => c.AddProfile<MapperProfile>()));
    }

    public async Task<MovieViewDto> CreateAsync(MovieForCreationDto dto)
    {
        var anyMovie = await _movieRepository.AnyAsync(movie =>
            movie.Name.Equals(dto.Name) && movie.PremiereDate.Equals(dto.PremiereDate));

        if (anyMovie)
        {
            throw new Exception("This movie already exist");
        }

        if (dto.ActorIds != null)
            foreach (var actorId in dto.ActorIds)
            {
                var anyActor = await _actorRepository.AnyAsync(actor => actor.Id == actorId);

                if (!anyActor)
                {
                    throw new Exception("Actor not found!");
                }
            }

        var movie = _mapper.Map<Movie>(dto);
        var resultMovie = (await _movieRepository.CreateAsync(movie)).Entity;
        await _dbContext.SaveChangesAsync();

        
        var viewDto = _mapper.Map<MovieViewDto>(resultMovie);
        
        if (dto.ActorIds != null)
        {
            var movieActors = dto.ActorIds.Select(actorId => new MovieActor
            {
                ActorId = actorId,
                MovieId = resultMovie.Id
            });

            await _movieActorRepository.CreateRangeAsync(movieActors);
            await _dbContext.SaveChangesAsync();

            viewDto.Actors = _movieActorRepository.Where(movieActor => movieActor.MovieId == resultMovie.Id)
                .Include(p => p.Actor).Select(actor => actor.Actor)!;
        }
        
        return viewDto;
    }

    public async Task<Movie> UpdateAsync(int id, Movie model)
    {
        var existMovie = await _movieRepository.GetAsync(movie => movie.Id == id);
        
        if (existMovie is null)
        {
            throw new HttpStatusCodeException(400, "Movie not found!");
        }
        
        // Updates goes here
        // model -> existMovie
        _movieRepository.Update(existMovie);
        await _dbContext.SaveChangesAsync();

        return existMovie;
    }

    public async Task<bool> DeleteAsync(Expression<Func<Movie, bool>> expression)
    {
        var movies = _movieRepository.Where(expression);
        
        if (!movies.Any())
        {
            throw new HttpStatusCodeException(400, "Movie not found!");
        }

        _movieRepository.DeleteRange(movies);
        await _dbContext.SaveChangesAsync();
        
        return true;
    }

    public Task<Movie?> GetAsync(Expression<Func<Movie, bool>> expression)
        => _movieRepository.GetAsync(expression);

    public Task<IEnumerable<Movie>> GetAllAsync(Expression<Func<Movie, bool>>? expression = null,
        PaginationParameters? parameters = null)
        => Task.FromResult<IEnumerable<Movie>>(_movieRepository.Where(expression).ToPagedList(parameters));
}
