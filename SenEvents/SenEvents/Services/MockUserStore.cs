using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

[assembly: Xamarin.Forms.Dependency(typeof(SenEvents.MockUserStore))]
namespace SenEvents
{
    public class MockUserStore : IUserStore
    {
        public const string BaseUrl = "http://192.168.1.66:8000";

        public async Task<string> GetCurrentUser()
        {
            return await Task.FromResult(string.Empty);
        }

        public string GetCurrentUserCity()
        {
            return Cities.Dakar;
        }

        public async Task<string> GetCurrentUserCityAsync()
        {
            return await Task.FromResult(Cities.Dakar);
        }

        public async Task<string> GetAllUsersAsync()
        {
            //var rxcui = "198440";
            var request = HttpWebRequest.Create(string.Format(@"{0}/users", BaseUrl));
            request.ContentType = "application/json";
            request.Method = "GET";

            using (HttpWebResponse response = await request.GetResponseAsync() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK) 
                    Debug.WriteLine("Error fetching data. Server returned status code: {0}", response.StatusCode);
                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                {
                    var content = reader.ReadToEnd();
                    if (string.IsNullOrWhiteSpace(content))
                    {
                        Debug.WriteLine("Response contained empty body...");
                    }
                    else
                    {
                        Debug.WriteLine("Response Body: \r\n {0}", content);
                    }

                    return await Task.FromResult(content);
                }
            }
        }
    }
}
