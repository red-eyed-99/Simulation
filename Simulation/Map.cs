using SimulationApp.Creatures;
using SimulationApp.Landscape;
using SimulationApp.Landscape.Surface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulationApp
{
    public class Map
    {
        public List<BaseCell> Cells { get; set; } = new List<BaseCell>();
        public List<Creature> Creatures { get; set; } = new List<Creature>();

        private int size;

        private Random random = new Random();

        public Map(int size)
        {
            this.size = size;
        }

        public void Generate()
        {
            GenerateGround();
            GenerateWater();
            GenerateTree();
            GenerateRock();

        }

        private void GenerateGround()
        {
            for (int x = 0; x < size; x++)
            {
                for (int y = 0; y < size; y++)
                {
                    Cells.Add(new Ground(x, y));
                }
            }
        }

        private void GenerateWater()
        {
            var randomWaterCellsCount = random.Next(1, 41);

            var waterCells = new List<Water>(randomWaterCellsCount);

            var randomCell = Cells[random.Next(Cells.Count)];
            //Cells.Remove(Cells.Single(cell => cell.X == randomWaterSource.X && cell.Y == randomWaterSource.Y));
            Cells.Remove(randomCell);
            waterCells.Add(new Water(randomCell.X, randomCell.Y));

            for (int i = 1; i < randomWaterCellsCount; i++)
            {
                var waterCellsNearGround = waterCells.Where(cell => GetNearbyGround(cell).Count > 0).ToList();
                var randomWaterCell = waterCellsNearGround[random.Next(waterCellsNearGround.Count)];
                var groundCellsNearWater = GetNearbyGround(randomWaterCell);
                var randomGroundNearWater = groundCellsNearWater[random.Next(groundCellsNearWater.Count)];
                Cells.Remove(randomGroundNearWater);
                waterCells.Add(new Water(randomGroundNearWater.X, randomGroundNearWater.Y));
            }

            Cells.AddRange(waterCells);
        }

        private void GenerateTree()
        {
            var treeCount = Cells.Count / 6;

            var grounds = Cells.Where(cell => cell is Ground).ToList();

            for (int i = 0; i < treeCount; i++) 
            {
                var randomGround = grounds[random.Next(grounds.Count)];

                grounds.Remove(randomGround);
                Cells.Remove(randomGround);
                Cells.Add(new Tree(randomGround.X, randomGround.Y));
            }
        }

        private void GenerateRock()
        {
            var rockCount = Cells.Count / 6;

            var grounds = Cells.Where(cell => cell is Ground).ToList();

            for (int i = 0; i < rockCount; i++)
            {
                var randomGround = grounds[random.Next(grounds.Count)];

                grounds.Remove(randomGround);
                Cells.Remove(randomGround);
                Cells.Add(new Rock(randomGround.X, randomGround.Y));
            }
        }

        private List<BaseCell> GetNearbyGround(BaseCell relativeCell)
        {
            return Cells
                .Where(cell => (Math.Abs(cell.X - relativeCell.X) == 1 && cell.Y == relativeCell.Y)
                || (Math.Abs(cell.Y - relativeCell.Y) == 1 && cell.X == relativeCell.X)
                && cell is Ground)
                .ToList();
        }

        
    }
}
