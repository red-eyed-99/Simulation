using Simulation.Creatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulation
{
    public class Map
    {
        public List<BaseCell> Cells { get; set; } = new List<BaseCell>();
        public List<Creature> Creatures { get; set; } = new List<Creature>();

    }
}
