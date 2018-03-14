using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KitBoxApp
{
    class SteelCorner
    {

        //---Attributes

        private string color;
        private int height;
        private int price;
        private bool cut;

        //---Constructors

        public SteelCorner(string color, int height, int price, bool cut)
        {
            this.color = color;
            this.height = height;
            this.price = price;
            this.cut = cut;
        }

        //---Getters and Setters

        public string Color { get => color; set => color = value; }
        public int Height { get => height; set => height = value; }
        public bool Cut { get => cut; set => cut = value; }

        public int GetPrice()
        {
            return this.price; //+ Supp;
        }

        //---Methods

        public Component ToComponent(int height)
        {
            //SQL find optimal height for the steelcorner
            Component steelcorner = new Component();
            steelcorner.Reference = "Cornières";
            steelcorner.Dimension = new int[] { this.Height, 0, 0 };
            steelcorner.Color = Color;

            return steelcorner;
        }
    }
}
