﻿using Microsoft.Identity.Client;
using RegionSyd.Model;
using RegionSyd.Model.Command;
using RegionSyd.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using RegionSyd.ViewModel;

namespace RegionSyd.ViewModel
{
    public class MainViewModel : ViewModelBase
    {

        // TODO : Determin whether to instantiate VMs during runtime, or do them all in the beginning and save them in memory until needed?

        public NavigationCommand NavCreatePatient { get; }
        public NavigationCommand NavCreateTransport { get; }
        public NavigationCommand NavOverview { get; }
        public NavigationCommand NavCreateAmbulance { get; }
        public NavigationCommand NavSearchTransport { get; }

        List<string> Statuses;
        private readonly IConfiguration _config;

        public event EventHandler VMChanged;

        private ViewModelBase _currentViewModel;
        public ViewModelBase CurrentViewModel
        {
            get { return _currentViewModel; }
            set
            {
                _currentViewModel = value;
                OnPropertyChanged(nameof(CurrentViewModel));
                VMChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        public MainViewModel(IConfiguration config)
        {

            _config = config;

            // Landing viewmodel
            _currentViewModel = new ShowHospitalViewModel(config);
             

            // navigation command setup
            NavCreatePatient = new(navCreatePatient);
            NavCreateTransport = new(navCreateTransport);
            NavOverview = new(navOverview);
            NavCreateAmbulance = new(navCreateAmbulance);
            NavSearchTransport = new(navSearchTransport);
        }

        // Could be rewrote into lambda instead of this, would be prettier code, but less readable
        private void navCreatePatient()
        {
            CurrentViewModel = new AddPatientViewModel(_config);
        }
        private void navCreateTransport()
        {
            CurrentViewModel = new AddTransportViewModel(_config);
        }
        private void navOverview()
        {
            CurrentViewModel = new OverviewViewModel(this, _config);
        }
        private void navCreateAmbulance()
        {
            CurrentViewModel = new AddAmbulanceViewModel(_config);
        }


        public void NavChangeAmbulance(Ambulance amb)
        {
            CurrentViewModel = new ChangeAmbulanceViewModel(_config, amb);
        }

        public void NavChangePatient(Patient patient)
        {
            CurrentViewModel = new ChangePatientViewModel(_config, patient);
        }

        public void NavChangeTransport(Transport transport)
        {
            CurrentViewModel = new ChangeTransportViewModel(_config, transport);
        }

        public void NavHospitalOverview(Hospital hospital)
        {

        }

        private void navSearchTransport()
        {
            CurrentViewModel = new SearchTransportViewModel(_config, this);
        }

   

        /* 
         * Additional navigation methods go here!
         */
    }
}
