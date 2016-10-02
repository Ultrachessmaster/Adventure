using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Box2DX.Common;
using Box2DX.Dynamics;

namespace Adventure
{
    class Goblin : Entity
    {
        Body body;
        public Goblin (Vector2 pos)
        {
            this.pos = pos;
            Tex = Adventure.CM.Load<Texture2D>("goblin_sprites/goblin_down_spr_0");
            update = Upd;
            draw = Drw;

            BodyDef bd = new BodyDef();
            bd.Position.Set(pos.X / Adventure.physicsScale, pos.Y / Adventure.physicsScale);
            body = Adventure.Area.AddBody(bd, this);

            PolygonDef sd = new PolygonDef();
            sd.Density = 0f;
            sd.Friction = 0;
            sd.SetAsBox(1f, 1f);
            body.CreateFixture(sd);
            body.SetMassFromShapes();
        }

        void Upd (int i)
        {
            Vector2 goal = Player.Pos - pos;
            if(goal != Vector2.Zero)
                goal.Normalize();
            goal = goal / 1.2f;
            pos += goal;

        }
    }
}
