using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KitBoxApp
{
    class CupboardConstraint : IConstraintChecker
    {
        private List<int> dephts;
        private List<int> widths;
        private int maxHeight;

        public CupboardConstraint(List<int> dephts, List<int> widths, int maxHeight)
        {
            this.dephts = dephts;
            this.widths = widths;
            this.maxHeight = maxHeight;
        }

        public bool Check<T>(Cupboard cb)  //How to check this part and resolve the problem of generic type ?
        {
            return true;
        }
}
    }
}
