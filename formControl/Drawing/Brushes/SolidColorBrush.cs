using FormControl.Component;
using Microsoft.Xna.Framework;

namespace FormControl.Drawing.Brushes
{
    /// <summary>
    /// Сплошная Заливка цветом
    /// </summary>
    public class SolidColorBrush : ColorBrush, ICloneable<SolidColorBrush>
    {
        /// <summary>
        /// Алгоритм отрисовки кисти
        /// </summary>
        /// <param name="graphics"></param>
        /// <param name="gameTime"></param>
        /// <param name="region"></param>
        public override void AlgorithmDrawable(Graphics graphics, GameTime gameTime, IDrawablingTransformation region)
        {
            graphics.FillRectangle(region.ClientRectangle, Color);
        }

        /// <summary></summary><returns></returns>
        protected override Brush GetInctance() => Clone();

        /// <summary>
        /// Клонировать Объект
        /// </summary>
        /// <returns></returns>
        public new SolidColorBrush Clone() => new SolidColorBrush(Color);
        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        /// <param name="color"></param>
        public SolidColorBrush(Color color) : base(color) { }
    }
}