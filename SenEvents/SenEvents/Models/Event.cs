using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Xamarin.Forms;

namespace SenEvents
{
    public class Event
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string PhotoUri { get; set; }
        public string Text { get; set; }
        public int Price { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Categories { get; set; }
        public string Place { get; set; }
        public string City { get; set; }
        public string Organization { get; set; }
        public String PublisherEmail { get; set; }

    }
}
