using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Base.Component
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
        private MouseButton b = MouseButton.Left;
        private Vector2 c = Vector2.Zero;
        private Single delta = 0f;
        public MouseEventArgs(MouseButton mb, MouseState prev, MouseState cur)
        {
            this.b = mb;
            this.c = new Vector2(cur.X, cur.Y);
            this.delta = cur.ScrollWheelValue;
            this.prevM = prev;
            this.thisM = cur;
        }

        MouseState prevM, thisM;
        /// <summary>
        /// Предыдущее Состояние мыши
        /// </summary>
        public MouseState PrevState { get { return this.prevM; } }
        /// <summary>
        /// Текущее состояние мыши
        /// </summary>
        public MouseState CurrentState { get { return this.thisM; } }
        /// <summary>
        /// Состояние Колесико Мыши (Значение колесика)
        /// </summary>
        public Single DeltaScroll { get { return this.delta; } }
        /// <summary>
        /// Кнопка действия
        /// </summary>
        public MouseButton Button { get { return b; } }
        /// <summary>
        /// FLOAT : Координаты курсора
        /// </summary>
        public Vector2 Coord { get { return c; } }
        /// <summary>
        /// INT32 : Координаты курсора
        /// </summary>
        public Point PointCoord
        {
            get
            {
                return new Point((int)Math.Round(Coord.X, 0, MidpointRounding.AwayFromZero), (int)Math.Round(Coord.Y, 0, MidpointRounding.AwayFromZero));
            }
        }
    }
    public class KeyEventArgs : EventArgs
    {
        public Keys KeyCode { get; private set; }
        public KeyState KeyState { get; private set; }
        public String KeyChar { get; private set; }
        public GameTime GameTime { get; set; }
        public KeyEventArgs(Keys keyCode, KeyState keyState, GameTime gameTime, bool is_shift = false)
        {
            this.KeyCode = keyCode;
            this.KeyState = keyState;
            this.GameTime = gameTime;
            this.KeyChar = KeyEventArgs.KeysToString(this.KeyCode, is_shift);
        }
        internal static string KeysToString(Keys keyCode, bool shift = false)
        {
            string key = "";
            switch (keyCode)
            {
                //Alphabet keys
                case Keys.A: if (shift) { key = "A"; } else { key = "a"; } break;
                case Keys.B: if (shift) { key = "B"; } else { key = "b"; } break;
                case Keys.C: if (shift) { key = "C"; } else { key = "c"; } break;
                case Keys.D: if (shift) { key = "D"; } else { key = "d"; } break;
                case Keys.E: if (shift) { key = "E"; } else { key = "e"; } break;
                case Keys.F: if (shift) { key = "F"; } else { key = "f"; } break;
                case Keys.G: if (shift) { key = "G"; } else { key = "g"; } break;
                case Keys.H: if (shift) { key = "H"; } else { key = "h"; } break;
                case Keys.I: if (shift) { key = "I"; } else { key = "i"; } break;
                case Keys.J: if (shift) { key = "J"; } else { key = "j"; } break;
                case Keys.K: if (shift) { key = "K"; } else { key = "k"; } break;
                case Keys.L: if (shift) { key = "L"; } else { key = "l"; } break;
                case Keys.M: if (shift) { key = "M"; } else { key = "m"; } break;
                case Keys.N: if (shift) { key = "N"; } else { key = "n"; } break;
                case Keys.O: if (shift) { key = "O"; } else { key = "o"; } break;
                case Keys.P: if (shift) { key = "P"; } else { key = "p"; } break;
                case Keys.Q: if (shift) { key = "Q"; } else { key = "q"; } break;
                case Keys.R: if (shift) { key = "R"; } else { key = "r"; } break;
                case Keys.S: if (shift) { key = "S"; } else { key = "s"; } break;
                case Keys.T: if (shift) { key = "T"; } else { key = "t"; } break;
                case Keys.U: if (shift) { key = "U"; } else { key = "u"; } break;
                case Keys.V: if (shift) { key = "V"; } else { key = "v"; } break;
                case Keys.W: if (shift) { key = "W"; } else { key = "w"; } break;
                case Keys.X: if (shift) { key = "X"; } else { key = "x"; } break;
                case Keys.Y: if (shift) { key = "Y"; } else { key = "y"; } break;
                case Keys.Z: if (shift) { key = "Z"; } else { key = "z"; } break;

                //Decimal keys
                case Keys.D0: if (shift) { key = ")"; } else { key = "0"; } break;
                case Keys.D1: if (shift) { key = "!"; } else { key = "1"; } break;
                case Keys.D2: if (shift) { key = "@"; } else { key = "2"; } break;
                case Keys.D3: if (shift) { key = "#"; } else { key = "3"; } break;
                case Keys.D4: if (shift) { key = "$"; } else { key = "4"; } break;
                case Keys.D5: if (shift) { key = "%"; } else { key = "5"; } break;
                case Keys.D6: if (shift) { key = "^"; } else { key = "6"; } break;
                case Keys.D7: if (shift) { key = "&"; } else { key = "7"; } break;
                case Keys.D8: if (shift) { key = "*"; } else { key = "8"; } break;
                case Keys.D9: if (shift) { key = "("; } else { key = "9"; } break;

                //Decimal numpad keys
                case Keys.NumPad0: key = "0"; break;
                case Keys.NumPad1: key = "1"; break;
                case Keys.NumPad2: key = "2"; break;
                case Keys.NumPad3: key = "3"; break;
                case Keys.NumPad4: key = "4"; break;
                case Keys.NumPad5: key = "5"; break;
                case Keys.NumPad6: key = "6"; break;
                case Keys.NumPad7: key = "7"; break;
                case Keys.NumPad8: key = "8"; break;
                case Keys.NumPad9: key = "9"; break;

                //Special keys
                case Keys.OemTilde: if (shift) { key = "~"; } else { key = "`"; } break;
                case Keys.OemSemicolon: if (shift) { key = ":"; } else { key = ";"; } break;
                case Keys.OemQuotes: if (shift) { key = "\""; } else { key = "\'"; } break;
                case Keys.OemQuestion: if (shift) { key += '?'; } else { key += '/'; } break;
                case Keys.OemPlus: if (shift) { key += '+'; } else { key += '='; } break;
                case Keys.OemPipe: if (shift) { key += '|'; } else { key += '\\'; } break;
                case Keys.OemPeriod: if (shift) { key += '>'; } else { key += '.'; } break;
                case Keys.OemOpenBrackets: if (shift) { key += '{'; } else { key += '['; } break;
                case Keys.OemCloseBrackets: if (shift) { key += '}'; } else { key += ']'; } break;
                case Keys.OemMinus: if (shift) { key += '_'; } else { key += '-'; } break;
                case Keys.OemComma: if (shift) { key += '<'; } else { key += ','; } break;
                case Keys.Space: key = " "; break;
            }
            return key;
        }
    }
    public class TickEventArgs : EventArgs
    {
        public Graphics Graphics { get { return Window.Graphics2D; } }
        public IWindow Window { get; private set; }
        public GameTime GameTime { get; private set; }
        public ContentManager ContentManager { get { return this.Window.Content; } }

        public TickEventArgs(IWindow window, GameTime gameTime)
        {
            this.Window = window;
            this.GameTime = gameTime;
        }
    }
    public class GridEventArgs : EventArgs
    {
        public Control UtilizingControl { get; set; }
        public GridEventArgs(Control control)
        {
            this.UtilizingControl = control;
        }
    }

    public delegate void ControlEventHandler(Control sender, EventArgs e);
    public delegate void MouseEventHandler(Control sender, MouseEventArgs e);
    public delegate void KeyEventHandler(Control sender, KeyEventArgs e);
    public delegate void TickEventHandler(Control sendred, TickEventArgs e);
    public delegate void GridEventHandler(GridControls sender, GridEventArgs e);

    public class ISingelton<T> where T : class
    {
        private static T instance;
        protected ISingelton() { }
        public static T GetInstance
        {
            get
            {
                if (instance == null)
                {
                    System.Reflection.ConstructorInfo s = typeof(T).GetConstructor(new Type[] { });
                    instance = (T)s.Invoke(null);
                }
                return instance;
            }
        }
    }
}
