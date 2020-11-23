using System.Windows;

namespace Lab1.ShapesInfo
{
    public interface ShapeInfo
    {
        bool IsTrigger { get; set; }
        Point Position { get; set; }

        bool IsIncluded(Point point);
    }
}
