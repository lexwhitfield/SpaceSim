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
    public class OrbitSystem : EntityProcessingSystem
    {
        private List<int> _processedEntities { get; set; }

        public OrbitSystem()
            : base(Aspect.All(typeof(OrbitComponent), typeof(PositionComponent)))
        {
            this._processedEntities = new List<int>();
        }

        protected override void ProcessEntities(IDictionary<int, Entity> entities)
        {
            this._processedEntities.Clear();
            base.ProcessEntities(entities);
        }

        public override void Process(Entity entity)
        {
            if (_processedEntities.Contains(entity.Id))
                return;

            OrbitComponent oc = entity.GetComponent<OrbitComponent>();

            if (oc.ParentId == 0)
            {
                _processedEntities.Add(entity.Id);
                return;
            }

            PositionComponent pc = entity.GetComponent<PositionComponent>();

            if (!_processedEntities.Contains(oc.ParentId))
                Process(this.EntityWorld.GetEntity(oc.ParentId));

            Entity parent = this.EntityWorld.GetEntity(oc.ParentId);
            PositionComponent ppc = parent.GetComponent<PositionComponent>();

            oc.AngleFromParent = MathFunctions.ClampAngle(oc.AngleFromParent + oc.AngleDelta);

            _processedEntities.Add(entity.Id);
        }
    }
}
