using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using RegionSyd.Model;

namespace RegionSyd.ViewModel
{
    public class ShowAmbulanceViewModel : ViewModelBase
    {
       public ObservableCollection<Transport> Transports { get;}
    }
}
