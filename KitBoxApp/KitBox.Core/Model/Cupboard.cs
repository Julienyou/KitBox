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
        private ObservableCollection<Box> m_Boxes = new ObservableCollection<Box> { };
        private int m_Width;
        private int m_Depth;
        private int m_Height;
        private string m_SteelCornerColor;
        private CupboardConstraint m_CupboardConstraint = new CupboardConstraint();
        #endregion

        #region Constructor
        public Cupboard()
        {
            m_Boxes.CollectionChanged += OnBoxesChanged;
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
                Height = m_Boxes.Sum(x => x.Height);
                m_CupboardConstraint.SteelCornerColors = ConstraintBuilder.GetAvailableSteelCornerColor(m_Height);
                foreach (Box newItem in e.NewItems)
                {

                    //Add listener for each item on PropertyChanged event
                    newItem.PropertyChanged += this.OnItemPropertyChanged;
                }
            }

            if (e.OldItems != null)
            {
                Height = m_Boxes.Sum(x => x.Height);
                m_CupboardConstraint.SteelCornerColors = ConstraintBuilder.GetAvailableSteelCornerColor(m_Height);
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
                Height = m_Boxes.Sum(x => x.Height);
                m_CupboardConstraint.SteelCornerColors = ConstraintBuilder.GetAvailableSteelCornerColor(m_Height);
            }

        }

        #endregion


        #region Properties
        public int Width
        {
            get => m_Width;
            set
            {
                m_Width = value;
                m_CupboardConstraint.Depths = ConstraintBuilder.GetAvailableDepth(m_Width);
                if (!m_CupboardConstraint.Depths.Contains(m_Depth))
                {
                    m_Depth = m_CupboardConstraint.Depths[0];
                }

                foreach (Box b in m_Boxes)
                {
                    b.BoxConstraint.HColors = ConstraintBuilder.GetAvailableHPaneColor(m_Width, m_Depth);
                    if (!b.BoxConstraint.HColors.Contains(b.HorizontalColor))
                    {
                        b.HorizontalColor = b.BoxConstraint.HColors[0];
                    }
                    b.BoxConstraint.VColors = ConstraintBuilder.GetAvailableVPaneColor(m_Width, m_Depth, b.Height);
                    Console.WriteLine("w  " + b.BoxConstraint.VColors.Count);
                    if (!b.BoxConstraint.VColors.Contains(b.LateralColor))
                    {
                        b.LateralColor = b.BoxConstraint.VColors[0];
                    }
                    ((Door)b.Accessories[0]).DoorConstraint.Colors = ConstraintBuilder.GetAvailableDoorStyle(m_Width, b.Height);
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
            get => m_SteelCornerColor;
            set
            {
                m_SteelCornerColor = value;
                Notify("SteelCornerColor");
            }
        }

        public int Depth
        {
            get => m_Depth;
            set
            {
                m_Depth = value;
                foreach (Box b in m_Boxes)
                {
                    b.BoxConstraint.HColors = ConstraintBuilder.GetAvailableHPaneColor(m_Width, m_Depth);
                    if (!b.BoxConstraint.HColors.Contains(b.HorizontalColor))
                    {
                        b.HorizontalColor = b.BoxConstraint.HColors[0];
                    }
                    b.BoxConstraint.VColors = ConstraintBuilder.GetAvailableVPaneColor(m_Width, m_Depth, b.Height);
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
            get => m_Height;
            set
            {
                m_Height = value;
                Notify("Height");
            }
        }

        public ObservableCollection<Box> Boxes
        {
            get => m_Boxes;
        }

        public CupboardConstraint CupboardConstraint { get => m_CupboardConstraint; }
        #endregion

        #region Methods
        public void AddBox()
        {
            Box b = new Box(this);
            b.BoxConstraint.Heights = ConstraintBuilder.GetAvailableHeight();

            b.AddAccessory(new Door());
            b.BoxConstraint.HColors = ConstraintBuilder.GetAvailableHPaneColor(m_Width, m_Depth);
            b.HorizontalColor = b.BoxConstraint.HColors[0];
            b.Height = b.BoxConstraint.Heights[0];
            m_Boxes.Add(b);
            Notify("Height");
        }

        public void RemoveBox(Box b)
        {
            m_Boxes.Remove(b);
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
                    if (Height > m_CupboardConstraint.MaxHeight)
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
