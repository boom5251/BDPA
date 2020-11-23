using Lab1.ShapesInfo;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Lab1
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            ShapeInfos = new ShapeInfo[]
            {
                new RectInfo(-30, -30, 60, false),
                new CircleInfo(-60, -60, 60, true)
            };
            Loaded += MainWindow_Loaded;
        }



        public ShapeInfo[] ShapeInfos { get; private set; }



        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < ShapeInfos.Length; i++)
            {
                Shape shape = null;
                ShapeInfo shapeInfo = ShapeInfos[i];

                if (ShapeInfos[i] is RectInfo)
                {
                    RectInfo rectInfo = shapeInfo as RectInfo;

                    shape = new Rectangle()
                    {
                        Width = rectInfo.Edge,
                        Height = rectInfo.Edge,
                    };
                }
                else if (ShapeInfos[i] is CircleInfo)
                {
                    CircleInfo circleInfo = ShapeInfos[i] as CircleInfo;

                    shape = new Ellipse()
                    {
                        Width = circleInfo.Radius * 2,
                        Height = circleInfo.Radius * 2
                    };
                }


                if (shapeInfo != null)
                {
                    if (shapeInfo.IsTrigger)
                    {
                        shape.Fill = new SolidColorBrush(Colors.Black);
                    }
                    else
                    {
                        shape.Fill = new SolidColorBrush(Colors.White);
                        Panel.SetZIndex(shape, 10);
                    }
                        

                    Canvas.SetLeft(shape, shapeInfo.Position.X + drawArea.ActualWidth / 2);
                    Canvas.SetBottom(shape, shapeInfo.Position.Y + drawArea.ActualHeight / 2);

                    drawArea.Children.Add(shape);
                }
            }
        }



        private void Button_Click(object sender, RoutedEventArgs e)
        {
            bool isTriggerEntered = false;
            Point point;

            try
            {
                point = new Point(double.Parse(xTextBox.Text), double.Parse(yTextBox.Text));
            }
            catch (FormatException)
            {
                MessageBox.Show("Данные введены неверно", "Некорректный ввод");
                return;
            }


            Ellipse ellipse = new Ellipse() { Width = 4, Height = 4, Fill = new SolidColorBrush(Colors.Red) };
            Canvas.SetLeft(ellipse, point.X + drawArea.ActualWidth / 2 - 2);
            Canvas.SetBottom(ellipse, point.Y + drawArea.ActualHeight / 2 - 2);
            Panel.SetZIndex(ellipse, 20);

            drawArea.Children.Add(ellipse);


            for (int i = 0; i < ShapeInfos.Length; i++)
            {
                bool isIncluded = ShapeInfos[i].IsIncluded(point);

                if (!ShapeInfos[i].IsTrigger)
                {
                    if (isIncluded)
                    {
                        resultLabel.Content = "Не попадает";
                        return;
                    }
                }
                else
                {
                    if (isIncluded)
                    {
                        Debug.WriteLine(isIncluded);
                        isTriggerEntered = true;
                    }

                    Debug.WriteLine(isIncluded);
                }
            }

            if (isTriggerEntered)
                resultLabel.Content = "Попадает";
            else
                resultLabel.Content = "Не попадает";
        }
    }
}
