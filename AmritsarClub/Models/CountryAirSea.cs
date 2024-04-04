using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AmritsarClub.Models
{
    public class CountryAirSea
    {
        public string ID{ get; set; }
        public string coun_name { get; set; }

        public string dest_coun_code { get; set; }
        public string dest_airport_name { get; set; }

        public string seaport_code { get; set; }
        public string dest_seaport { get; set; }
    }
}