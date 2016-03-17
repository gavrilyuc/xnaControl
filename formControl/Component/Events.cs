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
        internal MouseEventArgs() {
            
        }
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
        public string KeyChar { get; internal set; }
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
            KeyChar = KeysToString(KeyCode, isShift);
        }
        internal static string KeysToString(Keys keyCode, bool shift = false)
        {
            char key;
            switch (keyCode)
            {
                //Alphabet keys
                case Keys.A: key = shift ? 'A' : 'a'; break;
                case Keys.B: key = shift ? 'B' : 'b'; break;
                case Keys.C: key = shift ? 'C' : 'c'; break;
                case Keys.D: key = shift ? 'D' : 'd'; break;
                case Keys.E: key = shift ? 'E' : 'e'; break;
                case Keys.F: key = shift ? 'F' : 'f'; break;
                case Keys.G: key = shift ? 'G' : 'g'; break;
                case Keys.H: key = shift ? 'H' : 'h'; break;
                case Keys.I: key = shift ? 'I' : 'i'; break;
                case Keys.J: key = shift ? 'J' : 'j'; break;
                case Keys.K: key = shift ? 'K' : 'k'; break;
                case Keys.L: key = shift ? 'L' : 'l'; break;
                case Keys.M: key = shift ? 'M' : 'm'; break;
                case Keys.N: key = shift ? 'N' : 'n'; break;
                case Keys.O: key = shift ? 'O' : 'o'; break;
                case Keys.P: key = shift ? 'P' : 'p'; break;
                case Keys.Q: key = shift ? 'Q' : 'q'; break;
                case Keys.R: key = shift ? 'R' : 'r'; break;
                case Keys.S: key = shift ? 'S' : 's'; break;
                case Keys.T: key = shift ? 'T' : 't'; break;
                case Keys.U: key = shift ? 'U' : 'u'; break;
                case Keys.V: key = shift ? 'V' : 'v'; break;
                case Keys.W: key = shift ? 'W' : 'w'; break;
                case Keys.X: key = shift ? 'X' : 'x'; break;
                case Keys.Y: key = shift ? 'Y' : 'y'; break;
                case Keys.Z: key = shift ? 'Z' : 'z'; break;

                //Decimal keys
                case Keys.D0: key = shift ? ')' : '0'; break;
                case Keys.D1: key = shift ? '!' : '1'; break;
                case Keys.D2: key = shift ? '@' : '2'; break;
                case Keys.D3: key = shift ? '#' : '3'; break;
                case Keys.D4: key = shift ? '$' : '4'; break;
                case Keys.D5: key = shift ? '%' : '5'; break;
                case Keys.D6: key = shift ? '^' : '6'; break;
                case Keys.D7: key = shift ? '&' : '7'; break;
                case Keys.D8: key = shift ? '*' : '8'; break;
                case Keys.D9: key = shift ? '(' : '9'; break;

                //Decimal numpad keys
                case Keys.NumPad0: key = '0'; break;
                case Keys.NumPad1: key = '1'; break;
                case Keys.NumPad2: key = '2'; break;
                case Keys.NumPad3: key = '3'; break;
                case Keys.NumPad4: key = '4'; break;
                case Keys.NumPad5: key = '5'; break;
                case Keys.NumPad6: key = '6'; break;
                case Keys.NumPad7: key = '7'; break;
                case Keys.NumPad8: key = '8'; break;
                case Keys.NumPad9: key = '9'; break;

                //Special keys
                case Keys.OemTilde: key = shift ? '~' : '`'; break;
                case Keys.OemSemicolon: key = shift ? ':' : ';'; break;
                case Keys.OemQuotes: key = shift ? '"' : '\''; break;
                case Keys.OemQuestion: key = shift ? '?' : '/'; break;
                case Keys.OemPlus: key = shift ? '+' : '='; break;
                case Keys.OemPipe: key = shift ? '|' : '\\'; break;
                case Keys.OemPeriod: key = shift ? '>' : '.'; break;
                case Keys.OemOpenBrackets: key = shift ? '{' : '['; break;
                case Keys.OemCloseBrackets: key = shift ? '}' : ']'; break;
                case Keys.OemMinus: key = shift ? '_' : '-'; break;
                case Keys.OemComma: key = shift ? '<' : ','; break;
                case Keys.Decimal: key = '.'; break;
                case Keys.Enter: key = '\n'; break;
                case Keys.Space: key = ' '; break;

                default: return "";
            }
            return key.ToString();
        }
        internal void SetKeyShiftingChar(bool isShift = false)
        {
            KeyChar = KeysToString(KeyCode, isShift);
        }
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
