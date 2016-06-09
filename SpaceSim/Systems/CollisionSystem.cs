using Artemis;
using Artemis.System;
using SpaceSim.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceSim.Systems
{
    public class CollisionSystem : EntityProcessingSystem
    {
        private List<int> _checkedEntities;

        public CollisionSystem()
            : base(Aspect.All(typeof(CollisionComponent), typeof(PositionComponent)))
        {
            this._checkedEntities = new List<int>();
        }

        protected override void ProcessEntities(IDictionary<int, Entity> entities)
        {
            this._checkedEntities.Clear();

            foreach (Entity e in this.ActiveEntities)
            {
                Process(e);
                this._checkedEntities.Add(e.Id);
            }
        }

        public override void Process(Entity entity)
        {
            CollisionComponent subject_cc = entity.GetComponent<CollisionComponent>();
            PositionComponent subject_pc = entity.GetComponent<PositionComponent>();
            //RotationComponent rc = (entity.HasComponent<RotationComponent>()) ? entity.GetComponent<RotationComponent>() : new RotationComponent(0, 0, false);

            foreach (Entity target in this.ActiveEntities)
            {
                if (target.Id != entity.Id && !this._checkedEntities.Contains(target.Id))
                {
                    CollisionComponent target_cc = target.GetComponent<CollisionComponent>();
                    PositionComponent target_pc = target.GetComponent<PositionComponent>();

                    // AABB check
                    bool collision = false;


                    if (collision)
                    {
                        ResolveCollision(entity, target);
                    }
                }
            }
        }

        private void ResolveCollision(Entity e1, Entity e2)
        {

        }
    }
}
