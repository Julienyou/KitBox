using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KitBoxApp
{
    class SteelCorner
    {
        private string color;
        private int height;
        private int price;
        private bool cut;

        public SteelCorner(string color, int height, int price, bool cut)
        {
            this.color = color;
            this.height = height;
            this.price = price;
            this.cut = cut;
        }

        public int GetPrice()
        {
            return this.price; //+ Supp;
        }
    }
}
