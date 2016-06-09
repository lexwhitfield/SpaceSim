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
    [ArtemisEntitySystem(ExecutionType = ExecutionType.Synchronous, GameLoopType = GameLoopType.Update, Layer = 1)]
    public class NavigationSystem : EntityProcessingSystem
    {
        public NavigationSystem()
            : base(Aspect.All(typeof(NavigationComponent), typeof(ThrustComponent), typeof(RotationComponent), typeof(PositionComponent)))
        {

        }

        public override void Process(Entity entity)
        {
            NavigationComponent nc = entity.GetComponent<NavigationComponent>();

            if (nc.TargetLocation.HasValue)
            {
                PositionComponent pc = entity.GetComponent<PositionComponent>();
                float deltaX = pc.Position.X - nc.TargetLocation.Value.X;
                float deltaY = pc.Position.Y - nc.TargetLocation.Value.Y;
                float distanceToTarget = (float)Math.Sqrt((deltaX * deltaX) + (deltaY * deltaY));

                if (distanceToTarget < 15)
                {
                    nc.TargetLocation = null;
                    entity.GetComponent<ThrustComponent>().ForwardThrust = false;
                    entity.GetComponent<ThrustComponent>().BackwardThrust = true;
                    return;
                }

                float angle = (float)Math.Atan2((nc.TargetLocation.Value.Y - pc.Position.Y), (nc.TargetLocation.Value.X - pc.Position.X)) + MathHelper.PiOver2;
                float angleDegrees = MathHelper.ToDegrees(angle);
                if (angleDegrees < 0)
                    angleDegrees += 360;

                RotationComponent rc = entity.GetComponent<RotationComponent>();

                float angleDiff = angleDegrees - rc.Angle;

                if (angleDiff > 180)
                    angleDiff -= 360;
                if (angleDiff < -180)
                    angleDiff += 360;

                if (angleDiff < -rc.RotationSpeed)
                {
                    entity.GetComponent<ThrustComponent>().ForwardThrust = false;
                    entity.GetComponent<ThrustComponent>().BackwardThrust = true;
                    rc.Angle -= rc.RotationSpeed;
                    if (rc.Angle < 0)
                        rc.Angle += 360;
                }
                else if (angleDiff > rc.RotationSpeed)
                {
                    entity.GetComponent<ThrustComponent>().ForwardThrust = false;
                    entity.GetComponent<ThrustComponent>().BackwardThrust = true;
                    rc.Angle += rc.RotationSpeed;
                    if (rc.Angle > 360)
                        rc.Angle -= 360;
                }
                else
                {
                    ThrustComponent tc = entity.GetComponent<ThrustComponent>();
                    InertiaComponent ic = entity.GetComponent<InertiaComponent>();
                    
                    float ticksToTarget = distanceToTarget / ic.Inertia.Length();
                    float ticksToStop = (float)Math.Log(ic.Inertia.Length() / tc.Deceleration) / tc.Deceleration;               

                    if (ticksToTarget > ticksToStop + 15)
                    {
                        tc.ForwardThrust = true;
                        tc.BackwardThrust = false;
                    }
                    else
                    {
                        tc.ForwardThrust = false;
                        tc.BackwardThrust = true;
                    }
                }
            }
            else
            {
                if (entity.GetComponent<InertiaComponent>().Inertia.Length() == 0)
                {
                    entity.GetComponent<ThrustComponent>().ForwardThrust = false;
                    entity.GetComponent<ThrustComponent>().BackwardThrust = false;
                }
            }
        }
    }
}
