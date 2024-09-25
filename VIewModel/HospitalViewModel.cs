using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RegionSyd.Model.Store;
using RegionSyd.ViewModel;

namespace RegionSyd.ViewModel
{
    internal class HospitalViewModel : ViewModelBase
    {
        public ObservableCollection <Hospital> HospitalList { get; set; }

        public HospitalViewModel()
        {
        }
    }
    
}
