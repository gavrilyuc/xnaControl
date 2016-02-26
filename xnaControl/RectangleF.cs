using System;
using Microsoft.Xna.Framework;

namespace Core
{
    public struct RectangleF : IEquatable<RectangleF>, IEquatable<Rectangle>
    {
        private float _x, _y, _x2, _y2;
        private float _width, _height;

        public Rectangle ToRectangle() => new Rectangle((int)_x, (int)_y, (int)_width, (int)_height);

        public RectangleF(float pX, float pY, float pWidth, float pHeight)
        {
            _x = pX;
            _y = pY;
            _width = pWidth;
            _height = pHeight;
            _x2 = pX + pWidth;
            _y2 = pY + pHeight;
        }

        /// <summary>
        /// Содержит ли данная точка в этом прямоугольнике
        /// </summary>
        /// <param name="pPoint"></param>
        /// <returns></returns>
        public bool Contains(Vector2 pPoint) => (pPoint.X >= _x) && (pPoint.X <= _x2) && (pPoint.Y >= _y) && (pPoint.Y <= _y2);

        public RectangleF Union(RectangleF rect1, RectangleF rect2)
        {
            RectangleF tempRect = new RectangleF
            {
                _x = rect1._x < rect2._x ? rect1._x : rect2._x,
                _x2 = rect1._x2 > rect2._x2 ? rect1._x2 : rect2._x2
            };
            tempRect._width = tempRect._x2 - tempRect._x;
            tempRect._y = rect1._y < rect2._y ? rect1._y : rect2._y;
            tempRect._y2 = rect1._y2 > rect2._y2 ? rect1._y2 : rect2._y2;
            tempRect._height = tempRect._y2 - tempRect._y;
            return tempRect;
        }
        public float X
        {
            get { return _x; }
            set
            {
                _x = value;
                _x2 = _x + _width;
            }
        }
        public float Y
        {
            get { return _y; }
            set
            {
                _y = value;
                _y2 = _y + _height;
            }
        }
        public float Width
        {
            get { return _width; }
            set
            {
                _width = value;
                _x2 = _x + _width;
            }
        }
        public float Height
        {
            get { return _height; }
            set
            {
                _height = value;
                _y2 = _y + _height;
            }
        }
        public float Right => _x2;
        public float Bottom => _y2;
        public RectangleF Clone() => new RectangleF(X, Y, Width, Height);
        public bool Intersets(RectangleF r) => IsCollision(this, r);
        public bool Intersets(Rectangle r) => IsCollision(this, r.ConvertSingle());
        internal static bool IsCollision(RectangleF r2, RectangleF r1) => (r1.X + r1.Width >= r2.X && r1.Y + r1.Height >= r2.Y && r1.X <= r2.X + r2.Width && r1.Y <= r2.Y + r2.Height);
        public bool Equals(RectangleF other) => Math.Abs(other.X - X) < SingleTolerance && Math.Abs(other.Y - Y) < SingleTolerance &&
                                                Math.Abs(Width - other.Width) < SingleTolerance &&
                                                Math.Abs(Height - other.Height) < SingleTolerance;
        public bool Equals(Rectangle other) => Math.Abs(other.X - X) < SingleTolerance && Math.Abs(other.Y - Y) < SingleTolerance &&
                                               Math.Abs(Width - other.Width) < SingleTolerance &&
                                               Math.Abs(Height - other.Height) < SingleTolerance;
        public static RectangleF Empty => new RectangleF(0, 0, 0, 0);

        public const float SingleTolerance = 0.0002f;// fix floatings equals...
    }

}
