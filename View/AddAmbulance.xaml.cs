using System;
using System.Collections.Generic;
using System.Linq;
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
using RegionSyd.Model;

namespace RegionSyd.View
{
    /// <summary>
    /// Interaction logic for AddAmbulance.xaml
    /// </summary>
    public partial class AddAmbulance : Window
    {
        public AddAmbulance()
        {
            InitializeComponent();
        }

        private void AddAmbulance_Click(object sender, RoutedEventArgs e)
        {
            string ambulanceID = WriteAmbulanceID.Text;
            string station = WriteStation.Text;
            string status = WriteStatus.Text;
            

            if (string.IsNullOrEmpty(ambulanceID) || string.IsNullOrEmpty(station))
            {
                MessageBox.Show("Udfyld begge felter!", "Fejl", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

            else
            {
                Ambulance ambulance = new Ambulance(ambulanceID, station, status)

                {
                    ID = ambulanceID,
                    Station = station
                };

                MessageBox.Show("Ambulance ID oprettet!", "Succes", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}
