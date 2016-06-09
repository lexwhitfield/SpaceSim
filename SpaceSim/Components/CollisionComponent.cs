using Artemis.Interface;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceSim.Components
{
    public class CollisionComponent : IComponent
    {
        public bool IsSensor { get; private set; }

        public Rectangle Fixture { get; private set; }

        public CollisionComponent(bool sensor)
        {
            this.IsSensor = sensor;
        }
    }
}
