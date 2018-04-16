using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SenEvents
{
    class SignUpViewModel : BaseViewModel
    {
        public List<string> CityList;
        public string pickedImagePath = string.Empty;

        public SignUpViewModel()
        {
            CityList = Cities.All.ToList<string>();
        }
    }
}
