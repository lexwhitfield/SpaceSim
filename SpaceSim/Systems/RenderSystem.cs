using Artemis;
using Artemis.Attributes;
using Artemis.Manager;
using Artemis.System;
using GameLibrary.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpaceSim.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceSim.Systems
{
    [ArtemisEntitySystem(ExecutionType = ExecutionType.Synchronous, GameLoopType = GameLoopType.Draw, Layer = 1)]
    public class RenderSystem : EntityProcessingSystem
    {
        private BoundedCamera _boundedCamera { get { return EntitySystem.BlackBoard.GetEntry<BoundedCamera>("BoundedCamera"); } }

        public RenderSystem()
            : base(Aspect.All(typeof(TextureComponent), typeof(PositionComponent)))
        {

        }

        protected override void ProcessEntities(IDictionary<int, Entity> entities)
        {
            // draw background
            ScreenManager.Instance.SpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.LinearWrap, null, null, null, this._boundedCamera.TranslationMatrix);

            //ScreenManager.Instance.SpriteBatch.Draw(ResourceManager.Instance.GetTexture("starfield0"), new Rectangle((int)(this._boundedCamera.Bounds.Point1.X), (int)(this._boundedCamera.Bounds.Point1.Y), (this._boundedCamera.Bounds.Point2.X - this._boundedCamera.Bounds.Point1.X), (this._boundedCamera.Bounds.Point2.Y - this._boundedCamera.Bounds.Point1.Y)), Color.White);
            //ScreenManager.Instance.SpriteBatch.Draw(ResourceManager.Instance.GetTexture("starfield2"), new Rectangle((int)(this._boundedCamera.Position.X * 0.4f), (int)(this._boundedCamera.Position.Y * 0.4f), 1228, 1228), Color.White);
            ScreenManager.Instance.SpriteBatch.Draw(ResourceManager.Instance.GetTexture("starfield2"), new Vector2(this._boundedCamera.Bounds.Point1.X, this._boundedCamera.Bounds.Point1.Y), new Rectangle(0, 0, 1228 * 20, 1228 * 20), Color.White);

            ScreenManager.Instance.SpriteBatch.End();

            ScreenManager.Instance.SpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, this._boundedCamera.TranslationMatrix);

            foreach (Entity entity in entities.Values)
            {
                Process(entity);
            }

            ScreenManager.Instance.SpriteBatch.End();
        }

        public override void Process(Entity entity)
        {
            TextureComponent tc = entity.GetComponent<TextureComponent>();
            PositionComponent pc = entity.GetComponent<PositionComponent>();
            RotationComponent rc = (entity.HasComponent<RotationComponent>()) ? entity.GetComponent<RotationComponent>() : new RotationComponent(0, 0, false);
            ShadowComponent sc = (entity.HasComponent<ShadowComponent>()) ? entity.GetComponent<ShadowComponent>() : null;

            Rectangle sourceRect = ResourceManager.Instance.GetSpriteSheet(tc.Texture).GetSourceRect(tc.Position.X, tc.Position.Y);
            Rectangle destRect = new Rectangle((int)pc.Position.X, (int)pc.Position.Y, sourceRect.Width, sourceRect.Height);
            Texture2D texture = ResourceManager.Instance.GetSpriteSheet(tc.Texture).Texture;

            ScreenManager.Instance.SpriteBatch.Draw(texture, destRect, sourceRect, Color.White, MathHelper.ToRadians(rc.Angle), tc.Origin, SpriteEffects.None, 0f);

            if (sc != null)
            {
                Rectangle shadowSourceRect = ResourceManager.Instance.GetSpriteSheet(sc.Texture).GetSourceRect(sc.Position.X, sc.Position.Y);
                Texture2D shadowTexture = ResourceManager.Instance.GetSpriteSheet(sc.Texture).Texture;
                ScreenManager.Instance.SpriteBatch.Draw(shadowTexture, destRect, shadowSourceRect, Color.White, sc.Angle, sc.Origin, SpriteEffects.None, 0f);
            }
        }
    }
}
