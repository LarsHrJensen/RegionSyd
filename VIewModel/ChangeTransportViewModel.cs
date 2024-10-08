using Microsoft.Extensions.Configuration;
using RegionSyd.Model;
using RegionSyd.Model.Repository;
using RegionSyd.Model.Store;
using RegionSyd.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using RegionSyd.Model.Command;


namespace RegionSyd.ViewModel
{
    public class ChangeTransportViewModel : ViewModelBase 
    {
        public Transport CurrentTransport { get;  set; }

        private Hospital fromHospital;
        public Hospital FromHospital { get { return fromHospital; }
            set 
            {
                fromHospital = value;
                CurrentTransport.StartHospital = value;
                SaveChanges.RaiseCanExecuteChanged();
            }
        }

        private Hospital toHospital;
        public Hospital ToHospital { get { return toHospital; } 
            set
            {
                toHospital = value;
                CurrentTransport.DestinationHospital = value;
                SaveChanges.RaiseCanExecuteChanged();
            }
        }
        //public int TravelTime {  get; set; }

        private DateTime dateOfArrival;
        public DateTime DateOfArrival { get { return dateOfArrival; } 
            set 
            { 
                dateOfArrival = value;
                CurrentTransport.ArrivalTime = value;
                SaveChanges?.RaiseCanExecuteChanged();
            } 
        }


        private string timeOfArrival;
        public string TimeOfArrival { get { return timeOfArrival; }

            set 
            { 
                timeOfArrival = value;
                // currentransport arrival time time only
                SaveChanges?.RaiseCanExecuteChanged();
            }
        }

        private Patient patient;
        public Patient Patient { get {return patient; }

            set
            {
                patient = value;
                CurrentTransport.Patient = value;
                SaveChanges?.RaiseCanExecuteChanged();
            }      
        }

        public ObservableCollection<Hospital> Hospitals { get;}
        public ObservableCollection<Patient> Patients { get;}

        public RelayCommand SaveChanges { get; }
     
        public ChangeTransportViewModel(IConfiguration config, Transport transport) 
        {

            SaveChanges = new RelayCommand(CanSaveTransportData, SaveTransportData);

            HospitalRepository hospitalRepo = new HospitalRepository(config);
            PatientRepository patientRepo = new PatientRepository(config);

            CurrentTransport = transport;

            List<Hospital> hospitals = new(hospitalRepo.GetAll());
            List<Patient> patients = new(patientRepo.GetAll());

            Hospitals = new(hospitals);
            Patients = new(patients);

            // following code is haunted by null references
            FromHospital = hospitals.Find((hospital) =>
            {
                return (hospital.Id == transport.StartHospital.Id);
            });

            ToHospital = hospitals.Find((hospital) =>
            {
                return (hospital.Id == transport.DestinationHospital.Id);
            });

            Patient = patients.Find((patient) =>
            {
                return (patient.Id == transport.Patient.Id);
            });
            // exorcism has been performed past this point

            // TODO : Insert datetime as text instead of datepicker
            DateOfArrival = CurrentTransport.ArrivalTime;

            TimeOfArrival = $"{CurrentTransport.ArrivalTime.TimeOfDay.Hours}:{CurrentTransport.ArrivalTime.TimeOfDay.Minutes}";

        }

        public bool CanSaveTransportData(object param)
        {
            return CurrentTransport.IsValid();
        }

        public void SaveTransportData(object param)
        {

        }


    }
    


}
