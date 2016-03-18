using System;
using FormControl.Component.Controls;
using FormControl.Component.Layout;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace FormControl.Component
{
    /// <summary>
    /// Тип кнопки мыши
    /// </summary>
    public enum MouseButton
    {
        /// <summary>
        /// Null
        /// </summary>
        None=0,
        /// <summary>
        /// Левая кнопка
        /// </summary>
        Left = 1,
        /// <summary>
        /// Центральна кнопка (нажатие колесика)
        /// </summary>
        Midle = 2,
        /// <summary>
        /// правая кнопка
        /// </summary>
        Right = 3
    }

    /// <summary>
    /// Состояние кнопки
    /// </summary>
    public enum KeyState
    {
        /// <summary>
        /// Null
        /// </summary>
        None = 0,
        /// <summary>
        /// Нажатая кнопка
        /// </summary>
        KeyDown = 1,
        /// <summary>
        /// Отпущеная кнопка
        /// </summary>
        KeyUp = 2
    }

    /// <summary>
    /// Представляет структуру параметров для событий мышки
    /// </summary>
    public class MouseEventArgs
    {
        internal MouseEventArgs() { }
        /// <summary>
        /// конструктор по умолчанию
        /// </summary>
        /// <param name="mb"></param>
        /// <param name="prev"></param>
        /// <param name="cur"></param>
        /// <param name="gameTime"></param>
        public MouseEventArgs(MouseButton mb, MouseState prev, MouseState cur, GameTime gameTime)
        {
            Button = mb;
            Coord = new Vector2(cur.X, cur.Y);
            DeltaScroll = cur.ScrollWheelValue;
            PrevState = prev;
            CurrentState = cur;
            GameTime = gameTime;
        }
        /// <summary>
        /// Игровое время которое прошло с момента последнего обновления кадра
        /// </summary>
        public GameTime GameTime { get; internal set; }
        /// <summary>
        /// Предыдущее Состояние мыши
        /// </summary>
        public MouseState PrevState { get; internal set; }
        /// <summary>
        /// Текущее состояние мыши
        /// </summary>
        public MouseState CurrentState { get; internal set; }
        /// <summary>
        /// Состояние Колесико Мыши (Значение колесика)
        /// </summary>
        public float DeltaScroll { get; internal set; }
        /// <summary>
        /// Кнопка действия
        /// </summary>
        public MouseButton Button { get; internal set; }
        /// <summary>
        /// FLOAT : Координаты курсора
        /// </summary>
        public Vector2 Coord { get; internal set; }
        /// <summary>
        /// INT32 : Координаты курсора
        /// </summary>
        public Point PointCoord => new Point((int)Math.Round(Coord.X, 0, MidpointRounding.AwayFromZero), (int)Math.Round(Coord.Y, 0, MidpointRounding.AwayFromZero));
    }

    /// <summary>
    /// Представляет структуру параметров для событий клавиатуры
    /// </summary>
    public class KeyEventArgs
    {
        internal KeyEventArgs() { }
        /// <summary>
        /// нажатая клавиша
        /// </summary>
        public Keys KeyCode { get; internal set; }
        /// <summary>
        /// Состояние клавишы
        /// </summary>
        public KeyState KeyState { get; internal set; }
        /// <summary>
        /// клавиша в строковом виде
        /// </summary>
        public string KeyChar => KeyCode.ToStringChar(IsShift);
        /// <summary>
        /// Игровое время которое прошло с момента последнего обновления кадра
        /// </summary>
        public GameTime GameTime { get; internal set; }
        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        /// <param name="keyCode"></param>
        /// <param name="keyState"></param>
        /// <param name="gameTime"></param>
        /// <param name="isShift"></param>
        public KeyEventArgs(Keys keyCode, KeyState keyState, GameTime gameTime, bool isShift = false)
        {
            KeyCode = keyCode;
            KeyState = keyState;
            GameTime = gameTime;
            IsShift = isShift;
        }
        internal bool IsShift { get; set; }
    }

    /// <summary>
    /// Представляет структуру параметров для событий обновления
    /// </summary>
    public class TickEventArgs
    {
        /// <summary>
        /// Объект графики
        /// </summary>
        public Graphics Graphics => Window.Graphics2D;
        /// <summary>
        /// Окно в котором происходит обновление
        /// </summary>
        public IWindow Window { get; }
        /// <summary>
        /// Игровое время которое прошло с момента последнего обновления кадра
        /// </summary>
        public GameTime GameTime { get; internal set; }
        /// <summary>
        /// Менеджер контента
        /// </summary>
        public ContentManager ContentManager => Window.Content;
        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        /// <param name="window"></param>
        /// <param name="gameTime"></param>
        public TickEventArgs(IWindow window, GameTime gameTime)
        {
            Window = window;
            GameTime = gameTime;
        }
    }

    /// <summary>
    /// Делегат событий контрола
    /// </summary>
    /// <param name="sender"></param>
    public delegate void EventHandler(Control sender);
    /// <summary>
    /// Делегат событий мыши на контроле
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public delegate void MouseEventHandler(Control sender, MouseEventArgs e);
    /// <summary>
    /// Делегат событий клавиатуры на контроле
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public delegate void KeyEventHandler(Control sender, KeyEventArgs e);
    /// <summary>
    /// Делегат событий обновления на контроле
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public delegate void TickEventHandler(Control sender, TickEventArgs e);
    /// <summary>
    /// Делегат событий контейнера контролов
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="utilizingControl"></param>
    public delegate void GridEventHandler(DefaultLayuout sender, Control utilizingControl);
}
