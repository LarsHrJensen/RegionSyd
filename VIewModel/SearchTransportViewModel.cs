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
using RegionSyd.Model.Command;
using System.Windows;

namespace RegionSyd.ViewModel
{
    public class SearchTransportViewModel : ViewModelBase
    {

        IConfiguration _configuration;
        MainViewModel _mainViewModel;


        public string IsEmpty { get; private set; }
        public Visibility HideWarning { get; private set; } 

        public static SearchTransportViewModel Instance { get; private set; }

        public ObservableCollection<Hospital> Hospitals { get; }

        public ObservableCollection<Transport> Transports { get; private set; }
        public ObservableCollection<Patient> Patients { get; }

        private Transport selectedTransport;
        public Transport SelectedTransport
        {
            get { return selectedTransport; }
            set 
            { 
                selectedTransport = value;
            }
        }


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


        private Ambulance ambulance;
        public Ambulance Ambulance
        {
            get { return ambulance; }
            set { ambulance = value; }
        }

        Action<Transport> addTransportCallback;
        public RelayCommand AddTransport { get; }

        public SearchTransportViewModel(IConfiguration config, MainViewModel mv, Ambulance amb=null, Action<Transport> addTransport=null)
        {
            _configuration = config;
            _mainViewModel = mv;

            _hospitalRepository = new(_configuration);
            _transportRepository = new(_configuration);

            PatientRepository patientRepository = new(_configuration);

            Hospitals = new ObservableCollection<Hospital>(_hospitalRepository.GetAll());
            Transports = new ObservableCollection<Transport>(_transportRepository.GetAll());
            Patients = new ObservableCollection<Patient>(patientRepository.GetAll());

            if(amb != null)
            {
                fromHospital = amb.Hospital;
                ambulance = amb;
            }
            if (addTransport != null)
            {
                RemoveAssignedTransports();
                addTransportCallback = addTransport;
                AddTransport = new RelayCommand(CanAddTransport, AddTransportMethod);

            }

            Instance = this;
            updateSearch();
        }


        private void RemoveAssignedTransports()
        {
            Predicate<Transport> isAssigned = (transport) =>
            {
                return _transportRepository.IsAssigned(transport);
            };

            List<Transport> unAssignedTransports = new List<Transport>(Transports);

            unAssignedTransports.RemoveAll(isAssigned);

            Transports = new ObservableCollection<Transport>(unAssignedTransports);

            
        }

        private void AddTransportMethod(object param)
        {
            addTransportCallback?.Invoke(selectedTransport);   
        }

        private bool CanAddTransport(object param)
        {
            // implement
            return true;
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


            if(ambulance!=null) RemoveAssignedTransports();

            if(Transports.Count == 0)
            {
                IsEmpty = "No transports available to show.";
                HideWarning = Visibility.Visible;
            }
            else
            {
                IsEmpty = "";
                HideWarning = Visibility.Hidden;
            }

            OnPropertyChanged(nameof(IsEmpty));
            OnPropertyChanged(nameof(HideWarning));

            OnPropertyChanged(nameof(Transports));
        }

    }
}
