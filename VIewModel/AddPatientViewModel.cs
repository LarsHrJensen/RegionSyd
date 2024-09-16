﻿using RegionSyd.Model.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using RegionSyd.Model;
using RegionSyd.Model.Repository;
using RegionSyd.Model.Message;
using System.Diagnostics;

namespace RegionSyd.ViewModel
{
    public class AddPatientViewModel : ViewModelBase
    {
        private string? _patientName;
        public string? PatientName { 
            get
            {
                return _patientName;
            }
            set
            {
                _patientName = value;
                CreatePatient.RaiseCanExecuteChanged();
            }
        }
        private string? _patientCPR;
        public string? PatientCPR { 
            get
            {
                return _patientCPR;
            }
            set 
            { 
                _patientCPR = value;
                CreatePatient.RaiseCanExecuteChanged();
            }
            }
        private string? _patientActuality;
        public string? PatientActuality {
            get
            {
                return _patientActuality;
            }
            set
            {
                _patientActuality = value;
                CreatePatient.RaiseCanExecuteChanged();
            }
        }

        public RelayCommand CreatePatient {  get; set; }

        ObservableCollection<Patient> AddedPatients { get; set; }

        public event EventHandler<SuccessMessage> PatientAdded;

        public AddPatientViewModel()
        {
            AddedPatients = new ObservableCollection<Patient>();
            CreatePatient = new(IsValidPatientData, CreateNewPatient);
        }

        public bool IsValidPatientData(object param)
        {
            if (String.IsNullOrEmpty(PatientName)) { return false; }
            if (String.IsNullOrEmpty(PatientCPR)) { return false; }
            if (String.IsNullOrEmpty(PatientActuality)) { return false; }

            // TODO : perhaps regex for cpr 

            if (PatientCPR.Length != 10) return false;

            return true;
        }
        

        public void CreateNewPatient(object param)
        {
            PatientRepository repo = PatientRepository.GetInstance();

            Patient patient = new Patient(PatientCPR, PatientName, PatientActuality);

            repo.Add(patient);

            SuccessMessage msg = new SuccessMessage();
            msg.Message = $"Successfully added {PatientName}.";

            PatientAdded?.Invoke(this, msg);

            AddedPatients.Add(patient);

            Debug.WriteLine("Hello?");
        }
    }
}