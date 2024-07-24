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

            var map = new Map(40);

            var window = new MainWindow(map);

            app.Run(window);
        }
    }

}
