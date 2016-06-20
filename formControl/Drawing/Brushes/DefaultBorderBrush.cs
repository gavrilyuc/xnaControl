using System;
using FormControl.Component;
using Microsoft.Xna.Framework;

namespace FormControl.Drawing.Brushes
{
    /// <summary>
    /// Стандартная кисть рамки
    /// </summary>
    public class DefaultBorderBrush : BorderBrush
    {
        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        /// <param name="borderLength"></param>
        /// <param name="color"></param>
        public DefaultBorderBrush(int borderLength, Color color) : base(borderLength, color) {  }

        /// <summary></summary><returns></returns>
        protected override Brush GetInctance => new DefaultBorderBrush(BorderLenght, Color);

        /// <summary>
        /// Алгоритм отрисовки кисти
        /// </summary>
        /// <param name="graphics"></param>
        /// <param name="gameTime"></param>
        /// <param name="region"></param>
        public override void AlgorithmDrawable(Graphics graphics, GameTime gameTime, IDrawablingTransformation region)
        {
            AlgorithmDrawable(graphics, gameTime, region.ClientRectangle);
        }
        /// <summary>
        /// Алгоритм отрисовки кисти
        /// </summary>
        /// <param name="graphics"></param>
        /// <param name="gameTime"></param>
        /// <param name="position"></param>
        public override void AlgorithmDrawable(Graphics graphics, GameTime gameTime, Vector2 position)
        {
            Point p = position.ConvertToPoint();
            graphics.DrawRectangle(new Rectangle(p.X, p.Y, 50, 50), Color, BorderLenght);
        }
        /// <summary>
        /// Алгоритм отрисовки кисти
        /// </summary>
        /// <param name="graphics"></param>
        /// <param name="gameTime"></param>
        /// <param name="rectangle"></param>
        public override void AlgorithmDrawable(Graphics graphics, GameTime gameTime, Rectangle rectangle)
        {
            graphics.DrawRectangle(rectangle, Color, BorderLenght);
        }
    }
}