using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KitBoxApp
{
    class Pane
    {
        private string color;
        private int price;
        private string type;

        public Pane(string color, int price, string type)
        {
            this.color = color;
            this.price = price;
            this.type = type;
        }

        public int GetPrice(int height)
        {
            return this.price; // How to integer the height with our class diagram ?
        }
        public string Type
        {
            get => type;
        }
    }
}
