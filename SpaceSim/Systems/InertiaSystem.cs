using Artemis;
using Artemis.Attributes;
using Artemis.Manager;
using Artemis.System;
using Microsoft.Xna.Framework;
using SpaceSim.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceSim.Systems
{
    [ArtemisEntitySystem(ExecutionType = ExecutionType.Synchronous, GameLoopType = GameLoopType.Update, Layer = 2)]
    internal class InertiaSystem : EntityProcessingSystem
    {
        public InertiaSystem()
            : base(Aspect.All(typeof(InertiaComponent), typeof(PositionComponent), typeof(RotationComponent)))
        {

        }

        public override void Process(Entity entity)
        {
            entity.GetComponent<PositionComponent>().Position += entity.GetComponent<InertiaComponent>().Inertia;
        }
    }
}
