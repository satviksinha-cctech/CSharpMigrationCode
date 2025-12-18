using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using GE = Sketcher3D.GeometryEngine;

namespace Sketcher3D
{
    public partial class MainWindow : Window
    {
        private GE.Shape currentShape;

        private bool isRotating = false;
        private GE.Point lastMouse;

        private double rotX = 0;
        private double rotY = 0;
        private double scale = 150;

        public MainWindow()
        {
            InitializeComponent();
        }

        // ================= SHAPE DRAW =================

        private void DrawShape(GE.Shape shape)
        {
            currentShape = shape;
            Viewport.Children.Clear();

            GE.Triangulation tri = shape.GetTriangulation();

            IReadOnlyList<GE.Point> points = tri.GetPoints();
            IReadOnlyList<GE.Triangle> triangles = tri.GetTriangles();

            foreach (var t in triangles)
            {
                GE.Point p1 = Project(points[t.A]);
                GE.Point p2 = Project(points[t.B]);
                GE.Point p3 = Project(points[t.C]);

                Polygon poly = new Polygon
                {
                    Stroke = Brushes.Black,
                    Fill = Brushes.SteelBlue,
                    StrokeThickness = 1
                };

                poly.Points.Add(new System.Windows.Point(p1.GetX(), p1.GetY()));
                poly.Points.Add(new System.Windows.Point(p2.GetX(), p2.GetY()));
                poly.Points.Add(new System.Windows.Point(p3.GetX(), p3.GetY()));

                Viewport.Children.Add(poly);
            }
        }

        // ================= PROJECTION =================

        private GE.Point Project(GE.Point p)
        {
            double x = p.GetX();
            double y = p.GetY();
            double z = p.GetZ();

            // Rotate Y
            double cosY = System.Math.Cos(rotY);
            double sinY = System.Math.Sin(rotY);
            double x1 = x * cosY + z * sinY;
            double z1 = -x * sinY + z * cosY;

            // Rotate X
            double cosX = System.Math.Cos(rotX);
            double sinX = System.Math.Sin(rotX);
            double y1 = y * cosX - z1 * sinX;

            double screenX = x1 * scale + Viewport.ActualWidth / 2;
            double screenY = -y1 * scale + Viewport.ActualHeight / 2;

            return new GE.Point(screenX, screenY, 0);
        }

        // ================= MOUSE =================

        private void Viewport_MouseDown(object sender, MouseButtonEventArgs e)
        {
            isRotating = true;
            var p = e.GetPosition(Viewport);
            lastMouse = new GE.Point(p.X, p.Y, 0);
            Viewport.CaptureMouse();
        }

        private void Viewport_MouseUp(object sender, MouseButtonEventArgs e)
        {
            isRotating = false;
            Viewport.ReleaseMouseCapture();
        }

        private void Viewport_MouseMove(object sender, MouseEventArgs e)
        {
            if (!isRotating || currentShape == null)
                return;

            var p = e.GetPosition(Viewport);

            double dx = p.X - lastMouse.GetX();
            double dy = p.Y - lastMouse.GetY();

            rotY += dx * 0.01;
            rotX += dy * 0.01;

            lastMouse = new GE.Point(p.X, p.Y, 0);

            DrawShape(currentShape);
        }

        // ================= BUTTONS =================

        private void Cuboid_Click(object sender, RoutedEventArgs e)
        {
            DrawShape(new GE.Cuboid("Cuboid", 1, 0.7, 0.5));
        }

        private void Cube_Click(object sender, RoutedEventArgs e)
        {
            DrawShape(new GE.Cube("Cube", 1));
        }

        private void Cone_Click(object sender, RoutedEventArgs e)
        {
            DrawShape(new GE.Cone("Cone", 0.5, 1));
        }

        private void Sphere_Click(object sender, RoutedEventArgs e)
        {
            DrawShape(new GE.Sphere("Sphere", 0.5));
        }

        private void Cylinder_Click(object sender, RoutedEventArgs e)
        {
            DrawShape(new GE.Cylinder("Cylinder", 0.5, 1));
        }

        private void Pyramid_Click(object sender, RoutedEventArgs e)
        {
            DrawShape(new GE.Pyramid("Pyramid", 1, 1, 1));
        }
    }
}
