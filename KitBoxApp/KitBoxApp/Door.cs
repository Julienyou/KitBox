using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KitBoxApp
{
    public class Door : IAccessory, INotifyPropertyChanged
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
            set { color = value; Notify("Color");} 
        }

        public bool Knop
        {
            get => knop;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        void Notify(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
