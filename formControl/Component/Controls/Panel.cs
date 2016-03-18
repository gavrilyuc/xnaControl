using FormControl.Component.Layout;
namespace FormControl.Component.Controls
{
    /// <summary>
    /// Панель, Обычный Контейнер для контролов.
    /// </summary>
    public class Panel : BorderedControlBase
    {
        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        public Panel() : this(new DefaultLayuout()) { }
        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        public Panel(IControlLayout layout) : base(layout) { }

    }
}
