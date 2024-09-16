using RegionSyd.Model.Command;
using RegionSyd.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegionSyd.ViewModel
{
    internal class MainViewModel : ViewModelBase
    {

        // TODO : Determin whether to instantiate VMs during runtime, or do them all in the beginning and save them in memory until needed?

        public NavigationCommand NavCreatePatient { get; }
        public NavigationCommand NavCreateTransport { get; }
        public NavigationCommand NavOverview { get; }


        private ViewModelBase _currentViewModel;
        public ViewModelBase CurrentViewModel
        {
            get { return _currentViewModel; }
            set
            {
                _currentViewModel = value;
                OnPropertyChanged(nameof(CurrentViewModel));
            }
        }


        public MainViewModel()
        {
            // Landing viewmodel
            _currentViewModel = new OverviewViewModel();

            // navigation command setup
            NavCreatePatient = new(navCreatePatient);
            NavCreateTransport = new(navCreateTransport);
            NavOverview = new(navOverview);

        }

        // Could be rewrote into lambda instead of this, would be prettier code, but less readable
        private void navCreatePatient()
        {
            CurrentViewModel = new AddPatientViewModel();
        }
        private void navCreateTransport()
        {
            CurrentViewModel = new AddTransportViewModel();
        }
        private void navOverview()
        {
            CurrentViewModel = new OverviewViewModel();
        }
        /* 
         * Additional navigation methods go here!
         */
    }
}
