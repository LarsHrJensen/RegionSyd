using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using RegionSyd.Model;
using RegionSyd.Model.Repository;
using RegionSyd.Model.Repository.Repository;
using RegionSyd.Model.Store;


namespace RegionSyd.ViewModel
{
    internal class OverviewViewModel : ViewModelBase
    {
        public ObservableCollection<Ambulance> Ambulances { get; }
        public ObservableCollection<Patient> Patients { get; }
        public ObservableCollection<Hospital> Hospitals { get; }
        public ObservableCollection<Transport> Transports { get; }

        IConfiguration _configuration;

        MainViewModel mainViewmodel;

        // Make instance availabel to codebehind for clickable listview edit things
        private static OverviewViewModel _instance;
        public static OverviewViewModel Instance { get { return _instance; } }

        public OverviewViewModel(MainViewModel mv, IConfiguration config)
        {
            mainViewmodel = mv;
            _instance = this;
            _configuration = config;

            AmbulanceRepository ambulanceRepository = new(config);
            PatientRepository patientRepository = new(config);
            HospitalRepository hospitalRepository = new(config);
            TransportRepository transportRepository = new(config);

            Ambulances = new ObservableCollection<Ambulance>(ambulanceRepository.GetAll());
            Hospitals = new ObservableCollection<Hospital>(hospitalRepository.GetAll());
            Transports = new ObservableCollection<Transport> (transportRepository.GetAll());
            Patients = new ObservableCollection<Patient>(patientRepository.GetAll());
        }

        public void NavigateChangeAmbulance(Ambulance selectedAmbulance)
        {
            if(selectedAmbulance == null) return;
            mainViewmodel.NavChangeAmbulance(selectedAmbulance);
        }

        public void NavigateChangeTransport(Transport selectedTransport)
        {
            if(selectedTransport == null) return;
            mainViewmodel.NavChangeTransport(selectedTransport);
        }

        public void NavigateChangePatient(Patient selectedPatient)
        {
            if(selectedPatient == null) return;
            mainViewmodel.NavChangePatient(selectedPatient);
        }

        public void NavigateShowHospital(Hospital selectedHospital)
        {
            if(selectedHospital == null) return;
            mainViewmodel.NavShowHospital(selectedHospital);
        }

        public void NavigateShowAmbulance(Ambulance ambulance)
        {
            if(ambulance == null) return;
            mainViewmodel.NavShowAmbulance(ambulance);
        }
    }
}
