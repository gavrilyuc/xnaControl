using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Core.Base.Component.Controls;
using Core.Input;

namespace Core.Base.Component
{
    public class Form : Game, IWindow
    {
        private readonly Control _formcontrol;
        /// <summary>
        /// Применить Изменения Расширения Экрана
        /// </summary>
        protected void AplyScreenSize()
        {
            GraphicsDeviceManager.PreferredBackBufferWidth = Screen.X;
            GraphicsDeviceManager.PreferredBackBufferHeight =Screen.Y;
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
        public GridControls Controls { get; }

        #region Constructor
        public Form(FormSettings settings)
        {
            GraphicsDeviceManager = new GraphicsDeviceManager(this);
            Controls = new GridControls(this);
            _formcontrol = new Control(this) { Drawabled = true, Focused = true };
            Point screen;
            screen.X = settings.ScreenSize.X > GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width ? GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width : settings.ScreenSize.X;
            screen.Y = settings.ScreenSize.Y > GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height ? GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height : settings.ScreenSize.Y;
            Screen = screen;
            Content.RootDirectory = settings.ContentDirectory;
            GraphicsDeviceManager.IsFullScreen = !settings.Windowed;
            IsMouseVisible = settings.WindowMouseView;
            inicialize_events();
            AplyScreenSize();
        }
        private void inicialize_events()
        {
            _formcontrol.MouseMove += __formcontrol_MouseMove;
            _formcontrol.MouseDown += __formcontrol_MouseDown;
            _formcontrol.MouseDrag += __formcontrol_MouseDrag;
            _formcontrol.MouseClick += __formcontrol_MouseClick;
            _formcontrol.MouseUp += __formcontrol_MouseUp;
            _formcontrol.ScrollDelta += __formcontrol_ScrollDelta;
            _formcontrol.KeyDown += __formcontrol_KeyDown;
            _formcontrol.KeyUp += __formcontrol_KeyUp;
            _formcontrol.Paint += __formcontrol_Paint;
            _formcontrol.Invalidate += __formcontrol_Invalidate;

            _formcontrol.Location = new Vector2(0, 0);
            _formcontrol.Size = new Vector2(this.Screen.X, this.Screen.Y);
            this.Controls.Add(_formcontrol);
        }
        void __formcontrol_Invalidate(Control sendred, TickEventArgs e) { this.Invalidate(null, e); }
        void __formcontrol_Paint(Control sendred, TickEventArgs e) { this.Paint(null, e); }
        void __formcontrol_KeyUp(Control sender, KeyEventArgs e) { this.KeyUp(null, e); }
        void __formcontrol_KeyDown(Control sender, KeyEventArgs e) { this.KeyDown(null, e); }
        void __formcontrol_ScrollDelta(Control sender, MouseEventArgs e) { this.ScrollDelta(null, e); }
        void __formcontrol_MouseUp(Control sender, MouseEventArgs e) { this.MouseUp(null, e); }
        void __formcontrol_MouseClick(Control sender, MouseEventArgs e) { this.MouseClick(null, e); }
        void __formcontrol_MouseDrag(Control sender, MouseEventArgs e) { this.MouseDrag(null, e); }
        void __formcontrol_MouseDown(Control sender, MouseEventArgs e) { this.MouseDown(null, e); }
        void __formcontrol_MouseMove(Control sender, MouseEventArgs e) { this.MouseMove(null, e); }
        #endregion

        #region Realize Form & Game Event's
        protected override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            Graphics2D.Begin();
            _formcontrol.Draw(gameTime);
            foreach (var ch in Controls) ch.Draw(gameTime);
            Graphics2D.End();
        }
        protected override void Update(GameTime gameTime)
        {
            InputManager.GetInstance.Update(gameTime);
            base.Update(gameTime);
            _formcontrol.Update(gameTime);// update Form.
            for (int i = Controls.Count - 1; i >= 0; i--) Controls[i].Update(gameTime);
        }
        protected override void LoadContent()
        {
            Graphics2D = new Graphics(this.GraphicsDevice);
            this.LoadResourses(null, new TickEventArgs(this, default(GameTime)));
            base.LoadContent();
            foreach (var ch in Controls) (ch as IContent)?.LoadContent(Content);
        }
        protected override void UnloadContent()
        {
            base.UnloadContent();
            foreach (var ch in this.Controls) (ch as IContent)?.UnloadContent();
        }
        protected override void Initialize()
        {
            base.Initialize();
            this.Load(null, new EventArgs());
            foreach (var ch in this.Controls) (ch as IInicializator)?.Inicialize();
        }
        #endregion

        #region Event's
        /// <summary>
        /// Метод Inicialize
        /// </summary>
        public event ControlEventHandler Load = delegate { };
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

        #region Заглушки
        private new const int Components = -1;
        #endregion
    }
}