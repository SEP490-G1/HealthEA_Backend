using Domain.Models.Entities;

namespace Domain.Interfaces.IRepositories;

public interface IEventRepository
{
    Task<Event> GetEventByIdAsync(int id); // Read
    Task<IEnumerable<Event>> GetAllEventsAsync(); // Read all
    Task AddEventAsync(Event eventEntity); // Create
    Task UpdateEventAsync(Event eventEntity); // Update
    Task DeleteEventAsync(int id); // Delete
}
