using FormControl.Component.Controls.Base;
using FormControl.Drawing;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FormControl.Component.Controls
{
    /// <summary>
    /// Представление Кнопки
    /// </summary>
    public class Button : TextControlBase
    {
        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        /// <param name="brush"></param>
        public Button(TextBrush brush) : base(brush)
        {
            ColorText = Color.Black;
            AutoSize = false;
        }
    }
}
