using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulationApp.Creatures.Predators
{
    public class Predator : BaseCreature
    {
        public Predator(int x, int y) : base(x, y) { }

        public override void MakeMove()
        {
            throw new NotImplementedException();
        }
    }
}
