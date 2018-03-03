using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KitBoxApp
{
    class BoxConstraint : IConstraintChecker
    {
        private List<int> heights;
        private List<string> colors;

        public BoxConstraint(List<int> heights, List<string> colors)
        {
            this.heights = heights;
            this.colors = colors;
        }

        public bool check<T>(Box b)  //How to check this part and resolve the problem of generic type ?
        {
            return true;
        }
    }
}
