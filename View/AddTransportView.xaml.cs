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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RegionSyd.View
{
    /// <summary>
    /// Interaction logic for AddTransportView.xaml
    /// </summary>
    public partial class AddTransportView : UserControl
    {
        public AddTransportView()
        {
            InitializeComponent();
        }

        private void HospitalSelection(object sender, SelectionChangedEventArgs e)
        {
            if (FromHospital.SelectedValue is null || ToHospital.SelectedValue is null) return;

            Hospital fromHospital, toHospital;
            fromHospital = FromHospital.SelectedValue as Hospital;
            toHospital = ToHospital.SelectedValue as Hospital;

            if(fromHospital == toHospital)
            {
                MessageBox.Show("Can't have same destination and start.");

                ComboBox cb = sender as ComboBox;
                cb.SelectedValue = null;

            }
        }
    }
}
