using KitBox.Core.Constraint;
using KitBox.Core.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KitBox.Core.Model
{
    public class Door : IAccessory, INotifyPropertyChanged
    {
        #region Attributes
        private string color;
        private bool knop = false;
        private DoorConstraint doorConstraint = new DoorConstraint();
        #endregion

        #region Properties
        public string Color
        {
            get => color;
            set { color = value; Notify("Color");} 
        }

        public bool Knop
        {
            get => knop;
            set { Knop = value; }
        }
        public DoorConstraint DoorConstraint { get => doorConstraint; }
        #endregion

        #region Properties
        public event PropertyChangedEventHandler PropertyChanged;
        void Notify(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
        #endregion
    }
}
