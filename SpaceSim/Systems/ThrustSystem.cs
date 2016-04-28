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
    public class ThrustSystem : EntityProcessingSystem
    {
        public ThrustSystem()
            : base(Aspect.All(typeof(ThrustComponent), typeof(InertiaComponent)))
        {

        }

        public override void Process(Entity entity)
        {
            ThrustComponent tc = entity.GetComponent<ThrustComponent>();

            if (tc.ForwardThrust)
            {
                InertiaComponent ic = entity.GetComponent<InertiaComponent>();
                RotationComponent rc = entity.GetComponent<RotationComponent>();

                double angleInDegrees = MathHelper.ToRadians(rc.Angle);
                ic.Inertia.X += (float)Math.Sin(angleInDegrees) * 0.15f;
                ic.Inertia.Y += -(float)Math.Cos(angleInDegrees) * 0.15f;

                if (ic.Inertia.Length() > tc.MaxSpeed)
                {
                    ic.Inertia.Normalize();
                    ic.Inertia = Vector2.Multiply(ic.Inertia, tc.MaxSpeed);
                }
            }

            if (tc.BackwardThrust)
            {
                InertiaComponent ic = entity.GetComponent<InertiaComponent>();
                //RotationComponent rc = entity.GetComponent<RotationComponent>();

                ic.Inertia = Vector2.Multiply(ic.Inertia, 0.98f);

                if (ic.Inertia.Length() < 0.01f)
                {
                    ic.Inertia = Vector2.Multiply(ic.Inertia, 0);
                }
            }

            if (tc.LeftThrust)
            {
                InertiaComponent ic = entity.GetComponent<InertiaComponent>();
                RotationComponent rc = entity.GetComponent<RotationComponent>();

                double angleInDegrees = MathHelper.ToRadians(rc.Angle - 90); // 90 degrees anti-clockwise
                ic.Inertia.X += (float)Math.Sin(angleInDegrees) * 0.05f;
                ic.Inertia.Y += -(float)Math.Cos(angleInDegrees) * 0.05f;

                if (ic.Inertia.Length() > tc.MaxSpeed)
                {
                    ic.Inertia.Normalize();
                    ic.Inertia = Vector2.Multiply(ic.Inertia, tc.MaxSpeed);
                }
            }

            if (tc.RightThrust)
            {
                InertiaComponent ic = entity.GetComponent<InertiaComponent>();
                RotationComponent rc = entity.GetComponent<RotationComponent>();

                double angleInDegrees = MathHelper.ToRadians(rc.Angle + 90); // 90 degrees clockwise
                ic.Inertia.X += (float)Math.Sin(angleInDegrees) * 0.05f;
                ic.Inertia.Y += -(float)Math.Cos(angleInDegrees) * 0.05f;

                if (ic.Inertia.Length() > tc.MaxSpeed)
                {
                    ic.Inertia.Normalize();
                    ic.Inertia = Vector2.Multiply(ic.Inertia, tc.MaxSpeed);
                }
            }
        }
    }
}
