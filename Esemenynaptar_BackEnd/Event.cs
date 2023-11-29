using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
using Newtonsoft.Json;

namespace Esemenynaptar_BackEnd
{
    public class Event
    {
        [JsonProperty("id")]
        public int ID { get; set; }
        [JsonProperty("ido")]
        //TimeOnly
        public string Ido { get; set; }
        [JsonProperty("nev")]
        public string Nev { get; set; }
        [JsonProperty("datum")]
        //DateOnly
        public DateTime Datum { get; set; }
        [JsonProperty("prioritas")]
        public string Prioritas { get; set; }
        [JsonProperty("reszletek")]
        public string Reszletek { get; set; }
        [JsonProperty("egeszNapos")]
        public bool EgeszNapos { get; set; }
        [JsonProperty("emlekezteto")]
        //DateOnly
        //public DateTime Emlekezteto { get; set; }
        public DateTime Emlekezteto { get; set; }
    }
}
