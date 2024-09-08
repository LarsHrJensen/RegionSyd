using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegionSyd.Model
{
    public class AmbulanceRepo
    {
        public void Add(Ambulance ambulance)
        {
            ambulances.Add(ambulance);
            Console.WriteLine($"Ny ambulance (id: {ambulance.Id} er oprettet");
        }

        public void Remove(Ambulance ambulance) 
        { 
            ambulances.Remove(ambulance);
            Console.WriteLine($"Ambulance (id: {ambulance.Id} er slettet");
        }

        public void ChangeStatus(Ambulance ambulance)
        {

        }
    }
}
