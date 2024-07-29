using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Animation;

namespace SimulationApp.Landscape.Surface
{
    public class Ground : BaseCell
    {
        public Ground(int x, int y) : base(x, y)
        {

        }

        public override bool CanStep()
        {
            return true;
        }
    }
}
