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

        private string reference;
        private string code;
        private string color; 
        private int[] dimension = new int[3](); //Could this be unsafe? One could set a int[4] for example... 
        private int price;
        private int quantity;
        private bool instore;
        

        //---Constructors

        public Component(string reference, string color, string code, int price, int quantity)
        {
            this.reference = reference;
            this.code = code;
            this.dimension = dimension;
            this.color = color;
            this.price = price;
            this.quantity = quantity;
        }


        //---Getters and Setters

        public int[] Dimension { get => dimension; set => dimension = value; }
        public string Reference { get => reference; set => reference = value; }
        public string Code { get => code; set => code = value; }
        public string Color { get => color; set => color = value; }
        public int Price { get => price; set => price = value; }
        public int Quantity { get => quantity; set => quantity = value; }
        public bool Instore { get => instore; set => instore = value; }

        /* 
        This method could directly create a component from its ID from the database
        
        public Component(string idfromdatabase)
        {

        }

        */

        //---Methods
    }
}
