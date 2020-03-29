using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Simulation
{
    class Particle
    {
        private double radius;
        private Vector2d position; //zamienic na Vector2d
        private Vector2d speed; //zamienic na Vector2d

        public Particle(double radius, Vector2d position, Vector2d speed)
        {
            this.radius = radius;
            this.position = position;
            this.speed = speed;
        }

        public void SetRadius(double radius)
        {
            this.radius = radius;
        }

        public void SetPosition(double x, double y)
        {
            this.position.X = x;
            this.position.Y = y;
        }

        public void SetSpeed(double x, double y)
        {
            this.speed.X = x;
            this.speed.Y = y;
        }

        public void SetSpeed(Vector2d speed)
        {
            this.speed = speed;
        }

        public Vector2d GetSpeed()
        {
            return this.speed;
        }

        public double GetRadius()
        {
            return this.radius;
        }

        public Vector2d GetPosition()
        {
            return this.position;
        }


        public bool IsColliding(Particle particle)
        {
            if (this.position.DistanceTo(particle.GetPosition()) <= (radius + particle.GetRadius()))
                return true;
                
            return false;
        }

        public WallBounce.Orientation IsColliding(Layer layer)
        {
            double a = layer.GetA();
            double sx = layer.GetPosition().X;
            double sy = layer.GetPosition().Y;

            // Left wall
            if (position.X - radius <= sx && speed.X < 0)
                return WallBounce.Orientation.Vertical;

            // Right wall
            if (position.X + radius >= sx + a && speed.X > 0)
                return WallBounce.Orientation.Vertical;

            // Top wall
            if (position.Y - radius <= sy && speed.Y < 0)
                return WallBounce.Orientation.Horizontal;

            // Bottom wall
            if (position.Y + radius >= sy + a && speed.Y > 0)
                return WallBounce.Orientation.Horizontal;

            return WallBounce.Orientation.None;

        }

        public void Draw(Canvas canvas)
        {
            Ellipse ellipse = new Ellipse()
            {
                Height = 2 * this.radius,
                Width = 2 * this.radius,
                //Fill = Brushes.Blue,
                Stroke = Brushes.Black
            };

            canvas.Children.Add(ellipse);
            ellipse.SetValue(Canvas.LeftProperty, (double)position.X - this.radius);
            ellipse.SetValue(Canvas.TopProperty, (double)position.Y - this.radius);

        }

        public void Move()
        {
            position = position + speed;
        }

        public override string ToString()
        {
            return radius.ToString() + ";" + position.ToString()  + ";" + speed.ToString();
        }
    }
}
