using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KitBoxApp
{
    public class CupboardConstraint : IConstraintChecker<Cupboard>
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

        public bool Check(Cupboard cb)
        {
            if (dephts.Contains(cb.Depth) && widths.Contains(cb.Width) && maxHeight > cb.GetHeight())
            {
                return true;
            }
            else
                return false;
        }

        public List<int> Widths
        { get => widths; }

        public List<int> Depths
        { get => dephts; }
    }
}
