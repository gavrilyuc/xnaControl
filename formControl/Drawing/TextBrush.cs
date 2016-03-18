using FormControl.Component;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FormControl.Drawing
{
    /// <summary>
    /// Кисть для рисования Текста
    /// </summary>
    public abstract class TextBrush : ColorBrush
    {
        /// <summary>
        /// Шрифт
        /// </summary>
        public SpriteFont Font { get; }
        /// <summary>
        /// Рисуемый текст
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="font"></param>
        /// <param name="color"></param>
        protected TextBrush(SpriteFont font, Color color) : base(color) { Font = font; }
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
    }

}