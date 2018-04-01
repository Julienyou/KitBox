using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KitBoxApp
{
    public class Cupboard : INotifyPropertyChanged
    {
        private ObservableCollection<Box> boxes = new ObservableCollection<Box> {};
        private int width;
        private int depth;
        private string steelCornerColor;
        private CupboardConstraint cupboardConstraint = new CupboardConstraint();


        // INotifyPropertyChanged Member
        public event PropertyChangedEventHandler PropertyChanged;
        void Notify(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

        public int GetHeight()
        {
            int totalheight = 0;

            foreach (Box box in boxes)
            {
                totalheight += box.Height;
            }
            return totalheight;
        }

        public int Width
        {
            get => width;
            set
            {
                width = value;
                cupboardConstraint.Depths = ConstraintBuilder.GetAvailableDepth(width);
                foreach(Box b in boxes)
                {
                    b.BoxConstraint.HColors = ConstraintBuilder.GetAvailableHPaneColor(width, depth);
                    b.BoxConstraint.VColors = ConstraintBuilder.GetAvailableVPaneColor(width, depth, b.Height);
                    ((Door) b.Accessories[0]).DoorConstraint.Colors = ConstraintBuilder.GetAvailableDoorStyle(width, b.Height);
                }
                Notify("Width");
            }
        }

        public string SteelCornerColor
        {
            get => steelCornerColor;
            set
            {
                steelCornerColor = value;
                Notify("SteelCornerColor");
            }
        }

        public int Depth
        {
            get => depth;
            set
            {
                depth = value;
                foreach (Box b in boxes)
                {
                    b.BoxConstraint.HColors = ConstraintBuilder.GetAvailableHPaneColor(width, depth);
                    b.BoxConstraint.VColors = ConstraintBuilder.GetAvailableVPaneColor(width, depth, b.Height);
                }
                Notify("Depth");
            }
        }

        public ObservableCollection<Box> Boxes
        {
            get => boxes;
        }

        public void AddBox(Box b)
        {
            boxes.Add(b);
        }

        public void RemoveBox(Box b)
        {
            boxes.Remove(b);
        }

        public CupboardConstraint CupboardConstraint { get => cupboardConstraint; }
    }
}
