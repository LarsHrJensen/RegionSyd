using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            
            // Set listeners
            //HospitalListOverview.SelectionChanged += ()
            AmbulanceListOverview.SelectionChanged += (obj, e) =>
            {
                ListView lv = (ListView)obj;

                Ambulance selected = (Ambulance)lv.SelectedValue;

                OverviewViewModel ovm = (OverviewViewModel)DataContext;
                

            };
            //PatientListOverview
            //TransportListOverview


        }
    }
}
