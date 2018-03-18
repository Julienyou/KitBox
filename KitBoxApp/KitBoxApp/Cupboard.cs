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
        private int width;
        private int depth;
        private string steelCornerColor;

        public Cupboard(List<Box> boxes, string steelCornerColor, int width, int depth)
        {
            this.boxes = boxes;
            this.steelCornerColor = steelCornerColor;
            this.width = width;
            this.depth = depth;
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
            }
        }

        public List<Box> Boxes
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
