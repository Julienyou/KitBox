using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KitBoxApp
{
    class DoorConstraint : IConstraintChecker<Door>
    {
        private bool knop;
        private List<string> colors;
        private List<Tuple<int,int>> doorDimensions;

        public DoorConstraint(bool knop, List<string> colors, List<Tuple<int,int>> doorDimensions)
        {
            this.knop = knop;
            this.colors = colors;
            this.doorDimensions = doorDimensions;
        }

        public bool Check(Door d, Box b)
        {
            Tuple<int, int> constrain = new Tuple<int, int>(b.Height - 4, (b.GetWidth() - 4) / 2);

            if (colors.Contains(d.Color) && doorDimensions.Contains(constrain))
            {
                if (d.Color == "Verre" && d.Knop == true)
                {
                    return false;
                }
                else if (d.Color != "Verre" && d.Knop == false)
                {
                    return false;
                }
                return true;
            }
            
            return false;
        }
    }
}
