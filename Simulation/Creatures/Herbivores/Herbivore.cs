using SimulationApp.Food;
using SimulationApp.Landscape.Surface;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SimulationApp.Creatures.Herbivores
{
    public class Herbivore : BaseCreature
    {
        public Herbivore(int x, int y) : base(x, y) { }

        public override void MakeMove()
        {
            throw new NotImplementedException();
        }

        public void FindPathToGrass(Map map)
        {
            var nearGrass = GetNearGrass(map);

            var wayToGrass = new List<BaseCell>();

            var checkedCells = new List<BaseCell>();
            var cellsToCheck = new List<BaseCell> { this };

            ApproximatePathLength = GetApproximatePathLength(this, nearGrass);

            while (cellsToCheck.Count > 0)
            {
                var currentCell = cellsToCheck.OrderBy(cell => cell.ExpectedApproximateFullPathLength).First();

                if (CheckAvailableCellToEat(map, currentCell) is Grass grass)
                {
                    if (grass == nearGrass)
                    {
                        grass.PreviousCell = currentCell;
                        wayToGrass = GetPathToCell(grass);
                        return;
                    }
                }

                cellsToCheck.Remove(currentCell);
                checkedCells.Add(currentCell);

                var availableCells = GetAvailableCellsToMove(map, currentCell);

                foreach (var cell in availableCells)
                {
                    if (checkedCells.Count(checkedCell => checkedCell.X == cell.X && checkedCell.Y == cell.Y) > 0)
                    {
                        continue;
                    }

                    var cellToCheck = cellsToCheck.FirstOrDefault(cellToCheck => cellToCheck.X == cell.X && cellToCheck.Y == cell.Y);

                    if (cellToCheck is null)
                    {
                        cell.PreviousCell = currentCell;
                        cell.PathLengthFromStart = currentCell.PathLengthFromStart + 1;
                        cell.ApproximatePathLength = GetApproximatePathLength(cell, nearGrass);
                        cellsToCheck.Add(cell);
                    }
                    else if ((currentCell.PathLengthFromStart + 1) < cell.PathLengthFromStart)
                    {
                        cell.PathLengthFromStart = currentCell.PathLengthFromStart + 1;
                        cell.PreviousCell = currentCell;
                    }
                }
            }
        }

        private List<BaseCell> GetPathToCell(BaseCell cell)
        {
            var path = new List<BaseCell>();

            var currentCell = cell;

            while (currentCell != null)
            {
                path.Add(currentCell);
                currentCell = currentCell.PreviousCell;
            }

            var targetCell = path.SingleOrDefault(pathCell => pathCell.GetType() == typeof(Grass));
            if (targetCell is not null)
            {
                path.Remove(targetCell);
            }

            var startCell = path.SingleOrDefault(pathCell => pathCell is BaseCreature);
            if (startCell is not null)
            {
                path.Remove(startCell);
            }

            if (path.Count > 1)
            {
                path.Reverse();
            }

            return path;
        }

        private int GetApproximatePathLength(BaseCell cell, Grass grass)
        {
            var dX = Math.Abs(cell.X - grass.X);
            var dY = Math.Abs(cell.Y - grass.Y);

            var pathLength = (int)Math.Ceiling((double)(dX + dY) / Speed);

            if ((dX == 0 && dY > 0) || (dX > 0 && dY == 0))
            {
                return pathLength - 1;
            }

            return pathLength;
        }

        private List<BaseCell> GetAvailableCellsToMove(Map map, BaseCell selectedCell)
        {
            var cellsRange = 2;

            if (map.GetCell(selectedCell.X, selectedCell.Y) is Water)
            {
                cellsRange = 1;
            }

            var availableCellsToMoveX = new List<BaseCell>();

            for (int x = -cellsRange; x <= cellsRange; x++)
            {
                if (x == 0)
                {
                    continue;
                }

                var cellX = selectedCell.X + x;
                var cellY = selectedCell.Y;

                if (cellX < 0 || cellX >= map.Size || cellY < 0 || cellY >= map.Size)
                {
                    continue;
                }

                var cell = map.GetCellOrCreature(cellX, cellY);

                if (cell.CanStep())
                {
                    availableCellsToMoveX.Add(cell);
                }
                else if (x < 0)
                {
                    availableCellsToMoveX.Clear();
                }
                else
                {
                    break;
                }
            }

            var availableCellsToMoveY = new List<BaseCell>();

            for (int y = -cellsRange; y <= cellsRange; y++)
            {
                if (y == 0)
                {
                    continue;
                }

                var cellX = selectedCell.X;
                var cellY = selectedCell.Y + y;

                if (cellX < 0 || cellX >= map.Size || cellY < 0 || cellY >= map.Size)
                {
                    continue;
                }

                var cell = map.GetCellOrCreature(cellX, cellY);

                if (cell.CanStep())
                {
                    availableCellsToMoveY.Add(cell);
                }
                else if (y < 0)
                {
                    availableCellsToMoveY.Clear();
                }
                else
                {
                    break;
                }
            }

            return availableCellsToMoveX.Concat(availableCellsToMoveY).ToList();
        }

        private BaseCell? CheckAvailableCellToEat(Map map, BaseCell selectedCell)
        {
            return map.Cells
                .FirstOrDefault(cell => ((Math.Abs(cell.X - selectedCell.X) == 1 && cell.Y == selectedCell.Y)
                || (Math.Abs(cell.Y - selectedCell.Y) == 1 && cell.X == selectedCell.X))
                && cell is Grass);
        }

        /// <summary>
        /// Get grass that is near to the herbivore, but not necessarily the nearest
        /// because obstacles and surface type are not taken into account.
        /// </summary>
        private Grass GetNearGrass(Map map)
        {
            var grasses = map.Cells.OfType<Grass>().ToList();

            var nearGrass = grasses[0];

            foreach (var grass in grasses)
            {
                if (Math.Abs(grass.X - X) + Math.Abs(grass.Y - Y) < Math.Abs(nearGrass.X - X) + Math.Abs(nearGrass.Y - Y))
                {
                    nearGrass = grass;
                }
            }

            return nearGrass;
        }
    }
}
