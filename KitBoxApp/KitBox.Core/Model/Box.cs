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

    public class Box : INotifyPropertyChanged
    {
        #region Attributes
        private int m_Height;
        private List<IAccessory> m_Accessories = new List<IAccessory> { };
        private Cupboard m_Cupboard;
        private string m_CateralColor;
        private string m_HorizontalColor;
        private BoxConstraint m_BoxConstraint;
        #endregion

        #region Property changed members
        // INotifyPropertyChanged Member
        public event PropertyChangedEventHandler PropertyChanged;
        void Notify(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
        #endregion

        #region Constructor
        public Box(Cupboard cupboard)
        {
            this.m_Cupboard = cupboard;
            this.m_BoxConstraint = new BoxConstraint();
        }
        #endregion

        #region Methods
        public void AddAccessory(IAccessory c)
        {
            m_Accessories.Add(c);
        }

        public void RemoveAccessory(IAccessory c)
        {
            m_Accessories.Remove(c);
        }
        #endregion

        #region Properties
        public List<IAccessory> Accessories
        {
            get => m_Accessories;
        }

        public int GetWidth()
        {
            return m_Cupboard.Width;
        }

        public int Height
        {
            get => m_Height;
            set
            {
                m_Height = value;

                ((Door)m_Accessories[0]).DoorConstraint.Colors = ConstraintBuilder.GetAvailableDoorStyle(m_Cupboard.Width, m_Height);
                if (!((Door)m_Accessories[0]).DoorConstraint.Colors.Contains(((Door)m_Accessories[0]).Color))
                {
                    ((Door)m_Accessories[0]).Color = ((Door)m_Accessories[0]).DoorConstraint.Colors[0];
                }
                m_BoxConstraint.VColors = ConstraintBuilder.GetAvailableVPaneColor(m_Cupboard.Width, m_Cupboard.Depth, m_Height);
                if (!m_BoxConstraint.VColors.Contains(m_CateralColor))
                {
                    m_CateralColor = m_BoxConstraint.VColors[0];
                }
                Notify("Height");

                //if (cupboard.Height-height+value >= cupboard.CupboardConstraint.MaxHeight)
                //{
                //    throw new WarningException("You have reached max cupboard height");
                //}
                //else
                //{
                //    height = value;
                
                //    ((Door)accessories[0]).DoorConstraint.Colors = ConstraintBuilder.GetAvailableDoorStyle(cupboard.Width, height);
                //    if (!((Door)accessories[0]).DoorConstraint.Colors.Contains(((Door)accessories[0]).Color))
                //    {
                //        ((Door)accessories[0]).Color = ((Door)accessories[0]).DoorConstraint.Colors[0];
                //    }
                //    boxConstraint.VColors = ConstraintBuilder.GetAvailableVPaneColor(cupboard.Width, cupboard.Depth, height);
                //    if (!boxConstraint.VColors.Contains(lateralColor))
                //    {
                //        lateralColor = boxConstraint.VColors[0];
                //    }
                //    Notify("Height");
                //}
            }
        }

        public Cupboard Cupboard
        {
            get => m_Cupboard;
        }

        public string LateralColor
        {
            get => m_CateralColor;
            set
            {
                m_CateralColor = value;
                Notify("LateralColor");
            }
        }

        public string HorizontalColor
        {
            get => m_HorizontalColor;
            set
            {
                m_HorizontalColor = value;
                Notify("HorizontalColor");
            }
        }

        public BoxConstraint BoxConstraint { get => m_BoxConstraint; }

        #endregion
    }
}
