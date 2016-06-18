using System;
using Microsoft.Xna.Framework;

namespace FormControl.Component.Forms
{

    /// <summary>
    /// Базовый Объект окна
    /// </summary>
    public abstract class GameWindow : Game, IWindow
    {
        internal Action LoadContentAction;
        internal Action BeginRunAction;
        internal Action EndRunAction;
        internal Action<GameTime> UpdateAction;
        internal Action<GameTime> DrawAction;
        internal Func<bool> BeginDrawAction;
        internal Action InitializeAction;

        internal Action<bool> DisposeAction;
        internal Action EndDrawAction;
        internal System.EventHandler OnActivatedAction;
        internal System.EventHandler OnDeactivatedAction;
        internal System.EventHandler OnExitingAction;
        internal Func<Exception, bool> ShowMissingRequirementMessageAction;
        internal Action UnloadContentAction;

        /// <summary>
        /// Объект рисования контролов
        /// </summary>
        public Graphics Graphics2D { get; private set; }
        /// <summary>
        /// Размер окна
        /// </summary>
        public Point Screen { get; set; }
        /// <summary>
        /// Девайс Менеджер (в шаблоне XNA в классе Game1 -> graphics)
        /// </summary>
        public GraphicsDeviceManager GraphicsDeviceManager { get; }
        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        protected GameWindow()
        {
            GraphicsDeviceManager = new GraphicsDeviceManager(this);
        }

        #region Game Window Porting
        /// <summary>
        /// Вызывается после конструктора, предназначен для загрузки ресурсов
        /// </summary>
        protected override void LoadContent()
        {
            Graphics2D = new Graphics(GraphicsDevice);
            LoadContentAction();
            base.LoadContent();
        }
        /// <summary>
        /// Вызывается после инициализации всех компонентов, но перед первым обновлением в цикле игры.
        /// </summary>
        protected override void BeginRun()
        {
            BeginRunAction();
            base.BeginRun();
        }
        /// <summary>
        /// Вызывается после остановки цикла игры перед выходом.
        /// </summary>
        protected override void EndRun()
        {
            EndRunAction();
            base.EndRun();
        }
        /// <summary>
        /// Шаг обновления кадра
        /// </summary>
        /// <param name="gameTime">Время, прошедшее с момента последнего вызова Update.</param>
        protected override void Update(GameTime gameTime)
        {
            UpdateAction(gameTime);
            base.Update(gameTime);
        }
        /// <summary>
        /// Запускает создание кадра. За этим методом следуют вызовы методов Draw и EndDraw.
        /// </summary>
        protected override bool BeginDraw()
        {
            return BeginDrawAction() && base.BeginDraw();
        }
        /// <summary>
        /// Вызывается после создания объектов Game и GraphicsDevice, но до метода LoadContent.  Reference page contains code sample.
        /// </summary>
        protected override void Initialize()
        {
            InitializeAction();
            base.Initialize();
        }
        /// <summary>
        /// Шаг рисования объектов
        /// </summary>
        /// <param name="gameTime">Время, прошедшее с момента последнего вызова Draw.</param>
        protected override void Draw(GameTime gameTime)
        {
            DrawAction(gameTime);
            base.Draw(gameTime);
        }
        /// <summary>
        /// Освобождает все ресурсы, используемые классом Game.
        /// </summary>
        /// <param name="disposing">true для освобождения управляемых и неуправляемых ресурсов; false для освобождения только неуправляемых ресурсов.</param>
        protected override void Dispose(bool disposing)
        {
            DisposeAction(disposing);
            base.Dispose(disposing);
        }
        /// <summary>
        /// Завершает отрисовку кадра. Перед этим методом инициируются вызовы методов Draw и BeginDraw.
        /// </summary>
        protected override void EndDraw()
        {
            EndDrawAction();
            base.EndDraw();
        }
        /// <summary>
        /// Создает событие Activated. Переопределите этот метод, чтобы добавить код для обработки при получении игрой фокуса.
        /// </summary>
        /// <param name="sender">Объект Game.</param><param name="args">Аргументы события Activated.</param>
        protected override void OnActivated(object sender, EventArgs args)
        {
            OnActivatedAction(sender, args);
            base.OnActivated(sender, args);
        }
        /// <summary>
        /// Создает событие Deactivated. Переопределите этот метод, чтобы добавить код для обработки при потере игрой фокуса.
        /// </summary>
        /// <param name="sender">Объект Game.</param><param name="args">Аргументы события Deactivated.</param>
        protected override void OnDeactivated(object sender, EventArgs args)
        {
            OnDeactivatedAction(sender, args);
            base.OnDeactivated(sender, args);
        }
        /// <summary>
        /// Создает событие Exiting. Переопределите этот метод, чтобы добавить код для обработки при выходе из игры.
        /// </summary>
        /// <param name="sender">Объект Game.</param><param name="args">Аргументы события Exiting.</param>
        protected override void OnExiting(object sender, EventArgs args)
        {
            OnExitingAction(sender, args);
            base.OnExiting(sender, args);
        }
        /// <summary>
        /// Используется для отображения сообщения об ошибке при отсутствии надлежащего графического устройства или звуковой платы.
        /// </summary>
        /// <param name="exception">Отображаемое исключение.</param>
        protected override bool ShowMissingRequirementMessage(Exception exception)
        {
            return ShowMissingRequirementMessageAction(exception) && base.ShowMissingRequirementMessage(exception);
        }
        /// <summary>
        /// Вызывается, когда нужно выгрузить графические ресурсы. Переопределите этот метод для выгрузки любых связанных с игрой графических ресурсов.
        /// </summary>
        protected override void UnloadContent()
        {
            UnloadContentAction();
            base.UnloadContent();
        }
        #endregion

    }
}