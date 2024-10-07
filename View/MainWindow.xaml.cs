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
using System.Diagnostics;
using Microsoft.Extensions.Configuration;

namespace RegionSyd.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {


        MainViewModel mainViewModel;
        public MainWindow(IConfiguration config)
        {
            
            mainViewModel = new MainViewModel(config);
            this.DataContext = mainViewModel;
            
            // Message shower
            MessageStore.Updated += (obj, e) =>
            {
                if (string.IsNullOrEmpty(MessageStore.Message)) return;

                MessageBox.Show(MessageStore.Message);
                MessageStore.Message = string.Empty;
            };

            mainViewModel.VMChanged += HighlightNavButton;

            InitializeComponent();
        }

        public MainWindow(IConfiguration config, ViewModelBase shown)
        {
            mainViewModel = new MainViewModel(config);
            this.DataContext = mainViewModel;


            mainViewModel.CurrentViewModel = shown;

            // Message shower
            MessageStore.Updated += (obj, e) =>
            {
                if (string.IsNullOrEmpty(MessageStore.Message)) return;

                MessageBox.Show(MessageStore.Message);
                MessageStore.Message = string.Empty;
            };

            mainViewModel.VMChanged += HighlightNavButton;

            InitializeComponent();

            NavBar.Visibility = Visibility.Hidden;
        }

        private void HighlightNavButton(object? sender, EventArgs e)
        {

            ClearMarkings();
            switch (mainViewModel.CurrentViewModel)
            {
                case (OverviewViewModel):
                    HighlightButton(Overview);
                    break;

                case (AddPatientViewModel):
                    HighlightButton(CreatePatient);
                    break;

                case (AddTransportViewModel): 
                    HighlightButton(CreateTransport);
                    break;

                case (AddAmbulanceViewModel):
                    HighlightButton(CreateAmbulance);
                    break;

                case (SearchTransportViewModel):
                    HighlightButton(SearchTransport);
                    break;
            }
        }

        private void ClearMarkings()
        {
            foreach (Button item in NavBar.Children)
            {
                item.Background = Brushes.LightGray;

            }
        }

        private void HighlightButton(Button button)
        {
            button.Background = Brushes.CadetBlue;
            //button.Foreground = Brushes.White;
        }

    }
}
