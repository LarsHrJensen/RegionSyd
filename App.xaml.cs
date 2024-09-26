using RegionSyd.Model.Repository.Repository;
using RegionSyd.Model.Repository;
using RegionSyd.Model.Store;
using System.Configuration;
using System.Data;
using System.Windows;


using RegionSyd.View;
using RegionSyd.Model;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.ServiceProcess;

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

            // shittest of shitcode
            // navigate from bin8.0/debug blablabla to root of project so we can access appconfig.json
            string ProjectRootPath = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory)?.Parent?.Parent?.Parent?.FullName;
            string appsettingsFilePath = ProjectRootPath + "/appconfig.json";


            IConfigurationRoot config = new ConfigurationBuilder().AddJsonFile(appsettingsFilePath).Build();


            // setup main window 
            MainWindow mw = new(config);
            mw.Show();
        }
    }

}
