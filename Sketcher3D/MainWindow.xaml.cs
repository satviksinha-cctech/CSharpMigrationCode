using System.Windows;
using System.Windows.Media;
using System.Windows.Controls;
using System.Windows.Shapes;
using Sketcher3D.GeometryEngine;

using EngineShape = Sketcher3D.GeometryEngine.Shape;
using EnginePoint = Sketcher3D.GeometryEngine.Point;

namespace Sketcher3D
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Clear()
        {
            Viewport.Children.Clear();
        }

        private void DrawShape(EngineShape shape)
        {
            Clear();

            var tri = shape.GetTriangulation();
            var points = tri.GetPoints();
            var triangles = tri.GetTriangles();

            foreach (var t in triangles)
            {
                Polygon poly = new Polygon
                {
                    Stroke = Brushes.Black,
                    Fill = Brushes.SteelBlue,
                    StrokeThickness = 1
                };

                poly.Points.Add(Project(points[t.A]));
                poly.Points.Add(Project(points[t.B]));
                poly.Points.Add(Project(points[t.C]));

                Viewport.Children.Add(poly);
            }
        }

        private System.Windows.Point Project(EnginePoint p)
        {
            double scale = 120;
            double cx = Viewport.ActualWidth / 2;
            double cy = Viewport.ActualHeight / 2;

            return new System.Windows.Point(
                cx + p.GetX() * scale,
                cy - p.GetY() * scale
            );
        }

        private void Cuboid_Click(object sender, RoutedEventArgs e)
        {
            DrawShape(new Cuboid("Cuboid", 1, 1, 1));
        }

        private void Cube_Click(object sender, RoutedEventArgs e)
        {
            DrawShape(new Cube("Cube", 1));
        }

        private void Cone_Click(object sender, RoutedEventArgs e)
        {
            DrawShape(new Cone("Cone", 0.5, 1));
        }

        private void Sphere_Click(object sender, RoutedEventArgs e)
        {
            DrawShape(new Sphere("Sphere", 0.5));
        }

        private void Cylinder_Click(object sender, RoutedEventArgs e)
        {
            DrawShape(new Cylinder("Cylinder", 0.5, 1));
        }

        private void Pyramid_Click(object sender, RoutedEventArgs e)
        {
            DrawShape(new Pyramid("Pyramid", 1, 1, 1));
        }
    }
}
