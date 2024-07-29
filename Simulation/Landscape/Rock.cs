using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulationApp.Landscape
{
    public class Rock : BaseCell
    {
        public Rock(int x, int y) : base(x, y) { }

        public override bool CanStep()
        {
            return false;
        }

    }
}
