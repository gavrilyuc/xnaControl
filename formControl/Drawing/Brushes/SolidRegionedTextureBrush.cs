using System;
using FormControl.Component;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FormControl.Drawing.Brushes
{
    /// <summary>
    /// Кисть которая рисует кусочек текстуры в обычном виде
    /// </summary>
    public class SolidRegionedTextureBrush : RegionedTextureBrush
    {
        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        /// <param name="texture"></param>
        /// <param name="region"></param>
        /// <param name="color"></param>
        public SolidRegionedTextureBrush(Texture2D texture, Rectangle region, Color color): base(texture, region, color)
        {
        }
        /// <summary/>
        protected override Brush GetInctance => new SolidRegionedTextureBrush(Texture, Region, Color);
        /// <summary/>
        public override void AlgorithmDrawable(Graphics graphics, GameTime gameTime, Rectangle rectangle)
        {
            graphics.Draw(Texture, rectangle, Region, Color);
        }
        /// <summary/>
        public override void AlgorithmDrawable(Graphics graphics, GameTime gameTime, Vector2 position)
        {
            graphics.Draw(Texture, position, Region, Color);
        }
        /// <summary/>
        public override void AlgorithmDrawable(Graphics graphics, GameTime gameTime, IDrawablingTransformation region)
        {
            graphics.Draw(Texture, region.ClientRectangle, Region, Color);
        }
    }
}