using RegionSyd.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegionSyd.ViewModel
{
    public class ChangePatientViewModel : ViewModelBase
    {

        private Patient _patient;
        public Patient Patient { get { return _patient; } }

        public ChangePatientViewModel(Patient patient)
        {
            _patient = patient;
        }
    }
}
