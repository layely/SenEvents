using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SenEvents
{
    class Categories
    {
        public const string
            Concours = "Concours",
            Cinema = "Cinema",
            Concert = "Concert",
            Sport = "Sport",
            Religion = "Religion",
            Sabar = "Sabar",
            Set_Settal = "Set-Setal",
            Politique = "Politique",
            Marche = "Marche",
            Music = "Music",
            Education = "Education",
            Autre = "Autre";

        public static IList<string> All => new List<string> { Concours, Cinema, Concert, Sport,
            Religion, Sabar, Set_Settal, Politique, Marche, Music, Education, Autre};
    }
}
