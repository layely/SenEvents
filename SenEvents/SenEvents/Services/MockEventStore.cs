using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

[assembly: Xamarin.Forms.Dependency(typeof(SenEvents.MockEventStore))]
namespace SenEvents
{
    class MockEventStore : IEventStore<Event>
    {
        List<Event> events;

        public MockEventStore()
        {
            events = new List<Event>();
            var mockEvents = new List<Event>
            {
                new Event { Id = 0,
                    Title = "Concert Davido à Dakar",
                    PhotoUri = "https://blog.socedo.com/wp-content/uploads/2016/09/Events.jpg",
                    Text = "Davido Sera à dakar pour un Grand Show au monument de la renaissance bla bla bla ...",
                    Price = 20000,
                    StartDate = DateTime.Today,
                    EndDate = DateTime.Today.AddHours(2),
                    Categories = Categories.Music,
                    Place = "Monument de la Renaissance",
                    City = Cities.Dakar,
                    Organization = "Studio Sankara",
                    PublisherEmail = "ablayelyfondou@gmail.com"
                    //EventPublisher = new User {
                    //        Email = "ablayelyfondou@gmail.com",
                    //        Name = "Ly",
                    //        Surname = "Abdoulaye",
                    //        City = Cities.Dakar,
                    //        Phone = 77868581
                    //}
                },
                new Event { Id = 1,
                    Title = "Set Setal à Grand Yoff",
                    PhotoUri = "https://blog.socedo.com/wp-content/uploads/2016/09/Events.jpg",
                    Text = "Grand set setal à grand yoff. Tous les jeunes sont invités à y prendre part. bla bla bla ...",
                    Price = 0,
                    StartDate = DateTime.Today.AddDays(15),
                    EndDate = DateTime.Today.AddDays(15).AddHours(2),
                    Categories = Categories.Set_Settal,
                    Place = "Mairie Grand Yoff",
                    City = Cities.Dakar,
                    Organization = "Jeunes de Grand Yoff",
                    PublisherEmail = "thomas@gmail.com"
                },
                new Event { Id = 3,
                    Title = "Grand Marche de l'université de Thiès",
                    PhotoUri = "https://blog.socedo.com/wp-content/uploads/2016/09/Events.jpg",
                    Text = "Grand Marche pour réclammer nos droits. Tous les étudiants sont invités à y prendre part. bla bla bla ...",
                    Price = 0,
                    StartDate = DateTime.Today.AddDays(3),
                    EndDate = DateTime.Today.AddDays(3).AddHours(3),
                    Categories = Categories.Marche,
                    Place = "Site VCN Thiès None",
                    City = Cities.Thies,
                    Organization = "Studio Sankara",
                    PublisherEmail = "thomas@gmail.com",
                }
            };

            foreach (var _event in mockEvents)
            {
                events.Add(_event);
            }
        }

        public async Task<string> AddItemAsync(Event _event)
        {
            var request = HttpWebRequest.Create(string.Format(@"{0}/events/", ServiceConstants.BASE_URL));
            request.ContentType = "application/json";
            request.Method = "POST";
            var data = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(_event));

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

        public async Task<bool> DeleteItemAsync(int id)
        {
            var _event = events.Where((Event arg) => arg.Id == id).FirstOrDefault();
            events.Remove(_event);

            return await Task.FromResult(true);
        }

        public async Task<Event> GetItemAsync(int id)
        {
            return await Task.FromResult(events.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Event>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(events);
        }

        public async Task<bool> UpdateItemAsync(Event item)
        {
            //throw new NotImplementedException();
            var _event = events.Where((Event arg) => arg.Id == item.Id).FirstOrDefault();
            events.Remove(_event);
            events.Add(_event);

            return await Task.FromResult(true);
        }

        public async Task<bool> IsAttending(string userEmail)
        {
            return await Task.FromResult(DateTime.Now.Second % 2 == 0 ? true : false);
        }
    }
}
