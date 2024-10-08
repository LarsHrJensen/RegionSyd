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

        public static ShowAmbulanceViewModel Instance { get; private set; }

        private Ambulance ambulance;
        public Ambulance Ambulance
        {
            get { return ambulance; }
            set { ambulance = value; }
        }

        public NavigationCommand AddTransport { get; }
        public NavigationCommand ChangeAmbulance { get; }
        public RelayCommand UpdateThing { get; }

        private MainWindow mainWindow;
        private MainViewModel mainViewModel;

        public ObservableCollection<Transport> TransportList { get; private set; }

        AmbulanceRepository ambulanceRepository;
        TransportRepository transportRepository;

        public ShowAmbulanceViewModel(IConfiguration config, Ambulance ambulance, MainViewModel mv)
        {
            this.ambulance = ambulance;
            _configuration = config;

            mainViewModel = mv;

            ambulanceRepository = new(config);
            transportRepository = new(config);

            Instance = this;

            TransportList = new(transportRepository.GetByAmbulance(ambulance));
            AddTransport = new NavigationCommand(AddTransportMethod);
            ChangeAmbulance = new NavigationCommand(ChangeAmbulanceMethod);
        }

        public void AddTransportMethod()
        {
            if(mainWindow != null)
            {
                mainWindow.Close();
            }
            SearchTransportViewModel searchTransportViewModel = new SearchTransportViewModel(_configuration, null, ambulance, AddTransportCallback);
            mainWindow = new MainWindow(_configuration, searchTransportViewModel);
            mainWindow.Show();
        }

        public void AddTransportCallback(Transport transport)
        {
            mainWindow.Close();
            transportRepository.AssignAmbulanceToTransport(ambulance, transport);
            TransportList = new(transportRepository.GetByAmbulance(ambulance));
            OnPropertyChanged(nameof(TransportList));
        }

        public void ChangeAmbulanceMethod()
        {
            mainViewModel.NavChangeAmbulance(Ambulance);
        }
    }
}
