using FormControl.Component;
using Microsoft.Xna.Framework;

namespace FormControl.Drawing.Brushes
{
    /// <summary>
    /// Стандартная кисть рамки
    /// </summary>
    public class DefaultBorderBrush : BorderBrush, ICloneable<DefaultBorderBrush>
    {
        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        /// <param name="borderLength"></param>
        /// <param name="color"></param>
        public DefaultBorderBrush(int borderLength, Color color) : base(borderLength, color) {  }

        /// <summary></summary><returns></returns>
        protected override Brush GetInctance => Clone();
        /// <summary>
        /// Клонировать Объект
        /// </summary>
        /// <returns></returns>
        public new DefaultBorderBrush Clone() => new DefaultBorderBrush(BorderLenght, Color);
    }
}