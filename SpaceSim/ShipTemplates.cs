using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceSim
{
    public class ShipTemplate
    {
        public string Name { get; private set; }
        public int Type { get; private set; }
        public string TypeName { get { return ShipConstants.ShipTypes[this.Type]; } }
    }

    public static class ShipConstants
    {
        public static Dictionary<int, string> ShipTypes { get; set; }
    }

    public struct ShipType
    {
        public string Name { get; private set; }
    }
}
