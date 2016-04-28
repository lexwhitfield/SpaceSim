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
    public class PositionComponent : IComponent
    {
        public Vector2 Position { get; set; }

        public PositionComponent(Vector2 p)
        {
            this.Position = p;
        }
    }
}
