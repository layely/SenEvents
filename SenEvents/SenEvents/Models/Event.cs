using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SenEvents
{
    class Event
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Photo { get; set; }
        public string Text { get; set; }
        public int Price { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<string> Categories { get; set; }
        public string Place { get; set; }
        public string City { get; set; }
        public string Organization { get; set; }
        public User EventPublisher { get; set; }
    }
}
