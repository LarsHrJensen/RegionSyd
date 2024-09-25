using RegionSyd.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegionSyd.ViewModel
{
    public class ChangeAmbulanceViewModel : ViewModelBase
    {
        private Ambulance _ambulance;
        public Ambulance Ambulance { get { return _ambulance; } }

        public ChangeAmbulanceViewModel(Ambulance ambulance)
        {
            _ambulance = ambulance;
        }
    }
}
