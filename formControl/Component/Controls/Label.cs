using FormControl.Component.Layout;
using FormControl.Drawing;

namespace FormControl.Component.Controls
{
    /// <summary>
    /// Текстовый Блок
    /// </summary>
    public class Label : TextControlBase
    {
        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        public Label() : this(new DefaultLayuout()) { }
        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        /// <param name="layout"></param>
        public Label(IControlLayout layout) : base(layout) { Paint += Label_Paint; }

        private void Label_Paint(Control sender, TickEventArgs e)
        {
            TextBrush?.AlgorithmDrawable(e.Graphics, e.GameTime, this);
        }
    }
}
