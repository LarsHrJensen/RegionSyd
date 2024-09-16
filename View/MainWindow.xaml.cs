using RegionSyd.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using RegionSyd.Model.Store;
using RegionSyd.Model.Repository;
using RegionSyd.Model.Repository.Repository;

namespace RegionSyd.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        

        public MainWindow()
        {
            InitializeComponent();

            MainViewModel mainViewModel = new MainViewModel();
            this.DataContext = mainViewModel;

            HospitalStore.Hospitals = new List<Hospital>();

            // Hardcoded shithousery
            HospitalStore.Hospitals.Add(new("Rigshospitalet", "Blegdamsvej 9", "København 2100", "Region Hovedstaden"));
            HospitalStore.Hospitals.Add(new("Odense Universitetshospital", "J. B. Winsløws Vej 4", "Odense 5000", "Region Syddanmark"));
            HospitalStore.Hospitals.Add(new("Aarhus Universitetshospital", "Palle Juul-Jensens Boulevard 99", " Aarhus N 8200", "Region Midtjylland"));



            PatientRepository patientRepo = PatientRepository.GetInstance();
            patientRepo.Add(new("0301001233", "Rasmus Kallehauge", "4"));
            patientRepo.Add(new("1109016995", "Lars Jensen ", "2"));
            patientRepo.Add(new("0404961944", "Mathilde Johnsen-Zaavi", "1"));


            AmbulanceRepository ambulanceRepo = AmbulanceRepository.GetInstance();
        }
    }
}
