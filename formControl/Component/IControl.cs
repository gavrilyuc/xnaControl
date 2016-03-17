using FormControl.Component.Layout;
using Microsoft.Xna.Framework;

namespace FormControl.Component
{
    /// <summary>
    /// Представление контрола
    /// </summary>
    public interface IControl : ITransformation, IDrawablingTransformation
    {
        /// <summary>
        /// Имя контрола
        /// </summary>
        string Name { get; }
        /// <summary>
        /// Контейнер
        /// </summary>
        IControlLayout Controls { get; }
        /// <summary>
        /// Реагирует на события
        /// </summary>
        bool Enabled { get; }
        /// <summary>
        /// Рисовать контрол
        /// </summary>
        bool Visibled { get; }
        /// <summary>
        /// Родитель
        /// </summary>
        IControl Parent { get; }

        /// <summary>
        /// Задатие мыши на объекту
        /// </summary>
        event MouseEventHandler MouseDown;
        /// <summary>
        /// Отпускание мыши на объекту
        /// </summary>
        event MouseEventHandler MouseUp;
        /// <summary>
        /// Нажатие мыши на объекту
        /// </summary>
        event MouseEventHandler Click;
        /// <summary>
        /// Вызывается когда происходит зажатое движение мыши
        /// </summary>
        event MouseEventHandler MouseDrag;
        /// <summary>
        /// Движение мыши на объекту
        /// </summary>
        event MouseEventHandler MouseMove;
        /// <summary>
        /// Вхождение мыши в Объект
        /// </summary>
        event MouseEventHandler MouseInput;
        /// <summary>
        /// Выход мыши из Объекта
        /// </summary>
        event MouseEventHandler MouseLeave;
        /// <summary>
        /// Скроллинг мыши (Колесико)
        /// </summary>
        event MouseEventHandler ScrollDelta;
        /// <summary>
        /// Нажатие Кнопки
        /// </summary>
        event KeyEventHandler KeyDown;
        /// <summary>
        /// Отпускание Кнопки
        /// </summary>
        event KeyEventHandler KeyUp;
        /// <summary>
        /// Вызывается когда Кнопка зажата(и не отпускается)
        /// </summary>
        event KeyEventHandler KeyPresed;
        /// <summary>
        /// Вызывается вместе с методом Draw
        /// </summary>
        event TickEventHandler Paint;
        /// <summary>
        /// Вызывается вместе с Методом Update
        /// </summary>
        event TickEventHandler Invalidate;
        /// <summary>
        /// Вызывается когда изменяется размеры контрола
        /// </summary>
        event EventHandler ResizeControl;
        /// <summary>
        /// Вызывается когда изменяется позиция контрола
        /// </summary>
        event EventHandler LocationChangeControl;
        /// <summary>
        /// Вызывается когда изменяется контейнер у контрола
        /// </summary>
        event EventHandler ParentChanged;

    }

    /// <summary>
    /// Представление объекта, который умеет трансформироваться в пространстве
    /// </summary>
    public interface ITransformation
    {
        /// <summary>
        /// Позиция
        /// </summary>
        Vector2 Location { get; }
        /// <summary>
        /// Размеры
        /// </summary>
        Vector2 Size { get; }
        /// <summary>
        /// Прямоугольный срез.
        /// Внимание всегда X = Y = 0
        /// </summary>
        RectangleF ClientSize { get; }
    }

    /// <summary>
    /// Рисуемая трансформация
    /// </summary>
    public interface IDrawablingTransformation
    {
        /// <summary>
        /// Позиция
        /// </summary>
        Vector2 DrawabledLocation { get; }
        /// <summary>
        /// Рисуемый прямоугольник 
        /// </summary>
        Rectangle ClientRectangle { get; }
    }

    /// <summary>
    /// Представляет Объект который может сам себя клонировать
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ICloneable<out T> where T : class
    {
        /// <summary>
        /// Склонировать Объект
        /// </summary>
        /// <returns></returns>
        T Clone();
    }
}