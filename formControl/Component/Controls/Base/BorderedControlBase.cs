using FormControl.Drawing;

namespace FormControl.Component.Controls.Base
{
    /// <summary>
    /// Контрол который имеет Рамку и задний фон
    /// </summary>
    public abstract class BorderedControlBase : Control
    {
        /// <summary>
        /// Кисть для отрисовки Рамки.
        /// </summary>
        public BorderBrush BorderBrush { get; set; }
        /// <summary>
        /// Задняя часть Контрола
        /// </summary>
        public Brush Background { get; set; }

        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        protected BorderedControlBase()
        {
            Paint += Panel_Paint;
        }

        private void Panel_Paint(Control sender, TickEventArgs e)
        {
            Background?.AlgorithmDrawable(e.Graphics, e.GameTime, this);
            BorderBrush?.AlgorithmDrawable(e.Graphics, e.GameTime, this);
        }
    }

}