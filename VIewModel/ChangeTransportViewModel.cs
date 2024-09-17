using RegionSyd.Model;
using RegionSyd.Model.Store;
using RegionSyd.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegionSyd.ViewModel
{
    public class ChangeTransportViewModel : ViewModelBase 
    {
         public Transport CurrentTransport { get;  set; }
         public Hospital FromHospital { get; set; }
         public Hospital ToHospital { get; set; }
         public int TravelTime {  get; set; }
         public DateTime Arrival { get; set; }
    
         public List<Hospital> Hospitals { get;}
     
        public ChangeTransportViewModel() 
        {
            Hospitals = HospitalStore.Hospitals;
        }
    }
    


}
