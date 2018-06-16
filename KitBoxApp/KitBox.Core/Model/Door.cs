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
        private string m_Color;
        private bool m_knop = false;
        private DoorConstraint m_DoorConstraint = new DoorConstraint();
        #endregion

        #region Properties
        public string Color
        {
            get => m_Color;
            set { m_Color = value; Notify("Color");} 
        }

        public bool Knop
        {
            get => m_knop;
            set { Knop = value; }
        }
        public DoorConstraint DoorConstraint { get => m_DoorConstraint; }
        #endregion

        #region Property changed members
        public event PropertyChangedEventHandler PropertyChanged;
        void Notify(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
        #endregion
    }
}
