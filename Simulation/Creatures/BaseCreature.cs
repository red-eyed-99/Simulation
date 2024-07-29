using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulationApp.Creatures
{
    public abstract class BaseCreature : BaseCell
    {
        public int HealthPoint { get; set; }
        public int Speed { get; set; }
        public int RunningSpeed { get; private protected set; }

        public BaseCreature(int x, int y) : base(x, y) 
        {
            Speed = 2;
        }

        public abstract void MakeMove();

        public override bool CanStep()
        {
            return false;
        }
    }
}
