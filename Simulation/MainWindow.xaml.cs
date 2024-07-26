using SimulationApp.Landscape.Surface;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SimulationApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public void DrawMapGrid(int mapSize)
        {
            var cellSize = new GridLength(60);

            for (int row = 0; row < mapSize; row++)
            {
                var definition = new RowDefinition();
                definition.Height = cellSize;
                mapGrid.RowDefinitions.Add(definition);
            }

            for (int column = 0; column < mapSize; column++)
            {
                var definition = new ColumnDefinition();
                definition.Width = cellSize;
                mapGrid.ColumnDefinitions.Add(definition);
            }
        }
    }
}