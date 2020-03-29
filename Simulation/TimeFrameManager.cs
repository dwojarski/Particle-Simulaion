using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Simulation
{
    class TimeFrameManager
    {
        private const int timeStep = 10;
        private List<Particle> particles;

        private double time = 0.0;
        private double deltaTime;
        private Canvas canvas;
        private Layer layer;
        private Thread renderThread;
        private SimulationWindow simulationWindow;
        private Originator origin;
        private Caretaker caretaker;

        public TimeFrameManager(double deltaTime, Canvas canvas, Layer layer, SimulationWindow simulationWindow)
        {
            this.deltaTime = deltaTime;
            this.canvas = canvas;
            this.layer = layer;
            this.simulationWindow = simulationWindow;
            particles = new List<Particle>();
            renderThread = new Thread(CreateFrame);
        }

        public void CreateParicles()
        {
            Random rng = new Random();
            int amount = rng.Next(2, 10);
            for (int i = 0; i < 20; i++)
            {
                int[] values = this.RandomValuesGenerator(rng);
                Particle p = new Particle(values[0], new Vector2d(values[1], values[2]), new Vector2d(values[3], values[4]));
                particles.Add(p);
            }
        }

        private int[] RandomValuesGenerator(Random rng)
        {
            int[] values = new int[5];
            values[0] = rng.Next(50, 200) / 10;
            values[1] = rng.Next(0 + values[0], 450 - values[0]);
            values[2] = rng.Next(0 + values[0], 450 - values[0]);
            values[3] = rng.Next(-4, 2) + 1;
            values[4] = rng.Next(-4, 2) + 1;
            return values;
        }

        public double GetActualTime()
        {
            return time;
        }

        //Thread function
        public void CreateFrame()
        {
            while (true)
            {
                this.Physics();
                this.Move();
                this.Render();
                Thread.Sleep((int)(1000 * deltaTime));
            }
        }

        private void Move()
        {
            // Move particles
            foreach (Particle p in particles)
            {
                p.Move();
            }
        }

        private void Physics()
        {
            List<Particle> checkedParticles = new List<Particle>();
            foreach (Particle p in particles)
            {
                foreach (Particle pp in particles)
                {
                    if (pp != p && !checkedParticles.Contains(pp))
                    {
                        if (p.IsColliding(pp))
                        {
                            Vector2d p_speed = new Vector2d(p.GetSpeed().X, p.GetSpeed().Y); 
                            Vector2d pp_speed = new Vector2d(pp.GetSpeed().X, pp.GetSpeed().Y);
                            pp.SetSpeed(p_speed);
                            p.SetSpeed(pp_speed);
                            checkedParticles.Add(p);
                        }
                    }
                }
                p.SetSpeed(WallBounce.GetWallReflectionVector(p.GetSpeed(), p.IsColliding(layer)));
            }
        }

        private void Render()
        {
            simulationWindow.Dispatcher.Invoke((Action)(() =>
            {
                canvas.Children.Clear();
                foreach (Particle p in particles)
                {
                    p.Draw(canvas);
                }
            }));

        }
        
        public void Start()
        {
            renderThread = new Thread(CreateFrame);
            renderThread.Start();
        }

        public void Stop()
        {
            renderThread.Abort();
        }

        public void Save()
        {
            this.caretaker = new Caretaker();
            foreach (Particle p in this.particles)
            {
                this.origin = new Originator(p);
                caretaker.SaveToFile(this.origin.CreateMemento());
            }
            caretaker.CloseFile();
        }

        public void Load()
        {
            string[] fulldata = this.caretaker.ReadFromFile();
            string[] singledata;
            int j = 0;
            foreach (Particle p in this.particles)
            {
                singledata = fulldata[j].Split(';');
                p.SetRadius(Convert.ToDouble(singledata[0]));
                p.SetPosition(Convert.ToDouble(singledata[1]), Convert.ToDouble(singledata[2]));
                p.SetSpeed(Convert.ToDouble(singledata[3]), Convert.ToDouble(singledata[4]));
                if (j < fulldata.Length - 1) j++;
            }
        }

         ~TimeFrameManager()
        {
            this.renderThread.Abort();
        }
    }
}
