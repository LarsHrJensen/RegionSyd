using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RegionSyd.Model;
using RegionSyd.Model.Command;
using RegionSyd.Model.Repository;
using RegionSyd.Model.Store;

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


        public ObservableCollection<Hospital> Hospitals { get; }
        public ObservableCollection<Patient> Patients { get; }

        private ObservableCollection<Transport> _transports;
        public ObservableCollection<Transport> Transports { get { return _transports; } }

        public RelayCommand CreateTransport { get; }



        public AddTransportViewModel()
        {
            Hospitals = new ObservableCollection<Hospital>(HospitalStore.Hospitals);

            // Get patientrepository instance, get patients from instance and create observable collection from list<patient>
            Patients = new(PatientRepository.GetInstance().GetAll());

            _transports = new(TransportRepository.GetInstance().GetAll());
            

            CreateTransport = new(CanCreateTransport, CreateTransportMethod);
        }


        public bool CanCreateTransport(object param)
        {
            if (_fromHospital == _toHospital) return false;
            if (_fromHospital == null || _toHospital == null) return false;
            if (_patient == null) return false;
            if (_arrival == null) return false;

            return true;
        }

        public void CreateTransportMethod(object param)
        {
            MessageStore.Message = "Successfully created transport.";
            
            TransportRepository repo = TransportRepository.GetInstance();


            Transport t = new(_fromHospital, _toHospital, (DateTime)_arrival, _patient);

            repo.Add(t);

            _transports = new(repo.GetAll());

            // raise opc so right list updates
            OnPropertyChanged(nameof(Transports));

            ResetFields();
        }

        public void ResetFields()
        {
            _fromHospital = null;
            _toHospital = null;
            _patient = null;
            _arrival = null;

            OnPropertyChanged(nameof(FromHospital));
            OnPropertyChanged(nameof(ToHospital));
            OnPropertyChanged(nameof(Patient));
            OnPropertyChanged(nameof(Arrival));

        }
    }
}
