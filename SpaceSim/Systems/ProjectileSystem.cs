using Artemis;
using Artemis.Attributes;
using Artemis.Manager;
using Artemis.System;
using SpaceSim.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceSim.Systems
{
    [ArtemisEntitySystem(ExecutionType = ExecutionType.Synchronous, GameLoopType = GameLoopType.Update, Layer = 1)]
    public class ProjectileSystem:EntityProcessingSystem
    {
        public ProjectileSystem()
            : base(Aspect.All(typeof(ProjectileComponent)))
        {

        }

        public override void Process(Entity entity)
        {
            ProjectileComponent pc = entity.GetComponent<ProjectileComponent>();

            if (DateTime.UtcNow.Subtract(pc.Birth) > pc.LifeSpan)
            {
                pc.Alive = false;
                entity.Delete();
            }            
        }
    }
}
