using System.ComponentModel;
using FormControl.Component.Layout;
using FormControl.Drawing;

namespace FormControl.Component.Controls
{
    /// <summary>
    /// Контрол который имеет Рамку и задний фон
    /// </summary>
    public abstract class BorderedControlBase : Control
    {
        private BorderBrush _border;
        private Brush _background;

        /// <summary>
        /// Кисть для отрисовки Рамки.
        /// </summary>
        [Category(PropertyGridCategoriesText.GraphicsCategory)] public BorderBrush Border
        {
            get { return _border; }
            set
            {
                _border = value;
                BorderChanged?.Invoke(this);
            }
        }

        /// <summary>
        /// Задняя часть Контрола
        /// </summary>
        [Category(PropertyGridCategoriesText.GraphicsCategory)] public Brush Background
        {
            get { return _background; }
            set
            {
                _background = value;
                BackgroundChanged?.Invoke(this);
            }
        }

        /// <summary>
        /// Вызывается тогда когда изменяется свойство Border
        /// </summary>
        public event EventHandler BorderChanged;
        /// <summary>
        /// Вызывается тогда когда изменяется свойство Background
        /// </summary>
        public event EventHandler BackgroundChanged;

        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        protected BorderedControlBase() : this(new DefaultLayuout()) { }
        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        protected BorderedControlBase(IControlLayout layout) : base(layout)
        {
            Paint += Panel_Paint;
        }

        private void Panel_Paint(Control sender, TickEventArgs e)
        {
            Background?.AlgorithmDrawable(e.Graphics, e.GameTime, this);
            Border?.AlgorithmDrawable(e.Graphics, e.GameTime, this);
        }
    }

}