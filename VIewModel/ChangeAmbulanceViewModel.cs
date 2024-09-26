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
using System.Windows.Data;

namespace RegionSyd.ViewModel
{
    public class ChangeAmbulanceViewModel : ViewModelBase
    {
        public Hospital AmbulanceFromHospital { get; set; }
        public int AmbulanceId { get;}
        public string AmbulanceStatus {  get; set; }


        //Defination af ambulance-objekternes egenskaber(property). Der kan ændres (set) i objekternes værdier.
        private Ambulance _ambulanceId;
        public Ambulance Ambulance
        {
            get
            {
                return _ambulanceId;
            }
            set
            {
                _ambulanceId = value;
                CreateAmbulance.RaiseCanExecuteChanged();
            }
        }
        private Ambulance _ambulanceFromHospital;
        public Ambulance ambulanceFromHospital
        {
            get
            {
                return _ambulanceFromHospital;
            }
            set
            {
                _ambulanceFromHospital = value;
                CreateAmbulance.RaiseCanExecuteChanged();
            }
        }
        private Ambulance _ambulanceStatus;
        public Ambulance ambulanceStatus
        {
            get
            {
                return _ambulanceStatus;
            }
            set
            {
                _ambulanceStatus = value;
                CreateAmbulance.RaiseCanExecuteChanged();
            }
        }
        //Definition felter og egenskaber, der skal burges i et VM til at håndtere ambulancer
        public RelayCommand CreateAmbulance { get; set; }

        IConfiguration _configuration;
        public ObservableCollection<Ambulance> AddedAmbulanceNumber {  get; set; }
        AmbulanceRepository ambulanceRepo;

        //Initialiserer et VM, som håndterer ambulancedata. Opretter forbindelse til Repo-> henter eksisterende ambulancer -> konfigurer kommandoer til at validere ->opretter ny ambulance 

        public void AddAmbulanceViewModel(IConfiguration config) 
        {
            _configuration = config;
            ambulanceRepo = new AmbulanceRepository(config);
            AddedAmbulanceNumber = new(ambulanceRepo.GetAll());
            CreateAmbulance = new(IsValidAmbulanceData, CreateNewAmbulance);

        }
        //Tjekker at al information er udfyldt korrekt
        public bool IsValidAmbulanceData(object param) 
        {
            if (AmbulanceId < 1) { return false; }
            if (AmbulanceFromHospital == null) { return false; }
            if (String.IsNullOrEmpty(AmbulanceStatus)) { return false; }

            return true;
        }
        //Opretter og indsætter ny ambulance i Repository. Viser det på UI.
        public void CreateNewAmbulance(object param) 
        {
            AmbulanceRepository repo = new AmbulanceRepository(_configuration);
            Ambulance ambulance = new Ambulance(AmbulanceId, AmbulanceFromHospital, AmbulanceStatus);

            repo.Insert(ambulance);

            AddedAmbulanceNumber = new(repo.GetAll());
            OnPropertyChanged(nameof(AddedAmbulanceNumber));

            MessageStore.Message = "Ambulance added successfully.";
            ResetFields();
        
        }
        //Nulstiller ambulancen
        private void ResetFields() 
        {
            _ambulanceFromHospital = null;
            _ambulanceId = null;
            _ambulanceStatus = null;
        }


    }



    
}
