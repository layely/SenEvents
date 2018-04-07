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

        public SignUpViewModel()
        {
            CityList = Cities.All.ToList<string>();
        }
    }
}
