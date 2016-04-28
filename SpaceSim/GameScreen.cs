using Artemis;
using Artemis.System;
using FarseerPhysics.Dynamics;
using GameLibrary.Core;
using GameLibrary.Networks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SpaceSim.Components;
using SpaceSim.Systems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceSim
{
    public class GameScreen : Screen
    {
       
        

        private double frameRate = 0;
        private int frameCounter = 0;
        private TimeSpan elapsedTime = TimeSpan.Zero;

        private EntityWorld _entityEngine;
        private int playerEntityID;

        private World _physicsEngine;

        public GameScreen(Game game, GraphicsDeviceManager gdm)
            : base(game, gdm)
        {

        }

        public override void Activate()
        {
            this.Active = true;
        }

        public override void Deactivate()
        {
            this.Active = false;
        }

        public override void Initialise()
        {
            
            this._camera = new BoundedCamera(new Vector2(_gdm.GraphicsDevice.Viewport.Width, _gdm.GraphicsDevice.Viewport.Height),
                new Vector2(0, 0),
                new PointPair(new Vector2i(-10000, -10000), new Vector2i(10000, 10000)));

            this._entityEngine = new EntityWorld();
            this._physicsEngine = new World(new Microsoft.Xna.Framework.Vector2(0, 0));
           
            EntitySystem.BlackBoard.SetEntry<BoundedCamera>("BoundedCamera", this._camera);
            EntitySystem.BlackBoard.SetEntry<World>("PhysicsEngine", this._physicsEngine);

            this._entityEngine.InitializeAll(true);

            // ENTITIES
            Entity planet = this._entityEngine.CreateEntity();
            planet.Group = "PLANETS";
            planet.Tag = "PLANET1";
            planet.AddComponent(new PositionComponent(new Vector2(100, 100)));
            planet.AddComponent(new RotationComponent(50, 0f, false));
            planet.AddComponent(new TextureComponent(new Coord(0, 0), "blue1", new Vector2(550, 550)));
            //planet.AddComponent(new ShadowComponent(new Coord(1, 0), "blue1", new Vector2(266, 266)));

            Entity playerShip = this._entityEngine.CreateEntity();
            playerShip.Group = "PLAYER";
            playerShip.Tag = "PLAYER";
            playerShip.AddComponent(new PositionComponent(new Vector2(50, 50)));
            playerShip.AddComponent(new RotationComponent(0, 2f, false));
            playerShip.AddComponent(new TextureComponent(new Coord(0, 0), "ships", new Vector2(64, 64)));
            playerShip.AddComponent(new InertiaComponent());
            playerShip.AddComponent(new PlayerControlComponent());
            playerShip.AddComponent(new ThrustComponent());
            this.playerEntityID = playerShip.Id;

            Entity enemyShip = this._entityEngine.CreateEntity();
            enemyShip.Group = "TARGETS";
            enemyShip.Tag = "ENEMY1";
            enemyShip.AddComponent(new PositionComponent(new Vector2(-100, -100)));
            enemyShip.AddComponent(new RotationComponent(0, 2f, false));
            enemyShip.AddComponent(new TextureComponent(new Coord(1, 0), "ships", new Vector2(64, 64)));
            enemyShip.AddComponent(new InertiaComponent());
            //enemyShip.AddComponent(new PlayerControlComponent());
            enemyShip.AddComponent(new ThrustComponent());            

            this.Active = true;
            this.Initialised = true;
        }

        public override void LoadContent()
        {
            ResourceManager.Instance.LoadSpriteSheet("planets", "planets", 532, 532);
            ResourceManager.Instance.LoadSpriteSheet("ships", "ships", 128, 128);
            ResourceManager.Instance.LoadSpriteSheet("lasers", "lasers", 9, 37);
            ResourceManager.Instance.LoadSpriteSheet("blue1", "blue1", 1100, 1100);
            ResourceManager.Instance.LoadTexture("starfield0", "starfield0");
            ResourceManager.Instance.LoadTexture("starfield2", "starfield2");

            ResourceManager.Instance.LoadFont("font", "font");
        }

        public override void UnloadContent()
        {
            throw new NotImplementedException();
        }

        public override void HandleInput()
        {
            InputManager.Instance.Update();

            if (this.Active)
            {
                if (InputManager.Instance.isKeyDown(Keys.Escape))
                    ScreenManager.Instance.Push(new MenuScreen(_game, _gdm));

                if (InputManager.Instance.MouseScrollWheelDelta < 0)
                    _camera.AdjustZoom(-0.1f);
                if (InputManager.Instance.MouseScrollWheelDelta > 0)
                    _camera.AdjustZoom(0.1f);
            }
        }

        public override void Update(GameTime gameTime)
        {
            HandleInput();

            this._entityEngine.Update();

            _camera.Position = _entityEngine.GetEntity(playerEntityID).GetComponent<PositionComponent>().Position;

            elapsedTime += gameTime.ElapsedGameTime;

            if (elapsedTime > TimeSpan.FromSeconds(1))
            {
                elapsedTime -= TimeSpan.FromSeconds(1);
                frameRate = frameCounter;
                frameCounter = 0;
            }
        }

        public override void Draw(GameTime gameTime)
        {
            _gdm.GraphicsDevice.Clear(Color.FromNonPremultiplied(5, 5, 5, 255));

            this._entityEngine.Draw();

#if DEBUG
            ScreenManager.Instance.SpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, null);

            frameCounter++;
            ScreenManager.Instance.SpriteBatch.DrawString(ResourceManager.Instance.GetFont("font"), string.Format("Active entities: {0}", _entityEngine.EntityManager.ActiveEntities.Count), new Vector2(10, 10), Color.LightGray);
            ScreenManager.Instance.SpriteBatch.DrawString(ResourceManager.Instance.GetFont("font"), string.Format("Memory used: {0}MB", Math.Round((double)GC.GetTotalMemory(false) / 1048576, 2)), new Vector2(10, 30), Color.LightGray);
            ScreenManager.Instance.SpriteBatch.DrawString(ResourceManager.Instance.GetFont("font"), string.Format("Frames per second: {0}", frameRate), new Vector2(10, 50), Color.LightGray);
            ScreenManager.Instance.SpriteBatch.DrawString(ResourceManager.Instance.GetFont("font"), string.Format("Mouse Position: {0},{1}", InputManager.Instance.CurrentMousePosition.X, InputManager.Instance.CurrentMousePosition.Y), new Vector2(10, 70), Color.LightGray);

            ScreenManager.Instance.SpriteBatch.End();
#endif
        }
    }
}
