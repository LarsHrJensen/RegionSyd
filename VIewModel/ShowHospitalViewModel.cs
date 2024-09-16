using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RegionSyd.Model.Repository;
using RegionSyd.Model;
using System.Windows.Controls;
using RegionSyd.ViewModel;

namespace RegionSyd.VIewModel
{
    public class ShowHospitalViewModel : ViewModelBase
    {
        public ObservableCollection<Transport> TransportList { get; set; }

        public ShowHospitalViewModel()
        {
            TransportList = new ObservableCollection<Transport>(TransportRepository.GetInstance().GetAll());
        }
    }
}
