using RegionSyd.Model;
using RegionSyd.Model.Store;
using RegionSyd.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using RegionSyd.Model;
using RegionSyd.Model.Repository;
using System.Collections.ObjectModel;


namespace RegionSyd.ViewModel
{
    public class ChangeTransportViewModel : ViewModelBase 
    {
        public Transport CurrentTransport { get;  set; }
        public Hospital FromHospital { get; set; }
        public Hospital ToHospital { get; set; }
        public int TravelTime {  get; set; }
        public DateTime Arrival { get; set; }
    
        public ObservableCollection<Hospital> Hospitals { get;}


        IConfiguration _configuration;

        public ChangeTransportViewModel(IConfiguration config) 
        {
            _configuration = config;
            HospitalRepository repo = new HospitalRepository(config);
            Hospitals = new ObservableCollection<Hospital>(repo.GetAll());
        }

    }
    


}
