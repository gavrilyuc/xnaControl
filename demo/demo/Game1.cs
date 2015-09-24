using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Core.Base.Component;
using Core.Base.Mehanic;

namespace example
{
    public class Game1 : Form
    {
        Panel p;
        Button b;
        TextBox textBox;
        SpriteFont baseFont;
        public Game1(FormSettings settings) : base(settings)
        {
            // Form Constructor
            this.Paint += Game1_Paint;
            this.Invalidate += Game1_Invalidate;
            this.Load += Game1_Load;
            this.LoadResourses += Game1_LoadResourses;

            this.MouseClick += Game1_MouseClick;
        }

        void Game1_Load(Control sender, EventArgs e)
        {
            // Form Inicialize & Generate GUI
            // and Other Inicializator...
            // xna method: Inicialize

            p = new Panel() {
                Location = new Vector2(200, 100),
                Size = new Vector2(400, 300)
            };
            b = new Button(baseFont) {
                Location = new Vector2(10, 10),
                Size = new Vector2(150, 40),
                Text = "Template Button",
                ColorText = Color.Black,
                Name = "Super Button"
            };
            p.Controls.Add(b);
            textBox = new TextBox(baseFont) {
                IsBorder = true,
                Name = "Super TextBox",
                Location = new Vector2(500, 50),
                Size = new Vector2(250, 30)
            };

            b.MouseClick += b_MouseClick;
            p.MouseClick += p_MouseClick;

            // Loadding Screen (Only Game-State)
            LoadingScreen l = new LoadingScreen(this, baseFont) {
                BackGroundThread = new GameThread(delegate {
                    System.Threading.Thread.Sleep(3000);// Sleep 3 seconds. :D

                    this.is_Drawing = true;// Example variable.

                    return true;
                }),
                NextState = "main"// Name to Next State
            };
            // Create Game State
            GameState state = new GameState(this) { Name = "main" };
            state.Controls.Add(p);// Add Controls for Game State
            state.Controls.Add(textBox);

            this.Controls.Add(l);// Add to Form Controls
            this.Controls.Add(state);

            l.Show();// Show Game State
            //(GameState Object).Change("stateName"); - Change State
        }

        void b_MouseClick(Control sender, MouseEventArgs e)
        {
            this.Window.Title = "[Button] Mouse Click: " + (j++).ToString();
        }
        void Game1_LoadResourses(Control sendred, TickEventArgs e)
        {
            // Load Content

            baseFont = e.ContentManager.Load<SpriteFont>("Arial");
        }
        void Game1_MouseClick(Control sender, MouseEventArgs e)
        {
            this.Window.Title = "[Form] Mouse Click: " + (j++).ToString();
        }
        int j = 0;
        void p_MouseClick(Control sender, MouseEventArgs e)
        {
            this.Window.Title = "[Panel] Mouse Click: " + (j++).ToString();
        }
        void Game1_Invalidate(Control sendred, TickEventArgs e)
        {
            var gameTime = e.GameTime;
            // Form Update
            // xna Method: Update
        }

        bool is_Drawing = false;
        void Game1_Paint(Control sendred, TickEventArgs e)
        {
            this.GraphicsDevice.Clear(Color.Black);
            // Form Paint
            var graphics = e.Graphics;
            if (is_Drawing)
            {
                graphics.DrawRectangle(new Rectangle(50, 50, 200, 200), Color.Red, 3f);
            }
        }
    }

}
