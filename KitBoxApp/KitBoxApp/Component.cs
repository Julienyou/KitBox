using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KitBoxApp
{
    class Component
    {

        //---Atributes

        public string reference { get; private set; }
        public string color { get; private set; }
        public string code { get; private set; }
        public int price { get; private set; }
        public int quantity { get; private set; }
        public bool instore { get; private set; }

        //---Constructors

        public Component(string reference, string color, string code, int price, int quantity, bool instore)
        {
            this.reference = reference;
            this.code = code;
            this.color = color;
            this.price = price;
            this.quantity = quantity;
            this.instore = instore;
        }

        /* 
        This method could directly create a component from its ID from the database
        
        public Component(string idfromdatabase)
        {

        }

        */

        //---Methods
    }
}
