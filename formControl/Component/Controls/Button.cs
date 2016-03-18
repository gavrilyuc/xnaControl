using FormControl.Component.Layout;
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
        public Button(TextBrush brush) : this(brush, new DefaultLayuout()) { }
        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        /// <param name="brush"></param>
        /// <param name="layout"></param>
        public Button(TextBrush brush, IControlLayout layout) : base(brush, layout)
        {
            AutoSize = false;
            Paint += Button_Paint;
        }

        private void Button_Paint(Control sender, TickEventArgs e)
        {
            TextBrush?.AlgorithmDrawable(e.Graphics, e.GameTime, this);
        }
    }
}
