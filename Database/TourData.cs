using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database
{
    public class TourData
    {
        //public int TourId { get; set; }
        public string TourName { get; set; }
        public string Description { get; set; }
        public string FromLocation { get; set; }
        public string ToLocation { get; set; }
        public string Transport { get; set; }
        public string Distance { get; set; }
        public string EstimatedTime { get; set; }
        public string DateTime { get; set; }
        public string Difficulty { get; set; }
        public string TotalTime { get; set; }
        public string Rating { get; set; }
        public string Comment { get; set; }
    }
}
