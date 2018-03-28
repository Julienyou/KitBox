using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KitBoxApp
{
    public class CupboardConstraint : IConstraintChecker<Cupboard>
    {
        private List<int> depths;
        private List<int> widths;
        private List<string> steelCornerColors;
        private int maxHeight;
        

        public bool Check(Cupboard cb)
        {
            if (depths.Contains(cb.Depth) && widths.Contains(cb.Width) && maxHeight > cb.GetHeight())
            {
                return true;
            }
            else
                return false;
        }

        public List<int> Widths
        {
            get => widths;
            set { widths = value; }
        }

        public List<int> Depths
        {
            get => depths;
            set { depths = value; }
        }
        public List<string> SteelCornerColors
        {
            get => steelCornerColors;
            set { steelCornerColors = value; }
        }
        public int MaxHeight
        {
            get => maxHeight;
            set { maxHeight = value; }
        }


    }
}
