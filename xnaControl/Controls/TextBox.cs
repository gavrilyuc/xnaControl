using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Base.Component
{
    /// <summary>
    /// Свойства Коретки
    /// </summary>
    public struct Coretka
    {
        public Color Color;
        public int Size;
        public Coretka(Color color, int size)
        {
            this.Color = color;
            this.Size = size;
        }
    }

    /// <summary>
    /// Текстовое Поле
    /// </summary>
    public class TextBox : Panel
    {
        private const float ANIM_COLLDOWN = 0.5f;// Coretka Animation Change
        private const float PRESED_CHECK = 0.1f;// Presed Checker Changer
        private const float PRESED_CHECK_BEGIN = 0.8f;// Presed Checker Changer

        private float anim_time = 0f, ticked = 0f, ticked_pres = 0f;
        private bool is_press = false, is_plus = false;
        private int position_coretka = 0;
        private Coretka coretka;
        public SpriteFont Font { get; set; }
        public string Text { get; set; }
        public Color ColorText { get; set; }
        public Coretka CoretkaInfo { get { return coretka; } set { coretka = value; } }
        public bool AutoSize { get; set; }

        public TextBox(SpriteFont font) : base()
        {
            this.AutoSize = false;
            this.Font = font;
            this.Text = "";
            this.ColorText = Color.Black;
            this.Name = "TextBox::Control";
            this.CoretkaInfo = new Coretka(Color.Red, 1);

            this.Paint += TextBox_Paint;
            this.Invalidate += TextBox_Invalidate;

            this.KeyDown += TextBox_KeyDown;
            this.KeyUp += TextBox_KeyUp;
            this.KeyPresed += TextBox_KeyPresed;
            this.MouseDown += TextBox_MouseDown;
        }

        #region Event's
        void TextBox_MouseDown(Control sender, MouseEventArgs e)
        {
            Vector2 pos = e.Coord - this.DrawabledLocation;
            char ch = '\0';
            Vector2 sz = Vector2.Zero;
            int coretka_index = -1;
            for (int i = 0; i < this.Text.Length; i++)
            {
                ch = this.Text[i];
                sz += this.Font.MeasureString(ch.ToString());
                if (pos.X > sz.X) coretka_index = i;
                if (pos.X < sz.X) break;
            }
            this.position_coretka = coretka_index + 1;
        }
        void TextBox_KeyUp(Control sender, KeyEventArgs e)
        {
            is_press = false;
        }
        void TextBox_KeyPresed(Control sender, KeyEventArgs e)
        {
            bool is_ok_keys = false;
            switch (e.KeyCode) { case Keys.Back: case Keys.Delete: case Keys.Left: case Keys.Right: is_ok_keys = true; break; }
            if (e.KeyChar.Length >= 1 || is_ok_keys)
            {
                ticked_pres += (float)e.GameTime.ElapsedGameTime.TotalSeconds;
                if (ticked_pres >= PRESED_CHECK_BEGIN)
                {
                    is_press = true;
                    ticked_pres = 0f;
                    return;
                }
                ticked += (float)e.GameTime.ElapsedGameTime.TotalSeconds;
                if (ticked >= PRESED_CHECK && is_press)
                {
                    TextBox_KeyDown(sender, e);
                    ticked = 0f;
                }
            }
        }
        void TextBox_KeyDown(Control sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Left: this.position_coretka = Math.Max(this.position_coretka - 1, 0); break;
                case Keys.Right: this.position_coretka = Math.Min(this.position_coretka + 1, this.Text.Length); break;
                case Keys.Home: this.position_coretka = 0; break;
                case Keys.End: this.position_coretka = this.Text.Length; break;
                #region Remove
                case Keys.Delete:
                    {
                        if (this.Text.Length >= 1 && this.Text.Length - this.position_coretka > 0)
                            this.Text = this.Text.Remove(this.position_coretka, 1);
                    } break;
                case Keys.Back:
                    {
                        if (this.Text.Length >= 1)
                        {
                            if (this.position_coretka >= this.Text.Length)
                            {
                                this.Text = this.Text.Remove(this.Text.Length - 1);
                                this.position_coretka--;
                            }
                            else if (this.position_coretka >= 1)
                            {
                                this.Text = this.Text.Remove(this.position_coretka - 1, 1);
                                this.position_coretka--;
                            }
                        }
                    } break;
                #endregion
                default:
                    {
                        if (e.KeyChar.Length >= 1) this.Text = this.Text.Insert(this.position_coretka++, e.KeyChar);
                    } break;
            }
            ticked = 0f;
        }
        void TextBox_Invalidate(Control sendred, TickEventArgs e)
        {
            if (this.AutoSize)
            {
                float f = (this.BorderLenght + 2 + this.Font.MeasureString(this.Text).Y);
                if (this.Size.Y != f) this.Size = new Vector2(this.Size.X, f);
            }

            if (this.Focused)
            {
                anim_time += (float)e.GameTime.ElapsedGameTime.TotalMilliseconds;
                if (anim_time >= ANIM_COLLDOWN)
                {
                    if (this.CoretkaInfo.Color.A == 0) is_plus = true;
                    if (this.CoretkaInfo.Color.A >= 255) is_plus = false;

                    if (this.CoretkaInfo.Color.A > 0 && !is_plus) this.coretka.Color.A -= 10;
                    if (is_plus && this.CoretkaInfo.Color.A <= 0) this.coretka.Color.A += 10;
                }
            }
        }
        void TextBox_Paint(Control sendred, TickEventArgs e)
        {
            if (this.Text != null && this.Font != null)
            {
                Vector2 sizeString = Vector2.Zero;
                char ch = '\0';
                Vector2 beginDraw = this.DrawabledLocation;
                beginDraw.X += 3;
                int i = 0;
                for (; i < this.Text.Length; i++)
                {
                    if (this.Focused && this.position_coretka == i)
                    {
                        e.Graphics.FillRectangle(beginDraw, new Vector2(this.CoretkaInfo.Size, this.Size.Y), this.CoretkaInfo.Color);
                        beginDraw.X += this.CoretkaInfo.Size + 1;
                    }

                    ch = this.Text[i];
                    e.Graphics.DrawString(this.Font, ch.ToString(), beginDraw, this.ColorText);
                    sizeString = this.Font.MeasureString(ch.ToString());
                    beginDraw.X += sizeString.X;
                }
                if (this.Focused && this.position_coretka == i)
                {
                    e.Graphics.FillRectangle(beginDraw, new Vector2(this.CoretkaInfo.Size, this.Size.Y), this.CoretkaInfo.Color);
                    beginDraw.X += this.CoretkaInfo.Size + 1;
                }
            }
        }
        #endregion
    }
}
