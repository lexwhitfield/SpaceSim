using Artemis;
using Artemis.Interface;
using GameLibrary.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceSim.Components
{
    public class OrbitComponent : IComponent
    {
        public int ParentId { get; set; }
        public int Radius { get; set; }
        public float AngleFromParent { get; set; }
        public float AngleDelta { get; set; }

        public OrbitComponent(int parent, int r, float delta)
        {
            this.ParentId = parent;
            this.Radius = r;
            this.AngleFromParent = RandomNumberGenerator.Instance.Next(360);
            this.AngleDelta = delta;
        }
    }
}
