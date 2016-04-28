using Artemis;
using Artemis.Interface;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceSim.Components
{
    public class InertiaComponent : IComponent
    {
        public Vector2 Inertia;
        
        public InertiaComponent()
        {
            this.Inertia = new Vector2(0, 0);
        }

        public InertiaComponent(Vector2 v)
        {
            this.Inertia = v;
        }
    }
}
