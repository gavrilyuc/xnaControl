using FormControl.Component;
using Microsoft.Xna.Framework;

namespace FormControl.Drawing
{
    /// <summary>
    /// Базовая кисть для отрисовки Рамки
    /// </summary>
    public abstract class BorderBrush : ColorBrush
    {
        /// <summary>
        /// Размер рамки
        /// </summary>
        public int BorderLenght { get; }
        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        /// <param name="len"></param>
        /// <param name="color"></param>
        protected BorderBrush(int len, Color color) : base(color) { BorderLenght = len; }

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