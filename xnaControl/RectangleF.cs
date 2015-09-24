using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core
{
    public struct RectangleF : IEquatable<RectangleF>, IEquatable<Rectangle>
    {
        private float _x, _y, _x2, _y2;
        private float _width, _height;

        public Rectangle toRectangle()
        {
            Rectangle myReturn = new Rectangle((int)_x, (int)_y, (int)_width, (int)_height);
            return myReturn;
        }

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
        public bool Contains(Vector2 pPoint)
        {
            if ((pPoint.X >= this._x) && (pPoint.X <= this._x2) && (pPoint.Y >= this._y) && (pPoint.Y <= this._y2)) return true;
            else return false;
        }
        public RectangleF Union(RectangleF rect1, RectangleF rect2)
        {
            RectangleF tempRect = new RectangleF();
            if (rect1._x < rect2._x) tempRect._x = rect1._x;
            else tempRect._x = rect2._x;
            if (rect1._x2 > rect2._x2) tempRect._x2 = rect1._x2;
            else tempRect._x2 = rect2._x2;
            tempRect._width = tempRect._x2 - tempRect._x;
            if (rect1._y < rect2._y) tempRect._y = rect1._y;
            else tempRect._y = rect2._y;
            if (rect1._y2 > rect2._y2) tempRect._y2 = rect1._y2;
            else tempRect._y2 = rect2._y2;
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
        public float Right
        {
            get { return _x2; }
        }
        public float Bottom
        {
            get { return _y2; }
        }
        public RectangleF Clone()
        {
            RectangleF myReturn = new RectangleF(X, Y, Width, Height);
            return myReturn;
        }
        public bool Intersets(RectangleF r)
        {
            return RectangleF.IsCollision(this, r);
        }
        public bool Intersets(Rectangle r)
        {
            return RectangleF.IsCollision(this, r.ConvertSingle());
        }
        internal static bool IsCollision(RectangleF r2, RectangleF r1)
        {
            bool myReturn = false;

            if ((r1.X + r1.Width >= r2.X && r1.Y + r1.Height >= r2.Y && r1.X <= r2.X + r2.Width && r1.Y <= r2.Y + r2.Height))
            {
                myReturn = true;
            }

            return myReturn;
        }

        public bool Equals(RectangleF other)
        {
            if (other.X == this.X && other.Y == this.Y && this.Width == other.Width && this.Height == other.Height) return true;
            return false;
        }
        public bool Equals(Rectangle other)
        {
            if (other.X == this.X && other.Y == this.Y && this.Width == other.Width && this.Height == other.Height) return true;
            return false;
        }
        public static RectangleF Empty
        {
            get
            {
                return new RectangleF(0, 0, 0, 0);
            }
        }
    }

}
