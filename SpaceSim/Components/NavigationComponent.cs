using Artemis.Interface;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceSim.Components
{
    public class NavigationComponent : IComponent
    {
        public Vector2? TargetLocation { get; set; }

        public NavigationComponent()
        {

        }
    }
}
