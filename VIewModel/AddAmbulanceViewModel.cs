﻿using System;
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

namespace RegionSyd.ViewModel
{
    public class AddAmbulanceViewModel : ViewModelBase 
    {
     public string AmbulanceName { get; set; }
     public string AmbulanceStatus { get; set; }
     public Hospital AmbulanceHospital { get; set; }
     public List<string> Statuses { get;}

     public List<Ambulance> Ambulances { get; }

        public AddAmbulanceViewModel(List<string> statuses)
        {
            Statuses = statuses;
        }

        public RelayCommand CreateAmbulance { get; set; }
        public ObservableCollection<Ambulance> AddedAmbulances { get; set; }
        AmbulanceRepository ambulanceRepo;


        public AddAmbulanceViewModel()
        {
            ambulanceRepo = AmbulanceRepository.GetInstance();
            AddedAmbulances = new(ambulanceRepo.GetAll());
            CreateAmbulance = new(IsValidAmbulanceData, CreateNewAmbulance);
        }

        public bool IsValidAmbulanceData(object param)
        {
            if (String.IsNullOrEmpty(AmbulanceName)) { return false; }
            if (String.IsNullOrEmpty(AmbulanceStatus)) { return false; }
            if (AmbulanceHospital == null) { return false; }

            // TODO : perhaps regex for cpr 


            return true;
        }

        public void CreateNewAmbulance(object param)
        {
            AmbulanceRepository repo = AmbulanceRepository.GetInstance();

            Ambulance ambulance = new Ambulance(AmbulanceName, AmbulanceHospital, AmbulanceStatus);

            repo.Add(ambulance);

            AddedAmbulances = new(repo.GetAll());
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
