using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
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

        MainViewModel mainViewmodel;

        // Make instance availabel to codebehind for clickable listview edit things
        private static OverviewViewModel _instance;
        public static OverviewViewModel Instance { get { return _instance; } }

        public OverviewViewModel()
        {
            Ambulances = new(AmbulanceRepository.GetInstance().GetAll());
            Patients = new(PatientRepository.GetInstance().GetAll());
            Transports = new(TransportRepository.GetInstance().GetAll());
            Hospitals = new(HospitalStore.Hospitals);
            _instance = this;
        }

        public OverviewViewModel(MainViewModel mv)
        {
            Ambulances = new(AmbulanceRepository.GetInstance().GetAll());
            Patients = new(PatientRepository.GetInstance().GetAll());
            Transports = new(TransportRepository.GetInstance().GetAll());
            Hospitals = new(HospitalStore.Hospitals);
            mainViewmodel = mv;
            _instance = this;
        }

        public void NavigateChangeAmbulance(Ambulance selectedAmbulance)
        {
            if(selectedAmbulance == null) return;
            Debug.WriteLine(selectedAmbulance.Name);
        }

        public void NavigateChangeTransport(Transport selectedTransport)
        {
            if(selectedTransport == null) return;
            Debug.WriteLine(selectedTransport.DestinationHospital.City);
        }

        public void NavigateChangePatient(Patient selectedPatient)
        {
            if(selectedPatient == null) return;
            Debug.WriteLine(selectedPatient.FullName);
        }

    }
}
