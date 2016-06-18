using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using FormControl.Component.Controls;
using FormControl.Component.Layout;
using FormControl.Input;
using Microsoft.Xna.Framework.Content;

namespace FormControl.Component.Forms
{
    /// <summary>
    /// Форма
    /// </summary>
    public abstract class Form : Control, IWindow, IDisposable
    {
        private readonly GameWindow _gameWindow;

        #region Properties
        /// <summary>
        /// Образец, для рисование 2D Объектов на Экране
        /// </summary>
        public Graphics Graphics2D => _gameWindow.Graphics2D;
        /// <summary>
        /// Размеры окна
        /// </summary>
        public Point Screen
        {
            get { return _gameWindow.Screen; }
            set
            {
                _gameWindow.Screen = value;
            }
        }
        /// <summary>
        /// Девайс Менеджер (в шаблоне XNA в классе Game1 -> graphics)
        /// </summary>
        public GraphicsDevice GraphicsDevice => _gameWindow.GraphicsDevice;
        /// <summary>
        /// Получает или устанавливает текущий ContentManager.
        /// </summary>
        public ContentManager Content => _gameWindow.Content;
        /// <summary>
        /// Получает начальные параметры в LaunchParameters.
        /// </summary>
        public LaunchParameters LaunchParameters => _gameWindow.LaunchParameters;
        /// <summary>
        /// Получает GameServiceContainer, в котором содержатся все поставщики услуг, имеющие отношение к Game.
        /// </summary>
        public GameServiceContainer Services => _gameWindow.Services;
        /// <summary>
        /// Указывает, является ли игра активным приложением в данный момент.
        /// </summary>
        public bool IsActive => _gameWindow.IsActive;
        /// <summary>
        /// Получает базовое окно операционной системы.
        /// </summary>
        public Microsoft.Xna.Framework.GameWindow GameWindow => _gameWindow.Window;
        #endregion

        #region Events
        /// <summary>
        /// Возникает, когда для игры установлен фокус.
        /// </summary>
        public event EventHandler<EventArgs> Activated;
        /// <summary>
        /// Возникает при потере фокуса игрой.
        /// </summary>
        public event EventHandler<EventArgs> Deactivated;
        /// <summary>
        /// Возникает при выходе из игры.
        /// </summary>
        public event EventHandler<EventArgs> Exiting;
        /// <summary>
        /// Возникает при удалении игры.
        /// </summary>
        public event EventHandler<EventArgs> Disposed;
        #endregion

        private Form() { }
        private Form(IControlLayout layout) : base(layout) { }

        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        protected Form(FormSettings settings) : this(settings.FormContolLayout)
        {
            _gameWindow = settings.Window ?? new DefaultGameWindow();

            #region Screen Size Corrections
            Point screen;
            screen.X = settings.ScreenSize.X
                       > GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width
                ? GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width
                : settings.ScreenSize.X;
            screen.Y = settings.ScreenSize.Y
                       > GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height
                ? GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height
                : settings.ScreenSize.Y;
            _gameWindow.Screen = screen;
            Content.RootDirectory = settings.ContentDirectory;
            #endregion

            ControlInicializer(_gameWindow);
            Controls = settings.FormContolLayout;

            _gameWindow.GraphicsDeviceManager.IsFullScreen = !settings.Windowed;
            _gameWindow.IsMouseVisible = settings.WindowMouseView;

            _gameWindow.TargetElapsedTime = TimeSpan.FromSeconds(1.0f / 100.0f);
            _gameWindow.IsFixedTimeStep = false;
            _gameWindow.GraphicsDeviceManager.SynchronizeWithVerticalRetrace = false;
            AplyScreenSize();
            InicializeFormComponent();
        }

        #region Inicialize Form Component
        private void InicializeFormComponent()
        {
            _gameWindow.LoadContentAction = LoadContent;
            _gameWindow.BeginRunAction = BeginRun;
            _gameWindow.EndRunAction = EndRun;
            _gameWindow.UpdateAction = Update;
            _gameWindow.DrawAction = Draw;
            _gameWindow.BeginDrawAction = BeginDraw;
            _gameWindow.InitializeAction = Initialize;
            _gameWindow.DisposeAction = Dispose;
            _gameWindow.EndDrawAction = EndDraw;
            _gameWindow.OnActivatedAction = OnActivated;
            _gameWindow.OnDeactivatedAction = OnDeactivated;
            _gameWindow.OnExitingAction = OnExiting;
            _gameWindow.ShowMissingRequirementMessageAction = ShowMissingRequirementMessage;
            _gameWindow.UnloadContentAction = UnloadContent;

            _gameWindow.Activated += _gameWindow_Activated;
            _gameWindow.Deactivated += _gameWindow_Deactivated;
            _gameWindow.Exiting += _gameWindow_Exiting;
            _gameWindow.Disposed += _gameWindow_Disposed;
        }
        private void _gameWindow_Disposed(object sender, EventArgs e) => Disposed?.Invoke(sender, e);
        private void _gameWindow_Exiting(object sender, EventArgs e)=> Exiting?.Invoke(sender, e);
        private void _gameWindow_Deactivated(object sender, EventArgs e) => Deactivated?.Invoke(sender, e);
        private void _gameWindow_Activated(object sender, EventArgs e) => Activated?.Invoke(sender, e);
        #endregion

        #region Protected Realese Proxy Game Class
        /// <summary>
        /// Вызывается после конструктора, предназначен для загрузки ресурсов
        /// </summary>
        protected virtual void LoadContent()
        {
            foreach (IContent ch in Controls.OfType<IContent>()) ch.LoadContent(Content);
        }
        /// <summary>
        /// Вызывается после инициализации всех компонентов, но перед первым обновлением в цикле игры.
        /// </summary>
        protected virtual void BeginRun()
        {
        }
        /// <summary>
        /// Вызывается после остановки цикла игры перед выходом.
        /// </summary>
        protected virtual void EndRun()
        {
        }
        /// <summary>
        /// Шаг обновления кадра
        /// </summary>
        /// <param name="gameTime">Время, прошедшее с момента последнего вызова Update.</param>
        protected override void Update(GameTime gameTime)
        {
            PKInputManager.GetInstance.Update();
            base.Update(gameTime);
        }
        /// <summary>
        /// Запускает создание кадра. За этим методом следуют вызовы методов Draw и EndDraw.
        /// </summary>
        protected virtual bool BeginDraw()
        {
            return true;
        }
        /// <summary>
        /// Вызывается после создания объектов Game и GraphicsDevice, но до метода LoadContent.  Reference page contains code sample.
        /// </summary>
        protected virtual void Initialize()
        {
            foreach (Control control in Controls)
            {
                IInicializator ch = control;
                ch?.Inicialize();
            }
        }
        /// <summary>
        /// Шаг рисования объектов
        /// </summary>
        /// <param name="gameTime">Время, прошедшее с момента последнего вызова Draw.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            Graphics2D.Begin();
            base.Draw(gameTime);
            Graphics2D.End();
        }
        /// <summary>
        /// Освобождает все ресурсы, используемые классом Game.
        /// </summary>
        /// <param name="disposing">true для освобождения управляемых и неуправляемых ресурсов; false для освобождения только неуправляемых ресурсов.</param>
        protected virtual void Dispose(bool disposing)
        {
        }
        /// <summary>
        /// Завершает отрисовку кадра. Перед этим методом инициируются вызовы методов Draw и BeginDraw.
        /// </summary>
        protected virtual void EndDraw()
        {
        }
        /// <summary>
        /// Создает событие Activated. Переопределите этот метод, чтобы добавить код для обработки при получении игрой фокуса.
        /// </summary>
        /// <param name="sender">Объект Game.</param><param name="args">Аргументы события Activated.</param>
        protected virtual void OnActivated(object sender, EventArgs args)
        {
        }
        /// <summary>
        /// Создает событие Deactivated. Переопределите этот метод, чтобы добавить код для обработки при потере игрой фокуса.
        /// </summary>
        /// <param name="sender">Объект Game.</param><param name="args">Аргументы события Deactivated.</param>
        protected virtual void OnDeactivated(object sender, EventArgs args)
        {
        }
        /// <summary>
        /// Создает событие Exiting. Переопределите этот метод, чтобы добавить код для обработки при выходе из игры.
        /// </summary>
        /// <param name="sender">Объект Game.</param><param name="args">Аргументы события Exiting.</param>
        protected virtual void OnExiting(object sender, EventArgs args)
        {
        }
        /// <summary>
        /// Используется для отображения сообщения об ошибке при отсутствии надлежащего графического устройства или звуковой платы.
        /// </summary>
        /// <param name="exception">Отображаемое исключение.</param>
        protected virtual bool ShowMissingRequirementMessage(Exception exception)
        {
            return true;
        }
        /// <summary>
        /// Вызывается, когда нужно выгрузить графические ресурсы. Переопределите этот метод для выгрузки любых связанных с игрой графических ресурсов.
        /// </summary>
        protected virtual void UnloadContent()
        {
            foreach (IContent ch in Controls.OfType<IContent>()) ch.UnloadContent();
        }
        #endregion

        /// <summary>
        /// Применить Изменения Расширения Экрана
        /// </summary>
        protected void AplyScreenSize()
        {
            _gameWindow.GraphicsDeviceManager.PreferredBackBufferWidth = Screen.X;
            _gameWindow.GraphicsDeviceManager.PreferredBackBufferHeight = Screen.Y;
            Size = new Vector2(Screen.X, Screen.Y);
            _gameWindow.GraphicsDeviceManager.ApplyChanges();
        }

        #region Public Reaslese Proxy Game Class
        /// <summary>
        /// Освободить ресурсы формы
        /// </summary>
        public void Dispose()
        {
            GC.SuppressFinalize(_gameWindow);
            _gameWindow.Dispose();
        }
        /// <summary>
        /// Вызывайте этот метод для инициализации игры, запуска цикла игры и начала обработки событий в игре.
        /// </summary>
        public void Run() => _gameWindow.Run();
        /// <summary>
        /// Выполняет игру в режиме моделирования, что произойдет за один такт игровых часов; данный метод предназначен исключительно для отладки.
        /// </summary>
        public void RunOneFrame() => _gameWindow.RunOneFrame();
        /// <summary>
        /// Обновляет игровые часы и вызывает методы Update и Draw.
        /// </summary>
        public void Tick() => _gameWindow.Tick();
        /// <summary>
        /// Сбрасывает счетчик прошедшего времени.
        /// </summary>
        public void ResetElapsedTime() => _gameWindow.ResetElapsedTime();
        /// <summary>
        /// Предотвращает вызовы Draw до следующего Update.
        /// </summary>
        public void SuppressDraw() => _gameWindow.SuppressDraw();
        /// <summary>
        /// Вызывает выход из игры.
        /// </summary>
        public void Exit() => _gameWindow.Exit();
        #endregion
    }
}