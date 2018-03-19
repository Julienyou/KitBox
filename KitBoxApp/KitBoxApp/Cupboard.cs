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
        private ObservableCollection<Box> boxes = new ObservableCollection<Box> { };
        private SteelCorner steelCorner;
        private int width = 100;
        private int depth;

        public Cupboard(ObservableCollection<Box> boxes, SteelCorner steelCorner, int width, int depth)
        {
            this.boxes = boxes;
            this.steelCorner = steelCorner;
            this.width = width;
            this.depth = depth;
        }

        public Cupboard() { }

        public int GetPrice()
        {
            return 1;  //Implement the method !
        }

        public Dictionary<string, int> GetCrossPiece()
        {
            return new Dictionary<string, int>();  //Implement the method !
        }

        public Dictionary<string, int> GetPane()
        {
            return new Dictionary<string, int>();  //Implement the method !
        }

        public Dictionary<string, int> GetMount()
        {
            return new Dictionary<string, int>();  //Implement the method !
        }

        public SteelCorner ChooseSteelCorner()
        {
            return new SteelCorner("red", 1, 1, true);  //Implement the method !
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

        public void AddBox(Box box)
        {
            boxes.Add(box);
        }

        public void DeleteBox(Box box)
        {
            boxes.Remove(box);
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
            }
        }

        public ObservableCollection<Box> Boxes
        {
            get => boxes;
        }

        // INotifyPropertyChanged Member
        public event PropertyChangedEventHandler PropertyChanged;
        void Notify(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
