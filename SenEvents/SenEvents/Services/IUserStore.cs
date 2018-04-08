using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SenEvents
{
    public interface IUserStore
    {
        Task<string> GetCurrentUser();
        string GetCurrentUserCity();
        Task<string> GetCurrentUserCityAsync();
        Task<string> GetAllUsersAsync();
        Task<User> GetUserAsync(string email);
        Task<string> AddUserAsync(User user);
    }
}
