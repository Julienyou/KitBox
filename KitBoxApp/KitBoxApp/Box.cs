using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KitBoxApp
{
    class Box
    {

        //---Atributes

        private int height;
        private List<IAccessory> accessories;
        private int price;
        private List<Pane> panes;
        private Cupboard cupboard;

        //---Constructors

        public Box(int height, List<IAccessory> accessories, int price, List<Pane> panes, Cupboard cupboard)
        {
            this.height = height;
            
            this.accessories = accessories;
            this.price = price;
            this.panes = panes;
            this.cupboard = cupboard;
        }

        //---Getters-Setters

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
            get { return height; }
        }

        public List<Pane> Panes
        {
            get => panes;
        }

        //---Methods

        public List<Component> GetComponents()
        {
            string[] dimmensions = new string[] {
                Height.ToString(),                      //Height
                this.cupboard.Width().ToString(),       //Width
                this.cupboard.Depth().ToString() };     //Depth
            
            List<Component> components;
            Component sidepane = new Component("PAG" + dimmensions[0] + dimmensions [2] + this.panes[0]);
            components.Add(new Component());

        }
    }
}
