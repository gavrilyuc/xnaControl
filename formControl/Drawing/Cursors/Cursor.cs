using System.IO.Pipes;
using FormControl.Component.Forms;
using FormControl.Drawing.Brushes;
using FormControl.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FormControl.Drawing.Cursors
{
    /// <summary>
    /// Представление Курсора
    /// </summary>
    public class Cursor
    {
        /// <summary>
        /// Окно, к которому привязаны курсоры
        /// </summary>
        protected Form Window { get; set; }
        /// <summary>
        /// Объект рисования текстуры
        /// </summary>
        protected TextureBrush TextureBrush { get; set; }
        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        /// <param name="form">форма, к которому пренадлежит данный курсор</param>
        /// <param name="brush">кисть рисования курсора</param>
        public Cursor(Form form, TextureBrush brush)
        {
            Window = form;
            TextureBrush = brush;

            Window.Paint += Window_Paint;
            Window.MouseMove += Window_MouseMove;
        }

        private Vector2 _position;
        private void Window_MouseMove(Component.Controls.Control sender, Component.MouseEventArgs e)
        {
            _position = e.Coord;
        }

        private void Window_Paint(Component.Controls.Control sender, Component.TickEventArgs e)
        {
            TextureBrush?.AlgorithmDrawable(e.Graphics, e.GameTime, _position);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public static class Cursors
    {
        private static Texture2D _texture;

        #region Cursors
        /// <summary>
        /// Обычный курсор
        /// </summary>
        public static Cursor Default { get; private set; }
        #endregion

        internal static void InicializeCursors(Form window, string texturePath)
        {
            _texture = window.Content.Load<Texture2D>(texturePath);
            // todo: реализовать объектную структуру курсоров. Обновить и оптимизировать реализацию ControlMover (потому что данный код был взят из очень древнего проекта, который реализовывал ещё в молодости)
            Default = new Cursor(window, new SolidTextureBrush(_texture, Color.White));
        }
    }
}