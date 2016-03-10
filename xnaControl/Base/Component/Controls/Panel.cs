namespace Core.Base.Component.Controls
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    public class Panel : Control
    {
        #region Property
        private int _borderLen;
        /// <summary>
        /// Размер Рамки
        /// </summary>
        public int BorderLenght { get { return _borderLen; } set { _borderLen = (value > 0 ? value : 1); } }
        /// <summary>
        /// Ли рисовать Рамку вокруг Панели ?
        /// </summary>
        public bool IsBorder { get; set; }
        /// <summary>
        /// Цвет Рамки
        /// </summary>
        public Color BorderColor { get; set; }
        /// <summary>
        /// Текстура Заднего Фона
        /// </summary>
        public Texture2D BackGroundTexture { get; set; }
        /// <summary>
        /// Цвет Задрего Фона
        /// </summary>
        public Color BackgroundColor { get; set; }
        #endregion

        public Panel()
        {
            Paint += Panel_Paint;
            IsBorder = true;
            BorderColor = Color.Lime;
            BorderLenght = 1;
            BackgroundColor = Color.White;
        }

        private void Panel_Paint(Control sender, TickEventArgs e)
        {
            var a = e.Graphics;
            var clientREctangle = ClientRectangle;

            if (BackGroundTexture == null)
            {
                if (BackgroundColor != Color.Transparent)
                    a.FillRectangle(clientREctangle, this.BackgroundColor);
            } else {
                if (BackgroundColor != Color.Transparent)
                    a.Draw(BackGroundTexture, clientREctangle, BackgroundColor);
            }

            if (IsBorder) a.DrawRectangle(clientREctangle, BorderColor, BorderLenght);
        }
    }
}
