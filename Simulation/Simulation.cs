using SimulationApp.Creatures.Herbivores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace SimulationApp
{
    public class Simulation
    {
        public Map Map { get; set; }

        public MapRenderer Renderer { get; set; }

        public int MovesCount { get; set; }

        public Simulation()
        {
            Map = new Map(40);

            var mainWindow = (MainWindow)App.Current.MainWindow;
            mainWindow.DrawMapGrid(Map.Size);

            Renderer = new MapRenderer(Map, (Grid)mainWindow.FindName("mapGrid"));
        }
    }
}
