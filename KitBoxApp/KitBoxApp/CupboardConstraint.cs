using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KitBoxApp
{
    class CupboardConstraint : IConstraintChecker<Cupboard>
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

        public void AddDepht(int d)
        {
            dephts.Add(d);
        }

        public void RemoveDepht(int d)
        {
            dephts.Remove(d);
        }

        public void AddWidth(int w)
        {
            widths.Add(w);
        }

        public void RemoveWidth(int w)
        {
            widths.Remove(w);
        }

        public int MaxHeight
        {
            set { maxHeight = value; }
        }

        public bool Check(Cupboard cb)  //How to check this part and resolve the problem of generic type ?
        {
            if (dephts.Contains(cb.Depth) && widths.Contains(cb.Width) && maxHeight > cb.GetHeight())
            {
                return true;
            }
            else
                return false;
        }
    }
}
