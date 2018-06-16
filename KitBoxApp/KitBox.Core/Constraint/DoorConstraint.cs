using KitBox.Core.Interface;
using KitBox.Core.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KitBox.Core.Constraint
{

    public class DoorConstraint : IConstraintChecker<Box>, INotifyPropertyChanged
    {
        #region Attributes
        private List<string> m_Colors;
        private List<Tuple<int,int>> m_DoorDimensions;
        #endregion

        #region Methods
        public bool Check(Box b)
        {
            foreach (IAccessory element in b.Accessories)
            {
                if (element.GetType() == typeof(Door))
                {
                    Door d = (Door) element;

                    Tuple<int, int> constrain = new Tuple<int, int>(b.Height - 4, (b.Cupboard.Width- 4) / 2);

                    if (m_Colors.Contains(d.Color) && m_DoorDimensions.Contains(constrain))
                    {
                        if (d.Color == "Verre" && d.Knop == true)
                        {
                            return false;
                        }
                        else if (d.Color != "Verre" && d.Knop == false)
                        {
                            return false;
                        }
                        return true;
                    }

                    break;
                }
            }
            
            return false;
        }

        #endregion

        #region Property changed members
        public event PropertyChangedEventHandler PropertyChanged;
        void Notify(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
        #endregion

        #region Propreties
        public List<string> Colors
        {
            get => m_Colors;
            set
            {
                m_Colors = value;
                Notify("Colors");
            }
        }
        #endregion
    }
}
