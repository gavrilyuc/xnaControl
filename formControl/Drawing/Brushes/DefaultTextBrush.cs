using System;
using FormControl.Component;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FormControl.Drawing.Brushes
{
    /// <summary>
    /// Стандартная Текстовая кисть
    /// </summary>
    public class DefaultTextBrush : TextBrush
    {
        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        /// <param name="font"></param>
        /// <param name="color"></param>
        public DefaultTextBrush(SpriteFont font, Color color) : base(font, color)
        {
            
        }
        /// <summary>
        /// Алгоритм отрисовки кисти
        /// </summary>
        /// <param name="graphics"></param>
        /// <param name="gameTime"></param>
        /// <param name="region"></param>
        public override void AlgorithmDrawable(Graphics graphics, GameTime gameTime, IDrawablingTransformation region)
        {
            AlgorithmDrawable(graphics, gameTime, region.DrawabledLocation);
        }
        /// <summary>
        /// Алгоритм отрисовки кисти
        /// </summary>
        /// <param name="graphics"></param>
        /// <param name="gameTime"></param>
        /// <param name="position"></param>
        public override void AlgorithmDrawable(Graphics graphics, GameTime gameTime, Vector2 position)
        {
            if (Font != null && Text != null)
                graphics.DrawString(Font, Text, position, Color);

        }
        /// <summary>
        /// Алгоритм отрисовки кисти
        /// </summary>
        /// <param name="graphics"></param>
        /// <param name="gameTime"></param>
        /// <param name="rectangle"></param>
        public override void AlgorithmDrawable(Graphics graphics, GameTime gameTime, Rectangle rectangle)
        {
            AlgorithmDrawable(graphics, gameTime, rectangle.Location.ConvertToVector());
        }


        /// <summary/>
        protected override Brush GetInctance => new DefaultTextBrush(Font, Color)
        {
            Text = Text
        };
    }
}