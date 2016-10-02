using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Box2DX.Collision;
using Box2DX.Dynamics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adventure
{
    class Player : Entity {
	
	    PlayStates playSt = PlayStates.MOVE;
        static Player inst;
        
        Animation anim;
        Body body;

        Texture2D text;
        public Player () {
		    update = Upd;
            draw = Drw;

            //Animation
            text = Adventure.CM.Load<Texture2D>("color_sprites/green_spr_0");
            int animlength = 4;
            var texs = new Texture2D[animlength];
            for(int i = 0; i < texs.Length; i++)
            {
                texs[i] = Adventure.CM.Load<Texture2D>("player_sprites/player_down_still_spr_" + i.ToString());
            }
            anim = new Animation(texs, true, 0.01f);

            BodyDef bd = new BodyDef();
            bd.Position.Set(pos.X / Adventure.tilesize, pos.Y / Adventure.tilesize);
            body = Adventure.Area.AddBody(bd, this);

            PolygonDef sd = new PolygonDef();
            sd.Density = 1f;
            sd.Friction = 0;
            float size = 1f / 2f;
            sd.SetAsBox(size, size);
            body.CreateFixture(sd);
            body.SetMassFromShapes();

            inst = this;
        }

        void Upd(int num) {

            switch (playSt) {
            case PlayStates.MOVE: Move(); break;
            case PlayStates.PAUSE: break;
            case PlayStates.STOP: break; //Todo: Make sure animation still plays in stop, but not pause
        
            }
            Tex = anim.SetTexture();
            Tex = text;
        }

        void Move() {
            Vector2 vel = new Vector2(0, 0);
            if (Input.IsKeyPressed(Keys.Right)) vel.X++;
            if (Input.IsKeyPressed(Keys.Left)) vel.X--;
            if (Input.IsKeyPressed(Keys.Up)) vel.Y--;
            if (Input.IsKeyPressed(Keys.Down)) vel.Y++;
            body.SetLinearVelocity(vel);
            //pos += vel;
        }

        public static Vector2 Pos { get { return inst.pos; } }
    }   
}
