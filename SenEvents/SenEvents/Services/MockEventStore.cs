using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[assembly: Xamarin.Forms.Dependency(typeof(SenEvents.MockEventStore))]
namespace SenEvents
{
    class MockEventStore : IDataStore<Event>
    {
        List<Event> events;

        public MockEventStore()
        {
            events = new List<Event>();
            var mockEvents = new List<Event>
            {
                new Event { Id = 0,
                    Title = "Concert Davido à Dakar",
                    Photo = "",
                    Text = "Davido Sera à dakar pour un Grand Show au monument de la renaissance bla bla bla ...",
                    Price = 20000,
                    StartDate = DateTime.Today,
                    EndDate = DateTime.Today.AddHours(2),
                    Categories = new List<string> {Categories.Music},
                    Place = "Monument de la Renaissance",
                    City = Cities.Dakar,
                    Organization = "Studio Sankara",
                    EventPublisher = new User {
                            Email = "ablayelyfondou@gmail.com",
                            Name = "Ly",
                            Surname = "Abdoulaye",
                            City = Cities.Dakar,
                            Phone = 77868581
                    }
                },
                new Event { Id = 1,
                    Title = "Set Setal à Grand Yoff",
                    Photo = "",
                    Text = "Grand set setal à grand yoff. Tous les jeunes sont invités à y prendre part. bla bla bla ...",
                    Price = 0,
                    StartDate = DateTime.Today.AddDays(15),
                    EndDate = DateTime.Today.AddDays(15).AddHours(2),
                    Categories = new List<string> {Categories.Set_Settal},
                    Place = "Mairie Grand Yoff",
                    City = Cities.Dakar,
                    Organization = "Jeunes de Grand Yoff",
                    EventPublisher = new User {
                            Email = "thomas@gmail.com",
                            Name = "Djina",
                            Surname = "Thomas",
                            City = Cities.Dakar,
                            Phone = 765854455
                    }
                },
                new Event { Id = 3,
                    Title = "Grand Marche de l'université de Thiès",
                    Photo = "",
                    Text = "Grand Marche pour réclammer nos droits. Tous les étudiants sont invités à y prendre part. bla bla bla ...",
                    Price = 0,
                    StartDate = DateTime.Today.AddDays(3),
                    EndDate = DateTime.Today.AddDays(3).AddHours(3),
                    Categories = new List<string> {Categories.Marche},
                    Place = "Site VCN Thiès None",
                    City = Cities.Thies,
                    Organization = "Studio Sankara",
                    EventPublisher = new User {
                            Email = "thomas@gmail.com",
                            Name = "Djina",
                            Surname = "Thomas",
                            City = Cities.Thies,
                            Phone = 78954525
                    }
                }
            };

            foreach (var _event in mockEvents)
            {
                events.Add(_event);
            }
        }

        public async Task<bool> AddItemAsync(Event _event)
        {
            events.Add(_event);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            var _event = events.Where((Event arg) => arg.Id.ToString() == id).FirstOrDefault();
            events.Remove(_event);

            return await Task.FromResult(true);
        }

        public async Task<Event> GetItemAsync(string id)
        {
            return await Task.FromResult(events.FirstOrDefault(s => s.Id.ToString().Equals(id)));
        }

        public async Task<IEnumerable<Event>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(events);
        }

        public async Task<bool> UpdateItemAsync(Event item)
        {
            //throw new NotImplementedException();
            var _event = events.Where((Event arg) => arg.Id.ToString() == id).FirstOrDefault();
            events.Remove(_event);
            events.Add(_event);

            return await Task.FromResult(true);
        }
    }
}
