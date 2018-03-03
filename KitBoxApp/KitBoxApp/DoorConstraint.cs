using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KitBoxApp
{
    class DoorConstraint : IConstraintChecker
    {
        private bool knop;
        private List<string> colors;
        private List<Tuple<int>> doorDimensions;

        public DoorConstraint(bool knop, List<string> colors, List<Tuple<int>> doorDimensions)
        {
            this.knop = knop;
            this.colors = colors;
            this.doorDimensions = doorDimensions;
        }

        public bool check<T>(Door door)  //How to check this part and resolve the problem of generic type ?
        {
            return true;
        }
    }
}
