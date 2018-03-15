using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KitBoxApp
{
    public class Door : IAccessory
    {
        private string color;
        private bool knop;
        private int price;

        public Door(string color, bool knop, int price)
        {
            this.color = color;
            this.knop = knop;
            this.price = price;
        }

        public int GetPrice() // Add parameter string color ?
        {
            return this.price; // How to integrate the color with our class diagram ?
        }

        public string Color
        {
            get => color;
            set { color = value; } 
        }

        public bool Knop
        {
            get => knop;
        }
    }
}
