using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KitBoxApp
{
    class Box
    {
        private int height;
        private List<IAccessory> accessories;
        private Cupboard cupboard;
        private string lateralColor;
        private string horizontalColor;

        public Box(int height, List<IAccessory> accessories, Cupboard cupboard, string lateralColor, string horizontalColor)
        {
            this.height = height;
            this.accessories = accessories;
            this.cupboard = cupboard;
            this.lateralColor = lateralColor;
            this.horizontalColor = horizontalColor;
        }

        public void AddAccessory(IAccessory c)
        {
            accessories.Add(c);
        }

        public void RemoveAccessory(IAccessory c)
        {
            accessories.Remove(c);
        }

        public List<IAccessory> Accessories
        {
            get => accessories;
        }

        public int GetWidth()
        {
            return cupboard.Width;
        }

        public int Height
        {
            get => height;
        }

        public Cupboard cupboard
        {
            get => cupboard;
        }

        public string LateralColor
        {
            get => lateralColor;
        }

        public string HorizontalColor
        {
            get => horizontalColor;
        }
    }
}
