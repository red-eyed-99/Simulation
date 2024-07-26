using System.Configuration;
using System.Data;
using System.Windows;

namespace SimulationApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        [STAThread]
        static void Main()
        {
            var app = new App();

            var mainWindow = new MainWindow();

            var simulation = new Simulation();

            app.Run(mainWindow);
        }
    }

}
