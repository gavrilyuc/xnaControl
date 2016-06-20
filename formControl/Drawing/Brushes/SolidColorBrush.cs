using FormControl.Component;
using Microsoft.Xna.Framework;

namespace FormControl.Drawing.Brushes
{
    /// <summary>
    /// Сплошная Заливка цветом
    /// </summary>
    public class SolidColorBrush : ColorBrush
    {
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
            AlgorithmDrawable(graphics, gameTime, new Rectangle(p.X, p.Y, 50, 50));
        }
        /// <summary>
        /// Алгоритм отрисовки кисти
        /// </summary>
        /// <param name="graphics"></param>
        /// <param name="gameTime"></param>
        /// <param name="rectangle"></param>
        public override void AlgorithmDrawable(Graphics graphics, GameTime gameTime, Rectangle rectangle)
        {
            graphics.FillRectangle(rectangle, Color);
        }
        /// <summary/>
        protected override Brush GetInctance => new SolidColorBrush(Color);

        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        /// <param name="color"></param>
        public SolidColorBrush(Color color) : base(color) { }
    }
}