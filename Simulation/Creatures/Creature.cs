using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulation.Creatures
{
    public abstract class Creature : BaseCell
    {
        public int HealthPoint { get; set; }
        public int Speed { get; set; }

        public abstract void MakeMove();
    }
}
