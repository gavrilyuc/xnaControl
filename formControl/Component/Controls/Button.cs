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
        public Button() : this(new DefaultLayuout()) { }
        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        /// <param name="layout"></param>
        public Button(IControlLayout layout) : base(layout)
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
