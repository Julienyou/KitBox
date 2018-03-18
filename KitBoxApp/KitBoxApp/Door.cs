using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KitBoxApp
{
    class Door : IAccessory
    {
        private string color;
        private bool knop;

        public Door(string color, bool knop)
        {
            this.color = color;
            this.knop = knop;
        }

        public string Color
        {
            get => color;
        }

        public bool Knop
        {
            get => knop;
        }
    }
}
