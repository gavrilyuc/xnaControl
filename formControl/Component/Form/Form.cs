using FormControl.Component.Controls;
using FormControl.Component.Layout;
using FormControl.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FormControl.Component.Form
{
    /// <summary>
    /// Форма
    /// </summary>
    public abstract class Form : Game, IWindow
    {
        private readonly Control _formcontrol;

        /// <summary>
        /// Применить Изменения Расширения Экрана
        /// </summary>
        protected void AplyScreenSize()
        {
            GraphicsDeviceManager.PreferredBackBufferWidth = Screen.X;
            GraphicsDeviceManager.PreferredBackBufferHeight = Screen.Y;
            _formcontrol.Size = new Vector2(Screen.X, Screen.Y);
            GraphicsDeviceManager.ApplyChanges();
        }
        /// <summary>
        /// Девайс Менеджер (в шаблоне XNA в классе Game1 -> graphics)
        /// </summary>
        protected GraphicsDeviceManager GraphicsDeviceManager;
        /// <summary>
        /// Образец, для рисование 2D Объектов на Экране
        /// </summary>
        public Graphics Graphics2D { get; set; }
        /// <summary>
        /// Размеры Окна
        /// </summary>
        public Point Screen { get; }
        /// <summary>
        /// Список Контролов (В Основном используется для хранение Игровых Состояний)
        /// </summary>
        public IControlLayout Controls => _formcontrol.Controls;

        #region Constructor
        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        protected Form()
            : this(
                new FormSettings() {
                    ContentDirectory = "Content", ScreenSize = new Point(300, 300), WindowMouseView = true,
                    Windowed = true
                }) { }
        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        /// <param name="settings"></param>
        protected Form(FormSettings settings) : this(settings, new DefaultLayuout()) { }
        /// <summary>
        /// Конструктор по умолчанию с указанием Определёного Контейнера
        /// </summary>
        /// <param name="settings"></param>
        /// <param name="layoutControl"></param>
        protected Form(FormSettings settings, IControlLayout layoutControl)
        {
            GraphicsDeviceManager = new GraphicsDeviceManager(this);
            _formcontrol = new Control(this, layoutControl) { Visibled = true, Focused = true };
            Point screen;
            screen.X = settings.ScreenSize.X
                       > GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width
                ? GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width
                : settings.ScreenSize.X;
            screen.Y = settings.ScreenSize.Y
                       > GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height
                ? GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height
                : settings.ScreenSize.Y;
            Screen = screen;
            Content.RootDirectory = settings.ContentDirectory;
            GraphicsDeviceManager.IsFullScreen = !settings.Windowed;
            IsMouseVisible = settings.WindowMouseView;
            inicialize_events();
            AplyScreenSize();
        }

        private void inicialize_events()
        {
            _formcontrol.MouseMove += __Form_MouseMove;
            _formcontrol.MouseDown += __Form_MouseDown;
            _formcontrol.MouseDrag += __Form_MouseDrag;
            _formcontrol.Click += __Form_MouseClick;
            _formcontrol.MouseUp += __Form_MouseUp;
            _formcontrol.ScrollDelta += __Form_ScrollDelta;
            _formcontrol.KeyDown += __Form_KeyDown;
            _formcontrol.KeyPresed += __Form_KeyPresed;
            _formcontrol.KeyUp += __Form_KeyUp;
            _formcontrol.Paint += __Form_Paint;
            _formcontrol.Invalidate += __Form_Invalidate;

            _formcontrol.Location = new Vector2(0, 0);
            _formcontrol.Size = new Vector2(Screen.X, Screen.Y);
        }

        private void __Form_KeyPresed(Control sender, KeyEventArgs e) => OnKeyPresed(e);
        private void __Form_Invalidate(Control sendred, TickEventArgs e) => OnInvalidate(e);
        private void __Form_Paint(Control sendred, TickEventArgs e) => OnPaint(e);
        private void __Form_KeyUp(Control sender, KeyEventArgs e) => OnKeyUp(e);
        private void __Form_KeyDown(Control sender, KeyEventArgs e) => OnKeyDown(e);
        private void __Form_ScrollDelta(Control sender, MouseEventArgs e) => OnScrollDelta(e);
        private void __Form_MouseUp(Control sender, MouseEventArgs e) => OnMouseUp(e);
        private void __Form_MouseClick(Control sender, MouseEventArgs e) => MouseClick(null, e);
        private void __Form_MouseDrag(Control sender, MouseEventArgs e) => OnMouseDrag(e);
        private void __Form_MouseDown(Control sender, MouseEventArgs e) => OnMouseDown(e);
        private void __Form_MouseMove(Control sender, MouseEventArgs e) => OnMouseMove(e);
        #endregion

        #region Realize Form & Game Event's
        /// <summary>
        /// Отрисовка формы
        /// </summary>
        /// <param name="gameTime"></param>
        protected override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            Graphics2D.Begin();
            Graphics2D.GraphicsDevice.Clear(Color.Black);
            _formcontrol.Draw(gameTime);
            Graphics2D.End();
        }
        /// <summary>
        /// Обновленгия кадра формы
        /// </summary>
        /// <param name="gameTime"></param>
        protected override void Update(GameTime gameTime)
        {
            PkInputManager.GetInstance.Update(gameTime);
            base.Update(gameTime);
            _formcontrol.Update(gameTime); // update Form.
        }
        /// <summary>
        /// Загрузка контента формы
        /// </summary>
        protected override void LoadContent()
        {
            Graphics2D = new Graphics(this.GraphicsDevice);
            LoadResourses(null, new TickEventArgs(this, default(GameTime)));
            base.LoadContent();
            foreach (Control ch in Controls) (ch as IContent)?.LoadContent(Content);
        }
        /// <summary>
        /// Выгрузка контента формы
        /// </summary>
        protected override void UnloadContent()
        {
            base.UnloadContent();
            foreach (Control ch in Controls) (ch as IContent)?.UnloadContent();
        }
        /// <summary>
        /// Инициализация Формы
        /// </summary>
        protected override void Initialize()
        {
            base.Initialize();
            Load(null);
            foreach (Control ch in Controls) (ch as IInicializator)?.Inicialize();
        }
        #endregion

        #region Event's
        /// <summary>
        /// Вызывается когда форма загружена и нужно загрузить контролы и прочие Объекты
        /// </summary>
        public event EventHandler Load = delegate { };
        /// <summary>
        /// Вызывается когда зажимается кнопка мыши в пределах формы
        /// </summary>
        public event MouseEventHandler MouseDown = delegate { };
        /// <summary>
        /// Вызывается когда отпускается кнопка мыши в пределах формы
        /// </summary>
        public event MouseEventHandler MouseUp = delegate { };
        /// <summary>
        /// Вызывается когда происходит Нажатие и Отпускание мыши в пределах формы (Клик мышкой)
        /// </summary>
        public event MouseEventHandler MouseClick = delegate { };
        /// <summary>
        /// Вызывается когда происходит Нажатие и Движение мыши в пределах формы
        /// </summary>
        public event MouseEventHandler MouseDrag = delegate { };
        /// <summary>
        /// Вызывается когда происходит Движение мыши в пределах формы
        /// </summary>
        public event MouseEventHandler MouseMove = delegate { };
        /// <summary>
        /// Вызывается когда покрутили Колесико на мышке в пределах Формы
        /// </summary>
        public event MouseEventHandler ScrollDelta = delegate { };
        /// <summary>
        /// Вызывается когда происходит Нажатие на кнопку в пределах формы
        /// </summary>
        public event KeyEventHandler KeyDown = delegate { };
        /// <summary>
        /// Вызывается когда происходит Отпускание кнопки в пределах формы
        /// </summary>
        public event KeyEventHandler KeyUp = delegate { };
        /// <summary>
        /// Вызывается когда Кнопка зажата
        /// </summary>
        public event KeyEventHandler KeyPresed = delegate { }; 
        /// <summary>
        /// Метод Рисования, Draw
        /// </summary>
        public event TickEventHandler Paint = delegate { };
        /// <summary>
        /// Метод Обновление Кадра, Update
        /// </summary>
        public event TickEventHandler Invalidate = delegate { };
        /// <summary>
        /// Метод Загрузки Ресурсов, LoadContent
        /// </summary>
        public event TickEventHandler LoadResourses = delegate { };
        #endregion

        #region Virtuals methods On Event's
        /// <summary>
        /// Вызывает делегат нажатия кнопки мыши
        /// </summary>
        /// <param name="e"></param>
        protected internal virtual void OnMouseDown(MouseEventArgs e)
        {
            MouseDown(null, e);
        }
        /// <summary>
        /// Вызывает делегат отпускания кнопки мыши
        /// </summary>
        /// <param name="e"></param>
        protected internal virtual void OnMouseUp(MouseEventArgs e)
        {
            MouseUp(null, e);
        }
        /// <summary>
        /// Вызывает делегат движения мыши
        /// </summary>
        /// <param name="e"></param>
        protected internal virtual void OnMouseMove(MouseEventArgs e)
        {
            MouseMove(null, e);
        }
        /// <summary>
        /// Вызывает делегат движение мыши с зажатой кнопкой мыши
        /// </summary>
        /// <param name="e"></param>
        protected internal virtual void OnMouseDrag(MouseEventArgs e)
        {
            MouseDrag(null, e);
        }
        /// <summary>
        /// Вызывает делегат колесика
        /// </summary>
        /// <param name="e"></param>
        protected internal virtual void OnScrollDelta(MouseEventArgs e)
        {
            ScrollDelta(null, e);
        }
        /// <summary>
        /// Вызывает делегат нажатия клавиши
        /// </summary>
        /// <param name="e"></param>
        protected internal virtual void OnKeyDown(KeyEventArgs e)
        {
            KeyDown(null, e);
        }
        /// <summary>
        /// Вызывает делегат отпускания клавиши
        /// </summary>
        /// <param name="e"></param>
        protected internal virtual void OnKeyUp(KeyEventArgs e)
        {
            KeyUp(null, e);
        }
        /// <summary>
        /// Вызывает делегат зажатия клавиши
        /// </summary>
        /// <param name="e"></param>
        protected internal virtual void OnKeyPresed(KeyEventArgs e)
        {
            KeyPresed(null, e);
        }
        /// <summary>
        /// Вызывает делегат Обновления кадра Формы
        /// </summary>
        /// <param name="e"></param>
        protected internal virtual void OnInvalidate(TickEventArgs e)
        {
            Invalidate(null, e);
        }
        /// <summary>
        /// Вызывает делегат Отрисовки кадра Формы
        /// </summary>
        /// <param name="e"></param>
        protected internal virtual void OnPaint(TickEventArgs e)
        {
            Paint(null, e);
        }
        #endregion
    }
}