using System.Windows;

namespace Lab1.ShapesInfo
{
    public class RectInfo : ShapeInfo
    {
        public RectInfo(double x, double y, int edge, bool isTrigger = true)
        {
            IsTrigger = isTrigger;
            Position = new Point(x, y);
            Edge = edge;
        }


        public bool IsTrigger { get; set; }
        public Point Position { get; set; }
        public int Edge { get; set; }


        public bool IsIncluded(Point point)
        {
            bool x, y;

            if (point.X >= Position.X && point.X <= Position.X + Edge)
                x = true;
            else
                return false;

            if (point.Y >= Position.Y && point.Y <= Position.Y + Edge)
                y = true;
            else
                return false;

            if (x && y)
                return true;
            else
                return false;
        }
    }
}
