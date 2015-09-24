using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Base.Component
{
    public class Form : Microsoft.Xna.Framework.Game, IWindow
    {
        private Control __formcontrol;
        /// <summary>
        /// Применить Изменения Расширения Экрана
        /// </summary>
        protected void AplyScreenSize()
        {
            GraphicsDeviceManager.PreferredBackBufferWidth = this.Screen.X;
            GraphicsDeviceManager.PreferredBackBufferHeight = this.Screen.Y;
            __formcontrol.Size = new Vector2(this.Screen.X, this.Screen.Y);
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
        public Point Screen { get; private set; }

        private GridControls controls;
        /// <summary>
        /// Список Контролов (В Основном используется для хранение Игровых Состояний)
        /// </summary>
        public GridControls Controls { get { return this.controls; } }

        #region Constructor
        public Form(FormSettings settings) : base()
        {
            GraphicsDeviceManager = new Microsoft.Xna.Framework.GraphicsDeviceManager(this);
            controls = new GridControls(this);
            __formcontrol = new Control(this) { Drawabled = true, Focused = true };
            Point screen;
            if (settings.ScreenSize.X > GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width)
                screen.X = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            else screen.X = settings.ScreenSize.X;
            if (settings.ScreenSize.Y > GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height)
                screen.Y = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            else screen.Y = settings.ScreenSize.Y;

            this.Screen = screen;
            this.Content.RootDirectory = settings.ContentDirectory;
            GraphicsDeviceManager.IsFullScreen = !settings.Windowed;
            this.IsMouseVisible = settings.WindowMouseView;

            this.inicialize_events();

            this.AplyScreenSize();
        }
        void inicialize_events()
        {
            __formcontrol.MouseMove += __formcontrol_MouseMove;
            __formcontrol.MouseDown += __formcontrol_MouseDown;
            __formcontrol.MouseDrag += __formcontrol_MouseDrag;
            __formcontrol.MouseClick += __formcontrol_MouseClick;
            __formcontrol.MouseUp += __formcontrol_MouseUp;
            __formcontrol.ScrollDelta += __formcontrol_ScrollDelta;
            __formcontrol.KeyDown += __formcontrol_KeyDown;
            __formcontrol.KeyUp += __formcontrol_KeyUp;
            __formcontrol.Paint += __formcontrol_Paint;
            __formcontrol.Invalidate += __formcontrol_Invalidate;

            __formcontrol.Location = new Vector2(0, 0);
            __formcontrol.Size = new Vector2(this.Screen.X, this.Screen.Y);
            this.Controls.Add(__formcontrol);
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
            //this.Graphics2D.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.Default,
            //    RasterizerState.CullNone, null, Matrix.Identity);
            this.Graphics2D.Begin();
            __formcontrol.Draw(gameTime);
            foreach (var ch in this.Controls) ch.Draw(gameTime);
            this.Graphics2D.End();
        }
        protected override void Update(GameTime gameTime)
        {
            InputManager.GetInstance.Update(gameTime);
            base.Update(gameTime);
            for (int i = this.Controls.Count - 1; i >= 0; i--) this.Controls[i].Update(gameTime);
            __formcontrol.Update(gameTime);// update Form.
        }
        protected override void LoadContent()
        {
            Graphics2D = new Graphics(this.GraphicsDevice);
            this.LoadResourses(null, new TickEventArgs(this, default(GameTime)));
            base.LoadContent();
            foreach (var ch in this.Controls) if ((ch as IContent) != null) ((IContent)ch).LoadContent(this.Content);
        }
        protected override void UnloadContent()
        {
            base.UnloadContent();
            foreach (var ch in this.Controls) if ((ch as IContent) != null) ((IContent)ch).UnloadContent();
        }
        protected override void Initialize()
        {
            base.Initialize();
            this.Load(null, new EventArgs());
            foreach (var ch in this.Controls) if ((ch as IInicializator) != null) ((IInicializator)ch).Inicialize();
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
        new private const int Components = -1;
        #endregion
    }
}