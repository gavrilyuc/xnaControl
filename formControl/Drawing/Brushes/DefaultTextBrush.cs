using System;
using FormControl.Component;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FormControl.Drawing.Brushes
{
    /// <summary>
    /// Стандартная Текстовая кисть
    /// </summary>
    public class DefaultTextBrush : TextBrush, ICloneable<DefaultTextBrush>
    {
        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        /// <param name="font"></param>
        /// <param name="color"></param>
        public DefaultTextBrush(SpriteFont font, Color color) : base(font, color)
        {
            
        }

        /// <summary></summary><returns></returns>
        protected override Brush GetInctance => Clone();
        /// <summary>
        /// Клонировать Объект
        /// </summary>
        /// <returns></returns>
        public new DefaultTextBrush Clone() => new DefaultTextBrush(Font, Color) {
            Text = Text
        };
    }
}