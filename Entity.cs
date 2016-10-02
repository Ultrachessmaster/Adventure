using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adventure
{
    public class Entity {
	    public Texture2D Tex
        {
            get { return tex; }
            set { tex = value; }
        }
        private Texture2D tex;
        
        public Vector2 pos;
        public Vector2 scale = new Vector2(1, 1);
        public float rotation;

        protected bool visible = true;

        public Action<int> Update { get { return update; } }
        protected Action<int> update;

        public Action<SpriteBatch, int> Draw { get { return draw; } }
        protected Action<SpriteBatch, int> draw;

        protected void Drw (SpriteBatch sb, int pxlratio)
        {
            if(visible) sb.Draw(Tex, new Rectangle((int)Math.Round(pos.X) * pxlratio, (int)Math.Round(pos.Y) * pxlratio, Tex.Width * pxlratio, Tex.Height * pxlratio), Color.White);
        }
    }
}
