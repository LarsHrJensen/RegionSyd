using Microsoft.Extensions.Configuration;
using RegionSyd.Model;
using RegionSyd.Model.Command;
using RegionSyd.Model.Repository.Repository;
using RegionSyd.Model.Store;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Collections.ObjectModel;
using RegionSyd.Model.Repository;
using RegionSyd.Model.Store;
using System.Diagnostics;
using RegionSyd.Model.Command;
using RegionSyd.Model.Repository.Repository;


namespace RegionSyd.ViewModel
{
    public class ChangeAmbulanceViewModel : ViewModelBase
    {
        private Ambulance _ambulance;

        private string ambulanceName;

        public string AmbulanceName
        {
            get { return ambulanceName; }
            set 
            {
                ambulanceName = value; 
                _ambulance.Name = value;
                SaveChanges.RaiseCanExecuteChanged();
            }
        }

        private Hospital ambulanceHospital;

        public Hospital AmbulanceHospital
        {
            get { return ambulanceHospital; }
            set
            {
                ambulanceHospital = value;
                _ambulance.Hospital = value;
                SaveChanges.RaiseCanExecuteChanged();
            }
        }

        private string ambulanceStatus;

        public string AmbulanceStatus
        {
            get { return ambulanceStatus; }
            set
            {
                ambulanceStatus = value;
                _ambulance.Status = value;
                SaveChanges.RaiseCanExecuteChanged();
            }
        }

        public ObservableCollection<Hospital> Hospitals { get; }
        public ObservableCollection<string> Statuses { get; }

        public RelayCommand SaveChanges { get; }

        IConfiguration _configuration;
        public ChangeAmbulanceViewModel(IConfiguration config, Ambulance ambulance)
        {
            _ambulance = ambulance;

            _configuration = config;
            int idOfHospital = ambulance.Hospital.Id;
            ambulanceStatus = ambulance.Status;
            ambulanceName = ambulance.Name;

            HospitalRepository hospitalRepo = new HospitalRepository(config);
            Hospitals = new ObservableCollection<Hospital>(hospitalRepo.GetAll());


            /* 
            ComboBox selectedItem wouldn't properly bind unless the Hospital from ambulance object and the 
            corresponding Hospital from the hospitalRepo were the EXACT SAME FUCKING OBJECT, so we do this.
             */ 
            foreach (Hospital hospital in Hospitals)
            {
                if(hospital.Id == idOfHospital)
                {
                    ambulanceHospital = hospital;
                }
            }

            Statuses = new (StatusStore.Statuses);

            SaveChanges = new RelayCommand(CanSaveAmbulanceData, SaveAmbulanceData);

        }

        public bool CanSaveAmbulanceData(object param)
        {
            return _ambulance.IsValid();
        }

        public void SaveAmbulanceData(object param)
        {
            AmbulanceRepository repo = new AmbulanceRepository(_configuration);

            repo.Update(_ambulance);
        }

        public void DeleteAmbulanceData()
        {

            _ambulanceFromHospital = null;
            _ambulanceId = null;
            _ambulanceStatus = null;
        }


    }



    
}
