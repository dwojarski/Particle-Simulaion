using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulation
{
    class Originator
    {
        private string state;

        public Originator(Particle particle)
        {
            state = particle.ToString();
        }

        public void SetMemento(Memento m)
        {
            this.state = m.GetState();
        }

        public Memento CreateMemento()
        {
            return new Memento(state);
        }
    }
}
