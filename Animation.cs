using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adventure
{
    class Animation
    {
        Texture2D[] texs;
        int idx = 0;
        public bool loop;
        float speedinseconds;

        public Animation (Texture2D[] texs, bool loop, float speedinseconds)
        {
            this.texs = texs;
            this.loop = loop;
            this.speedinseconds = speedinseconds;
            Update(0);
        }

        public Texture2D SetTexture ()
        {
            return texs[idx];
        }
        void Update(int i)
        {
            if(loop) { idx = (idx + 1) % texs.Length; }
            else { idx = Math.Min(idx + 1, texs.Length - 1); }
            Timer timer = new Timer(Update, speedinseconds);
        }
    }
}
