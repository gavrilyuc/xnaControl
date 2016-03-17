using System;
using FormControl.Component.Controls.Base;
using FormControl.Drawing;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using static Microsoft.Xna.Framework.Input.Keys;

namespace FormControl.Component.Controls
{
    /// <summary>
    /// Свойства Коретки
    /// </summary>
    public struct Coretka
    {
        /// <summary>
        /// Цвет коретки
        /// </summary>
        public Color Color;
        /// <summary>
        /// Размер коретки
        /// </summary>
        public int Size;
        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        /// <param name="color"></param>
        /// <param name="size"></param>
        public Coretka(Color color, int size)
        {
            Color = color;
            Size = size;
        }
    }

    /// <summary>
    /// Текстовое Поле
    /// </summary>
    public class TextBox : TextControlBase
    {
        private const float AnimColldown = 0.5f;// Coretka Animation Change
        private const float PresedCheck = 0.1f;// Presed Checker Changer
        private const float PresedCheckBegin = 0.8f;// Presed Checker Changer

        private float _animTime, _ticked, _tickedPres;
        private bool _isPress, _isPlus;
        private int _positionCoretka;
        private Coretka _coretka;

        #region Properties
        /// <summary>
        /// Свойства коретки
        /// </summary>
        public Coretka CoretkaInfo
        {
            get { return _coretka; }
            set
            {
                _coretka = value;
                _coretkaSize = new Vector2(_coretka.Size, Size.Y);
            }
        }
        /// <summary>
        /// Максимальная длина вводимого текста
        /// </summary>
        public int MaxLenght { get; set; } = -1;
        #endregion

        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        /// <param name="brush">DON'T NULL</param>
        public TextBox(TextBrush brush) : base(brush)
        {
            AutoSize = false;
            CoretkaInfo = new Coretka(Color.Red, 1);

            Paint += TextBox_Paint;
            Invalidate += TextBox_Invalidate;

            KeyDown += TextBox_KeyDown;
            KeyUp += TextBox_KeyUp;
            KeyPresed += TextBox_KeyPresed;
            MouseDown += TextBox_MouseDown;
            ResizeControl += TextBox_ResizeControl;
            AutoSizeChanged += TextBox_ResizeControl;

            Text = GetType().FullName;
        }

        #region Event's
        private void TextBox_ResizeControl(Control sender)
        {
            _coretkaSize = new Vector2(_coretka.Size, Size.Y);
            Vector2 szS = Font.MeasureString("Q");
            if (AutoSize) MaxLenght = (int)Math.Ceiling(Size.X / szS.X) - 1;
        }
        private void TextBox_MouseDown(Control sender, MouseEventArgs e)
        {
            Vector2 pos = e.Coord - DrawabledLocation;
            char ch;
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
        private static readonly Keys[] KeyInPresedLonger = new[] {
            Back, Delete, Left, Right
        };
        private void TextBox_KeyPresed(Control sender, KeyEventArgs e)
        {
            if (KeyInPresedLonger.IndexOfOnArray(e.KeyCode) == -1) return;// if Not Presed Longer

            _tickedPres += (float)e.GameTime.ElapsedGameTime.TotalSeconds;
            if (_tickedPres >= PresedCheckBegin)
            {
                _isPress = true;
                _tickedPres = 0f;
                return;
            }
            _ticked += (float)e.GameTime.ElapsedGameTime.TotalSeconds;
            if (_ticked < PresedCheck || !_isPress) return;
            TextBox_KeyDown(sender, e);
            _ticked = 0f;
        }

        private void TextBox_KeyDown(Control sender, KeyEventArgs e)
        {
            // ReSharper disable once SwitchStatementMissingSomeCases
            switch (e.KeyCode)
            {
                case Left: _positionCoretka = Math.Max(_positionCoretka - 1, 0); break;
                case Right: _positionCoretka = Math.Min(_positionCoretka + 1, Text.Length); break;
                case Home: _positionCoretka = 0; break;
                case End: _positionCoretka = Text.Length; break;
                case Delete: {
                    if (Text.Length >= 1 && Text.Length - _positionCoretka > 0) Text = Text.Remove(_positionCoretka, 1);
                } break;
                case Back: {
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
                default: {
                    if (e.KeyChar.Length == 0) break;

                    if (MaxLenght >= 0)
                    {
                        if (Text.Length < MaxLenght) Text = Text.Insert(_positionCoretka++, e.KeyChar);
                    }
                    else Text = Text.Insert(_positionCoretka++, e.KeyChar);
                } break;
            }
            _ticked = 0f;
        }
        private void TextBox_Invalidate(Control sendred, TickEventArgs e)
        {
            if (!Focused) return;
            _animTime += (float)e.GameTime.ElapsedGameTime.TotalMilliseconds;
            if (_animTime < AnimColldown) return;
            if (CoretkaInfo.Color.A < 10) _isPlus = true;
            if (CoretkaInfo.Color.A > 250) _isPlus = false;
            _coretka.Color.A = (byte)(_coretka.Color.A + (_isPlus ? 10 : -10));
            _animTime = 0f;
        }

        #region Statics Fields
        private struct MyVector : IDrawablingTransformation
        {
            public Vector2 DrawabledLocation => location;
            public Rectangle ClientRectangle => Rectangle.Empty;
            public Vector2 location;
            public Vector2 size;
            public MyVector(Vector2 location, Vector2 size)
            {
                this.location = location;
                this.size = size;
            }
        }
        private static MyVector _drawableProperty;
        private static char _paintChar = ' ';
        private static int _iterationPaint = 0;
        private Vector2 _coretkaSize;
        #endregion

        private void TextBox_Paint(Control sendred, TickEventArgs e)
        {
            if (Text == null || Font == null) return;
            _drawableProperty.location = DrawabledLocation;
            _drawableProperty.location.X += 3;
            _iterationPaint = 0;
            for (; _iterationPaint < Text.Length; _iterationPaint++)
            {
                if (Focused && _positionCoretka == _iterationPaint)
                {
                    e.Graphics.FillRectangle(_drawableProperty.location, _coretkaSize, CoretkaInfo.Color);
                    _drawableProperty.location.X += CoretkaInfo.Size + 1;
                }

                // set Parameters draw Text
                _paintChar = Text[_iterationPaint];
                _drawableProperty.size = Font.MeasureString(_paintChar.ToString());
                TextBrush.Text = _paintChar.ToString();

                TextBrush.AlgorithmDrawable(e.Graphics, e.GameTime, _drawableProperty);// draw text from Algoritme Brush

                _drawableProperty.location.X += _drawableProperty.size.X;
            }
            if (!Focused || _positionCoretka != _iterationPaint) return;
            e.Graphics.FillRectangle(_drawableProperty.location, new Vector2(CoretkaInfo.Size, Size.Y), CoretkaInfo.Color);
            _drawableProperty.location.X += CoretkaInfo.Size + CoretkaInfo.Size;
        }
        #endregion
    }
}
