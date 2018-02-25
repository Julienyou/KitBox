using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KitBoxApp
{
    class Cupboard : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private int height;
        private int width;
        private int depth;

        public Cupboard() { }

        public int Height
        {
            get => height;
            set
            {
                height = value;
                Notify("Height");
            }
        }

        public int Width
        {
            get => width;
            set
            {
                width = value;
                Notify("Width");
            }
        }
        public int Depth
        {
            get => depth;
            set
            {
                depth = value;
                Notify("Depth");
            }
        }

        void Notify(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
