using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FormControl.Drawing
{
    /// <summary>
    /// Текструная Кисть
    /// </summary>
    public abstract class TextureBrush : ColorBrush
    {
        /// <summary>
        /// Цвет
        /// </summary>
        public Texture2D Texture { get; set; }
        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        /// <param name="texture"></param>
        /// <param name="color"></param>
        protected TextureBrush(Texture2D texture, Color color) : base(color) { Texture = texture; }
    }
}