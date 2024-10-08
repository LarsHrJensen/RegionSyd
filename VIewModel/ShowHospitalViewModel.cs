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
                OnPropertyChanged(nameof(SelectedHospital));
                UpdateTransport();
            }
        }

        private void UpdateTransport()
        {
            Predicate<Transport> findTransportFromHospital = (trans) => 
            {
                return trans.StartHospital.Equals(selectedHospital);
            };

            Predicate<Transport> findTransportToHospital = (trans) =>
            {
                return trans.DestinationHospital.Equals(selectedHospital);
            };

            List<Transport> transport = new List<Transport>(transportRepo.GetAll());
            //transport.FindAll(findTransport);

            List<Transport> transportsFromHospital = transport.FindAll(findTransportFromHospital);
            List<Transport> transportsToHospital = transport.FindAll(findTransportToHospital);


            TransportList = new ObservableCollection<Transport>(transportsToHospital.Concat(transportsFromHospital));

            OnPropertyChanged(nameof(TransportList));

        }


        private readonly IConfiguration _configuration;

        public ShowHospitalViewModel(IConfiguration config, Hospital hospital)
        {
            _configuration = config;

            HospitalRepository hospitalRepo = new HospitalRepository(config);

            List<Hospital> hospitals = new(hospitalRepo.GetAll());

            // make sure selectedHospital actually exists in list for combobox 
            // So the selected item can be the hospital when this VM is opened
            foreach(var item in hospitals)
            {
                if (item.Equals(hospital))
                {
                    selectedHospital = item;
                }
            }


            transportRepo = new TransportRepository(config);

            TransportList = new ObservableCollection<Transport>(transportRepo.GetAll());
            HospitalList = new ObservableCollection<Hospital>(hospitals);

            UpdateTransport();
        }
    }
}
