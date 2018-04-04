using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SenEvents
{
    public class EventDetailViewModel : BaseViewModel
    {
        public Event Event { get; set; }

        public Image Image => new Image { Source = ImageSource.FromUri(new Uri(Event?.PhotoUri)) };

        public string ButtonAttendText = string.Empty;


        public string Date => DateUtil.GetDayOfWeekInFrench(Event?.StartDate.DayOfWeek.ToString())
        + " le " + Event?.StartDate.ToString("dd/MM/yyyy")
        + " à " + Event?.StartDate.ToString("HH:mm");

        public EventDetailViewModel(Event _event)
        {
            Event = _event;
            Title = _event?.Title;
        }

        public async Task<bool> InitAsync() {
            ButtonAttendText = await EventStore.IsAttending(await UserStore.GetCurrentUser()) ? "Je n'assisterai pas" : "J'assisterai";
            return await Task.FromResult(true);
        }
    }
}
