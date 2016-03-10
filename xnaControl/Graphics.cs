using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Core
{
    /// <summary>
    /// Базовый Класс Отрисовки.
    /// </summary>
    public class BasicGraphics : SpriteBatch
    {
        public Texture2D PixelTexture { get; protected set; }

        public BasicGraphics(GraphicsDevice graphicsDevice) : base(graphicsDevice)
        {
            PixelTexture = new Texture2D(GraphicsDevice, 1, 1);
            PixelTexture.SetData(new[] { Color.White });
        }
        
        #region Drawable Methods
        /// <summary>
        /// Draw a rectangle.
        /// </summary>
        /// <param name="rectangle">The rectangle to draw.</param>
        /// <param name="color">The draw color.</param>
        /// <param name="border">Size Border</param>
        /// <param name="layerDetpch">The Layer Deptch.</param>
        public void DrawRectangle(Rectangle rectangle, Color color, int border, float layerDetpch)
        {
            this.Draw(PixelTexture, new Rectangle(rectangle.Left, rectangle.Top, rectangle.Width, border), null, color, 0f, Vector2.Zero, SpriteEffects.None, layerDetpch);
            this.Draw(PixelTexture, new Rectangle(rectangle.Left, rectangle.Bottom, rectangle.Width, border), null, color, 0f, Vector2.Zero, SpriteEffects.None, layerDetpch);
            this.Draw(PixelTexture, new Rectangle(rectangle.Left, rectangle.Top, border, rectangle.Height), null, color, 0f, Vector2.Zero, SpriteEffects.None, layerDetpch);
            this.Draw(PixelTexture, new Rectangle(rectangle.Right, rectangle.Top, border, rectangle.Height + border), null, color, 0f, Vector2.Zero, SpriteEffects.None, layerDetpch);
        }
        #endregion
    }

    /// <summary>
    /// Класс Для работы с XNA 2D Отрисовкой.
    /// </summary>
    public class Graphics : BasicGraphics
    {
        public Graphics(GraphicsDevice graphicsDevice) : base(graphicsDevice) { }

        private static readonly Dictionary<string, List<Vector2>> CircleCache = new Dictionary<string, List<Vector2>>();

        #region FillRectangle
        /// <summary>
        /// Draws a filled rectangle
        /// </summary>
        /// <param name="rect">The rectangle to draw</param>
        /// <param name="color">The color to draw the rectangle in</param>
        public void FillRectangle(Rectangle rect, Color color)
        {
            Draw(PixelTexture, rect, color);
        }
        /// <summary>
        /// Draws a filled rectangle
        /// </summary>
        /// <param name="rect">The rectangle to draw</param>
        /// <param name="color">The color to draw the rectangle in</param>
        /// <param name="angle">The angle in radians to draw the rectangle at</param>
        public void FillRectangle(Rectangle rect, Color color, float angle)
        {
            Draw(PixelTexture, rect, null, color, angle, Vector2.Zero, SpriteEffects.None, 0);
        }
        /// <summary>
        /// Draws a filled rectangle
        /// </summary>
        /// <param name="location">Where to draw</param>
        /// <param name="size">The size of the rectangle</param>
        /// <param name="color">The color to draw the rectangle in</param>
        public void FillRectangle(Vector2 location, Vector2 size, Color color)
        {
            FillRectangle(location, size, color, 0.0f);
        }
        /// <summary>
        /// Draws a filled rectangle
        /// </summary>
        /// <param name="location">Where to draw</param>
        /// <param name="size">The size of the rectangle</param>
        /// <param name="angle">The angle in radians to draw the rectangle at</param>
        /// <param name="color">The color to draw the rectangle in</param>
        public void FillRectangle(Vector2 location, Vector2 size, Color color, float angle)
        {
            Draw(PixelTexture, location, null, color, angle, Vector2.Zero, size, SpriteEffects.None, 0);
        }
        /// <summary>
        /// Draws a filled rectangle
        /// </summary>
        /// <param name="x">The X coord of the left side</param>
        /// <param name="y">The Y coord of the upper side</param>
        /// <param name="w">Width</param>
        /// <param name="h">Height</param>
        /// <param name="color">The color to draw the rectangle in</param>
        public void FillRectangle(float x, float y, float w, float h, Color color)
        {
            FillRectangle(new Vector2(x, y), new Vector2(w, h), color, 0.0f);
        }
        /// <summary>
        /// Draws a filled rectangle
        /// </summary>
        /// <param name="x">The X coord of the left side</param>
        /// <param name="y">The Y coord of the upper side</param>
        /// <param name="w">Width</param>
        /// <param name="h">Height</param>
        /// <param name="color">The color to draw the rectangle in</param>
        /// <param name="angle">The angle of the rectangle in radians</param>
        public void FillRectangle(float x, float y, float w, float h, Color color, float angle)
        {
            FillRectangle(new Vector2(x, y), new Vector2(w, h), color, angle);
        }
        #endregion

        #region Draw Graphics Path
        /// <summary>
        /// Draws a list of connecting points
        /// </summary>
        /// /// <param name="position">Where to position the points</param>
        /// <param name="points">The points to connect with lines</param>
        /// <param name="color">The color to use</param>
        /// <param name="border">The border of the lines</param>
        public void DrawPath(Vector2 position, List<Vector2> points, Color color, float border)
        {
            if(points.Count < 1) return;
            if (points.Count < 2)
            {
                DrawPixel(points[0], color);
                return;
            }
            for (int i = 1; i < points.Count; i++)
            {
                DrawLine(points[i - 1] + position, points[i] + position, color, border);
            }
        }
        #endregion

        // TODO: Realize Draw Rectangle of Rotation Parameter

        #region Rectangle
        /// <summary>
        /// Draws a rectangle with the border provided
        /// </summary>
        /// <param name="rect">The rectangle to draw</param>
        /// <param name="color">The color to draw the rectangle in</param>
        public void DrawRectangle(Rectangle rect, Color color)
        {
            DrawRectangle(rect, color, 1.0f);
        }
        /// <summary>
        /// Draws a rectangle with the border provided
        /// </summary>
        /// <param name="rect">The rectangle to draw</param>
        /// <param name="color">The color to draw the rectangle in</param>
        /// <param name="border">The border of the lines</param>
        public void DrawRectangle(Rectangle rect, Color color, float border)
        {
            DrawLine(new Vector2(rect.X, rect.Y), new Vector2(rect.Right, rect.Y), color, border); // top
            DrawLine(new Vector2(rect.X + 1f, rect.Y), new Vector2(rect.X + 1f, rect.Bottom + border), color, border); // left
            DrawLine(new Vector2(rect.X, rect.Bottom), new Vector2(rect.Right, rect.Bottom), color, border); // bottom
            DrawLine(new Vector2(rect.Right + 1f, rect.Y), new Vector2(rect.Right + 1f, rect.Bottom + border), color, border); // right
        }
        /// <summary>
        /// Draws a rectangle with the border provided
        /// </summary>
        /// <param name="location">Where to draw</param>
        /// <param name="size">The size of the rectangle</param>
        /// <param name="color">The color to draw the rectangle in</param>
        public void DrawRectangle(Vector2 location, Vector2 size, Color color)
        {
            DrawRectangle(new Rectangle((int)location.X, (int)location.Y, (int)size.X, (int)size.Y), color, 1.0f);
        }
        /// <summary>
        /// Draws a rectangle with the border provided
        /// </summary>
        /// <param name="location">Where to draw</param>
        /// <param name="size">The size of the rectangle</param>
        /// <param name="color">The color to draw the rectangle in</param>
        /// <param name="border">The border of the line</param>
        public void DrawRectangle(Vector2 location, Vector2 size, Color color, float border)
        {
            DrawRectangle(new Rectangle((int)location.X, (int)location.Y, (int)size.X, (int)size.Y), color, border);
        }
        #endregion

        #region DrawLine
        /// <summary>
        /// Отрисовать Линию
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <param name="color"></param>
        public void DrawLine(float x1, float y1, float x2, float y2, Color color)
        {
            DrawLine(new Vector2(x1, y1), new Vector2(x2, y2), color, 1.0f);
        }
        /// <summary>
        /// Отрисовать Линию
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <param name="color"></param>
        /// <param name="border"></param>
        public void DrawLine(float x1, float y1, float x2, float y2, Color color, float border)
        {
            DrawLine(new Vector2(x1, y1), new Vector2(x2, y2), color, border);
        }
        /// <summary>
        /// Draws a line from point1 to point2 with an offset
        /// </summary>
        /// <param name="point1">The first point</param>
        /// <param name="point2">The second point</param>
        /// <param name="color">The color to use</param>
        public void DrawLine(Vector2 point1, Vector2 point2, Color color)
        {
            DrawLine(point1, point2, color, 1.0f);
        }
        /// <summary>
        /// Draws a line from point1 to point2 with an offset
        /// </summary>
        /// <param name="point1">The first point</param>
        /// <param name="point2">The second point</param>
        /// <param name="color">The color to use</param>
        /// <param name="border">The border of the line</param>
        public void DrawLine(Vector2 point1, Vector2 point2, Color color, float border)
        {
            float distance = Vector2.Distance(point1, point2);
            float angle = (float)Math.Atan2(point2.Y - point1.Y, point2.X - point1.X);
            DrawLine(point1, distance, angle, color, border);
        }
        /// <summary>
        /// Draws a line from point1 to point2 with an offset
        /// </summary>
        /// <param name="point">The starting point</param>
        /// <param name="length">The length of the line</param>
        /// <param name="angle">The angle of this line from the starting point in radians</param>
        /// <param name="color">The color to use</param>
        public void DrawLine(Vector2 point, float length, float angle, Color color)
        {
            DrawLine(point, length, angle, color, 1.0f);
        }
        /// <summary>
        /// Draws a line from point1 to point2 with an offset
        /// </summary>
        /// <param name="point">The starting point</param>
        /// <param name="length">The length of the line</param>
        /// <param name="angle">The angle of this line from the starting point</param>
        /// <param name="color">The color to use</param>
        /// <param name="border">The border of the line</param>
        public void DrawLine(Vector2 point, float length, float angle, Color color, float border)
        {
            Draw(PixelTexture, point, null, color, angle, Vector2.Zero, new Vector2(length, border), SpriteEffects.None, 0);
        }
        #endregion

        #region DrawPixel
        /// <summary>
        /// Отрисовать Пиксель
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="color"></param>
        public void DrawPixel(float x, float y, Color color)
        {
            DrawPixel(new Vector2(x, y), color);
        }
        /// <summary>
        /// Отрисовать Пиксель
        /// </summary>
        /// <param name="position"></param>
        /// <param name="color"></param>
        public void DrawPixel(Vector2 position, Color color)
        {
            Draw(PixelTexture, position, color);
        }
        #endregion

        #region DrawCircle
        /// <summary>
        /// Draw a circle
        /// </summary>
        /// <param name="center">The center of the circle</param>
        /// <param name="radius">The radius of the circle</param>
        /// <param name="sides">The number of sides to generate</param>
        /// <param name="color">The color of the circle</param>
        public void DrawCircle(Vector2 center, float radius, int sides, Color color)
        {
            DrawPath(center, CreateCircle(radius, sides), color, 1.0f);
        }
        public void DrawElipse(Rectangle rect, Color color, float border = 1.0f)
        {
            Vector2 center = new Vector2(rect.Center.X, rect.Center.Y);
            int sides = 180, radius = rect.Width;
            DrawPath(center, CreateCircle(radius, sides), color, border);
        }
        /// <summary>
        /// Draw a circle
        /// </summary>
        /// <param name="center">The center of the circle</param>
        /// <param name="radius">The radius of the circle</param>
        /// <param name="sides">The number of sides to generate</param>
        /// <param name="color">The color of the circle</param>
        /// <param name="border">The border of the lines used</param>
        public void DrawCircle(Vector2 center, float radius, int sides, Color color, float border)
        {
            DrawPath(center, CreateCircle(radius, sides), color, border);
        }
        /// <summary>
        /// Draw a circle
        /// </summary>
        /// <param name="x">The center X of the circle</param>
        /// <param name="y">The center Y of the circle</param>
        /// <param name="radius">The radius of the circle</param>
        /// <param name="sides">The number of sides to generate</param>
        /// <param name="color">The color of the circle</param>
        public void DrawCircle(float x, float y, float radius, int sides, Color color)
        {
            DrawPath(new Vector2(x, y), CreateCircle(radius, sides), color, 1.0f);
        }
        /// <summary>
        /// Draw a circle
        /// </summary>
        /// <param name="x">The center X of the circle</param>
        /// <param name="y">The center Y of the circle</param>
        /// <param name="radius">The radius of the circle</param>
        /// <param name="sides">The number of sides to generate</param>
        /// <param name="color">The color of the circle</param>
        /// <param name="border">The border of the lines used</param>
        public void DrawCircle(float x, float y, float radius, int sides, Color color, float border)
        {
            DrawPath(new Vector2(x, y), CreateCircle(radius, sides), color, border);
        }
        /// <summary>
        /// Draw a arc
        /// </summary>
        /// <param name="center">The center of the arc</param>
        /// <param name="radius">The radius of the arc</param>
        /// <param name="sides">The number of sides to generate</param>
        /// <param name="startingAngle">The starting angle of arc, 0 being to the east, increasing as you go clockwise</param>
        /// <param name="radians">The number of radians to draw, clockwise from the starting angle</param>
        /// <param name="color">The color of the arc</param>
        public void DrawArc(Vector2 center, float radius, int sides, float startingAngle, float radians, Color color)
        {
            DrawArc(center, radius, sides, startingAngle, radians, color, 1.0f);
        }
        /// <summary>
        /// Draw a arc
        /// </summary>
        /// <param name="center">The center of the arc</param>
        /// <param name="radius">The radius of the arc</param>
        /// <param name="sides">The number of sides to generate</param>
        /// <param name="startingAngle">The starting angle of arc, 0 being to the east, increasing as you go clockwise</param>
        /// <param name="radians">The number of radians to draw, clockwise from the starting angle</param>
        /// <param name="color">The color of the arc</param>
        /// <param name="border">The border of the arc</param>
        public void DrawArc(Vector2 center, float radius, int sides, float startingAngle, float radians, Color color, float border)
        {
            List<Vector2> arc = CreateArc(radius, sides, startingAngle, radians);
            DrawPath(center, arc, color, border);
        }
        #endregion

        #region Private Methods
        static List<Vector2> CreateCircle(double radius, int sides)
        {
            string circleKey = $"{radius}x{sides}";
            if (CircleCache.ContainsKey(circleKey))
            {
                return CircleCache[circleKey];
            }
            List<Vector2> vectors = new List<Vector2>();
            const double max = 2.0 * Math.PI;
            double step = max / sides;
            for (double theta = 0.0; theta < max; theta += step)
            {
                vectors.Add(new Vector2((float)(radius * Math.Cos(theta)), (float)(radius * Math.Sin(theta))));
            }
            vectors.Add(new Vector2((float)(radius * Math.Cos(0)), (float)(radius * Math.Sin(0))));
            CircleCache.Add(circleKey, vectors);
            return vectors;
        }
        static List<Vector2> CreateArc(float radius, int sides, float startingAngle, float radians)
        {
            List<Vector2> points = new List<Vector2>();
            points.AddRange(CreateCircle(radius, sides));
            points.RemoveAt(points.Count - 1);// remove the last point because it's a duplicate of the first
            double curAngle = 0.0;
            double anglePerSide = MathHelper.TwoPi / sides;
            while ((curAngle + (anglePerSide / 2.0)) < startingAngle)
            {
                curAngle += anglePerSide;
                points.Add(points[0]);
                points.RemoveAt(0);
            }
            points.Add(points[0]);
            int sidesInArc = (int)((radians / anglePerSide) + 0.5);
            points.RemoveRange(sidesInArc + 1, points.Count - sidesInArc - 1);
            return points;
        }
        #endregion
    }
}
