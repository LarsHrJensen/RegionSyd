using Microsoft.Extensions.Configuration;
using Microsoft.Identity.Client;
using RegionSyd.Model.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RegionSyd.Model;
using System.Diagnostics;

namespace RegionSyd.ViewModel
{
    public class SearchTransportViewModel : ViewModelBase
    {

        IConfiguration _configuration;
        MainViewModel _mainViewModel;

        public ObservableCollection<Hospital> Hospitals { get; }
        public ObservableCollection<Transport> Transports { get; private set; }
        public ObservableCollection<Patient> Patients { get; }

        TransportRepository _transportRepository;
        HospitalRepository _hospitalRepository;

        private Hospital fromHospital;

        public Hospital FromHospital
        {
            get { return fromHospital; }
            set 
            { 
                fromHospital = value;
                updateSearch();
            }
        }

        private Hospital toHospital;

        public Hospital ToHospital
        {
            get { return toHospital; }
            set 
            { 
                toHospital = value;
                updateSearch();
            }
        }

        private Patient patient;

        public Patient Patient
        {
            get { return patient; }
            set 
            { 
                patient = value;
                updateSearch();
            }
        }

        public SearchTransportViewModel(IConfiguration config, MainViewModel mv)
        {
            _configuration = config;
            _mainViewModel = mv;

            _hospitalRepository = new(_configuration);
            _transportRepository = new(_configuration);

            PatientRepository patientRepository = new(_configuration);

            Hospitals = new ObservableCollection<Hospital>(_hospitalRepository.GetAll());
            Transports = new ObservableCollection<Transport>(_transportRepository.GetAll());
            Patients = new ObservableCollection<Patient>(patientRepository.GetAll());

        }

        private void updateSearch()
        {
            Dictionary<string, object?> arguments = new Dictionary<string, object?>();

            if(fromHospital != null ) arguments.Add("FromHospital", fromHospital);
            if(toHospital != null ) arguments.Add("ToHospital", toHospital);
            if(patient != null ) arguments.Add("Patient", patient);

            foreach (KeyValuePair<string, object?> kvp in arguments)
            {
                Debug.WriteLine(kvp.Key);
                Debug.WriteLine(kvp.Value);
            }


            Transports = new(_transportRepository.Find(arguments));
            OnPropertyChanged(nameof(Transports));

        }
    }
}
