using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Base.Component
{
    public class Button : Panel
    {
        public SpriteFont Font { get; set; }
        public string Text { get; set; }
        public Color ColorText { get; set; }

        public bool AutoSize { get; set; }

        public Button(SpriteFont font) : base()
        {
            ColorText = Color.Black;
            Text = "";
            this.Paint += Button_Paint;
            this.Font = font;
            this.AutoSize = false;
            this.Invalidate += Button_Invalidate;
        }

        void Button_Invalidate(Control sendred, TickEventArgs e)
        {
            if (this.AutoSize)
            {
                var f = new Vector2(this.BorderLenght + 2, this.BorderLenght + 2) + this.Font.MeasureString(this.Text);
                if (this.Size != f) this.Size = f;
            }
        }

        void Button_Paint(Control sendred, TickEventArgs e)
        {
            if (this.Text != null && this.Font != null && ColorText != Color.Transparent)
            {
                var v = Font.MeasureString(this.Text) / 2;
                v = this.DrawabledLocation + (this.Size / 2) - v;
                e.Graphics.DrawString(this.Font, this.Text, v, this.ColorText);
            }
        }
    }
}
