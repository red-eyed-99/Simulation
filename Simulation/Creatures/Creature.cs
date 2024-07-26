using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulationApp.Creatures
{
    public abstract class Creature : BaseCell
    {
        public int HealthPoint { get; set; }
        public int Speed { get; set; }

        public Creature(int x, int y) : base(x, y) { }

        public abstract void MakeMove();
    }
}
