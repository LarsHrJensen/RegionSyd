using RegionSyd.Model.Repository.Repository;
using RegionSyd.Model.Repository;
using RegionSyd.Model.Store;
using System.Configuration;
using System.Data;
using System.Windows;

using RegionSyd.View;

namespace RegionSyd
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        /**
         * The skibideal is, the gyatt was rizzed from the start.
         */
        void InitializeApp(object sender, EventArgs e)
        {

            HospitalStore.Hospitals = new List<Hospital>();

            // add hospitals for testing
            HospitalStore.Hospitals.Add(new("Rigshospitalet", "Blegdamsvej 9", "København 2100", "Region Hovedstaden"));
            HospitalStore.Hospitals.Add(new("Odense Universitetshospital", "J. B. Winsløws Vej 4", "Odense 5000", "Region Syddanmark"));
            HospitalStore.Hospitals.Add(new("Aarhus Universitetshospital", "Palle Juul-Jensens Boulevard 99", " Aarhus N 8200", "Region Midtjylland"));

            // add patients for testing
            PatientRepository patientRepo = PatientRepository.GetInstance();
            patientRepo.Add(new("0301001233", "Rasmus Kallehauge", "4"));
            patientRepo.Add(new("1109016995", "Lars Jensen ", "2"));
            patientRepo.Add(new("0404961944", "Mathilde Johnsen-Zaavi", "1"));

            // add ambulances 
            AmbulanceRepository ambulanceRepo = AmbulanceRepository.GetInstance();


            // setup main window 
            MainWindow mw = new();
            mw.Show();
        }
    }

}
