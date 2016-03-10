namespace Core.Base.Component.Controls
{
    using System;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Input;
    /// <summary>
    /// Свойства Коретки
    /// </summary>
    public struct Coretka
    {
        public Color Color;
        public int Size;
        public Coretka(Color color, int size)
        {
            Color = color;
            Size = size;
        }
    }

    /// <summary>
    /// Текстовое Поле
    /// </summary>
    public class TextBox : Panel
    {
        private const float AnimColldown = 0.5f;// Coretka Animation Change
        private const float PresedCheck = 0.1f;// Presed Checker Changer
        private const float PresedCheckBegin = 0.8f;// Presed Checker Changer

        private float _animTime, _ticked, _tickedPres;
        private bool _isPress, _isPlus;
        private int _positionCoretka;
        private Coretka _coretka;
        public SpriteFont Font { get; set; }
        public string Text { get; set; }
        public Color ColorText { get; set; }
        public Coretka CoretkaInfo { get { return _coretka; } set { _coretka = value; } }
        public bool AutoSize { get; set; }

        public TextBox(SpriteFont font)
        {
            AutoSize = false;
            Font = font;
            Text = "";
            ColorText = Color.Black;
            Name = "TextBox::Control";
            CoretkaInfo = new Coretka(Color.Red, 1);

            Paint += TextBox_Paint;
            Invalidate += TextBox_Invalidate;

            KeyDown += TextBox_KeyDown;
            KeyUp += TextBox_KeyUp;
            KeyPresed += TextBox_KeyPresed;
            MouseDown += TextBox_MouseDown;
        }

        #region Event's
        private void TextBox_MouseDown(Control sender, MouseEventArgs e)
        {
            Vector2 pos = e.Coord - DrawabledLocation;
            char ch = '\0';
            Vector2 sz = Vector2.Zero;
            int coretkaIndex = -1;
            for (int i = 0; i < Text.Length; i++)
            {
                ch = Text[i];
                sz += Font.MeasureString(ch.ToString());
                if (pos.X > sz.X) coretkaIndex = i;
                if (pos.X < sz.X) break;
            }
            _positionCoretka = coretkaIndex + 1;
        }
        private void TextBox_KeyUp(Control sender, KeyEventArgs e)
        {
            _isPress = false;
        }
        private void TextBox_KeyPresed(Control sender, KeyEventArgs e)
        {
            bool isOkKeys = false;
            switch (e.KeyCode) { case Keys.Back: case Keys.Delete: case Keys.Left: case Keys.Right: isOkKeys = true; break; }
            if (e.KeyChar.Length >= 1 || isOkKeys)
            {
                _tickedPres += (float)e.GameTime.ElapsedGameTime.TotalSeconds;
                if (_tickedPres >= PresedCheckBegin)
                {
                    _isPress = true;
                    _tickedPres = 0f;
                    return;
                }
                _ticked += (float)e.GameTime.ElapsedGameTime.TotalSeconds;
                if (_ticked >= PresedCheck && _isPress)
                {
                    TextBox_KeyDown(sender, e);
                    _ticked = 0f;
                }
            }
        }
        private void TextBox_KeyDown(Control sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Left: _positionCoretka = Math.Max(_positionCoretka - 1, 0); break;
                case Keys.Right: _positionCoretka = Math.Min(_positionCoretka + 1, Text.Length); break;
                case Keys.Home: _positionCoretka = 0; break;
                case Keys.End: _positionCoretka = Text.Length; break;
                #region Remove
                case Keys.Delete:
                    {
                        if (Text.Length >= 1 && Text.Length - _positionCoretka > 0)
                            Text = Text.Remove(_positionCoretka, 1);
                    } break;
                case Keys.Back:
                    {
                        if (Text.Length >= 1)
                        {
                            if (_positionCoretka >= Text.Length)
                            {
                                Text = Text.Remove(Text.Length - 1);
                                _positionCoretka--;
                            }
                            else if (_positionCoretka >= 1)
                            {
                                Text = Text.Remove(_positionCoretka - 1, 1);
                                _positionCoretka--;
                            }
                        }
                    } break;
                #endregion
                default:
                    {
                        if (e.KeyChar.Length >= 1) Text = Text.Insert(_positionCoretka++, e.KeyChar);
                    } break;
            }
            _ticked = 0f;
        }
        private void TextBox_Invalidate(Control sendred, TickEventArgs e)
        {
            if (AutoSize)
            {
                float f = (BorderLenght + 2 + Font.MeasureString(Text).Y);
                if (Size.Y.CorrectEquals(f)) Size = new Vector2(Size.X, f);
            }

            if (!Focused) return;
            _animTime += (float)e.GameTime.ElapsedGameTime.TotalMilliseconds;
            if (_animTime >= AnimColldown)
            {
                if (CoretkaInfo.Color.A == 0) _isPlus = true;
                if (CoretkaInfo.Color.A >= 255) _isPlus = false;

                if (CoretkaInfo.Color.A > 0 && !_isPlus) _coretka.Color.A -= 10;
                if (_isPlus && CoretkaInfo.Color.A <= 0) _coretka.Color.A += 10;
            }
        }
        void TextBox_Paint(Control sendred, TickEventArgs e)
        {
            if (Text != null && Font != null)
            {
                Vector2 sizeString;
                char ch = '\0';
                Vector2 beginDraw = DrawabledLocation;
                beginDraw.X += 3;
                int i = 0;
                for (; i < Text.Length; i++)
                {
                    if (Focused && _positionCoretka == i)
                    {
                        e.Graphics.FillRectangle(beginDraw, new Vector2(CoretkaInfo.Size, Size.Y), CoretkaInfo.Color);
                        beginDraw.X += CoretkaInfo.Size + 1;
                    }

                    ch = Text[i];
                    e.Graphics.DrawString(Font, ch.ToString(), beginDraw, ColorText);
                    sizeString = Font.MeasureString(ch.ToString());
                    beginDraw.X += sizeString.X;
                }
                if (!Focused || _positionCoretka != i) return;
                e.Graphics.FillRectangle(beginDraw, new Vector2(CoretkaInfo.Size, Size.Y), CoretkaInfo.Color);
                beginDraw.X += CoretkaInfo.Size + 1;
            }
        }
        #endregion
    }
}
