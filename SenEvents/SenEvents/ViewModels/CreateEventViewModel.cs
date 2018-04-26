using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SenEvents
{
    class CreateEventViewModel : BaseViewModel
    {
        public List<string> CityList;
        public List<string> CategoryList;
        public string pickedImagePath = string.Empty;

        public CreateEventViewModel()
        {
            CityList = Cities.All.ToList<string>();
            CategoryList = Categories.All.ToList<string>();
        }
    }
}
