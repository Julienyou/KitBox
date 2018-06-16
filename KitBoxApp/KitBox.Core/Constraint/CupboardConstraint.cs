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
    public class CupboardConstraint : IConstraintChecker<Cupboard>, INotifyPropertyChanged
    {
        #region Attributes
        private List<int> m_Depths;
        private List<int> m_Widths;
        private List<string> m_SteelCornerColors;
        private int maxHeight;
        #endregion

        #region Property changed members
        public event PropertyChangedEventHandler PropertyChanged;
        void Notify(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
        #endregion

        #region methods
        public bool Check(Cupboard cb)
        {
            if (m_Depths.Contains(cb.Depth) && m_Widths.Contains(cb.Width) && maxHeight > cb.Height)
            {
                return true;
            }
            else
                return false;
        }
        #endregion

        #region Properties
        public List<int> Widths
        {
            get => m_Widths;
            set
            {
                m_Widths = value;
                Notify("Widths");
            }
        }

        public List<int> Depths
        {
            get => m_Depths;
            set
            {
                m_Depths = value;
                Notify("Depths");
            }
        }
        public List<string> SteelCornerColors
        {
            get => m_SteelCornerColors;
            set { m_SteelCornerColors = value; }
        }
        public int MaxHeight
        {
            get => maxHeight;
            set { maxHeight = value; }
        }
        #endregion

    }
}
