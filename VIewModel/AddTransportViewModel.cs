using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RegionSyd.Model;
using RegionSyd.Model.Store;

namespace RegionSyd.ViewModel
{
    public class AddTransportViewModel : ViewModelBase
    {
        public ObservableCollection<Transport> TransportList { get; set; }

        public AddTransportViewModel()
        {
            TransportList = new ObservableCollection<Transport>(TransportStore.Transports);
        }

    }
}
