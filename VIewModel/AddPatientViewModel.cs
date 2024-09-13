using RegionSyd.Model.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace RegionSyd.ViewModel
{
    public class AddPatientViewModel : ViewModelBase
    {
        public string PatientName { get; set; }
        public string PatientCPR { get; set; }
        public string PatientActuality { get; set; }

        public RelayCommand CreatePatient {  get; set; }




    }
}
