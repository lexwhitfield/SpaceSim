using GameLibrary.Core;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceSim
{
    public class HUD
    {
        private static HUD _instance;

        public static HUD Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new HUD();
                return _instance;
            }
        }

        public Point ScreenSize { get; set; }

        public Queue<string> Log { get; set; }

        public int LogLength { get; set; }

        public Dictionary<string, string> Metrics { get; set; }

        public HUD()
        {
            this.Log = new Queue<string>();
            this.Metrics = new Dictionary<string, string>();

            this.LogLength = 10;
        }

        public void AddUpdateMetric(string key, string value)
        {
            if (this.Metrics.ContainsKey(key))
                this.Metrics[key] = value;
            else
                this.Metrics.Add(key, value);
        }

        public void AddLog(string value)
        {
            this.Log.Enqueue(value);

            if (this.Log.Count > 10)
            {
                this.Log.Dequeue();
            }
        }

        public void Draw()
        {
            for (int i = 0; i < this.Metrics.Count; i++)
            {
                ScreenManager.Instance.SpriteBatch.DrawString(ResourceManager.Instance.GetFont("font"), this.Metrics.ElementAt(i).Value, new Vector2(10, (i * 20) + 10), Color.LightGray);
            }

            for (int i = 0; i < this.Log.Count; i++)
            {
                ScreenManager.Instance.SpriteBatch.DrawString(ResourceManager.Instance.GetFont("font"), this.Log.ElementAt(i), new Vector2(10, this.ScreenSize.Y - 20 - (i * 20)), Color.LightGray);
            }            
        }
    }
}
