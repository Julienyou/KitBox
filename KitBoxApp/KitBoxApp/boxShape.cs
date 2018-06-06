using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace KitBox
{
    public class BoxShape : Shape
    {
        #region Dependency Properties
        public static readonly DependencyProperty DoorProperty =
        DependencyProperty.Register("Door", typeof(string),
        typeof(BoxShape),
        new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender));

        public static readonly DependencyProperty BHeightProperty =
        DependencyProperty.Register("BHeight", typeof(int),
        typeof(BoxShape),
        new FrameworkPropertyMetadata(0, FrameworkPropertyMetadataOptions.AffectsMeasure));

        public static readonly DependencyProperty BWidthProperty =
        DependencyProperty.Register("BWidth", typeof(int),
        typeof(BoxShape),
        new FrameworkPropertyMetadata(0, FrameworkPropertyMetadataOptions.AffectsMeasure));
        #endregion

        #region Properties 
        public string Door
        {
            get { return (string)GetValue(DoorProperty); }
            set { SetValue(DoorProperty, value); }
        }

        public int BHeight
        {
            get { return (int)GetValue(BHeightProperty); }
            set { SetValue(BHeightProperty, value); }
        }

        public int BWidth
        {
            get { return (int)GetValue(BWidthProperty); }
            set { SetValue(BWidthProperty, value); }
        }


        public BoxShape()
        {

        }

        protected override Geometry DefiningGeometry
        {
            get { return GenerateMyWeirdGeometry(); }
        }

        #endregion

        #region Methods
        private Geometry GenerateMyWeirdGeometry()
        {

            int scale = 2;
            int cornerWidth = 2 * scale;
            int mountWidth = 2 * scale;
            int knopSize = 3 * scale;
            int knopDelta = 5 * scale;
            int width = BWidth * scale;
            int height = BHeight * scale;

            GeometryGroup geom = new GeometryGroup();

            RectangleGeometry mainRect = new RectangleGeometry(new Rect(0, 0, width, height));

            LineGeometry leftCorner = new LineGeometry(new Point(cornerWidth, 0),new Point(cornerWidth, height));

            LineGeometry rightCorner = new LineGeometry(new Point(width - cornerWidth, 0), new Point(width - cornerWidth, height));

            LineGeometry topMount = new LineGeometry(new Point(cornerWidth, mountWidth), new Point(width - cornerWidth, mountWidth));

            LineGeometry bottomMount = new LineGeometry( new Point(cornerWidth, height - mountWidth), new Point(width - cornerWidth, height - mountWidth));

            LineGeometry door = new LineGeometry(new Point(width / 2, mountWidth), new Point(width / 2, height - mountWidth));

            EllipseGeometry leftKnop = new EllipseGeometry(new Point(cornerWidth + knopDelta, height / 2), knopSize, knopSize);

            EllipseGeometry rightKnop = new EllipseGeometry(new Point(width - (cornerWidth + knopDelta), height / 2), knopSize, knopSize);

            geom.Children.Add(mainRect);
            geom.Children.Add(leftCorner);
            geom.Children.Add(rightCorner);
            geom.Children.Add(topMount);
            geom.Children.Add(bottomMount);

            if (Door != null && !Door.Equals("None"))
            {
                geom.Children.Add(door);
                geom.Children.Add(leftKnop);
                geom.Children.Add(rightKnop);
            }

            return geom;
        }
        #endregion
    }
}
