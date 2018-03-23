using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SenEvents
{
    public class EventDetailViewModel : BaseViewModel
    {
        public Event Event { get; set; }

        public EventDetailViewModel(Event _event) {
            Event = _event;
            Title = _event?.Title;
        }
    }
}
