using Core.Base.Component;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Base.Mehanic
{
    public class ThreadEventArgs
    {
    }
    public delegate bool ThreadEventHandler(GameThread thisTread, ThreadEventArgs e);
    public delegate void ThreadEventHandlerVoid(GameThread thisTread, ThreadEventArgs e);
    /// <summary>
    /// Игровой Поток для Обработки Данных или для загрузки неких Компонентов...
    /// </summary>
    public class GameThread : IDisposable
    {
        /// <summary>
        /// Вызывается когда поток закончил свою Работу
        /// </summary>
        public event ThreadEventHandlerVoid ThreadEnd = delegate { };
        private System.Threading.Thread th;
        public bool IsEnd { get; set; }

        const string GAME_TAG = "[Game Thread]";
        public string Name { get; set; }
        public System.Threading.ThreadPriority Priority { get; set; }
        public GameThread(ThreadEventHandler handler)
        {
            Name = "Inicializtion";
            IsEnd = false;
            th = new System.Threading.Thread(delegate()
            {
                while (true)
                {
                    if (handler(this, new ThreadEventArgs()))
                    {
                        IsEnd = true;
                        ThreadEnd(this, new ThreadEventArgs());
                        break;
                    }
                    else
                    {
                        IsEnd = false;
                    }
                }
            }) { IsBackground = true };
        }
        public void Start()
        {
            th.Name = string.Format("{0} {1}", GAME_TAG, this.Name);
            th.Priority = this.Priority;
            th.Start();
        }
        public void Stop()
        {
            th.Abort();
        }
        public void Dispose()
        {
            th = null;
            ThreadEnd = null;
        }
    }

    public class LoadingScreen : GameState
    {
        GameThread __thread;
        public GameThread BackGroundThread
        {
            get { return __thread; }
            set
            {
                if (__thread != value)
                {
                    __thread = value;
                    __thread.ThreadEnd += BackGroundThread_ThreadEnd;
                }
            }
        }
        public string NextState { get; set; }

        SpriteFont baseFont;
        string baseString = "Loading ";
        public LoadingScreen(Form form, SpriteFont font) : base(form)
        {
            baseFont = font;
            this.Paint += LoadingScreen_Paint;
            this.Invalidate += LoadingScreen_Invalidate;
            this.Size = this.Window.Screen.ConvertToVector();
            this.Location = Vector2.Zero;
            this.Name = "loading";

            Center = this.Window.Screen.Center();
            Center -= (baseFont.MeasureString(baseString) * Scale) / 2;
        }
        void BackGroundThread_ThreadEnd(GameThread thisTread, ThreadEventArgs e)
        {
            thisTread.Stop();
        }

        const float ANIM_CHANGE = 0.3f;// Seconds
        const float Scale = 2f;// Scale Text
        float all_time = 0f;
        int this_type = 0;
        Vector2 Center;
        void LoadingScreen_Invalidate(Control sendred, TickEventArgs e)
        {
            if (!BackGroundThread.IsEnd)
            {
                all_time += (float)e.GameTime.ElapsedGameTime.TotalSeconds;
                if (all_time >= ANIM_CHANGE)
                {
                    all_time = 0f;
                    this_type = this_type + 1 >= 3 ? 0 : this_type + 1;
                }
            }
            else
            {
                BackGroundThread.Stop();
                this.Change(NextState);
            }
        }
        void LoadingScreen_Paint(Control sendred, TickEventArgs e)
        {
            if (!BackGroundThread.IsEnd)
            {
                e.Graphics.DrawString(baseFont,
                    baseString + (this_type == 1 ? "." : this_type == 2 ? ".." : this_type == 3 ? "..." : ""),
                    Center, Color.Lime, 0f, Vector2.Zero, 2f, SpriteEffects.None, 0.09f);
                e.Graphics.FillRectangle(new Rectangle(0, 0, this.Window.Screen.X, this.Window.Screen.Y), new Color(64, 64, 64, 64));
            }
        }

        public override void Show()
        {
            base.Show();
            if (BackGroundThread != null)
                BackGroundThread.Start();
        }
    }
}
