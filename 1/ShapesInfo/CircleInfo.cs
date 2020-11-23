using System;
using System.Windows;

namespace Lab1.ShapesInfo
{
    public class CircleInfo : ShapeInfo
    {
        public CircleInfo(double x, double y, int radius, bool isTrigger = true)
        {
            IsTrigger = isTrigger;
            Position = new Point(x, y);
            Radius = radius;
        }


        public bool IsTrigger { get; set; }
        public Point Position { get; set; }
        public int Radius { get; set; }



        public bool IsIncluded(Point point)
        {
            Point center = new Point(Position.X + Radius, Position.Y + Radius);
            double funcResult = Math.Pow(point.X - center.X, 2) + Math.Pow(point.Y - center.Y, 2);

            if (funcResult <= Math.Pow(Radius, 2))
                return true;
            else
                return false;
        }
    }
}
