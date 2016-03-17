using System;
using Microsoft.Xna.Framework;

namespace FormControl
{
    /// <summary>
    /// Прямоугольник
    /// </summary>
    public struct RectangleF : IEquatable<RectangleF>, IEquatable<Rectangle>
    {
        private float _x, _y;
        private float _width, _height;
        /// <summary>
        /// Преобразовать в интовую сущность
        /// </summary>
        /// <returns></returns>
        public Rectangle ToRectangle() => new Rectangle((int)_x, (int)_y, (int)_width, (int)_height);
        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        /// <param name="pX"></param>
        /// <param name="pY"></param>
        /// <param name="pWidth"></param>
        /// <param name="pHeight"></param>
        public RectangleF(float pX = 0f, float pY = 0f, float pWidth = 1f, float pHeight = 1f)
        {
            _x = pX;
            _y = pY;
            _width = pWidth;
            _height = pHeight;
            Right = pX + pWidth;
            Bottom = pY + pHeight;
        }

        /// <summary>
        /// Содержит ли данная точка в этом прямоугольнике
        /// </summary>
        /// <param name="pPoint"></param>
        /// <returns></returns>
        public bool Contains(Vector2 pPoint) => (pPoint.X >= _x) && (pPoint.X <= Right) && (pPoint.Y >= _y) && (pPoint.Y <= Bottom);
        /// <summary>
        /// Содержит ли данная точка в этом прямоугольнике
        /// </summary>
        /// <param name="pPoint"></param>
        /// <returns></returns>
        public bool Contains(Point pPoint) => (pPoint.X >= _x) && (pPoint.X <= Right) && (pPoint.Y >= _y) && (pPoint.Y <= Bottom);
        /// <summary>
        /// Соединить оба прямоугольника
        /// </summary>
        /// <param name="rect1"></param>
        /// <param name="rect2"></param>
        /// <returns></returns>
        public RectangleF Union(RectangleF rect1, RectangleF rect2)
        {
            RectangleF tempRect = new RectangleF
            {
                _x = rect1._x < rect2._x ? rect1._x : rect2._x,
                Right = rect1.Right > rect2.Right ? rect1.Right : rect2.Right
            };
            tempRect._width = tempRect.Right - tempRect._x;
            tempRect._y = rect1._y < rect2._y ? rect1._y : rect2._y;
            tempRect.Bottom = rect1.Bottom > rect2.Bottom ? rect1.Bottom : rect2.Bottom;
            tempRect._height = tempRect.Bottom - tempRect._y;
            return tempRect;
        }
        /// <summary>
        /// X Координата
        /// </summary>
        public float X
        {
            get { return _x; }
            set
            {
                _x = value;
                Right = _x + _width;
            }
        }
        /// <summary>
        /// Y Координата
        /// </summary>
        public float Y
        {
            get { return _y; }
            set
            {
                _y = value;
                Bottom = _y + _height;
            }
        }
        /// <summary>
        /// Ширина
        /// </summary>
        public float Width
        {
            get { return _width; }
            set
            {
                _width = value;
                Right = _x + _width;
            }
        }
        /// <summary>
        /// Высота
        /// </summary>
        public float Height
        {
            get { return _height; }
            set
            {
                _height = value;
                Bottom = _y + _height;
            }
        }
        /// <summary>
        /// Правый угол
        /// </summary>
        public float Right { get; private set; }
        /// <summary>
        /// Нижний угол
        /// </summary>
        public float Bottom { get; private set; }
        /// <summary>
        /// Клонировать объект
        /// </summary>
        /// <returns></returns>
        public RectangleF Clone() => new RectangleF(X, Y, Width, Height);
        /// <summary>
        /// Ли входит данный объект в другой
        /// </summary>
        /// <param name="r"></param>
        /// <returns></returns>
        public bool Intersets(RectangleF r) => IsCollision(this, r);
        /// <summary>
        /// Ли входит данный объект в другой
        /// </summary>
        /// <param name="r"></param>
        /// <returns></returns>
        public bool Intersets(Rectangle r) => IsCollision(this, r.ConvertSingle());

        private static bool IsCollision(RectangleF r2, RectangleF r1) => (r1.X + r1.Width >= r2.X && r1.Y + r1.Height >= r2.Y && r1.X <= r2.X + r2.Width && r1.Y <= r2.Y + r2.Height);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(RectangleF other) => other.X.CorrectEquals(X) && other.Y.CorrectEquals(Y) &&
                                                other.Width.CorrectEquals(Width) && other.Height.CorrectEquals(Height);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(Rectangle other) => X.CorrectEquals(other.X) && Y.CorrectEquals(other.Y) &&
                                                Width.CorrectEquals(other.Width) && Height.CorrectEquals(other.Height);
        /// <summary>
        /// Пустой прямоугольник
        /// </summary>
        public static RectangleF Empty => new RectangleF();
    }

}
