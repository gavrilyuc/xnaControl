using Core.Base.Component;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Core.Base.Component.Controls;

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
        private System.Threading.Thread _th;
        public bool IsEnd { get; set; }

        private const string GameTag = "[Game Thread]";
        public string Name { get; set; }
        public System.Threading.ThreadPriority Priority { get; set; }
        public GameThread(ThreadEventHandler handler)
        {
            Name = "Inicializtion";
            IsEnd = false;
            _th = new System.Threading.Thread(delegate()
            {
                while (true)
                {
                    if (handler(this, new ThreadEventArgs()))
                    {
                        IsEnd = true;
                        ThreadEnd(this, new ThreadEventArgs());
                        break;
                    }
                    IsEnd = false;
                }
            }) { IsBackground = true };
        }
        public void Start()
        {
            _th.Name = $"{GameTag} {this.Name}";
            _th.Priority = Priority;
            _th.Start();
        }
        public void Stop()
        {
            _th.Abort();
        }
        public void Dispose()
        {
            _th = null;
            ThreadEnd = null;
        }
    }

    public class LoadingScreen : GameState
    {
        GameThread _thread;
        public GameThread BackGroundThread
        {
            get { return _thread; }
            set
            {
                if (_thread == value) return;
                _thread = value;
                _thread.ThreadEnd += BackGroundThread_ThreadEnd;
            }
        }
        public string NextState { get; set; }

        private readonly SpriteFont _baseFont;
        private const string BaseString = "Loading ";
        public LoadingScreen(Form form, SpriteFont font) : base(form)
        {
            _baseFont = font;
            Paint += LoadingScreen_Paint;
            Invalidate += LoadingScreen_Invalidate;
            Size = Window.Screen.ConvertToVector();
            Location = Vector2.Zero;
            Name = "loading";

            _center = Window.Screen.Center();
            _center -= (_baseFont.MeasureString(BaseString) * Scale) / 2;
        }
        private static void BackGroundThread_ThreadEnd(GameThread thisTread, ThreadEventArgs e)
        {
            thisTread.Stop();
        }

        private const float AnimChange = 0.3f;// Seconds
        private const float Scale = 2f;// Scale Text
        private float _allTime;
        private int _thisType;
        readonly Vector2 _center;
        private void LoadingScreen_Invalidate(Control sendred, TickEventArgs e)
        {
            if (!BackGroundThread.IsEnd)
            {
                _allTime += (float)e.GameTime.ElapsedGameTime.TotalSeconds;
                if (!(_allTime >= AnimChange)) return;
                _allTime = 0f;
                _thisType = _thisType + 1 >= 3 ? 0 : _thisType + 1;
            }
            else
            {
                BackGroundThread.Stop();
                Change(NextState);
            }
        }
        private void LoadingScreen_Paint(Control sendred, TickEventArgs e)
        {
            if (BackGroundThread.IsEnd) return;
            e.Graphics.DrawString(_baseFont,
                BaseString + (_thisType == 1 ? "." : _thisType == 2 ? ".." : _thisType == 3 ? "..." : ""),
                _center, Color.Lime, 0f, Vector2.Zero, 2f, SpriteEffects.None, 0.09f);
            e.Graphics.FillRectangle(new Rectangle(0, 0, this.Window.Screen.X, this.Window.Screen.Y), new Color(64, 64, 64, 64));
        }
        public override void Show()
        {
            base.Show();
            BackGroundThread?.Start();
        }
    }
}
