using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using RegionSyd.Model;
using RegionSyd.ViewModel;
namespace RegionSyd.View
{
    /// <summary>
    /// Interaction logic for OverviewView.xaml
    /// </summary>
    public partial class OverviewView : UserControl
    {
        public OverviewView()
        {
            InitializeComponent();

            OverviewViewModel ovm = OverviewViewModel.Instance;

            // Set listeners

            // TODO : Rewrite into generic

            HospitalListOverview.SelectionChanged += (obj, e) =>
            {
                ListView lv = (ListView)obj;
                Hospital selected = (Hospital)lv.SelectedItem;
                ovm.NavigateShowHospital(selected);

            };

            AmbulanceListOverview.SelectionChanged += (obj, e) =>
            {
                ListView lv = (ListView)obj;
                Ambulance selected = (Ambulance)lv.SelectedValue;
                ovm.NavigateShowAmbulance(selected);
            };

            PatientListOverview.SelectionChanged += (obj, e) =>
            {
                ListView lv = (ListView)obj;
                Patient selected = (Patient)lv.SelectedValue;
                ovm.NavigateChangePatient(selected);
            };
            TransportListOverview.SelectionChanged += (obj, e) =>
            {
                ListView lv = (ListView)obj;
                Transport transport = (Transport)lv.SelectedValue;
                ovm.NavigateChangeTransport(transport);
            };


        }
    }
}
