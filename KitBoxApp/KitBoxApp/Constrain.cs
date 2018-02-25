using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KitBoxApp
{
    public class Constrain:INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private List<int> height = new List<int> { 150, 130, 120};
        private List<int> width = new List<int> { 60, 70, 90 };
        private List<int> depth = new List<int> { 30, 40, 50 };
        public Constrain() { }

        public List<int> Height
        {
            get => new List<int>(height);
            set { Notify("Height"); }
        }

        public List<int> Width
        {
            get => new List<int>(width);
            set { Notify("Width"); }
        }

        public List<int> Depth
        {
            get => new List<int>(depth);
            set { Notify("Depth"); }
        }

        void Notify(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

    }
}
