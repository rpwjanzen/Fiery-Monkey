using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;

namespace FieryMonkey {
    public class IfsGame : Microsoft.Xna.Framework.Game {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        IfsCalculator flame;
        Texture2D texture;
        bool iterative = true;

        Ifs ifs;
        int maxRenderedPoints = 9200000;
        int pointsRendered = 0;
        int pointsPerUpdate = 2500;
        int tuneStep = 10;

        int avgMax;

        Random random;

        SpriteFont font;

        public IfsGame() {
            avgMax = pointsPerUpdate;
            int width = 16 * 50;
            int height = 9 * 50;
            int superSampling = 1;

            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = width;
            graphics.PreferredBackBufferHeight = height;            
            Content.RootDirectory = "Content";

            int imageWidth = width;
            int imageHeight = height;

            random = new Random();
            ifs = Generator.Generate(random);
            flame = new IfsCalculator(imageWidth * superSampling, imageHeight * superSampling, random, ifs);
            TargetElapsedTime = new TimeSpan(0, 0, 0, 0, 250);

            this.IsFixedTimeStep = true;
            IsMouseVisible = true;
            graphics.IsFullScreen = true;            
        }

        protected override void Initialize() {
            base.Initialize();
        }

        protected override void LoadContent() {
            font = Content.Load<SpriteFont>("DefaultFont");
            spriteBatch = new SpriteBatch(GraphicsDevice);
            if (!iterative) {
                flame.Solve(maxRenderedPoints);                
            }
            texture = flame.CreateTexture(GraphicsDevice);
        }

        KeyboardState lastKs;
        protected override void Update(GameTime gameTime) {            
            if (iterative && pointsRendered < maxRenderedPoints) {
                if (gameTime.IsRunningSlowly == false) {
                    pointsPerUpdate += tuneStep;
                    flame.Solve(pointsPerUpdate);
                    pointsRendered += pointsPerUpdate;
                } else {
                    pointsPerUpdate = (int)(pointsPerUpdate * 0.9);
                    pointsPerUpdate = Math.Max(pointsPerUpdate, tuneStep);
                }
            }

            if (iterative && shouldSetTexture && gameTime.IsRunningSlowly == false) {
                flame.SetTexture(texture);
                shouldSetTexture = false;
            }

            var ks = Keyboard.GetState();
            if ((ks.IsKeyDown(Keys.Space) && lastKs.IsKeyUp(Keys.Space))
                || pointsRendered >= maxRenderedPoints) {
                GenerateIfs();
                if (!iterative) {
                    flame.Solve(maxRenderedPoints);
                    flame.SetTexture(texture);
                }
            }
            lastKs = ks;

            base.Update(gameTime);
        }

        void GenerateIfs() {
            ifs = Generator.Generate(random);
            flame.SetIfs(ifs);
            pointsRendered = 0;
        }

        bool shouldSetTexture = true;
        protected override void Draw(GameTime gameTime) {
                GraphicsDevice.Clear(Color.Black);

                spriteBatch.Begin();
                spriteBatch.Draw(texture, Vector2.Zero, null, Color.White, 0.0f, Vector2.Zero, 1f, SpriteEffects.FlipVertically, 1.0f);

                spriteBatch.DrawString(font, pointsPerUpdate.ToString(), Vector2.Zero, Color.White);
                spriteBatch.End();

                shouldSetTexture = true;

            base.Draw(gameTime);
        }
    }
}
