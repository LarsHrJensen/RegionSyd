using RegionSyd.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;


namespace RegionSyd.ViewModel
{
    public class ChangeAmbulanceViewModel : ViewModelBase
    {
        private Ambulance _ambulance;
        public Ambulance Ambulance { get { return _ambulance; } }

        public ChangeAmbulanceViewModel(IConfiguration config, Ambulance ambulance)
        {
            _ambulance = ambulance;
        }
    }
}
