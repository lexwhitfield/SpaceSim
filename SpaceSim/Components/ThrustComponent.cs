using Artemis.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceSim.Components
{
    public class ThrustComponent : IComponent
    {
        public bool ForwardThrust { get; set; }
        public bool BackwardThrust { get; set; }
        public bool LeftThrust { get; set; }
        public bool RightThrust { get; set; }
        public float MaxSpeed { get; set; }
        public float Acceleration { get; set; }
        public float Deceleration { get; set; }

        public ThrustComponent()
        {
            this.ForwardThrust = false;
            this.BackwardThrust = false;
            this.LeftThrust = false;
            this.RightThrust = false;
            this.MaxSpeed = 10f;
            this.Acceleration = 0.15f;
            this.Deceleration = 0.95f;
        }
    }
}
