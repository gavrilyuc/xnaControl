using Microsoft.Xna.Framework;
using System;
namespace Core
{
    public static class Visitor
    {
        public static RectangleF ConvertSingle(this Rectangle a) => new RectangleF(a.X, a.Y, a.Width, a.Height);
        public static Vector2 ConvertToVector(this Point a) => new Vector2(a.X, a.Y);
        public static Vector2 Center(this Point a) => new Vector2(a.X / 2f, a.Y / 2f);
        public static Vector2 Center(this Vector2 a) => new Vector2(a.X / 2, a.Y / 2);
        public static Point ConvertToPoint(this Vector2 a) => new Point((int)Math.Round(a.X, 0, MidpointRounding.AwayFromZero), (int)Math.Round(a.Y, 0, MidpointRounding.AwayFromZero));

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
        /// Коректная проверка float типа float.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool CorrectEquals(this double a, double b)
        {
            return Math.Abs(a - b) <= SingleTolerance;
        }
    }
}
