using RegionSyd.ViewModel;
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

namespace RegionSyd.View
{
    /// <summary>
    /// Interaction logic for ShowAmbulance.xaml
    /// </summary>
    public partial class ShowAmbulanceView : UserControl
    {
        public ShowAmbulanceView()
        {
            InitializeComponent();

            ShowAmbulanceViewModel vm = ShowAmbulanceViewModel.Instance;

            if (vm == null)
            {
                throw new Exception("ShowAmbulanceViewModel somehow not instantiated, or static instance reference is just null for no reason. SEGFAULT CORE DUMPED and so on");
            }
        }

        
    }
}
