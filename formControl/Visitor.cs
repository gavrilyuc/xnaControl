using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Xna.Framework.Input;

[assembly: CLSCompliant(true)]
namespace FormControl
{
    /// <summary></summary>
    public static class Visitor
    {
        #region Key Convertor
        private struct CharKey
        {
            public char SimShift { get; }
            public char Sim { get; }

            public CharKey(char a, char b)
            {
                SimShift = a;
                Sim = b;
            }
        }
        private static readonly Dictionary<Keys, CharKey> CharsKeys = new Dictionary<Keys, CharKey>() {
            { Keys.A, new CharKey('A', 'a') }, { Keys.B, new CharKey('B', 'b') }, { Keys.C, new CharKey('C', 'c') },
            { Keys.D, new CharKey('D', 'd') }, { Keys.E, new CharKey('E', 'e') }, { Keys.F, new CharKey('F', 'f') },
            { Keys.G, new CharKey('G', 'g') }, { Keys.H, new CharKey('H', 'h') }, { Keys.I, new CharKey('I', 'i') },
            { Keys.J, new CharKey('J', 'j') }, { Keys.K, new CharKey('K', 'k') }, { Keys.L, new CharKey('L', 'l') },
            { Keys.M, new CharKey('M', 'm') }, { Keys.N, new CharKey('N', 'n') }, { Keys.O, new CharKey('O', 'o') },
            { Keys.P, new CharKey('P', 'p') }, { Keys.Q, new CharKey('Q', 'q') }, { Keys.R, new CharKey('R', 'r') },
            { Keys.S, new CharKey('S', 's') }, { Keys.T, new CharKey('T', 't') }, { Keys.U, new CharKey('U', 'u') },
            { Keys.V, new CharKey('V', 'v') }, { Keys.W, new CharKey('W', 'w') }, { Keys.X, new CharKey('X', 'x') },
            { Keys.Y, new CharKey('Y', 'y') }, { Keys.Z, new CharKey('Z', 'z') }, { Keys.D0, new CharKey(')', '0') },
            { Keys.D1, new CharKey('!', '1') }, { Keys.D2, new CharKey('@', '2') }, { Keys.D3, new CharKey('#', '3') },
            { Keys.D4, new CharKey('$', '4') }, { Keys.D5, new CharKey('%', '5') }, { Keys.D6, new CharKey('^', '6') },
            { Keys.D7, new CharKey('&', '7') }, { Keys.D8, new CharKey('*', '8') }, { Keys.D9, new CharKey('(', '9') },
            { Keys.NumPad0, new CharKey('0', '0') }, { Keys.NumPad1, new CharKey('1', '1') }, { Keys.NumPad2, new CharKey('2', '2') },
            { Keys.NumPad3, new CharKey('3', '3') }, { Keys.NumPad4, new CharKey('4', '4') }, { Keys.NumPad5, new CharKey('5', '5') },
            { Keys.NumPad6, new CharKey('6', '6') }, { Keys.NumPad7, new CharKey('7', '7') }, { Keys.NumPad8, new CharKey('8', '8') },
            { Keys.NumPad9, new CharKey('9', '9') }, { Keys.OemTilde, new CharKey('~', '`') }, { Keys.OemSemicolon, new CharKey(',', ';') },
            { Keys.OemQuotes, new CharKey('"', '\'') }, { Keys.OemQuestion, new CharKey('?', '/') }, { Keys.OemPlus, new CharKey('+', '=') },
            { Keys.OemPipe, new CharKey('|', '\\') }, { Keys.OemPeriod, new CharKey('>', '.') }, { Keys.OemOpenBrackets, new CharKey('{', '[') },
            { Keys.OemCloseBrackets, new CharKey('}', ']') }, { Keys.OemMinus, new CharKey('_', '-') }, { Keys.OemComma, new CharKey('<', ',') },
            { Keys.Decimal, new CharKey('.', '.') }, { Keys.Enter, new CharKey('\n', '\n') }, { Keys.Space, new CharKey(' ', ' ') }
        };
        /// <summary>
        /// Преобразовать Код Символа в Символьный вид.
        /// </summary>
        /// <param name="keyCode"></param>
        /// <param name="shift"></param>
        /// <returns></returns>
        public static string ToStringChar(this Keys keyCode, bool shift = false)
        {
            if (CharsKeys.ContainsKey(keyCode)) return shift ? CharsKeys[keyCode].SimShift.ToString() : CharsKeys[keyCode].Sim.ToString();
            return string.Empty;
        }
        #endregion

        /// <summary>
        /// Преобразовать Float в Int Существо
        /// </summary>
        /// <returns></returns>
        public static int ToInt(this float value) => (int)Math.Round(value, MidpointRounding.AwayFromZero);

        /// <summary>
        /// Преобразовать в Float Сущность
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        public static RectangleF ConvertSingle(this Rectangle a) => new RectangleF(a.X, a.Y, a.Width, a.Height);
        /// <summary>
        /// Преобразовать в Float Сущность
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        public static Vector2 ConvertToVector(this Point a) => new Vector2(a.X, a.Y);
        /// <summary>
        /// Центр
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        public static Vector2 Center(this Point a) => new Vector2(a.X / 2f, a.Y / 2f);
        /// <summary>
        /// Центр
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        public static Vector2 Center(this Vector2 a) => new Vector2(a.X / 2, a.Y / 2);
        /// <summary>
        /// Преобразовать в Int Сущность
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        public static Point ConvertToPoint(this Vector2 a) => new Point((int)Math.Round(a.X, 0, MidpointRounding.AwayFromZero), (int)Math.Round(a.Y, 0, MidpointRounding.AwayFromZero));
        /// <summary>
        /// Максимальная отклонение, Float которое считается как 0.
        /// </summary>
        public const float SingleTolerance = 0.0000001f;// fix floatings equals...

        /// <summary>
        /// Коректная проверка float
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool CorrectEquals(this float a, float b)
        {
            return Math.Abs(a - b) <= SingleTolerance;
        }

        /// <summary>
        /// Коректная проверка double типа
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool CorrectEquals(this double a, double b)
        {
            return Math.Abs(a - b) <= SingleTolerance;
        }
        /// <summary>
        /// Провести поиск по массиву
        /// </summary>
        /// <param name="keys"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static int IndexOfOnArray(this Keys[] keys, Keys key)
        {
            if (keys.Length == 0) return -1;

            int i = 0;
            for (; i < keys.Length; i++) if (keys[i] == key) return i;
            return -1;
        }
        /// <summary></summary><param name="s"></param><param name="p"></param><returns></returns>
        public static string DefaultFormat(this string s, params object[] p)
        {
            return string.Format(CultureInfo.InvariantCulture, s, p);
        }
    }
}
