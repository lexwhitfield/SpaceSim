using Artemis;
using Microsoft.Xna.Framework;
using SpaceSim.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceSim
{
    public class Player
    {
        private static Player _instance;

        public static Player Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new Player();

                return _instance;
            }
        }

        public Player()
        {

        }

        public void SetScreen(GameScreen screen)
        {
            this._screen = screen;
        }

        private GameScreen _screen { get; set; }

        public string Name { get; set; }

        public Vector2 CurrentLocation { get { return Flagship.GetComponent<PositionComponent>().Position; } }

        public Vector2? TargetLocation { get { return Flagship.GetComponent<NavigationComponent>().TargetLocation; } }

        public Entity Flagship { get; set; }
    }
}
