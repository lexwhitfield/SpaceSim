using Artemis;
using Artemis.Interface;
using GameLibrary.Core;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceSim.Components
{
    public class TextureComponent : IComponent
    {
        public Coord Position { get; set; }
        public string Texture { get; set; }
        public Vector2 Origin { get; set; }

        public TextureComponent(Coord p, string t, Vector2 origin)
        {
            this.Position = p;
            this.Texture = t;
            this.Origin = origin;
        }
    }
}
