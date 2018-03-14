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
        
        private int height, depth, width;
        private string color;
        private List<IAccessory> accessories = new List<IAccessory>();
        private int price;
        private List<Pane> panes;
        private List<Framework> frameworks;
        private Cupboard cupboard;


        //---Constructors

        public Box(int height, int depth, int width, string color, Cupboard cupboard)
        {
            this.height = height;
            this.depth = depth;
            this.width = width;
            this.color = color;
            this.cupboard = cupboard;
            foreach Pane pane in this.GetPanes()
            
        }


        //---Getters-Setters

        public int Height { get => height; set => height = value; }
        public int Depth { get => depth; set => depth = value; }
        public int Width { get => width; set => width = value; }
        public int Price { get => price; set => price = value; }

        public List<IAccessory> Accessories { get => accessories; set => accessories = value; }

        public void AddAccessory(IAccessory c)
        {
            accessories.Add(c);
        }

        public void RemoveAccessory(IAccessory c)
        {
            accessories.Remove(c);
        }
        
        
        public List<Pane> Panes { get => panes; set => panes = value; }

        public void AddPane(Pane pane)
        {
            this.panes.Add(pane);
        }

        public void RemovePane(Pane pane)
        {
            this.panes.Remove(pane);
        }


        public List<Framework> Frameworks { get => frameworks; set => frameworks = value; }

        public void AddFramework(Framework framework)
        {
            this.frameworks.Add(framework);
        }

        public void RemoveFramework(Framework framework)
        {
            this.frameworks.Remove(framework);
        }


        public Cupboard Cupboard { get => cupboard; set => cupboard = value; }

        
        //---Methods

        private List<Component> FindPanes()
        {
            List<Component> panes = new List<Component>();
            Component 
        }

        
    }
}
