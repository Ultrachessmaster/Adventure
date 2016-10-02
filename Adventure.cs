using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using Box2DX.Collision;
using Box2DX.Common;
using Box2DX.Dynamics;
using System;
using Microsoft.Xna.Framework.Content;

namespace Adventure
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Adventure : Game
    {
        /*Graphics*/
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        public static ContentManager CM;
        /*Graphics*/

        Player player;
        List<Entity> entities = new List<Entity>();
        Tile[,] tilemap = new Tile[100,100];
        Texture2D texatlas;
        static public Area Area { get { return area; } }
        static Area area = new Area(0);

        /*Statics*/
        public static List<Timer> Timers = new List<Timer>();
        /*Statics*/

        /*Constants*/
        public static int pxlratio = 2;
        public const int tilesize = 32;
        public const float physicsScale = 0.1f;
        /*Constants*/
        public Adventure()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferHeight = 620;
            graphics.PreferredBackBufferWidth = 1080;
            Content.RootDirectory = "Content";
            CM = Content;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            player = new Player();
            Goblin g = new Goblin(new Vector2(5 * tilesize, 5 * tilesize));
            entities.Add(g);
            entities.Add(player);
            Texture2D bs = Content.Load<Texture2D>("grass_spr_0");
            texatlas = Content.Load<Texture2D>("thing_tileset");
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            //if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            //    Exit();
            for (int i = 0; i < Timers.Count; i++)
            {
                var tim = Timers[i];
                tim.AddTime(gameTime.ElapsedGameTime.Milliseconds / 1000f);
            }
            for (int i = 0; i < entities.Count; i++) {
                var ent = entities[i];
                if (ent.Update != null)
                    ent.Update.Invoke(i);
            }
            area.Update(gameTime);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Microsoft.Xna.Framework.Color.CornflowerBlue);
            var dm = GraphicsDevice.DisplayMode;
            spriteBatch.Begin(samplerState: SamplerState.PointClamp, blendState: BlendState.NonPremultiplied);
            int[,] tilemap = area.Tiles;
            for (int i = 0; i < tilemap.GetUpperBound(0) + 1; i++)
            {
                for (int j = 0; j < tilemap.GetUpperBound(1) + 1; j++)
                {
                    if (tilemap[i, j] == -1) continue;
                    Rectangle destrect = new Rectangle(i * tilesize * pxlratio, j * tilesize * pxlratio, tilesize * 2, tilesize * 2);
                    Rectangle sourcerect = new Rectangle(tilemap[i, j] * tilesize, 0, tilesize, tilesize); //TODO: Properly get coordinates for tile in tex atlas, rather than just putting in place holder values
                    spriteBatch.Draw(texatlas, destrect, sourcerect, Microsoft.Xna.Framework.Color.White);
                }
            }
            for (int i = 0; i < entities.Count; i++) {
                entities[i].Draw.Invoke(spriteBatch, pxlratio);
            }
            spriteBatch.End();
            
            base.Draw(gameTime);
        }
    }
}
