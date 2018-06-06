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
        private List<int> heights;
        private List<string> vColors;
        private List<string> hColors;
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
            if (heights.Contains(b.Height))
            {
                return true;
            }
            return false;
        }
        #endregion

        #region Properties
        public List<string> VColors
        {
            get => vColors;
            set
            {
                vColors = value;
                Notify("VColors");
            }
        }
        public List<string> HColors
        {
            get => hColors;
            set
            {
                hColors = value;
                Notify("HColors");
            }
        }

        public List<int> Heights
        {
            get => heights;
            set
            {
                heights = value;
                Notify("Heights");
            }
        }
        #endregion
    }
}
