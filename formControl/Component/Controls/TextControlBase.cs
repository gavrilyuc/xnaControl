using System.ComponentModel;
using FormControl.Component.Controls;
using FormControl.Component.Layout;
using FormControl.Drawing;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FormControl.Component.Controls
{
    /// <summary>
    /// Контрол который может хранить текст, а также имеет и рисует рамку, задний фон
    /// </summary>
    public abstract class TextControlBase : BorderedControlBase
    {
        private string _s;
        private TextBrush _textBrush;

        /// <summary>
        /// Текст
        /// </summary>
        [Category(PropertyGridCategoriesText.UsersCategory)] public string Text
        {
            get { return _s; }
            set
            {
                _s = value;
                TextChanged(this);
            }
        }

        /// <summary>
        /// Цвет Текста
        /// </summary>
        [Category(PropertyGridCategoriesText.GraphicsCategory)]
        public Color ColorText { get { return TextBrush.Color; }set { TextBrush.Color = value; } }

        /// <summary>
        /// Шрифт взятый из Кисти отрисовки
        /// </summary>
        [Category(PropertyGridCategoriesText.GraphicsCategory)]
        public SpriteFont Font => TextBrush.Font;

        /// <summary>
        /// Кисть рисования Текста
        /// </summary>
        [Category(PropertyGridCategoriesText.GraphicsCategory)] public TextBrush TextBrush
        {
            get { return _textBrush; }
            set
            {
                _textBrush = value;
                TextBrushChanged(this);
            }
        }

        /// <summary>
        /// Автоматически определять размер кнопки
        /// </summary>
        [Category(PropertyGridCategoriesText.UsersCategory)]
        public bool AutoSize
        {
            get { return LockedTransformation; }
            set
            {
                if (value)
                {
                    Vector2 f = new Vector2(Border.BorderLenght + 2, Border.BorderLenght + 2) + TextBrush.Font.MeasureString(Text);
                    if (Size != f) Size = f;
                }
                LockedTransformation = value;
                AutoSizeChanged(this);
            }
        }

        /// <summary>
        /// Вызываетя тогда когда авто размер меняется
        /// </summary>
        public event EventHandler AutoSizeChanged = delegate (Control sender) { };
        /// <summary>
        /// Вызывается тогда когда изменяется текст
        /// </summary>
        public event EventHandler TextChanged = delegate (Control sender) { };
        /// <summary>
        /// Вызывается тогда когда изменяется Кисть для рисования текста
        /// </summary>
        public event EventHandler TextBrushChanged = delegate (Control sender) { };

        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        /// <param name="brush"></param>
        protected TextControlBase(TextBrush brush) : this(brush, new DefaultLayuout()) { }
        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        /// <param name="brush"></param>
        /// <param name="layout"></param>
        protected TextControlBase(TextBrush brush, IControlLayout layout) : base(layout)
        {
            if (brush == null) return;
            TextBrush = brush;
            TextBrush.Text = Text = GetType().FullName;
            AutoSize = false;
            TextChanged += TextControlBase_TextChanged;
        }

        private void TextControlBase_TextChanged(Control sender)
        {
            if (TextBrush == null) return;
            TextBrush.Text = Text;
        }
    }
}