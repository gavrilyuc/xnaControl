namespace Core.Base.Component.Controls
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
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
            Paint += Button_Paint;
            Font = font;
            AutoSize = false;
            Invalidate += Button_Invalidate;
        }

        private void Button_Invalidate(Control sendred, TickEventArgs e)
        {
            if (!AutoSize) return;
            var f = new Vector2(BorderLenght + 2, BorderLenght + 2) + Font.MeasureString(Text);
            if (Size != f) Size = f;
        }
        private void Button_Paint(Control sendred, TickEventArgs e)
        {
            if (Text == null || Font == null || ColorText == Color.Transparent) return;
            var v = Font.MeasureString(Text) / 2;
            v = DrawabledLocation + (Size / 2) - v;
            e.Graphics.DrawString(Font, Text, v, ColorText);
        }
    }
}
