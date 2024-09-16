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
            //_currentViewModel = new AddTransportViewModel();
            _currentViewModel = new AddPatientViewModel();
            NavCreatePatient = new(navCreatePatient);
            NavCreateTransport = new(navCreateTransport);

        }


        private void navCreatePatient()
        {
            CurrentViewModel = new AddPatientViewModel();
        }
        private void navCreateTransport()
        {
            CurrentViewModel = new AddTransportViewModel();
        }
    }
}
