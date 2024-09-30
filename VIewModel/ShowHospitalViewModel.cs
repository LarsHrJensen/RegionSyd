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
using Microsoft.Extensions.Configuration;
using System.Diagnostics;

namespace RegionSyd.ViewModel
{
    public class ShowHospitalViewModel : ViewModelBase
    {
        TransportRepository transportRepo;
        public ObservableCollection<Transport> TransportList { get; set; }
        public ObservableCollection<Hospital> HospitalList { get; set;}

        private Hospital selectedHospital;

        public Hospital SelectedHospital
        {
            get { return selectedHospital; }
            set 
            {
                selectedHospital = value;
                UpdateTransport();
            }
        }

        private void UpdateTransport()
        {
            Predicate<Transport> findTransport = (trans) => 
            {
                return trans.StartHospital.Equals(selectedHospital);
            };

            List<Transport> transport = new List<Transport>(transportRepo.GetAll());
            //transport.FindAll(findTransport);

            TransportList = new ObservableCollection<Transport>(transport.FindAll(findTransport));

            OnPropertyChanged(nameof(TransportList));

        }


        private readonly IConfiguration _configuration;

        public ShowHospitalViewModel(IConfiguration config)
        {
            _configuration = config;

            transportRepo = new TransportRepository(config);
            HospitalRepository hospitalRepo = new HospitalRepository(config);

            TransportList = new ObservableCollection<Transport>(transportRepo.GetAll());
            HospitalList = new ObservableCollection<Hospital>(hospitalRepo.GetAll());
        }
    }
}
