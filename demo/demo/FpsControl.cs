using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace demo
{
    public class FpsControl
    {
        public float AverageFramesPerSecond { get; private set; }
        public float CurrentFramesPerSecond { get; private set; }
        public const int MaximumSamples = 100;
        private static readonly Queue<float> SampleBuffer = new Queue<float>();

        public SpriteFont Font;
        public FpsControl(SpriteFont font) { Font = font; }

        public void Update(GameTime gameTime)
        {
            CurrentFramesPerSecond = 1.0f / (float)gameTime.ElapsedGameTime.TotalSeconds;

            SampleBuffer.Enqueue(CurrentFramesPerSecond);

            if (SampleBuffer.Count > MaximumSamples)
            {
                SampleBuffer.Dequeue();
                AverageFramesPerSecond = SampleBuffer.Average(i => i);
            }
            else
            {
                AverageFramesPerSecond = CurrentFramesPerSecond;
            }
        }
        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            Update(gameTime);
            spriteBatch.DrawString(Font, $"FPS Sredniu: {AverageFramesPerSecond}\nCUR FPS: {CurrentFramesPerSecond}", Vector2.Zero, Color.Red);
        }
    }
}