using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SenEvents
{
    public interface IUserStore
    {
        Task<string> GetCurrentUserEmailAsync();
        Task<bool> IsAUserConnectedAsync();
        Task<string> GetCurrentUserCityAsync();
        Task<bool> SaveConnetedUserAsync(User user);
        Task<bool> DeleteConnectedUserAsync();

        Task<string> GetAllUsersAsync();
        Task<User> GetUserAsync(string email);
        Task<string> AddUserAsync(User user);
    }
}
