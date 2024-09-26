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
using System.Windows;
using RegionSyd.Model.Command;


namespace RegionSyd.ViewModel
{
    public class ChangeTransportViewModel : ViewModelBase 
    {
        private Transport currentTransport;

        public Transport CurrentTransport
        {
            get { return currentTransport; }
            set { currentTransport = value; }
        }

        private Hospital toHospital;

        public Hospital ToHospital
        {
            get { return toHospital; }
            set { toHospital = value; }
        }

        private Hospital fromHospital;

        public Hospital FromHospital
        {
            get { return fromHospital; }
            set { fromHospital = value; }
        }

        private int travelTime;

        public int TravelTime
        {
            get { return travelTime; }
            set { travelTime = value; }
        }

        private DateTime arrivalDate;

        public DateTime ArrivalDate
        {
            get { return arrivalDate; }
            set { arrivalDate = value; }
        }

        public RelayCommand SaveChanges { get; }


        public ObservableCollection<Hospital> Hospitals { get;}
     
        public ChangeTransportViewModel(IConfiguration config) 
        {
            HospitalRepository repo = new HospitalRepository(config);


            Hospitals = new ObservableCollection<Hospital>(repo.GetAll());

            SaveChanges = new RelayCommand(CanSave, Save);
        }

        public bool CanSave(object param)
        {
            if (FromHospital == null) return false; 
            if (ToHospital == null) return false;
            if (ArrivalDate == null) return false;

            return true;
        }

        public void Save(object param)
        {
            
        }


    }
    


}
