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

public class ActorService : IActorService
{
    private readonly IMapper _mapper;
    private readonly IGenericRepository<Actor> _actorRepository;
    private readonly MovieEFDbContext _dbContext;
    
    public ActorService()
    {
        _dbContext = new MovieEFDbContext();
        _actorRepository = new GenericRepository<Actor>(_dbContext);
        _mapper = new Mapper(new MapperConfiguration(
            c => c.AddProfile<MapperProfile>()));

    }

    public async Task<Actor> CreateAsync(Actor model)
    {
        // Logic goes here...
        
        var result = (await _actorRepository.CreateAsync(model)).Entity;
        await _dbContext.SaveChangesAsync();

        return result;
    }

    public async Task<Actor> UpdateAsync(int id, Actor model)
    {
        var existActor = await _actorRepository.GetAsync(actor => actor.Id == id);
        
        if (existActor is null)
        {
            throw new HttpStatusCodeException(400, "Actor not found!");
        }
        
        // Updates goes here
        // model -> existActor
        _actorRepository.Update(existActor);
        await _dbContext.SaveChangesAsync();

        return existActor;
    }

    public async Task<bool> DeleteAsync(Expression<Func<Actor, bool>> expression)
    {
        var actors = _actorRepository.Where(expression);
        
        if (!actors.Any())
        {
            throw new HttpStatusCodeException(400, "Actor not found!");
        }

        _actorRepository.DeleteRange(actors);
        await _dbContext.SaveChangesAsync();
        
        return true;
    }

    public Task<Actor?> GetAsync(Expression<Func<Actor, bool>> expression)
        => _actorRepository.GetAsync(expression);

    public Task<IEnumerable<Actor>> GetAllAsync(Expression<Func<Actor, bool>>? expression = null,
        PaginationParameters? parameters = null)
        => Task.FromResult<IEnumerable<Actor>>(_actorRepository.Where(expression).ToPagedList(parameters));
}
