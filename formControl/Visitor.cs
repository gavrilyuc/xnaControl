using Microsoft.Xna.Framework;
using System;
using Microsoft.Xna.Framework.Input;
namespace FormControl
{
    /// <summary></summary>
    public static class Visitor
    {
        /// <summary>
        /// Преобразовать Float в Int Существо
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        public static int ToInt(this float a) => (int)Math.Round(a, MidpointRounding.AwayFromZero);

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
            int i = 0;
            for (; i < keys.Length; i++) if (keys[i] == key) return i;
            return -1;
        }
    }
}
