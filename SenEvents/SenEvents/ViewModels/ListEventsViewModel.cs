using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SenEvents
{
    public class ListEventsViewModel : BaseViewModel
    {
        public ObservableCollection<Event> Events { get; set; }
        public Command LoadEventsCommand { get; set; }
        public string UserCity;

        public ListEventsViewModel()
        {
            Title = "Evènements";
            Events = new ObservableCollection<Event>();
            LoadEventsCommand = new Command(async () => await ExecuteLoadEventsCommand());

            //MessagingCenter.Subscribe<NewItemPage, Item>(this, "AddItem", async (obj, item) =>
            //{
            //    var _item = item as Item;
            //    Events.Add(_item);
            //    await DataStore.AddItemAsync(_item);
            //});
        }

        public ListEventsViewModel(string city)
        {
            Title = city;
            Events = new ObservableCollection<Event>();
            LoadEventsCommand = new Command(async () => await ExecuteLoadEventsCommand());

            this.UserCity = city;
        }

        async Task ExecuteLoadEventsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Events.Clear();
                var events = await EventStore.GetItemsAsync(true);
                Debug.WriteLine("events count :::::: " + events.Count());
                foreach (var _event in events)
                {
                    if (string.IsNullOrEmpty(this.UserCity) || _event.City.Equals(this.UserCity))
                    {
                        Events.Add(_event);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
