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
        private int height;

        public Pane(string color, int price, string type, int height)
        {
            this.color = color;
            this.price = price;
            this.type = type;
            this.height = height;
        }

        public string Color
        {
            get => color;
        }

        public int Price
        {
            get => price;
        }

        public string Type
        {
            get => type;
        }

        public int Height
        {
            get => height;
        }
    }
}
