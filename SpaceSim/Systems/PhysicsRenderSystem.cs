using Artemis;
using Artemis.Attributes;
using Artemis.Manager;
using Artemis.System;
using GameLibrary.Core;
using Microsoft.Xna.Framework.Graphics;
using SpaceSim.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceSim.Systems
{
    [ArtemisEntitySystem(ExecutionType = ExecutionType.Synchronous, GameLoopType = GameLoopType.Draw, Layer = 2)]
    public class PhysicsRenderSystem : EntityProcessingSystem
    {
        private BoundedCamera _boundedCamera { get { return EntitySystem.BlackBoard.GetEntry<BoundedCamera>("BoundedCamera"); } }

        public PhysicsRenderSystem()
            : base(Aspect.All(typeof(CollisionComponent), typeof(PositionComponent)))
        {

        }

        protected override void ProcessEntities(IDictionary<int, Entity> entities)
        {
            ScreenManager.Instance.SpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, _boundedCamera.TranslationMatrix);

            foreach (Entity e in this.ActiveEntities)
            {
                Process(e);
            }

            ScreenManager.Instance.SpriteBatch.End();
        }

        public override void Process(Entity entity)
        {
            CollisionComponent cc = entity.GetComponent<CollisionComponent>();
            PositionComponent pc = entity.GetComponent<PositionComponent>();
            RotationComponent rc = (entity.HasComponent<RotationComponent>()) ? entity.GetComponent<RotationComponent>() : new RotationComponent(0, 0, false);
        }
    }
}
