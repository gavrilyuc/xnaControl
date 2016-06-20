using FormControl.Component;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FormControl.Drawing.Brushes
{
    /// <summary>
    /// Кисть для рисования текстуры
    /// </summary>
    public class SolidTextureBrush : TextureBrush
    {
        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        /// <param name="texture"></param>
        /// <param name="color"></param>
        public SolidTextureBrush(Texture2D texture, Color color) : base(texture, color) { }
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
            AlgorithmDrawable(graphics, gameTime, new Rectangle(p.X, p.Y, Texture.Width, Texture.Height));
        }
        /// <summary>
        /// Алгоритм отрисовки кисти
        /// </summary>
        /// <param name="graphics"></param>
        /// <param name="gameTime"></param>
        /// <param name="rectangle"></param>
        public override void AlgorithmDrawable(Graphics graphics, GameTime gameTime, Rectangle rectangle)
        {
            if (Texture != null)
                graphics.Draw(Texture, rectangle, Color);
        }
        /// <summary/>
        protected override Brush GetInctance => new SolidTextureBrush(Texture, Color);
    }
}