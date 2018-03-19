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
        private ObservableCollection<Box> boxes;
        private int width;
        private int depth;
        private string steelCornerColor;

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
                Notify("Width");
            }
        }

        public string SteelCornerColor
        {
            get => SteelCornerColor;
            set
            {
                steelCornerColor = value;
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
    }
}
