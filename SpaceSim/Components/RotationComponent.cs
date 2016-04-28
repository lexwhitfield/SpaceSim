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
    public class RotationComponent : IComponent
    {
        public float Angle { get; set; }
        public float RotationSpeed { get; set; }
        public bool AutoRotate { get; set; }

        public RotationComponent(float angle, float speed, bool auto)
        {
            this.Angle = angle;
            this.RotationSpeed = speed;           
            this.AutoRotate = auto;
        }
    }
}
