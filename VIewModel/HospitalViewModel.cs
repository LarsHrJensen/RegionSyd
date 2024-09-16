using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RegionSyd.ViewModel;

namespace RegionSyd.VIewModel
{
    internal class HospitalViewModel : ViewModelBase
    {
        public ObservableCollection <Hospital> HospitalList { get; set; }

        public HospitalViewModel()
        {

        }
    }
    
}
