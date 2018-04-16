using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

[assembly: Xamarin.Forms.Dependency(typeof(SenEvents.MockUserStore))]
namespace SenEvents
{
    public class MockUserStore : IUserStore
    {
        public const string BaseUrl = "http://192.168.1.66:8000";
        const string PropertyKey_Email = "email";
        const string PropertyKey_City = "city";

        public async Task<string> GetCurrentUserEmailAsync()
        {
            string email = string.Empty;

            if (Application.Current.Properties.ContainsKey(PropertyKey_Email))
            {
                email = Application.Current.Properties[PropertyKey_Email] as string;
            }

            return await Task.FromResult(email);
        }

        public async Task<bool> IsAUserConnectedAsync()
        {
            return await Task.FromResult(!string.IsNullOrWhiteSpace(await this.GetCurrentUserEmailAsync()));
        }

        public async Task<string> GetCurrentUserCityAsync()
        {
            string city = string.Empty;

            if (Application.Current.Properties.ContainsKey(PropertyKey_City))
            {
                city = Application.Current.Properties[PropertyKey_City] as string;
            }

            return await Task.FromResult(city);
        }

        public async Task<bool> SaveConnetedUserAsync(User user)
        {
            Application.Current.Properties[PropertyKey_Email] = user.Email;
            Application.Current.Properties[PropertyKey_City] = user.City;

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteConnectedUserAsync()
        {
            Application.Current.Properties[PropertyKey_Email] = string.Empty;
            Application.Current.Properties[PropertyKey_City] = string.Empty;

            return await Task.FromResult(true);
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

        public async Task<User> GetUserAsync(string email)
        {
            //var rxcui = "198440";
            var request = HttpWebRequest.Create(string.Format(@"{0}/user/{1}", BaseUrl, email));
            request.ContentType = "application/json";
            request.Method = "GET";

            User user = null;

            using (HttpWebResponse response = await request.GetResponseAsync() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    Debug.WriteLine("Error fetching data. Server returned status code: {0}", response.StatusCode);
                }
                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                {
                    var content = reader.ReadToEnd();
                    if (string.IsNullOrWhiteSpace(content) || content.Equals("null"))
                    {
                        Debug.WriteLine("Response contained empty body...");
                    }
                    else
                    {
                        Debug.WriteLine("Response Body: \r\n {0}", content);
                        user = JsonConvert.DeserializeObject<User>(content);
                    }

                    return await Task.FromResult(user);
                }
            }
        }

        public async Task<string> AddUserAsync(User user)
        {
            var request = HttpWebRequest.Create(string.Format(@"{0}/users/", BaseUrl));
            request.ContentType = "application/json";
            request.Method = "POST";
            var data = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(user));

            using (var stream = await request.GetRequestStreamAsync())
            {
                stream.Write(data, 0, data.Length);
            }

            using (HttpWebResponse response = await request.GetResponseAsync() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    Debug.WriteLine("Error fetching data. Server returned status code: {0}", response.StatusCode);
                }
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

        public async Task<bool> UserExistAsync(string userEmail)
        {
            return await Task.FromResult(await this.GetUserAsync(userEmail) != null);
        }
    }
}
