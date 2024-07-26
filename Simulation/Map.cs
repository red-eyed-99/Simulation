using SimulationApp.Creatures;
using SimulationApp.Creatures.Herbivores;
using SimulationApp.Food;
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

        public int Size { get; private set; }

        private Random _random = new Random();

        public Map(int size)
        {
            Size = size;

            GenerateLandscape();
            GenerateCreatures();
        }

        private void GenerateLandscape()
        {
            GenerateGround();
            GenerateWater();
            GenerateTree();
            GenerateRock();
            GenerateGrass();
        }

        private void GenerateCreatures()
        {
            GenerateHerbivores();
        }

        private void GenerateGround()
        {
            for (int x = 0; x < Size; x++)
            {
                for (int y = 0; y < Size; y++)
                {
                    Cells.Add(new Ground(x, y));
                }
            }
        }

        private void GenerateWater()
        {
            var randomWaterCellsCount = _random.Next(1, 41);

            var waterCells = new List<Water>(randomWaterCellsCount);

            var randomCell = Cells[_random.Next(Cells.Count)];
            //Cells.Remove(Cells.Single(cell => cell.X == randomWaterSource.X && cell.Y == randomWaterSource.Y));
            Cells.Remove(randomCell);
            waterCells.Add(new Water(randomCell.X, randomCell.Y));

            for (int i = 1; i < randomWaterCellsCount; i++)
            {
                var waterCellsNearGround = waterCells.Where(cell => GetNearbyGround(cell).Count > 0).ToList();
                var randomWaterCell = waterCellsNearGround[_random.Next(waterCellsNearGround.Count)];
                var groundCellsNearWater = GetNearbyGround(randomWaterCell);
                var randomGroundNearWater = groundCellsNearWater[_random.Next(groundCellsNearWater.Count)];
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
                var randomGround = grounds[_random.Next(grounds.Count)];

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
                var randomGround = grounds[_random.Next(grounds.Count)];

                grounds.Remove(randomGround);
                Cells.Remove(randomGround);
                Cells.Add(new Rock(randomGround.X, randomGround.Y));
            }
        }

        public void GenerateGrass()
        {
            var grounds = Cells.Where(cell => cell is Ground).ToList();

            for (int i = 0; i < grounds.Count / 10; i++)
            {
                var randomGround = grounds[_random.Next(grounds.Count)];

                grounds.Remove(randomGround);
                Cells.Remove(randomGround);
                Cells.Add(new Grass(randomGround.X, randomGround.Y));
            }
        }

        private void GenerateHerbivores()
        {
            var grounds = Cells.Where(cell => cell is Ground).ToList();

            for (int i = 0; i < 5; i++)
            {
                var randomGround = grounds[_random.Next(grounds.Count)];

                grounds.Remove(randomGround);
                Creatures.Add(new Ostrich(randomGround.X, randomGround.Y));
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
