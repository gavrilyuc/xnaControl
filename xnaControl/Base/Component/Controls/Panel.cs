using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Base.Component.Controls;


namespace Core.Base.Component
{
    public class Panel : Control
    {
        #region Property
        private int __borderLen = 1;
        private Boolean __isBorder = true;
        private Color __bordercolor = Color.Black;
        private Color __color = Color.Black;
        /// <summary>
        /// Размер Рамки
        /// </summary>
        public int BorderLenght { get { return __borderLen; } set { __borderLen = (value > 0 ? value : 1); } }
        /// <summary>
        /// Ли рисовать Рамку вокруг Панели ?
        /// </summary>
        public Boolean IsBorder { get { return __isBorder; } set { __isBorder = value; } }
        /// <summary>
        /// Цвет Рамки
        /// </summary>
        public Color BorderColor { get { return __bordercolor; } set { __bordercolor = value; } }
        /// <summary>
        /// Текстура Заднего Фона
        /// </summary>
        public Texture2D BackGroundTexture { get; set; }
        /// <summary>
        /// Цвет Задрего Фона
        /// </summary>
        public Color BackgroundColor { get { return __color; } set { __color = value; } }
        #endregion
        public Panel()
        {
            this.Paint += Panel_Paint;
            this.IsBorder = true;
            this.BorderColor = Color.Lime;
            this.BorderLenght = 1;
            this.BackgroundColor = Color.White;
        }

        void Panel_Paint(Control sendred, TickEventArgs e)
        {
            var a = e.Graphics;
            var clientREctangle = this.ClientRectangle;

            if (this.BackGroundTexture == null)
            {
                if (this.BackgroundColor != Color.Transparent)
                    a.FillRectangle(clientREctangle, this.BackgroundColor);
            } else {
                if (this.BackgroundColor != Color.Transparent)
                    a.Draw(this.BackGroundTexture, clientREctangle, this.BackgroundColor);
            }

            if (this.IsBorder) a.DrawRectangle(clientREctangle, this.BorderColor, this.BorderLenght);
        }
    }
}
