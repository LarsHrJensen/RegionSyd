using RegionSyd.Model.Repository.Repository;
using RegionSyd.Model.Repository;
using RegionSyd.Model.Store;
using System.Configuration;
using System.Data;
using System.Windows;


using RegionSyd.View;
using RegionSyd.Model;

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
            Hospital rigs = new("Rigshospitalet", "Blegdamsvej 9", "København 2100", "Region Hovedstaden");
            Hospital ouh = new("Odense Universitetshospital", "J. B. Winsløws Vej 4", "Odense 5000", "Region Syddanmark");
            Hospital auh = new("Aarhus Universitetshospital", "Palle Juul-Jensens Boulevard 99", "Aarhus N 8200", "Region Midtjylland");
            HospitalStore.Hospitals.Add(rigs);
            HospitalStore.Hospitals.Add(ouh);
            HospitalStore.Hospitals.Add(auh);

            // add patients for testing
            PatientRepository patientRepo = PatientRepository.GetInstance();

            Patient rasmus = new("0301001233", "Rasmus Kallehauge", "4");
            Patient lars = new("1109016995", "Lars Jensen ", "2");
            Patient mathilde = new("0404961944", "Mathilde Johnsen-Zaavi", "1");
            patientRepo.Add(rasmus);
            patientRepo.Add(lars);
            patientRepo.Add(mathilde);

            // add ambulances 
            AmbulanceRepository ambulanceRepo = AmbulanceRepository.GetInstance();
            Ambulance a1 = new("ZSK-1", "Beldringe", "Ledig");
            Ambulance a2 = new("ZSK-2", "HQ", "Ledig");
            ambulanceRepo.Add(a1);
            ambulanceRepo.Add(a2);

            // add transports for testing

            Transport t1 = new(rigs, auh, DateTime.Parse("17-03-2024 10:30"), rasmus, 120);
            Transport t2 = new(ouh, rigs, DateTime.Parse("20-04-2025 19:15"), mathilde);
            TransportRepository transportRepo = TransportRepository.GetInstance();
            transportRepo.Add(t1);
            transportRepo.Add(t2);

            // setup main window 
            MainWindow mw = new();
            mw.Show();
        }
    }

}
