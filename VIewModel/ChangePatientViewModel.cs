using Microsoft.Extensions.Configuration;
using RegionSyd.Model;
using RegionSyd.Model.Command;
using RegionSyd.Model.Repository;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegionSyd.ViewModel
{
    public class ChangePatientViewModel : ViewModelBase
    {

        private Patient _patient;

        private string patientName;

        public string PatientName
        {
            get { return patientName; }
            set
            {
                patientName = value;
                _patient.FullName = value;
                ChangePatient.RaiseCanExecuteChanged();
            }
        }

        private string patientCPR;

        public string PatientCPR
        {
            get { return patientCPR; }
            set 
            { 
                patientCPR = value;
                _patient.CPR = value; 
                ChangePatient.RaiseCanExecuteChanged(); 
            }
        }

        private string patientStatus;

        public string PatientStatus
        {
            get { return patientStatus; }
            set
            {
                patientStatus = value;
                _patient.Status = value;
                ChangePatient.RaiseCanExecuteChanged();
            }
        }

        public RelayCommand ChangePatient { get; }

        IConfiguration _configuration;

        public ChangePatientViewModel(IConfiguration config, Patient patient)
        {
            _configuration = config;
            _patient = patient;
            patientStatus = patient.Status;
            patientName = patient.FullName;
            patientCPR = patient.CPR;


            ChangePatient = new RelayCommand(CanSavePatientData, SavePatientData);
        }

        public bool CanSavePatientData(object param)
        {
            return _patient.IsValid();
        }

        public void SavePatientData(object param)
        {
            PatientRepository repo = new PatientRepository(_configuration);
            repo.Update(_patient);
        }
        public void DeletePatientData(object param)
        {
        }
    }
}
