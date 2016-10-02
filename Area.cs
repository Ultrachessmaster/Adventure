using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using System.Linq;
using System.Text;
using System.IO;
using Box2DX.Collision;
using Box2DX.Common;
using Box2DX.Dynamics;
using System.Threading.Tasks;

namespace Adventure
{
    public class Area
    {
        public int[,] Tiles { get { return tiles; } } 
        int[,] tiles;
        List<Entity> entities = new List<Entity>();
        Dictionary<Entity, Body> bodies = new Dictionary<Entity, Body>();
        World world;
        public Area (int id)
        {
            GetTilemap(id);
            MakePhysicsWorld();
        }

        private void MakePhysicsWorld()
        {
            AABB ab = new AABB();
            ab.UpperBound = new Vec2(10000f, 10000f);
            ab.LowerBound = new Vec2(-10000f, -10000f);
            world = new World(ab, new Vec2(0f, 0f), true);
        }

        private void GetTilemap(int id)
        {
            string directory = Directory.GetCurrentDirectory() + @"\Content\Area" + id + ".txt";
            string[] atext = File.ReadAllLines(directory);
            tiles = new int[int.Parse(atext[0].Remove(0, 9)), int.Parse(atext[1].Remove(0, 9))];
            for (int i = 6; i < atext.Length - 1; i++)
            {
                var line = atext[i];
                //line.Remove(line.Length - 2, 1);
                var lineoftiles = atext[i].Split(',');
                for (int j = 0; j < lineoftiles.Length - 1; j++)
                {
                    tiles[j, i - 6] = int.Parse(lineoftiles[j]);
                }
            }
        }

        public Body AddBody(BodyDef bodydef, Entity e)
        {
            Body body = world.CreateBody(bodydef);
            bodies[e] = body;
            return body;
        }

        public void Update(GameTime gt)
        {
            world.Step(gt.ElapsedGameTime.Milliseconds / 1000f, 1, 1);
            for(int i = 0; i < bodies.Count; i++)
            {
                var bodarray = bodies.ToArray();
                bodarray[i].Key.pos += bodarray[i].Value.GetLinearVelocity() * Adventure.physicsScale;
                Vector2 pos = bodarray[i].Value.GetPosition();
                int foo = 0;
            }
        }
    }
}
