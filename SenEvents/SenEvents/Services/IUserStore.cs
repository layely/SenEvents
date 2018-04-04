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
    }
}
