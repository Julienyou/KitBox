using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KitBoxApp.Properties;

namespace KitBoxApp
{
    class BoxConstraint : IConstraintChecker<Box>
    {
        private List<int> heights;
        private List<string> colors;

        public BoxConstraint(List<int> heights, List<string> colors)
        {
            this.heights = heights;
            this.colors = colors;
        }

        public bool Check(Box b)
        {
            if (heights.Contains(b.Height))
            {
                return true;
            }
            return false;
            //Don't forget to check the availibility of the parts (variable)
        }
    }
}
