using System.Collections.Generic;
using System.Threading.Tasks;

namespace SenEvents
{
    public interface IEventStore<T>
    {
        Task<string> AddEventAsync(T item);
        Task<Event> GetEventAsync(long id);
        Task<List<Event>> GetAllEventAsync();
        Task<bool> UpdateItemAsync(T item);
        Task<bool> DeleteItemAsync(long id);
        //Task<T> GetItemAsync(int id);
        Task<IEnumerable<T>> GetItemsAsync(bool forceRefresh = false);
        Task<bool> IsAttending(string userEmail);
    }
}