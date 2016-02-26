using System;
using Core;
using Core.Base.Component;
using Core.Base.Component.Controls;
using Core.Base.Mehanic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace demo
{
    public class Game1 : Form
    {
        Panel _p;
        Button _b;
        TextBox _textBox;
        SpriteFont _baseFont;
        public Game1(FormSettings settings) : base(settings)
        {
            // Form Constructor
            Paint += Game1_Paint;
            Invalidate += Game1_Invalidate;
            Load += Game1_Load;
            LoadResourses += Game1_LoadResourses;

            MouseClick += Game1_MouseClick;
        }

        private void Game1_Load(Control sender, EventArgs e)
        {
            // Form Inicialize & Generate GUI
            // and Other Inicializator...
            // xna method: Inicialize

            _p = new Panel() {
                Location = new Vector2(200, 100),
                Size = new Vector2(400, 300)
            };
            _b = new Button(_baseFont) {
                Location = new Vector2(10, 10),
                Size = new Vector2(150, 40),
                Text = "Template Button",
                ColorText = Color.Black,
                Name = "Super Button"
            };
            _p.Controls.Add(_b);
            _textBox = new TextBox(_baseFont) {
                IsBorder = true,
                Name = "Super TextBox",
                Location = new Vector2(500, 50),
                Size = new Vector2(250, 30)
            };

            _b.MouseClick += b_MouseClick;
            _p.MouseClick += p_MouseClick;

            // Loadding Screen (Only Game-State)
            LoadingScreen l = new LoadingScreen(this, _baseFont) {
                BackGroundThread = new GameThread(delegate {
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
        }

        private void b_MouseClick(Control sender, MouseEventArgs e)
        {
            Window.Title = "[Button] Mouse Click: " + (j++).ToString();
        }
        private void Game1_LoadResourses(Control sendred, TickEventArgs e)
        {
            // Load Content
            _baseFont = e.ContentManager.Load<SpriteFont>("Arial");
        }
        private void Game1_MouseClick(Control sender, MouseEventArgs e)
        {
            Window.Title = "[Form] Mouse Click: " + (j++).ToString();
        }
        int j = 0;
        private void p_MouseClick(Control sender, MouseEventArgs e)
        {
            Window.Title = "[Panel] Mouse Click: " + (j++).ToString();
        }
        private void Game1_Invalidate(Control sendred, TickEventArgs e)
        {
            var gameTime = e.GameTime;
            // Form Update
            // xna Method: Update
        }
        bool _isDrawing = false;
        void Game1_Paint(Control sendred, TickEventArgs e)
        {
            GraphicsDevice.Clear(Color.Black);
            // Form Paint
            var graphics = e.Graphics;
            if (_isDrawing)
            {
                graphics.DrawRectangle(new Rectangle(50, 50, 200, 200), Color.Red, 3f);
            }
        }
    }

}
