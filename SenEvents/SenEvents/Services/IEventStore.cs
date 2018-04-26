using System.Collections.Generic;
using System.Threading.Tasks;

namespace SenEvents
{
    public interface IEventStore<T>
    {
        Task<string> AddItemAsync(T item);
        Task<bool> UpdateItemAsync(T item);
        Task<bool> DeleteItemAsync(int id);
        Task<T> GetItemAsync(int id);
        Task<IEnumerable<T>> GetItemsAsync(bool forceRefresh = false);
        Task<bool> IsAttending(string userEmail);
    }
}