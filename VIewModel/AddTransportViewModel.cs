using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Extensions.Configuration;
using RegionSyd.Model;
using RegionSyd.Model.Command;
using RegionSyd.Model.Repository;
using RegionSyd.Model.Store;
using RegionSyd.Model.Helper;
using Validation = RegionSyd.Model.Helper.Validation;

namespace RegionSyd.ViewModel
{
    public class AddTransportViewModel : ViewModelBase
    {

        Hospital? _fromHospital;
        public Hospital? FromHospital {
            get { return _fromHospital; }
            set
            {
                _fromHospital = value;
                CreateTransport.RaiseCanExecuteChanged();
            }
        }

        Hospital? _toHospital;
        public Hospital? ToHospital
        {
            get { return _toHospital; }
            set
            {
                _toHospital = value;
                CreateTransport.RaiseCanExecuteChanged();
            }
        }

        Patient? _patient;
        public Patient? Patient {
            get { return _patient; }
            set
            {
                _patient = value;
                CreateTransport.RaiseCanExecuteChanged();
            }
        }

        DateTime? _arrival;
        public DateTime? Arrival
        {
            get { return _arrival; }
            set
            {
                _arrival = value;
                CreateTransport.RaiseCanExecuteChanged();
            }
        }

        string _arrivalText;
        public string ArrivalText
        {
            get { return _arrivalText; }
            set
            {
                _arrivalText = value;
                CreateTransport.RaiseCanExecuteChanged();
            }
        }



        public ObservableCollection<Hospital> Hospitals { get; }
        public ObservableCollection<Patient> Patients { get; }
        public ObservableCollection<Transport> Transports { get; private set; }

        public RelayCommand CreateTransport { get; }


        HospitalRepository _hospitalRepo;
        PatientRepository _patientRepository;
        TransportRepository _transportRepository;


        public AddTransportViewModel(IConfiguration config)
        {
            _hospitalRepo = new HospitalRepository(config);
            _patientRepository = new PatientRepository(config);
            _transportRepository = new TransportRepository(config);


            Hospitals = new ObservableCollection<Hospital>(_hospitalRepo.GetAll());
            Patients = new ObservableCollection<Patient>(_patientRepository.GetAll());
            Transports = new ObservableCollection<Transport>(_transportRepository.GetAll());

            CreateTransport = new(CanCreateTransport, CreateTransportMethod);
        }


        public bool CanCreateTransport(object param)
        {
            if (_fromHospital == _toHospital) return false;
            if (_fromHospital == null || _toHospital == null) return false;
            if (_patient == null) return false;
            if (_arrival == null) return false;
            if (_arrivalText == null) return false;
            if (!Validation.ValidHourMinute(_arrivalText)) return false;

            return true;
        }

        public void CreateTransportMethod(object param)
        {

            TransportRepository repo = _transportRepository;
            if(repo == null)
            {
                throw new ArgumentNullException("Transport repo not initialized");
            }

            DateOnly date = DateOnly.FromDateTime((DateTime)_arrival);
            TimeOnly time = TimeOnly.Parse(_arrivalText);

            DateTime timeOfArrival = date.ToDateTime(time);


            Transport t = new(_fromHospital, _toHospital, timeOfArrival, _patient);

            repo.Insert(t);

            Transports = new(repo.GetAll());

            OnPropertyChanged(nameof(Transports));

            ResetFields();
        }

        public void ResetFields()
        {
            _fromHospital = null;
            _toHospital = null;
            _patient = null;
            _arrival = null;
            _arrivalText = null;

            OnPropertyChanged(nameof(FromHospital));
            OnPropertyChanged(nameof(ToHospital));
            OnPropertyChanged(nameof(Patient));
            OnPropertyChanged(nameof(Arrival));
            OnPropertyChanged(nameof(ArrivalText));

        }
    }
}
