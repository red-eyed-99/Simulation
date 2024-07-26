using SimulationApp.Creatures.Herbivores;
using SimulationApp.Food;
using SimulationApp.Landscape;
using SimulationApp.Landscape.Surface;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SimulationApp
{
    public class MapRenderer
    {
        private Map _map;

        private Window _window;

        private Grid _mapGrid;

        public MapRenderer(Map map, Grid mapGrid)
        {
            _map = map;
            _mapGrid = mapGrid;

            RenderLandscape();
            RenderGrass();
            RenderOstrich();
        }

        public void RenderLandscape()
        {
            for (int row = 0; row < _mapGrid.RowDefinitions.Count; row++)
            {
                for (int column = 0; column < _mapGrid.ColumnDefinitions.Count; column++)
                {
                    var currentCell = _map.Cells.Single(cell => cell.X == column && cell.Y == row);

                    var image = new Image();

                    var imagePath = string.Empty;

                    switch(currentCell)
                    {
                        case Water:
                            imagePath = "Images/lake.png";
                            break;
                        case Tree:
                            imagePath = "Images/tree.png";
                            break;
                        case Rock:
                            imagePath = "Images/rock.png";
                            break;
                    }

                    image.Source = new BitmapImage(new Uri(imagePath, UriKind.Relative));
                    Grid.SetColumn(image, column);
                    Grid.SetRow(image, row);
                    _mapGrid.Children.Add(image);
                }
            }
        }

        public void RenderGrass()
        {
            var grasses = _map.Cells.Where(cell => cell is Grass).ToList();

            foreach(var grass in grasses)
            {
                var image = new Image();
                image.Source = new BitmapImage(new Uri("Images/grass.png", UriKind.Relative));
                Grid.SetColumn(image, grass.X);
                Grid.SetRow(image, grass.Y);
                _mapGrid.Children.Add(image);
            }
            }

        public void RenderOstrich()
        {
            var ostriches = _map.Creatures.Where(cell => cell is Ostrich).ToList();

            foreach (var ostrich in ostriches)
            {
                var image = new Image();
                image.Source = new BitmapImage(new Uri("Images/ostrich.png", UriKind.Relative));
                Grid.SetColumn(image, ostrich.X);
                Grid.SetRow(image, ostrich.Y);
                _mapGrid.Children.Add(image);
            }
        }
    }
}
