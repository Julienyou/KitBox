using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KitBoxApp
{
    public class DoorConstraint : IConstraintChecker<Box>
    {
        private List<string> colors;
        private List<Tuple<int,int>> doorDimensions;

        public DoorConstraint(List<string> colors, List<Tuple<int,int>> doorDimensions)
        {
 //         this.knop = knop;
            this.colors = colors;
            this.doorDimensions = doorDimensions;
        }

        public bool Check(Box b)
        {
            foreach (IAccessory element in b.Accessories)
            {
                if (element.GetType() == typeof(Door))
                {
                    Door d = (Door) element;

                    Tuple<int, int> constrain = new Tuple<int, int>(b.Height - 4, (b.Cupboard.Width- 4) / 2);

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

                    break;
                }
            }
            
            return false;
        }

        public List<string> Colors { get => colors; }
    }
}
