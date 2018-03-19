using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace KitBoxApp
{
    public class BoxShape : Shape
    {
        public static readonly DependencyProperty DoorProperty =
        DependencyProperty.Register("Door", typeof(string),
        typeof(BoxShape),
        new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender));

        public string Door
        {
            get { return (string)GetValue(DoorProperty); }
            set { SetValue(DoorProperty, value);  }
        }

        public static readonly DependencyProperty BHeightProperty =
        DependencyProperty.Register("BHeight", typeof(int),
        typeof(BoxShape),
        new FrameworkPropertyMetadata(0, FrameworkPropertyMetadataOptions.AffectsMeasure));

        public int BHeight
        {
            get { return (int)GetValue(BHeightProperty); }
            set { SetValue(BHeightProperty, value); }
        }


        public static readonly DependencyProperty BWidthProperty =
        DependencyProperty.Register("BWidth", typeof(int),
        typeof(BoxShape),
        new FrameworkPropertyMetadata(0, FrameworkPropertyMetadataOptions.AffectsMeasure));

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

        private Geometry GenerateMyWeirdGeometry()
        {

            int scale = 2;
            int cornerWidth = 5*scale;
            int mountWidth = 5*scale;
            int knopSize = 5*scale;
            int knopDelta = 10*scale;
            int width = BWidth * scale;
            int height = BHeight * scale;

            

            GeometryGroup geom = new GeometryGroup();


            
            RectangleGeometry mainRect = new RectangleGeometry();
            mainRect.Rect = new Rect(0,0, width, height);
            

            LineGeometry leftCorner = new LineGeometry();
            leftCorner.StartPoint = new Point(cornerWidth, 0);
            leftCorner.EndPoint = new Point(cornerWidth, height);

            LineGeometry rightCorner = new LineGeometry();
            rightCorner.StartPoint = new Point(width-cornerWidth, 0);
            rightCorner.EndPoint = new Point(width-cornerWidth, height);

            LineGeometry topMount = new LineGeometry();
            topMount.StartPoint = new Point(cornerWidth, mountWidth);
            topMount.EndPoint = new Point(width-cornerWidth, mountWidth);

            LineGeometry bottomMount = new LineGeometry();
            bottomMount.StartPoint = new Point(cornerWidth, height-mountWidth);
            bottomMount.EndPoint = new Point(width - cornerWidth, height-mountWidth);

            LineGeometry door = new LineGeometry();
            door.StartPoint = new Point(width/2, mountWidth);
            door.EndPoint = new Point(width/2, height - mountWidth);

            EllipseGeometry leftKnop = new EllipseGeometry();
            leftKnop.Center = new Point(cornerWidth + knopDelta, height / 2);
            leftKnop.RadiusX = knopSize;
            leftKnop.RadiusY = knopSize;

            EllipseGeometry rightKnop = new EllipseGeometry();
            rightKnop.Center = new Point(width-(cornerWidth + knopDelta), height / 2);
            rightKnop.RadiusX = knopSize;
            rightKnop.RadiusY = knopSize;


            geom.Children.Add(mainRect);
            geom.Children.Add(leftCorner);
            geom.Children.Add(rightCorner);
            geom.Children.Add(topMount);
            geom.Children.Add(bottomMount);

            if(Door!=null && !Door.Equals("None"))
            {
                geom.Children.Add(door);
                geom.Children.Add(leftKnop);
                geom.Children.Add(rightKnop);
            }



            return geom;
        }
    }
}
