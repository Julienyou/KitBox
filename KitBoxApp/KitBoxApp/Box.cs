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
        private int height=70;
        private List<IAccessory> accessories;
        private Cupboard cupboard;
        private string lateralColor;
        private string horizontalColor;


        public Box(Cupboard cupboard)
        {
            this.cupboard = cupboard;
            this.accessories = new List<IAccessory> { };
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
    }
}
