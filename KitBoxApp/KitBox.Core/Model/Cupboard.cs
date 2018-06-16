using KitBox.Core.Constraint;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace KitBox.Core.Model
{
    public class Cupboard : INotifyPropertyChanged, IDataErrorInfo
    {
        #region attributes
        private ObservableCollection<Box> boxes = new ObservableCollection<Box> { };
        private int width;
        private int depth;
        private int height;
        private string steelCornerColor;
        private CupboardConstraint cupboardConstraint = new CupboardConstraint();
        #endregion

        #region Constructor
        public Cupboard()
        {
            boxes.CollectionChanged += OnBoxesChanged;
        }
        #endregion

        #region Property changed members
        // INotifyPropertyChanged Member
        public event PropertyChangedEventHandler PropertyChanged;
        private void Notify(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
        #endregion

        #region event handlers

        private void OnBoxesChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            Console.WriteLine("box changed");
            if (e.NewItems != null)
            {
                Height = boxes.Sum(x => x.Height);
                cupboardConstraint.SteelCornerColors = ConstraintBuilder.GetAvailableSteelCornerColor(height);
                foreach (Box newItem in e.NewItems)
                {

                    //Add listener for each item on PropertyChanged event
                    newItem.PropertyChanged += this.OnItemPropertyChanged;
                }
            }

            if (e.OldItems != null)
            {
                Height = boxes.Sum(x => x.Height);
                cupboardConstraint.SteelCornerColors = ConstraintBuilder.GetAvailableSteelCornerColor(height);
                foreach (Box oldItem in e.OldItems)
                {
                    oldItem.PropertyChanged -= this.OnItemPropertyChanged;
                }
            }
        }

        void OnItemPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Height")
            {
                Height = boxes.Sum(x => x.Height);
                cupboardConstraint.SteelCornerColors = ConstraintBuilder.GetAvailableSteelCornerColor(height);
            }

        }

        #endregion


        #region Properties
        public int Width
        {
            get => width;
            set
            {
                width = value;
                cupboardConstraint.Depths = ConstraintBuilder.GetAvailableDepth(width);
                if (!cupboardConstraint.Depths.Contains(depth))
                {
                    depth = cupboardConstraint.Depths[0];
                }

                foreach (Box b in boxes)
                {
                    b.BoxConstraint.HColors = ConstraintBuilder.GetAvailableHPaneColor(width, depth);
                    if (!b.BoxConstraint.HColors.Contains(b.HorizontalColor))
                    {
                        b.HorizontalColor = b.BoxConstraint.HColors[0];
                    }
                    b.BoxConstraint.VColors = ConstraintBuilder.GetAvailableVPaneColor(width, depth, b.Height);
                    Console.WriteLine("w  " + b.BoxConstraint.VColors.Count);
                    if (!b.BoxConstraint.VColors.Contains(b.LateralColor))
                    {
                        b.LateralColor = b.BoxConstraint.VColors[0];
                    }
                    ((Door)b.Accessories[0]).DoorConstraint.Colors = ConstraintBuilder.GetAvailableDoorStyle(width, b.Height);
                    if (!((Door)b.Accessories[0]).DoorConstraint.Colors.Contains(((Door)b.Accessories[0]).Color))
                    {
                        ((Door)b.Accessories[0]).Color = ((Door)b.Accessories[0]).DoorConstraint.Colors[0];
                    }
                }
                Notify("Width");
            }
        }

        public string SteelCornerColor
        {
            get => steelCornerColor;
            set
            {
                steelCornerColor = value;
                Notify("SteelCornerColor");
            }
        }

        public int Depth
        {
            get => depth;
            set
            {
                depth = value;
                foreach (Box b in boxes)
                {
                    b.BoxConstraint.HColors = ConstraintBuilder.GetAvailableHPaneColor(width, depth);
                    if (!b.BoxConstraint.HColors.Contains(b.HorizontalColor))
                    {
                        b.HorizontalColor = b.BoxConstraint.HColors[0];
                    }
                    b.BoxConstraint.VColors = ConstraintBuilder.GetAvailableVPaneColor(width, depth, b.Height);
                    if (!b.BoxConstraint.VColors.Contains(b.LateralColor))
                    {
                        b.LateralColor = b.BoxConstraint.VColors[0];
                    }
                }
                Notify("Depth");
            }
        }

        public int Height
        {
            get => height;
            set
            {
                height = value;
                Notify("Height");
            }
        }

        public ObservableCollection<Box> Boxes
        {
            get => boxes;
        }

        public CupboardConstraint CupboardConstraint { get => cupboardConstraint; }
        #endregion

        #region Methods
        public void AddBox()
        {
            Box b = new Box(this);
            b.BoxConstraint.Heights = ConstraintBuilder.GetAvailableHeight();

            b.AddAccessory(new Door());
            b.BoxConstraint.HColors = ConstraintBuilder.GetAvailableHPaneColor(width, depth);
            b.HorizontalColor = b.BoxConstraint.HColors[0];
            b.Height = b.BoxConstraint.Heights[0];
            boxes.Add(b);
            Notify("Height");
        }

        public void RemoveBox(Box b)
        {
            boxes.Remove(b);
        }
        #endregion

        #region DataErrorInfo members
        public string Error
        {
            get { throw new NotImplementedException(); }
        }

        public string this[string columnName]
        {
            get
            {
                string result = null;
                if (columnName == "Height")
                {
                    if (Height > cupboardConstraint.MaxHeight)
                        result = "Cupboard reached maximum height";

                    if (Boxes.Count > 7)
                        result = "Maximum number of boxes";
                }

                return result;
            }

        }
        #endregion

    }
}
