using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KitBoxApp
{
    class Cupboard
    {
        private List<Box> boxes;
        private SteelCorner steelcorner;
        private int width;
        private int depth;

        public Cupboard(List<Box> boxes, SteelCorner steelCorner, int width, int depth)
        {
            this.boxes = boxes;
            this.steelcorner = steelCorner;
            this.width = width;
            this.depth = depth;
        }

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

        public int Width
        {
            get => width;
            set
            {
                width = value;
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
