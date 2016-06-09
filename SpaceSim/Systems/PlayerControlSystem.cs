using Artemis;
using Artemis.Attributes;
using Artemis.Manager;
using Artemis.System;
using GameLibrary.Core;
using GameLibrary.Utility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using SpaceSim.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceSim.Systems
{
    [ArtemisEntitySystem(ExecutionType = ExecutionType.Synchronous, GameLoopType = GameLoopType.Update, Layer = 1)]
    public class PlayerControlSystem : EntityProcessingSystem
    {
        public PlayerControlSystem()
            : base(Aspect.All(typeof(PlayerControlComponent), typeof(InertiaComponent), typeof(RotationComponent), typeof(PositionComponent)))
        {

        }

        public override void Process(Entity entity)
        {
            ThrustComponent tc = entity.GetComponent<ThrustComponent>();
            RotationComponent rc = entity.GetComponent<RotationComponent>();

            if (InputManager.Instance.isKeyReleased(Keys.Space))
            {
                PositionComponent pc = entity.GetComponent<PositionComponent>();
                InertiaComponent ic = entity.GetComponent<InertiaComponent>();
                Entity laser = this.EntityWorld.CreateEntity();

                laser.AddComponent(new ProjectileComponent(15, new TimeSpan(0, 0, 0, 0, 450)));
                laser.AddComponent(new PositionComponent(pc.Position));
                laser.AddComponent(new RotationComponent(rc.Angle, 0f, false));
                float radians = MathHelper.ToRadians(rc.Angle);
                laser.AddComponent(new InertiaComponent(Vector2.Multiply(Vector2.Normalize(new Vector2((float)Math.Sin(radians), -(float)Math.Cos(radians))), laser.GetComponent<ProjectileComponent>().MaxSpeed)));
                laser.AddComponent(new TextureComponent(new Coord(0, 0), "lasers", new Vector2(0, 0)));
            }
        }
    }
}
