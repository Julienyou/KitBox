using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KitBoxApp
{
    class Pane : Component
    {

        //---Atributes

        private string type;


        //---Constructors

        public Pane(string color, int price, string type) : base()
        {
            Reference = "Panneau " + type;
            Color = color;
            Price = price;
            this.type = type;
            
        }

        
        //---Getters and Setters

        public string Type { get => type; set => type = value; }

        
        //---Methods

        public List<Component> GetComponents(int[] boxdimension)
        {
            List<Component> panes = new List<Component>();

            Component backpanes = new Component();
            backpanes.Reference = "Panneau Ar";
            backpanes.Dimension = new int[] { boxdimension[0], 0, boxdimension[2]};
            backpanes.Color = Color;
            backpanes.Quantity = 1;
            panes.Add(backpanes);

            Component sidepanes = new Component();
            sidepanes.Reference = "Panneau GD";
            sidepanes.Dimension = new int[] { boxdimension[0], boxdimension[1], 0 };
            sidepanes.Color = Color;
            sidepanes.Quantity = 2;
            panes.Add(sidepanes);

            Component horizontalpanes = new Component();
            horizontalpanes.Reference = "Panneau HB";
            horizontalpanes.Dimension = new int[] { 0, boxdimension[1], boxdimension[2] };
            horizontalpanes.Color = Color;
            horizontalpanes.Quantity = 2;
            panes.Add(horizontalpanes);

            return panes;
        }

    }
}
