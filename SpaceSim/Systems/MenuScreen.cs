using GameLibrary.Core;
using GameLibrary.Networks;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceSim.Systems
{
    public class MenuScreen : Screen
    {
        public MenuScreen(Game game, GraphicsDeviceManager gdm)
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
                new PointPair(new Vector2i(0, 0), new Vector2i(0, 0)));

            // TODO: rest of init

            this.Active = true;
            this.Initialised = true;
        }

        public override void LoadContent()
        {
            if (ResourceManager.Instance.GetFont("font") == null)
                ResourceManager.Instance.LoadFont("font", "font");
        }

        public override void UnloadContent()
        {
            throw new NotImplementedException();
        }

        public override void HandleInput()
        {
            InputManager.Instance.Update();
        }

        public override void Update(GameTime gameTime)
        {
            HandleInput();
        }

        public override void Draw(GameTime gameTime)
        {
            _gdm.GraphicsDevice.Clear(Color.CornflowerBlue);
        }
    }
}
