using Artemis;
using Artemis.Attributes;
using Artemis.Manager;
using Artemis.System;
using GameLibrary.Core;
using GameLibrary.Utility;
using SpaceSim.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceSim.Systems
{
    [ArtemisEntitySystem(ExecutionType = ExecutionType.Synchronous, GameLoopType = GameLoopType.Update, Layer = 1)]
    public class RotationSystem : EntityProcessingSystem
    {
        public RotationSystem()
            : base(Aspect.All(typeof(RotationComponent)))
        {

        }

        public override void Process(Entity entity)
        {
            RotationComponent rc = entity.GetComponent<RotationComponent>();

            if (rc.AutoRotate)
                rc.Angle = MathFunctions.ClampAngle(rc.Angle + rc.RotationSpeed);
        }
    }
}
