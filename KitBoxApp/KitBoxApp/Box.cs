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
        private List<IComponent> components;
        private Tuple<int> boxDimensions;
        private int price;
        private List<Pane> panes;

        public Box(int height, List<IComponent> components, Tuple<int> boxDimensions, int price, List<Pane> panes)
        {
            this.height = height;
            this.components = components;
            this.boxDimensions = boxDimensions;
            this.price = price;
            this.panes = panes;
        }

        public int Price
        {
            get => price;
        }

        public void AddComponent(IComponent c)
        {
            components.Add(c);
        }

        public void RemoveComponent(IComponent c)
        {
            components.Remove(c);
        }

        public Tuple<int> BoxDimensions
        {
            get => boxDimensions;
        }
    }
}
