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
        public string Text
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
        public Color ColorText { get { return TextBrush.Color; }set { TextBrush.Color = value; } }

        /// <summary>
        /// Шрифт взятый из Кисти отрисовки
        /// </summary>
        public SpriteFont Font => TextBrush.Font;

        /// <summary>
        /// Кисть рисования Текста
        /// </summary>
        public TextBrush TextBrush
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
        public bool AutoSize
        {
            get { return LockedTransformation; }
            set
            {
                LockedTransformation = value;
                AutoSizeChanged(this);
            }
        }

        /// <summary>
        /// Вызываетя тогда когда авто размер меняется
        /// </summary>
        public event EventHandler AutoSizeChanged = delegate { };
        /// <summary>
        /// Вызывается тогда когда изменяется текст
        /// </summary>
        public event EventHandler TextChanged = delegate { };
        /// <summary>
        /// Вызывается тогда когда изменяется Кисть для рисования текста
        /// </summary>
        public event EventHandler TextBrushChanged = delegate { };

        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        protected TextControlBase() : this(new DefaultLayuout()) { }
        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        /// <param name="layout"></param>
        protected TextControlBase(IControlLayout layout) : base(layout)
        {
            AutoSize = false;
            TextChanged += TextControlBase_TextChanged;
            TextBrushChanged += TextControlBase_TextBrushChanged;
        }

        /// <summary>
        /// Вызывается после инициализации всех компонентов, но перед первым обновлением в цикле игры.
        /// </summary>
        protected override void BeginRun()
        {
            if (AutoSize)
            {
                int len = 0;
                if (Border != null) len = Border.BorderLenght;

                Vector2 sizeText = new Vector2(Text.Length * 15, 15);
                if (TextBrush != null) sizeText = TextBrush.Font.MeasureString(Text);

                Vector2 f = new Vector2(len + 2, len + 2) + sizeText;

                SetControlLockedTransformation(this, false);
                if (Size != f) Size = f;
                SetControlLockedTransformation(this);
            }
            base.BeginRun();
        }
        private void TextControlBase_TextBrushChanged(Control sender)
        {
            TextBrush.Text = Text;
        }
        private void TextControlBase_TextChanged(Control sender)
        {
            if (TextBrush == null) return;
            TextBrush.Text = Text;
        }
    }
}