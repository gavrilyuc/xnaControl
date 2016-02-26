using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using System;
using Core.Base.Component;
using Core.Base.Component.Controls;

namespace Core
{
    public enum MouseButton
    {
        Left = 1,
        Midle = 2,
        Right = 3
    }

    public enum KeyState
    {
        KeyDown = 1,
        KeyUp = 2
    }

    public class MouseEventArgs : EventArgs
    {
        public MouseEventArgs(MouseButton mb, MouseState prev, MouseState cur)
        {
            Button = mb;
            Coord = new Vector2(cur.X, cur.Y);
            DeltaScroll = cur.ScrollWheelValue;
            PrevState = prev;
            CurrentState = cur;
        }

        /// <summary>
        /// Предыдущее Состояние мыши
        /// </summary>
        public MouseState PrevState { get; }

        /// <summary>
        /// Текущее состояние мыши
        /// </summary>
        public MouseState CurrentState { get; }

        /// <summary>
        /// Состояние Колесико Мыши (Значение колесика)
        /// </summary>
        public Single DeltaScroll { get; }

        /// <summary>
        /// Кнопка действия
        /// </summary>
        public MouseButton Button { get; } = MouseButton.Left;

        /// <summary>
        /// FLOAT : Координаты курсора
        /// </summary>
        public Vector2 Coord { get; }

        /// <summary>
        /// INT32 : Координаты курсора
        /// </summary>
        public Point PointCoord => new Point((int)Math.Round(Coord.X, 0, MidpointRounding.AwayFromZero), (int)Math.Round(Coord.Y, 0, MidpointRounding.AwayFromZero));
    }

    public class KeyEventArgs : EventArgs
    {
        public Keys KeyCode { get; }
        public KeyState KeyState { get; private set; }
        public string KeyChar { get; private set; }
        public GameTime GameTime { get; set; }
        public KeyEventArgs(Keys keyCode, KeyState keyState, GameTime gameTime, bool is_shift = false)
        {
            KeyCode = keyCode;
            KeyState = keyState;
            GameTime = gameTime;
            KeyChar = KeysToString(this.KeyCode, is_shift);
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
                default: key = ' '; break;
            }
            return key.ToString();
        }
    }
    public class TickEventArgs : EventArgs
    {
        public Graphics Graphics => Window.Graphics2D;
        public IWindow Window { get; }
        public GameTime GameTime { get; private set; }
        public ContentManager ContentManager => Window.Content;

        public TickEventArgs(IWindow window, GameTime gameTime)
        {
            Window = window;
            GameTime = gameTime;
        }
    }
    public class GridEventArgs : EventArgs
    {
        public Control UtilizingControl { get; set; }
        public GridEventArgs(Control control)
        {
            UtilizingControl = control;
        }
    }

    public delegate void ControlEventHandler(Control sender, EventArgs e);
    public delegate void MouseEventHandler(Control sender, MouseEventArgs e);
    public delegate void KeyEventHandler(Control sender, KeyEventArgs e);
    public delegate void TickEventHandler(Control sendred, TickEventArgs e);
    public delegate void GridEventHandler(GridControls sender, GridEventArgs e);
}
