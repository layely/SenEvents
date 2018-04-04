using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[assembly: Xamarin.Forms.Dependency(typeof(SenEvents.MockUserStore))]
namespace SenEvents
{
    public class MockUserStore : IUserStore
    {
        public async Task<string> GetCurrentUser()
        {
            return await Task.FromResult(string.Empty);
        }

    }
}
