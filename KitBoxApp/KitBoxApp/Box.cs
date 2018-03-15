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
        private int height = 50;
        private List<IAccessory> accessories = new List<IAccessory> { new Door("Verre", true, 0) };
        private static int N = 0;
        private Cupboard cupboard;
        private int pos;

        public Box(int height, List<IAccessory> accessories, Cupboard cupboard)
        {
            this.height = height;
            this.accessories = accessories;
            this.cupboard = cupboard;
            N++;
            pos = N;

        }

        public Box(Cupboard cupboard)
        {
            this.cupboard = cupboard;
            N++;
            pos = N;
        }


        public void AddComponent(IAccessory c)
        {
            accessories.Add(c);
        }

        public void RemoveComponent(IAccessory c)
        {
            accessories.Remove(c);
        }
        
        public List<IAccessory> Accessories
        {
            get => accessories;
        }

        public Cupboard Cupboard
        {
            get => cupboard;
        }

        public int Height
        {
            get => height;
            set { height = value;  Notify("Height"); }
            
        }

        public int Position
        {
            get => pos;
        }

        // INotifyPropertyChanged Member
        public event PropertyChangedEventHandler PropertyChanged;
        void Notify(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
