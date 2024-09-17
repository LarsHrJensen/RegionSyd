using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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


        public OverviewViewModel()
        {
            Ambulances = new(AmbulanceRepository.GetInstance().GetAll());
            Patients = new(PatientRepository.GetInstance().GetAll());
            Transports = new(TransportRepository.GetInstance().GetAll());
            Hospitals = new(HospitalStore.Hospitals);
        }

        public OverviewViewModel(MainViewModel mv)
        {
            Ambulances = new(AmbulanceRepository.GetInstance().GetAll());
            Patients = new(PatientRepository.GetInstance().GetAll());
            Transports = new(TransportRepository.GetInstance().GetAll());
            Hospitals = new(HospitalStore.Hospitals);
            mainViewmodel = mv;
        }

        public void NavigateChangeAmbulance(Ambulance selectedAmbulance)
        {
            
        }

        
    }
}
