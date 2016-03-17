using Microsoft.Xna.Framework;

namespace FormControl.Drawing
{
    /// <summary>
    /// Цветовая Кисть
    /// </summary>
    public abstract class ColorBrush : Brush
    {
        /// <summary>
        /// Цвет
        /// </summary>
        public Color Color { get; set; }
        /// <summary>
        /// конструктор по умолчанию
        /// </summary>
        /// <param name="color"></param>
        protected ColorBrush(Color color)
        {
            Color = color;
        }
    }
}