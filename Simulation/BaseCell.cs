using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulationApp
{
    public abstract class BaseCell
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int PathLengthFromStart { get; set; }
        public BaseCell? PreviousCell { get; set; }
        public int ApproximatePathLength { get; set; }
        public int ExpectedApproximateFullPathLength
        {
            get
            {
                return PathLengthFromStart + ApproximatePathLength;
            }
        }
        public BaseCell(int x, int y)
        {
            X = x;
            Y = y;
        }

        public abstract bool CanStep();      
    }
}
