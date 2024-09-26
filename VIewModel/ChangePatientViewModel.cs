using Microsoft.Extensions.Configuration;
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

        IConfiguration _configuration;

        public ChangePatientViewModel(Patient patient, IConfiguration config)
        {
            _configuration = config;
            _patient = patient;
        }
    }
}
