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
    /// Interaction logic for SearchTransportView.xaml
    /// </summary>
    public partial class SearchTransportView : UserControl
    {
        SearchTransportViewModel vm;
        public SearchTransportView()
        {
            InitializeComponent();

            vm = SearchTransportViewModel.Instance;

            if (vm.Ambulance != null)
            {
                
            }
            else
            {
                AddButton.Visibility = Visibility.Hidden;
            }
        }
    }
}
