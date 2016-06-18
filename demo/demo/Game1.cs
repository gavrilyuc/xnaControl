using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using FormControl;
using FormControl.Component;
using FormControl.Component.Controls;
using FormControl.Component.Forms;
using FormControl.Drawing.Brushes;
using FormControl.Input;
using FormControl.Mehanic;

namespace demo
{
    public class Game1 : Form
    {
        private FpsControl _fps;

        private Panel _p;
        private Button _b;
        private TextBox _textBox;
        private SpriteFont _baseFont;

        public Game1(FormSettings settings) : base(settings)
        {
            Paint += Game1_Paint;
            Invalidate += Game1_Invalidate;
            Click += Game1_MouseClick;
        }

        protected override void LoadContent()
        {
            // Load Content
            _baseFont = Content.Load<SpriteFont>("Arial");

            // Form Inicialize & Generate GUI
            // and Other Inicializator...
            // xna method: Inicialize
            _fps = new FpsControl(_baseFont);

            _p = new Panel
            {
                Location = new Vector2(200, 100),
                Size = new Vector2(400, 300),
                Background = new SolidColorBrush(Color.White),
                Border = new DefaultBorderBrush(1, Color.Blue)
            };
            DefaultTextBrush defaultBrush = new DefaultTextBrush(_baseFont, Color.Black);
            _b = new Button(defaultBrush)
            {
                Location = new Vector2(10, 10),
                Size = new Vector2(150, 40),
                Text = "TMP Button",
                ColorText = Color.Black,
                Background = new SolidColorBrush(Color.White),
                Border = new DefaultBorderBrush(1, Color.Black),
                Name = "Super Button"
            };
            _p.Controls.Add(_b);

            _b.Click += b_MouseClick;
            _p.Click += p_MouseClick;

            _textBox = new TextBox(new DefaultTextBrush(_baseFont, Color.Purple))
            {
                AutoSize = false,
                Border = new DefaultBorderBrush(1, Color.Lime),
                Background = new SolidColorBrush(Color.Silver),
                Location = new Vector2(250, 250),
                Size = new Vector2(100, 30),
                MaxLenght = 13
            };

            // Loadding Screen (Only Game-State)
            LoadingScreen l = new LoadingScreen(this, _baseFont)
            {
                BackGroundThread = new GameThread(delegate
                {
                    System.Threading.Thread.Sleep(3000);// Sleep 3 seconds. :D

                    _isDrawing = true;// Example variable.

                    return true;
                }),
                NextState = "main"// Name to Next State
            };

            // Create Game State
            GameState state = new GameState(this) { Name = "main" };
            state.Controls.Add(_p);// Add Controls for Game State
            state.Controls.Add(_textBox);

            Controls.Add(l);// Add to Form Controls
            Controls.Add(state);

            l.Show();// Show Game State
            //(GameState Object).Change("stateName"); - Change State
            base.LoadContent();
        }

        private void b_MouseClick(Control sender, MouseEventArgs e)
        {
            GameWindow.Title = "[Button] Mouse Click: " + (j++);
        }
        private void Game1_MouseClick(Control sender, MouseEventArgs e)
        {
            GameWindow.Title = "[Form] Mouse Click: " + (j++);
        }

        int j;
        private void p_MouseClick(Control sender, MouseEventArgs e)
        {
            GameWindow.Title = "[Panel] Mouse Click: " + (j++);
        }

        private string _keyPresedsDraw;
        private const string Separator = " + ";
        private void Game1_Invalidate(Control sendred, TickEventArgs e)
        {
            GameTime gameTime = e.GameTime;
            _fps.Update(gameTime);

            // Form Update
            // xna Method: Update
            string tmp = PKInputManager.GetInstance.KeyboardState.GetPressedKeys().Aggregate("You Key Down: ",
                (current, key) => current + (key + Separator));
            tmp = tmp.TrimEnd(Separator);

            if (tmp == string.Empty) tmp = "hi! I showed downed keys";

            _keyPresedsDraw = tmp;
        }

        private bool _isDrawing;
        private void Game1_Paint(Control sendred, TickEventArgs e)
        {
            // Form Paint
            Graphics graphics = e.Graphics;

            _fps.Draw(graphics, e.GameTime);

            if (!_isDrawing) return;
            graphics.DrawRectangle(new Rectangle(50, 50, 200, 200), Color.Red, 3f);
            graphics.DrawString(_baseFont, _keyPresedsDraw, new Vector2(70, 70), Color.Lime);
        }
    }
}