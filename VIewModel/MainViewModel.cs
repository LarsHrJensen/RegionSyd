using Microsoft.Identity.Client;
using RegionSyd.Model;
using RegionSyd.Model.Command;
using RegionSyd.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using RegionSyd.ViewModel;
using System.Diagnostics;

namespace RegionSyd.ViewModel
{
    public class MainViewModel : ViewModelBase
    {

        // TODO : Determin whether to instantiate VMs during runtime, or do them all in the beginning and save them in memory until needed?

        public NavigationCommand NavCreatePatient { get; }
        public NavigationCommand NavCreateTransport { get; }
        public NavigationCommand NavOverview { get; }
        public NavigationCommand NavCreateAmbulance { get; }
        public NavigationCommand NavSearchTransport { get; }

        public NavigationCommand NavBack { get; }

        List<string> Statuses;
        private readonly IConfiguration _config;

        public event EventHandler VMChanged;

        private Stack<ViewModelBase> prevVM;

        private ViewModelBase _currentViewModel;
        public ViewModelBase CurrentViewModel
        {
            get { return _currentViewModel; }
            set
            {
                prevVM.Push(_currentViewModel);
                _currentViewModel = value;
                OnPropertyChanged(nameof(CurrentViewModel));
                VMChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        public MainViewModel(IConfiguration config)
        {
            _config = config;

            // Landing viewmodel
            _currentViewModel = new OverviewViewModel(this, config);

            if(prevVM == null)
            {
                prevVM = new Stack<ViewModelBase>();
            }

            NavBack = new(navBack);

            // navigation command setup
            NavCreatePatient = new(navCreatePatient);
            NavCreateTransport = new(navCreateTransport);
            NavOverview = new(navOverview);
            NavCreateAmbulance = new(navCreateAmbulance);
            NavSearchTransport = new(navSearchTransport);
        }


        private void navBack()
        {
            if (prevVM.Count == 0) return;
            ViewModelBase vm = prevVM.Pop();
            if (vm != null)
            {
                CurrentViewModel = vm;
            } 
        }

        // Could be rewrote into lambda instead of this, would be prettier code, but less readable
        private void navCreatePatient()
        {
            CurrentViewModel = new AddPatientViewModel(_config);
        }
        private void navCreateTransport()
        {
            CurrentViewModel = new AddTransportViewModel(_config);
        }
        private void navOverview()
        {
            CurrentViewModel = new OverviewViewModel(this, _config);
        }
        private void navCreateAmbulance()
        {
            CurrentViewModel = new AddAmbulanceViewModel(_config);
        }


        public void NavChangeAmbulance(Ambulance amb)
        {
            CurrentViewModel = new ChangeAmbulanceViewModel(_config, amb);
        }

        public void NavChangePatient(Patient patient)
        {
            CurrentViewModel = new ChangePatientViewModel(_config, patient);
        }

        public void NavChangeTransport(Transport transport)
        {
            CurrentViewModel = new ChangeTransportViewModel(_config, transport);
        }

        public void NavShowHospital(Hospital hospital)
        {
            // changehospital with this
            CurrentViewModel = new ShowHospitalViewModel(_config,  hospital);
        }

        public void NavShowAmbulance(Ambulance ambulance)
        {
            CurrentViewModel = new ShowAmbulanceViewModel(_config, ambulance, this);
        }

        private void navSearchTransport()
        {
            CurrentViewModel = new SearchTransportViewModel(_config, this);
        }



   

        /* 
         * Additional navigation methods go here!
         */
    }
}
