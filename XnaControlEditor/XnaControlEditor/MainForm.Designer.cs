using System;
using System.Collections.Generic;
using System.Linq;
using FormControl.Component;
using FormControl.Component.Controls;
using FormControl.Component.Forms;
using FormControl.Drawing;
using FormControl.Drawing.Brushes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using XnaControlEditor.Core;

namespace XnaControlEditor
{
    public partial class MainForm
    {
        private Panel grid;
        private Panel container;
        private Button btnProp;
        private Button btnControl;

        private void InicializeComponent()
        {
            grid = new Panel();
            container = new Panel();
            btnProp = new Button();
            btnControl = new Button();


            grid.Location = new Vector2(619, 26);
            grid.Name = "grid";
            grid.Size = new Vector2(258, 475);

            container.Location = new Vector2(1, 1);
            container.Name = "container";
            container.Size = new Vector2(616, 500);

            btnProp.Location = new Vector2(619, 1);
            btnProp.Name = "btnProp";
            btnProp.Size = new Vector2(59, 25);
            btnProp.Text = "Props";
            btnProp.AutoSize = true;

            btnControl.Location = new Vector2(678, 1);
            btnControl.Name = "btnControl";
            btnControl.Size = new Vector2(59, 25);
            btnControl.Text = "Controls";
            btnControl.AutoSize = true;

            Name = "Game1";
        }

        private SpriteFont _baseFont;


        private void InicializeLoadContent()
        {
            _baseFont = Content.Load<SpriteFont>("Arial");

            DefaultTextBrush defaultTextBrush = new DefaultTextBrush(_baseFont, Color.Black);
            BorderBrush borderBrush = new DefaultBorderBrush(1, Color.Pink);
            SolidColorBrush solidColorBrush = new SolidColorBrush(Color.White);

            grid.Background = solidColorBrush.Clone();
            grid.Border = (BorderBrush)borderBrush.Clone();
            container.Background = solidColorBrush.Clone();
            container.Border = (BorderBrush)borderBrush.Clone();


            btnProp.Background = solidColorBrush.Clone();
            btnControl.Background = solidColorBrush.Clone();

            btnControl.TextBrush = (DefaultTextBrush)defaultTextBrush.Clone();
            btnProp.TextBrush = (DefaultTextBrush)defaultTextBrush.Clone();

            btnProp.Border = (BorderBrush)borderBrush.Clone();
            btnControl.Border = (BorderBrush)borderBrush.Clone();

            Controls.Add(btnControl);
            Controls.Add(btnProp);
            Controls.Add(grid);
            Controls.Add(container);


            Label label = new Label()
            {
                Text = "MOVING LABEL",
                TextBrush = (TextBrush) defaultTextBrush.Clone(),
                Location = new Vector2(150, 150),
                AutoSize = false,
                Size = new Vector2(150, 50),
                Background = solidColorBrush.Clone(),
                Border = (BorderBrush) borderBrush.Clone()
            };
            ControlMover.SetResizeMode(label, label, TypeMover.Resize);
            Controls.Add(label);
        }
    }
}
