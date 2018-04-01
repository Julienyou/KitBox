using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KitBoxApp.Properties;

namespace KitBoxApp
{
    public class BoxConstraint : IConstraintChecker<Box>
    {
        private List<int> heights;
        private List<string> vColors;
        private List<string> hColors;


        public bool Check(Box b)
        {
            if (heights.Contains(b.Height))
            {
                return true;
            }
            return false;
            //Don't forget to check the availibility of the parts (variable)
        }

        public List<string> VColors
        {
            get => vColors;
            set { vColors = value; }
        }
        public List<string> HColors
        {
            get => hColors;
            set { hColors = value; }
        }

        public List<int> Heights
        {
            get => heights;
            set { heights = value; }
        }

    }
}
