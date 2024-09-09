using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegionSyd.Model
{
    public class Transport
    {
     public string StartHospital { get; set; }
     public string DestinationHospital { get; set; }
     public DateTime ArrivalTime { get; set; }
     public int TravelTime { get; set; }

        public Transport(string startHospital, string destinationHospital, DateTime arrivalTime, int travelTime)
        {
            StartHospital = startHospital;
            DestinationHospital = destinationHospital;
            ArrivalTime = arrivalTime;
            TravelTime = travelTime;
        }



    }
}
