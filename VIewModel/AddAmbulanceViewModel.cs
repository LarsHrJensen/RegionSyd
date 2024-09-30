using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RegionSyd.Model;
using RegionSyd.Model.Command;
using RegionSyd.Model.Repository;
using RegionSyd.Model.Repository.Repository;
using RegionSyd.Model.Store;
using Microsoft.Extensions.Configuration;
using System.Diagnostics;

namespace RegionSyd.ViewModel
{
    public class AddAmbulanceViewModel : ViewModelBase 
    {
        private string ambulanceName;
        public string AmbulanceName { 
            get
            {
                return ambulanceName;
            }
            set
            {
                ambulanceName = value;
                CreateAmbulance.RaiseCanExecuteChanged();
            }
        }

        private string ambulanceStatus;
        public string AmbulanceStatus { 
            get
            {
                return ambulanceStatus;
            }
            set
            {
                ambulanceStatus = value;
                CreateAmbulance.RaiseCanExecuteChanged();
            }
        }

        private Hospital ambulanceHospital;
        public Hospital AmbulanceHospital {
            get
            {
                return ambulanceHospital;
            }
            set
            {
                ambulanceHospital = value;
                CreateAmbulance.RaiseCanExecuteChanged();
            }
        }

        public List<string> Statuses { get;}

        public List<Ambulance> Ambulances { get; }
        public ObservableCollection<Hospital> Hospitals { get; }

        public RelayCommand CreateAmbulance { get; }
        public ObservableCollection<Ambulance> AddedAmbulances { get; set; }
        AmbulanceRepository ambulanceRepo;
        HospitalRepository hospitalRepo;

        IConfiguration _configuration;


        public AddAmbulanceViewModel(IConfiguration config)
        {
            _configuration = config;
            Statuses = StatusStore.Statuses;
            ambulanceRepo = new AmbulanceRepository(config);
            HospitalRepository hospitalRepo = new HospitalRepository(config);


            AddedAmbulances = new(ambulanceRepo.GetAll());
            Hospitals = new ObservableCollection<Hospital>(hospitalRepo.GetAll());
            CreateAmbulance = new(IsValidAmbulanceData, CreateNewAmbulance);
            Hospitals = new ObservableCollection<Hospital>(hospitalRepo.GetAll());
        }

        public bool IsValidAmbulanceData(object param)
        {


            Debug.WriteLine(AmbulanceName);
            if (String.IsNullOrEmpty(AmbulanceName)) { return false; }
            Debug.WriteLine(AmbulanceStatus);
            if (String.IsNullOrEmpty(AmbulanceStatus)) { return false; }
            Debug.WriteLine(ambulanceHospital.Name);
            if (AmbulanceHospital == null) { return false; }

            // TODO : perhaps regex for cpr 


            return true;
        }

        public void CreateNewAmbulance(object param)
        {

            Ambulance ambulance = new Ambulance(AmbulanceName, AmbulanceHospital, AmbulanceStatus);

            ambulanceRepo.Insert(ambulance);

            AddedAmbulances = new(ambulanceRepo.GetAll());
            OnPropertyChanged(nameof(AddedAmbulances));

            MessageStore.Message = "Ambulance added successfully.";
            ResetFields();

        }
        private void ResetFields()
        {
            AmbulanceName = null;
            AmbulanceStatus = null;
            AmbulanceHospital = null;

            OnPropertyChanged(nameof(AmbulanceName));
            OnPropertyChanged(nameof(AmbulanceStatus));
            OnPropertyChanged(nameof(AmbulanceHospital));
        }
    }
    
}
