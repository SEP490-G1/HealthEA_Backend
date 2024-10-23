using Domain.Interfaces.IRepositories;
using Domain.Models.Entities;
using Infrastructure.SQLServer;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class EventRepository 
    {
        //private readonly SqlDBContext _context;

        //public EventRepository(SqlDBContext context)
        //{
        //    _context = context;
        //}

        //public async Task AddEventAsync(Event eventEntity)
        //{
        //    await _context.Events.AddAsync(eventEntity);
        //    await _context.SaveChangesAsync();
        //}

        //public async Task<Event> GetEventByIdAsync(Guid id)
        //{
        //    return await _context.Events.FindAsync(id);
        //}

        //public async Task<IEnumerable<Event>> GetAllEventsAsync()
        //{
        //    return await _context.Events.ToListAsync();
        //}

        //public async Task UpdateEventAsync(Event eventEntity)
        //{
        //    _context.Events.Update(eventEntity);
        //    await _context.SaveChangesAsync();
        //}

        //public async Task DeleteEventAsync(Guid id)
        //{
        //    var eventEntity = await _context.Events.FindAsync(id);
        //    if (eventEntity != null)
        //    {
        //        _context.Events.Remove(eventEntity);
        //        await _context.SaveChangesAsync();
        //    }
        //}
    }
}
