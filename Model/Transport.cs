using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegionSyd.Model
{
    public class Transport
    {

        public int Id { get; private set; }
        public Hospital StartHospital { get; set; }
        public Hospital DestinationHospital { get; set; }
        public Patient Patient { get; set; }
        public DateTime ArrivalTime { get; set; }
        public int TravelTime { get; set; }

        public Transport(Hospital startHospital, Hospital destinationHospital, DateTime arrivalTime, Patient patient, int travelTime = 0, int id = 0)
        {
            StartHospital = startHospital;
            DestinationHospital = destinationHospital;
            ArrivalTime = arrivalTime;
            TravelTime = travelTime;
            Patient = patient;
            Id = id;
        }
    }
}
