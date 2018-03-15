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
        public bool Door { get; set; }
        private readonly Box box;
        
        public BoxShape(Box box)
        {
            this.Stroke = Brushes.Black;
            StrokeThickness = 2;
            this.box = box;
        }

        protected override Geometry DefiningGeometry
        {
            get { return GenerateMyWeirdGeometry(); }
        }

        private Geometry GenerateMyWeirdGeometry()
        {
            Console.WriteLine("coucou"+ box.Cupboard.Width+" "+box.Height);
            int cornerWidth = 5;
            int mountWidth = 5;
            int knopSize = 5;
            int knopDelta = 10;
            
            GeometryGroup geom = new GeometryGroup();

            Width = box.Cupboard.Width;
            Height = box.Height;
            


            RectangleGeometry mainRect = new RectangleGeometry();
            mainRect.Rect = new Rect(0,0, box.Cupboard.Width, box.Height);
            

            LineGeometry leftCorner = new LineGeometry();
            leftCorner.StartPoint = new Point(cornerWidth, 0);
            leftCorner.EndPoint = new Point(cornerWidth, box.Height);

            LineGeometry rightCorner = new LineGeometry();
            rightCorner.StartPoint = new Point(box.Cupboard.Width -cornerWidth, 0);
            rightCorner.EndPoint = new Point(box.Cupboard.Width -cornerWidth, box.Height);

            LineGeometry topMount = new LineGeometry();
            topMount.StartPoint = new Point(cornerWidth, mountWidth);
            topMount.EndPoint = new Point(box.Cupboard.Width -cornerWidth, mountWidth);

            LineGeometry bottomMount = new LineGeometry();
            bottomMount.StartPoint = new Point(cornerWidth, box.Height -mountWidth);
            bottomMount.EndPoint = new Point(box.Cupboard.Width - cornerWidth, box.Height -mountWidth);

            LineGeometry door = new LineGeometry();
            door.StartPoint = new Point(box.Cupboard.Width /2, mountWidth);
            door.EndPoint = new Point(box.Cupboard.Width /2, box.Height - mountWidth);

            EllipseGeometry leftKnop = new EllipseGeometry();
            leftKnop.Center = new Point(cornerWidth + knopDelta, box.Height / 2);
            leftKnop.RadiusX = knopSize;
            leftKnop.RadiusY = knopSize;

            EllipseGeometry rightKnop = new EllipseGeometry();
            rightKnop.Center = new Point(box.Cupboard.Width -(cornerWidth + knopDelta), box.Height / 2);
            rightKnop.RadiusX = knopSize;
            rightKnop.RadiusY = knopSize;


           geom.Children.Add(mainRect);
            geom.Children.Add(leftCorner);
            geom.Children.Add(rightCorner);
            geom.Children.Add(topMount);
            geom.Children.Add(bottomMount);

            if(Door)
            {
                geom.Children.Add(door);
                geom.Children.Add(leftKnop);
                geom.Children.Add(rightKnop);
            }

            Console.WriteLine(geom.Bounds);
            return geom;
        }
    }
}
