using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;

namespace SenEvents
{
    class Cities
    {
        public const string Dakar = "DAKAR";
        public const string Thies = "THIES";
        public const string Mbour = "MBOUR";
        public const string Kaolack = "KAOLACK";

        public static IList<string> All => new List<string> { Dakar, Thies, Mbour, Kaolack};
    }
}
