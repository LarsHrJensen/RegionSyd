using Microsoft.Extensions.Configuration;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RegionSyd.Model;
using RegionSyd.Model.Repository.Repository;
using RegionSyd.Model.Repository;
using System.Diagnostics;
using System.Collections.ObjectModel;
using RegionSyd.Model.Command;
using RegionSyd.Model;
using RegionSyd.View;

namespace RegionSyd.ViewModel
{
    class ShowAmbulanceViewModel : ViewModelBase
    {
        IConfiguration _configuration;

        private Ambulance ambulance;
        public Ambulance Ambulance
        {
            get { return ambulance; }
            set { ambulance = value; }
        }

        public NavigationCommand AddTransport { get; }
        public RelayCommand UpdateThing { get; }

        private MainWindow mv;

        public ObservableCollection<Transport> TransportList { get; private set; }

        AmbulanceRepository ambulanceRepository;
        TransportRepository transportRepository;

        public ShowAmbulanceViewModel(IConfiguration config, Ambulance ambulance)
        {
            this.ambulance = ambulance;
            _configuration = config;

            ambulanceRepository = new(config);
            transportRepository = new(config);


            TransportList = new(transportRepository.GetByAmbulance(ambulance));
            AddTransport = new NavigationCommand(AddTransportMethod);
        }

        public void AddTransportMethod()
        {
            if(mv != null)
            {
                mv.Close();
            }
            SearchTransportViewModel searchTransportViewModel = new SearchTransportViewModel(_configuration, null, ambulance, AddTransportCallback);
            mv = new MainWindow(_configuration, searchTransportViewModel);
            mv.Show();
        }

        public void AddTransportCallback(Transport transport)
        {
            mv.Close();
            transportRepository.AssignAmbulanceToTransport(ambulance, transport);
            TransportList = new(transportRepository.GetByAmbulance(ambulance));
            OnPropertyChanged(nameof(TransportList));
        }
    }
}
