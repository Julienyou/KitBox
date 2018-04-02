using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KitBoxApp
{
    public class CupboardConstraint : IConstraintChecker<Cupboard>, INotifyPropertyChanged
    {
        private List<int> depths;
        private List<int> widths;
        private List<string> steelCornerColors;
        private int maxHeight;

        public event PropertyChangedEventHandler PropertyChanged;
        void Notify(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

        public bool Check(Cupboard cb)
        {
            if (depths.Contains(cb.Depth) && widths.Contains(cb.Width) && maxHeight > cb.Height)
            {
                return true;
            }
            else
                return false;
        }

        public List<int> Widths
        {
            get => widths;
            set
            {
                widths = value;
                Notify("Widths");
            }
        }

        public List<int> Depths
        {
            get => depths;
            set
            {
                depths = value;
                Notify("Depths");
            }
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
