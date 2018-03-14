using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KitBoxApp
{
    class Pane
    {

        //---Atributes

        public string color { get; private set; }
        public int price { get; private set; }
        public string type { get; private set; }

        //---Constructors

        public Pane(string color, int price, string type)
        {
            this.color = color;
            this.price = price;
            this.type = type;
        }

        //---
    }
}
