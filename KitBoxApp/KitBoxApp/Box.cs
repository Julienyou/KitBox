using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KitBoxApp
{
    class Box
    {
        private int height;
        private List<IAccessory> accessories;
        private Tuple<int> boxDimensions;
        private int price;
        private List<Pane> panes;

        public Box(int height, List<IAccessory> accessories, Tuple<int> boxDimensions, int price, List<Pane> panes)
        {
            this.height = height;
            this.accessories = accessories;
            this.boxDimensions = boxDimensions;
            this.price = price;
            this.panes = panes;
        }

        public int Price
        {
            get => price;
        }

        public void AddComponent(IAccessory c)
        {
            accessories.Add(c);
        }

        public void RemoveComponent(IAccessory c)
        {
            accessories.Remove(c);
        }
        
        public List<IAccessory> Components
        {
            get => accessories;
        }

        public Tuple<int> BoxDimensions
        {
            get => boxDimensions;
        }

        public int Height
        {
            get { return height; }
        }

        public List<Pane> Panes
        {
            get => panes;
        }
    }
}
