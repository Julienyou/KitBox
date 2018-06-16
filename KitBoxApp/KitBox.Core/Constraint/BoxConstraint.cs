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
    public class BoxConstraint : IConstraintChecker<Box>,INotifyPropertyChanged
    {
        #region Attributes
        private List<int> m_Heights;
        private List<string> m_VColors;
        private List<string> m_HColors;
        #endregion

        #region Property changed members
        public event PropertyChangedEventHandler PropertyChanged;
        void Notify(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
        #endregion

        #region methods
        public bool Check(Box b)
        {
            if (m_Heights.Contains(b.Height))
            {
                return true;
            }
            return false;
        }
        #endregion

        #region Properties
        public List<string> VColors
        {
            get => m_VColors;
            set
            {
                m_VColors = value;
                Notify("VColors");
            }
        }
        public List<string> HColors
        {
            get => m_HColors;
            set
            {
                m_HColors = value;
                Notify("HColors");
            }
        }

        public List<int> Heights
        {
            get => m_Heights;
            set
            {
                m_Heights = value;
                Notify("Heights");
            }
        }
        #endregion
    }
}
