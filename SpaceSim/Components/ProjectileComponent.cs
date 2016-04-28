using Artemis.Interface;
using GameLibrary.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceSim.Components
{
    public class ProjectileComponent : IComponent
    {
        public int MaxSpeed { get; set; }
        public bool Alive { get; set; }
        public TimeSpan LifeSpan { get; set; }
        public DateTime Birth { get; set; }

        public ProjectileComponent(int speed, TimeSpan lifespan)
        {
            this.MaxSpeed = speed;
            this.Alive = true;
            this.LifeSpan = lifespan;
            this.Birth = DateTime.UtcNow;
        }
    }
}
