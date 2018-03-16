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
            get { Console.WriteLine("Door GET : " + GetValue(DoorProperty));  return (string)GetValue(DoorProperty); }
            set { Console.WriteLine("Door changed : " + GetValue(DoorProperty));  SetValue(DoorProperty, value);  }
        }
        

       //     public string Door { get; set; }


        public BoxShape()
        {
            Stroke = Brushes.Black;
            StrokeThickness = 2;
            //Door = "None";
            Console.WriteLine("bla : " + Door);
        }

        protected override Geometry DefiningGeometry
        {
            get { return GenerateMyWeirdGeometry(); }
        }

        private Geometry GenerateMyWeirdGeometry()
        {
            
            int cornerWidth = 5;
            int mountWidth = 5;
            int knopSize = 5;
            int knopDelta = 10;
            
            GeometryGroup geom = new GeometryGroup();


            
            RectangleGeometry mainRect = new RectangleGeometry();
            mainRect.Rect = new Rect(0,0,Width,Height);
            

            LineGeometry leftCorner = new LineGeometry();
            leftCorner.StartPoint = new Point(cornerWidth, 0);
            leftCorner.EndPoint = new Point(cornerWidth, Height);

            LineGeometry rightCorner = new LineGeometry();
            rightCorner.StartPoint = new Point(Width-cornerWidth, 0);
            rightCorner.EndPoint = new Point(Width-cornerWidth, Height);

            LineGeometry topMount = new LineGeometry();
            topMount.StartPoint = new Point(cornerWidth, mountWidth);
            topMount.EndPoint = new Point(Width-cornerWidth, mountWidth);

            LineGeometry bottomMount = new LineGeometry();
            bottomMount.StartPoint = new Point(cornerWidth, Height-mountWidth);
            bottomMount.EndPoint = new Point(Width - cornerWidth, Height-mountWidth);

            LineGeometry door = new LineGeometry();
            door.StartPoint = new Point(Width/2, mountWidth);
            door.EndPoint = new Point(Width/2, Height - mountWidth);

            EllipseGeometry leftKnop = new EllipseGeometry();
            leftKnop.Center = new Point(cornerWidth + knopDelta, Height / 2);
            leftKnop.RadiusX = knopSize;
            leftKnop.RadiusY = knopSize;

            EllipseGeometry rightKnop = new EllipseGeometry();
            rightKnop.Center = new Point(Width-(cornerWidth + knopDelta), Height / 2);
            rightKnop.RadiusX = knopSize;
            rightKnop.RadiusY = knopSize;


            geom.Children.Add(mainRect);
            geom.Children.Add(leftCorner);
            geom.Children.Add(rightCorner);
            geom.Children.Add(topMount);
            geom.Children.Add(bottomMount);

            Console.WriteLine("coucou "+Door);
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
