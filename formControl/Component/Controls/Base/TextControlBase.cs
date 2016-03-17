using FormControl.Drawing;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FormControl.Component.Controls.Base
{
    /// <summary>
    /// Контрол который может хранить текст, а также имеет и рисует рамку, задний фон
    /// </summary>
    public abstract class TextControlBase : BorderedControlBase
    {
        /// <summary>
        /// Текст
        /// </summary>
        public string Text { get; set; }

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
        public TextBrush TextBrush { get; set; }

        /// <summary>
        /// Автоматически определять размер кнопки
        /// </summary>
        public bool AutoSize
        {
            get { return LockedTransformation; }
            set
            {
                if (value)
                {
                    Vector2 f = new Vector2(BorderBrush.BorderLenght + 2, BorderBrush.BorderLenght + 2) + TextBrush.Font.MeasureString(Text);
                    if (Size != f) Size = f;
                }
                LockedTransformation = value;
                AutoSizeChanged(this);
            }
        }
        /// <summary>
        /// Вызываетя тогда когда авто размер меняется
        /// </summary>
        public event EventHandler AutoSizeChanged = delegate { }; 

        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        /// <param name="brush"></param>
        protected TextControlBase(TextBrush brush)
        {
            TextBrush = brush;
            Text = GetType().FullName;
            AutoSize = false;
        }
    }
}