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
    }

}