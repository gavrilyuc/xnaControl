using FormControl.Component;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using FormControl.Component.Controls;
using FormControl.Component.Form;
using FormControl.Input;
namespace FormControl.Mehanic
{
    /// <summary>
    /// Делегат Событий потока
    /// </summary>
    /// <param name="thisTread"></param>
    /// <returns></returns>
    public delegate bool ThreadEventHandler(GameThread thisTread);
    /// <summary>
    /// Делегат событий потока
    /// </summary>
    /// <param name="thisTread"></param>
    public delegate void ThreadEventHandlerVoid(GameThread thisTread);

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
        /// <summary>
        /// Закончился ли поток
        /// </summary>
        public bool IsEnd { get; set; }

        private const string GameTag = "[Game Thread]";
        /// <summary>
        /// Имя потока
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Приоритет
        /// </summary>
        public System.Threading.ThreadPriority Priority { get; set; }
        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        /// <param name="handler"></param>
        public GameThread(ThreadEventHandler handler)
        {
            Name = "Inicializtion";
            IsEnd = false;
            _th = new System.Threading.Thread(delegate()
            {
                while (true)
                {
                    if (handler(this))
                    {
                        IsEnd = true;
                        ThreadEnd(this);
                        break;
                    }
                    IsEnd = false;
                }
            }) { IsBackground = true };
        }
        /// <summary>
        /// Запустить поток
        /// </summary>
        public void Start()
        {
            _th.Name = $"{GameTag} {this.Name}";
            _th.Priority = Priority;
            _th.Start();
        }
        /// <summary>
        /// Остановить поток
        /// </summary>
        public void Stop()
        {
            _th.Abort();
        }
        /// <summary>
        /// Освободить рессурсы
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="has"></param>
        protected virtual void Dispose(bool has)
        {
            _th = null;
            ThreadEnd = null;
            if (has) GC.SuppressFinalize(this);
        }
    }

    /// <summary>
    /// Состояние загрузки
    /// </summary>
    public class LoadingScreen : GameState
    {
        GameThread _thread;
        /// <summary>
        /// Поток выполнение операции
        /// </summary>
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
        /// <summary>
        /// Следующее состояние, которое запустится после выполнения потока
        /// </summary>
        public string NextState { get; set; }

        private readonly SpriteFont _baseFont;
        private const string BaseString = "Loading ";
        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        /// <param name="form"></param>
        /// <param name="font"></param>
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
        private static void BackGroundThread_ThreadEnd(GameThread thisTread)
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

        /// <summary>
        /// Показать
        /// </summary>
        public override void Show()
        {
            base.Show();
            BackGroundThread?.Start();
        }
    }
}
