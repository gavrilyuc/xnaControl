using FormControl.Component;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FormControl.Drawing
{
    /// <summary>
    /// Кисть которая рисует кусочек текстуры
    /// </summary>
    public abstract class RegionedTextureBrush: TextureBrush
    {
        /// <summary>
        /// Регион текстуры
        /// </summary>
        public Rectangle Region { get; set; }
        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        /// <param name="texture"></param>
        /// <param name="region"></param>
        /// <param name="color"></param>
        protected RegionedTextureBrush(Texture2D texture, Rectangle region, Color color): base(texture, color)
        {
            Region = region;
        }
    }
}