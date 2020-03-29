﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulation
{
    class Layer
    {
        private double a;
        private Vector2d position;
         public Layer(double a, Vector2d position)
        {
            this.a = a;
            this.position = position;
        }

        public double GetA()
        {
            return this.a;
        }

        public Vector2d GetPosition()
        {
            return this.position;
        }
    }
}
