using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core
{
    public static class IVisitor
    {
        public static RectangleF ConvertSingle(this Rectangle a)
        {
            return new RectangleF(a.X, a.Y, a.Width, a.Height);
        }
        public static Vector2 ConvertToVector(this Point a)
        {
            return new Vector2(a.X, a.Y);
        }
        public static Vector2 Center(this Point a)
        {
            return new Vector2(a.X / 2, a.Y / 2);
        }
        public static Vector2 Center(this Vector2 a)
        {
            return new Vector2(a.X / 2, a.Y / 2);
        }
        public static Point ConvertToPoint(this Vector2 a)
        {
            return new Point((int)Math.Round(a.X, 0, MidpointRounding.AwayFromZero), (int)Math.Round(a.Y, 0, MidpointRounding.AwayFromZero));
        }
    }
}
