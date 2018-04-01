using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KitBoxApp
{

    public class Box : INotifyPropertyChanged
    {
        private int height;
        private List<IAccessory> accessories = new List<IAccessory> { };
        private Cupboard cupboard;
        private string lateralColor;
        private string horizontalColor;
        private BoxConstraint boxConstraint;


        public Box(Cupboard cupboard)
        {
            this.cupboard = cupboard;
            this.boxConstraint = new BoxConstraint();
        }

        // INotifyPropertyChanged Member
        public event PropertyChangedEventHandler PropertyChanged;
        void Notify(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

        public void AddAccessory(IAccessory c)
        {
            accessories.Add(c);
        }

        public void RemoveAccessory(IAccessory c)
        {
            accessories.Remove(c);
        }

        public List<IAccessory> Accessories
        {
            get => accessories;
        }

        public int GetWidth()
        {
            return cupboard.Width;
        }

        public int Height
        {
            get => height;
            set
            {
                height = value;
                ((Door)accessories[0]).DoorConstraint.Colors = ConstraintBuilder.GetAvailableDoorStyle(cupboard.Width, height);
                boxConstraint.VColors = ConstraintBuilder.GetAvailableVPaneColor(cupboard.Width, cupboard.Depth, height);
                Notify("Height");
            }
        }

        public Cupboard Cupboard
        {
            get => cupboard;
        }

        public string LateralColor
        {
            get => lateralColor;
            set
            {
                lateralColor = value;
                Notify("LateralColor");
            }
        }

        public string HorizontalColor
        {
            get => horizontalColor;
            set
            {
                horizontalColor = value;
                Notify("HorizontalColor");
            }
        }

        public BoxConstraint BoxConstraint { get => boxConstraint; }
    }
}
