using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace RareGoods

    {
    internal class Dot
        {

        private double rad = 360 / (Math.PI * 2);

        private SolidColorBrush colorBrush = new SolidColorBrush(Color.FromArgb(0x80, 0x40, 0xF0, 0x40));
        private RadialGradientBrush gradiBrush = new RadialGradientBrush() { GradientStops = new GradientStopCollection { new GradientStop(Colors.White, 0.0), new GradientStop(Colors.Black, 1.0) } };

        private double cx = 0;
        private double cy = 0;

        private double ox = 0;
        private double oy = 0;

        private double rd = 0;
        private double deg = 0;
        
        public Dot()
            {

            cx = 1;
            cy = 1;

            }

        public double x
            {
            get { return cx; }
            set {
                cx = value;
                CalculateRadius();
                CalculateDegree();
                }
            }

        public double y
            {
            get { return cy; }
            set {
                cy = value;
                CalculateRadius();
                CalculateDegree();
                }
            }
        public double originX
            {
            get { return ox; }
            set {
                ox = value;
                CalculateRadius();
                CalculateDegree();
                }
            }

        public double originY
            {
            get { return oy; }
            set {
                oy = value;
                CalculateRadius();
                CalculateDegree();
                }
            }

         public double radius
            {
            get { return rd; }
            set {
                rd = value;
                cx=CalculateX();
                cy=CalculateY();
                }
            }
        public double degree
            {
            get { return deg;}
            set {
                deg = value;
                cx=CalculateX();
                cy=CalculateY();
                }
            }

        private double CalculateX()
            {
            cx = rd * (Math.Sin(deg / rad)) + ox;   return cx;
            }

        private double CalculateY()
            {
            cy = rd * (Math.Cos(deg / rad)) + oy;  return cy;
            }

        private void CalculateRadius()
            {
            double tempX = Math.Abs(ox - cx);
            double tempY = Math.Abs(oy - cy);

            rd = Math.Sqrt((tempX * tempX) + (tempY * tempY));

            }

        private void CalculateDegree()
            {

            double tempX = Math.Abs(ox - cx);
            double tempY = Math.Abs(oy - cy);

            deg = (Math.Tan(tempX / tempY)) * rad;

            }
        public Canvas Draw(Color color,double size)
            {
            Ellipse colorDot = new Ellipse();
            Ellipse gradiDot = new Ellipse();
            Canvas drawCanvas = new Canvas();

            colorDot.Width = size; colorDot.Height = size;
            gradiDot.Width = size; gradiDot.Height = size;

            gradiBrush.RadiusX = 0.75;
            gradiBrush.RadiusY = 0.75;

            colorDot.Fill = new SolidColorBrush(color);
            gradiDot.Fill = gradiBrush;

            colorDot.Margin = new Thickness(-colorDot.Width/2, -colorDot.Height/2, 0, 0);
            gradiDot.Margin = new Thickness(-gradiDot.Width/2, -gradiDot.Height/2, 0, 0);

            cx = CalculateX();
            cy = CalculateY(); 
              
            drawCanvas.Margin = new Thickness(cx, cy,0,0); 

            drawCanvas.Children.Add(gradiDot);
            drawCanvas.Children.Add(colorDot);

            return drawCanvas;
            }
        }
    }