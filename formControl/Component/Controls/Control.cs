using System.ComponentModel;
using FormControl.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using FormControl.Component.Layout;

namespace FormControl.Component.Controls
{
    /// <summary>
    /// Базовый Объект Контрола
    /// </summary>
    public class Control : IControl, IInicializator
    {
        #region Variables
        private Vector2 _parrentLocation;
        private bool _isDraged;
        private Vector2 _l, _s;
        private bool _isInputMouse;
        private bool _isEnabled;
        private bool _isDrawabled;
        private Control _parentControl;
        private static bool _isBlockedMouse;
        private static bool _isBlockedKeyBoard;
        private static bool _isClick;
        private static Control _focus;
        private static bool _isMouseDown;
        private static readonly PKInputManager Input = PKInputManager.GetInstance; // Input (Mouse & KeyBoard) Manager.
        #endregion

        #region Constructor
        /// <summary>
        /// Констрктор для Базовой Инициализации
        /// </summary>
        /// <param name="window">Окно в котором будут Отображаться Контролы</param>
        /// <param name="layout"></param>
        internal Control(IWindow window, IControlLayout layout) : this(layout)
        {
            if (Graphics == null) Graphics = window.Graphics2D;
            if (Window == null) Window = window;
            _ticks = new TickEventArgs(Window, null);
            _mouseEventArgs = new MouseEventArgs();
            _keyEventArgs = new KeyEventArgs();
        }
        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        public Control() : this(new DefaultLayuout()) { }
        /// <summary>
        /// Конструктор, с помощью которого можно установить свой кастомный контейнер для хранение внутрених контролов
        /// </summary>
        /// <param name="layout"></param>
        public Control(IControlLayout layout)
        {
            Controls = layout;
            MouseDown += Control_MouseDown;
            MouseUp += Control_MouseUp;
            MouseMove += Control_MouseMove;
            Visibled = true;
            Enabled = true;
            Focused = false;
            Location = Vector2.Zero;
            Size = Vector2.Zero;
            Controls.ControlsAdded += Controls_ControlsAdded;
            Name = GetType().FullName;
            LocationChangeControl += Control_LocationChangeControl;
        }

        private void Control_LocationChangeControl(Control sender)
        {
            for (int i = 0; i < Controls.Count; i++)
                Controls[i].DrawabledLocation = DrawabledLocation;
        }
        #endregion

        #region Event's
        /// <summary>
        /// Задатие мыши на объекту
        /// </summary>
        public event MouseEventHandler MouseDown;
        /// <summary>
        /// Отпускание мыши на объекту
        /// </summary>
        public event MouseEventHandler MouseUp;
        /// <summary>
        /// Нажатие мыши на объекту
        /// </summary>
        public event MouseEventHandler Click;
        /// <summary>
        /// Вызывается когда происходит зажатое движение мыши
        /// </summary>
        public event MouseEventHandler MouseDrag;
        /// <summary>
        /// Движение мыши на объекту
        /// </summary>
        public event MouseEventHandler MouseMove;
        /// <summary>
        /// Вхождение мыши в Объект
        /// </summary>
        public event MouseEventHandler MouseInput;
        /// <summary>
        /// Выход мыши из Объекта
        /// </summary>
        public event MouseEventHandler MouseLeave;
        /// <summary>
        /// Скроллинг мыши (Колесико)
        /// </summary>
        public event MouseEventHandler ScrollDelta;
        /// <summary>
        /// Нажатие Кнопки
        /// </summary>
        public event KeyEventHandler KeyDown;
        /// <summary>
        /// Отпускание Кнопки
        /// </summary>
        public event KeyEventHandler KeyUp;
        /// <summary>
        /// Вызывается когда Кнопка зажата
        /// </summary>
        public event KeyEventHandler KeyPresed;
        /// <summary>
        /// Вызывается вместе с методом Draw
        /// </summary>
        public event TickEventHandler Paint;
        /// <summary>
        /// Вызывается вместе с Методом Update
        /// </summary>
        public event TickEventHandler Invalidate;
        /// <summary>
        /// Вызывается когда изменяется размеры контрола
        /// </summary>
        public event EventHandler ResizeControl;
        /// <summary>
        /// Вызывается когда изменяется позиция контрола
        /// </summary>
        public event EventHandler LocationChangeControl;
        /// <summary>
        /// Вызывается когда изменяется контейнер у контрола
        /// </summary>
        public event EventHandler ParentChanged;
        /// <summary>
        /// Вызывается тогда, когда у контрола изменяется Фокус.
        /// </summary>
        public event EventHandler FocusableChanged;
        #endregion

        #region Realise Event's & XNA Methods
        private static TickEventArgs _ticks;
        private static MouseEventArgs _mouseEventArgs;
        private static KeyEventArgs _keyEventArgs;

        internal virtual void Draw(GameTime gameTime)
        {
            _isBlockedMouse = false;
            _isBlockedKeyBoard = false;
            if (Visibled == false) return;
            _ticks.GameTime = gameTime;
            OnPaint(_ticks);
        }
        internal virtual void Update(GameTime gameTime)
        {
            if (Enabled == false) return;
            for (int i = Controls.Count - 1; i > -1; i--)
            {
                //Controls[i].DrawabledLocation = DrawabledLocation;
                Controls[i].Update(gameTime);
            }

            if (!_isBlockedMouse) MouseUpdate(gameTime);
            if (!_isBlockedKeyBoard) KeyboardUpdate(gameTime);

            _ticks.GameTime = gameTime;
            OnInvalidate(_ticks);
        }
        private void MouseUpdate(GameTime gameTime)
        {
            if (_isBlockedMouse) return;
            Vector2 mouse = new Vector2(Input.MouseState.X, Input.MouseState.Y);
            Vector2 prevMouse = new Vector2(Input.LastMouseState.X, Input.LastMouseState.Y);
            bool isInputPos =
                new RectangleF(DrawabledLocation.X, DrawabledLocation.Y, Size.X, Size.Y).Contains(
                    new Vector2(Input.MouseState.X, Input.MouseState.Y));

            _mouseEventArgs.GameTime = gameTime;
            _mouseEventArgs.Button = MouseButton.Left;
            _mouseEventArgs.PrevState = Input.LastMouseState;
            _mouseEventArgs.CurrentState = Input.MouseState;
            _mouseEventArgs.Coord = new Vector2(Input.MouseState.X, Input.MouseState.Y);
            _mouseEventArgs.DeltaScroll = Input.MouseState.ScrollWheelValue;

            if (isInputPos)
            {
                if (Input.MouseDown(MouseButton.Left)) OnMouseDown(_mouseEventArgs);
                else if (Input.MouseDown(MouseButton.Right))
                {
                    _mouseEventArgs.Button = MouseButton.Right;
                    OnMouseDown(_mouseEventArgs);
                }
                else if (Input.MouseDown(MouseButton.Midle))
                {
                    _mouseEventArgs.Button = MouseButton.Midle;
                    OnMouseDown(new MouseEventArgs(MouseButton.Midle, Input.LastMouseState, Input.MouseState, gameTime));
                }
                else if (Input.MouseUp(MouseButton.Left) && _isMouseDown) OnMouseUp(_mouseEventArgs);
                else if (Input.MouseUp(MouseButton.Right) && _isMouseDown)
                {
                    _mouseEventArgs.Button = MouseButton.Right;
                    OnMouseUp(_mouseEventArgs);
                }
                else if (Input.MouseUp(MouseButton.Midle) && _isMouseDown)
                {
                    _mouseEventArgs.Button = MouseButton.Midle;
                    OnMouseUp(_mouseEventArgs);
                }
                else if (prevMouse != mouse) OnMouseMove(_mouseEventArgs);
                else if (!_isInputMouse) OnMouseInput(_mouseEventArgs);

                if (Input.LastMouseState.ScrollWheelValue != Input.MouseState.ScrollWheelValue)
                    OnScrollDelta(_mouseEventArgs);
            }
            else if (_isInputMouse) OnMouseLeave(_mouseEventArgs);
        }
        private void KeyboardUpdate(GameTime gameTime)
        {
            if (_focus != this) return;
            Keys[] lastStateKeys = Input.LastKeyboardState.GetPressedKeys();
            Keys[] currentStateKeys = Input.KeyboardState.GetPressedKeys();

            _keyEventArgs.IsShift = Input.KeyDown(Keys.LeftShift) || Input.KeyDown(Keys.RightShift);
            _keyEventArgs.GameTime = gameTime;
            _keyEventArgs.KeyState = KeyState.KeyDown;
            for (int i = 0; i < currentStateKeys.Length; i++)
            {
                if (lastStateKeys.IndexOfOnArray(currentStateKeys[i]) == -1)
                {
                    _keyEventArgs.KeyCode = currentStateKeys[i];
                    OnKeyDown(_keyEventArgs);
                }
                else
                {
                    _keyEventArgs.KeyCode = currentStateKeys[i];
                    OnKeyPresed(_keyEventArgs);
                }
            }

            _keyEventArgs.KeyState = KeyState.KeyUp;
            for (int i = 0; i < lastStateKeys.Length; i++)
            {
                if (currentStateKeys.IndexOfOnArray(lastStateKeys[i]) != -1) continue;
                _keyEventArgs.KeyCode = lastStateKeys[i];
                OnKeyUp(_keyEventArgs);
            }
        }
        private void Controls_ControlsAdded(DefaultLayuout sender, Control utilizingControl)
        {
            utilizingControl.ParentControl = this;
            utilizingControl.DrawabledLocation = DrawabledLocation;
            utilizingControl.Enabled = Enabled;
            utilizingControl.Visibled = Visibled;
        }
        private void Control_MouseMove(Control sender, MouseEventArgs e)
        {
            if (_isDraged && Input.MouseState.X != Input.LastMouseState.X
                || Input.LastMouseState.Y != Input.MouseState.Y)
                OnMouseDrag(e);
        }
        private void Control_MouseUp(Control sender, MouseEventArgs e)
        {
            if (_isDraged) _isDraged = false;
            if (_isClick && _focus == this)
            {
                _isClick = false;
                Click?.Invoke(this, e);
            }
            else _isClick = false;
        }
        private void Control_MouseDown(Control sender, MouseEventArgs e)
        {
            if (!_isDraged) _isDraged = true;
            Focused = true;
            _isClick = true;
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// Родитель, который хранит текущий контрол
        /// </summary>
        public IControl Parent => ParentControl;
        /// <summary>
        /// Имя Контрола, Используется для распознавания Уникальных Контролов
        /// </summary>
        [Category(PropertyGridCategoriesText.BasicCategory)] public string Name { get; set; }
        /// <summary>
        /// Контейнер для Контролов
        /// </summary>
        public IControlLayout Controls { get; }

        /// <summary>
        /// Позиция контрола
        /// </summary>
        [Category(PropertyGridCategoriesText.BasicCategory)] public Vector2 Location
        {
            get { return _l; }
            set
            {
                if (LockedTransformation || _l == value) return;
                _l = value;
                LocationChangeControl?.Invoke(this);
            }
        }
        /// <summary>
        /// Размеры контрола
        /// </summary>
        [Category(PropertyGridCategoriesText.BasicCategory)] public Vector2 Size
        {
            get { return _s; }
            set
            {
                if (LockedTransformation || _s == value) return;
                _s = value;
                ResizeControl?.Invoke(this);
            }
        }
        /// <summary>
        /// Глобальные координаты контрола, относительно окна
        /// </summary>
        [DisplayName("Location Drawable"), Category(PropertyGridCategoriesText.RedaOnlyCategory)]
        public Vector2 DrawabledLocation
        {
            get { return Location + _parrentLocation; }
            protected internal set { _parrentLocation = value; }
        }
        /// <summary>
        /// Глобальная Клиенская область контрола, относительно окна
        /// </summary>
        [DisplayName("Client Rectangle"), Category(PropertyGridCategoriesText.RedaOnlyCategory)]
        public Rectangle ClientRectangle => new RectangleF(DrawabledLocation.X, DrawabledLocation.Y, Size.X, Size.Y).ToRectangle();
        /// <summary>
        /// Клиенская область контрола
        /// </summary>
        [DisplayName("Client Size"), Category(PropertyGridCategoriesText.RedaOnlyCategory)]
        public RectangleF ClientSize => new RectangleF(0f, 0f, Size.X, Size.Y);
        /// <summary>
        /// Ли данный Контрол Содержит Фокус пользователя.
        /// </summary>
        [Category(PropertyGridCategoriesText.OtherCategory)] public bool Focused
        {
            get { return _focus == this; }
            set
            {
                if (value) _focus = this;
                else if (_focus == this) _focus = null;
                FocusableChanged?.Invoke(this);
            }
        }

        /// <summary>
        /// Ли должен рисоваться данный Объект
        /// </summary>
        [Category(PropertyGridCategoriesText.BasicCategory)] public bool Visibled
        {
            get { return _isDrawabled; }
            set
            {
                _isDrawabled = value;
                OnVisibleControlsSetter(value);
            }
        }
        /// <summary>
        /// Должны Ли Обрабатываться События Контрола
        /// </summary>
        [Category(PropertyGridCategoriesText.BasicCategory)] public bool Enabled
        {
            get { return _isEnabled; }
            set
            {
                _isEnabled = value;
                OnEnableControlsSetter(value);
            }
        }

        /// <summary>
        /// Пользовательское Свойство, для хранение пользовательских Данных
        /// </summary>
        [Category(PropertyGridCategoriesText.BasicCategory)] public object Tag { get; set; }

        /// <summary>
        /// Объект Графики, для рисования
        /// </summary>
        protected static Graphics Graphics { get; private set; }
        /// <summary>
        /// Форма, в которой находится контрол
        /// </summary>
        protected static IWindow Window { get; private set; }
        #endregion

        #region Internal Properties
        internal Control ParentControl
        {
            get { return _parentControl; }
            set
            {
                _parentControl = value;
                ParentChanged?.Invoke(this);
            }
        }
        /// <summary>
        /// Заблокировать возможность изменять Location и Size
        /// </summary>
        protected bool LockedTransformation { get; set; }
        /// <summary>
        /// Установить Блок на изменение размеров и позиции контрола.
        /// </summary>
        /// <param name="control">контрол, которому нужно установить локинг</param>
        /// <param name="value">значение локинга: true установлен, false убран</param>
        public static void SetControlLockedTransformation(Control control, bool value = true)
        {
            control.LockedTransformation = value;
        }
        #endregion

        #region Virtuals Events Methods
        /// <summary>
        /// Вызывает делегат нажатия кнопки мыши
        /// </summary>
        /// <param name="e"></param>
        protected internal virtual void OnMouseDown(MouseEventArgs e)
        {
            MouseDown?.Invoke(this, e);
            _isBlockedMouse = true;
            _isMouseDown = true;
        }
        /// <summary>
        /// Вызывает делегат отпускания кнопки мыши
        /// </summary>
        /// <param name="e"></param>
        protected internal virtual void OnMouseUp(MouseEventArgs e)
        {
            MouseUp?.Invoke(this, e);
            _isBlockedMouse = true;
            _isMouseDown = false;
        }
        /// <summary>
        /// Вызывает делегат движения мыши
        /// </summary>
        /// <param name="e"></param>
        protected internal virtual void OnMouseMove(MouseEventArgs e)
        {
            MouseMove?.Invoke(this, e);
            _isBlockedMouse = true;
        }
        /// <summary>
        /// Вызывает делегат движение мыши с зажатой кнопкой мыши
        /// </summary>
        /// <param name="e"></param>
        protected internal virtual void OnMouseDrag(MouseEventArgs e)
        {
            MouseDrag?.Invoke(this, e);
        }
        /// <summary>
        /// Вызывает делегат входа мыши в контрол
        /// </summary>
        /// <param name="e"></param>
        protected internal virtual void OnMouseInput(MouseEventArgs e)
        {
            MouseInput?.Invoke(this, e);
            _isInputMouse = true;
            _isBlockedMouse = true;
        }
        /// <summary>
        /// Вызывает делегат выхода мыши из контрола
        /// </summary>
        /// <param name="e"></param>
        protected internal virtual void OnMouseLeave(MouseEventArgs e)
        {
            MouseLeave?.Invoke(this, e);
            _isInputMouse = false;
        }
        /// <summary>
        /// Вызывает делегат колесика
        /// </summary>
        /// <param name="e"></param>
        protected internal virtual void OnScrollDelta(MouseEventArgs e)
        {
            ScrollDelta?.Invoke(this, e);
            _isBlockedMouse = true;
        }
        /// <summary>
        /// Вызывает делегат нажатия клавиши
        /// </summary>
        /// <param name="e"></param>
        protected internal virtual void OnKeyDown(KeyEventArgs e)
        {
            KeyDown?.Invoke(this, e);
            _isBlockedKeyBoard = true;
        }
        /// <summary>
        /// Вызывает делегат отпускания клавиши
        /// </summary>
        /// <param name="e"></param>
        protected internal virtual void OnKeyUp(KeyEventArgs e)
        {
            KeyUp?.Invoke(this, e);
            _isBlockedKeyBoard = true;
        }
        /// <summary>
        /// Вызывает делегат зажатия клавиши
        /// </summary>
        /// <param name="e"></param>
        protected internal virtual void OnKeyPresed(KeyEventArgs e)
        {
            KeyPresed?.Invoke(this, e);
            _isBlockedKeyBoard = true;
        }
        /// <summary>
        /// Вызывает делегат Обновления кадра Контрола
        /// </summary>
        /// <param name="e"></param>
        protected internal virtual void OnInvalidate(TickEventArgs e)
        {
            Invalidate?.Invoke(this, e);
        }
        /// <summary>
        /// Вызывает делегат Отрисовки кадра Контрола
        /// Внимание! Переопределив его, вы должны реализовать отрисовку внутрених Контролов самостоятельно.
        /// Что бы заставить рисоваться контролов нужно вызвать метод: InvokePaint();
        /// </summary>
        /// <param name="e"></param>
        protected internal virtual void OnPaint(TickEventArgs e)
        {
            Paint?.Invoke(this, e);
            for (int i = 0; i < Controls.Count; i++) Controls[i].Draw(e.GameTime);
        }

        /// <summary>
        /// Виртуальный метод для возможности переопределения своего алгоритма установки
        /// Свойства: Enabled своим детишкам
        /// </summary><param name="value"></param><returns></returns>
        protected virtual void OnEnableControlsSetter(bool value)
        {
            for (int i = 0; i < Controls.Count; i++) Controls[i].Enabled = value;
        }
        /// <summary>
        /// Виртуальный метод для возможности переопределения своего алгоритма установки
        /// Свойства: Visibled своим детишкам
        /// </summary><param name="value"></param><returns></returns>
        protected virtual void OnVisibleControlsSetter(bool value)
        {
            for (int i = 0; i < Controls.Count; i++) Controls[i].Visibled = value;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Принудительно Вызвать рисование у контрола
        /// Советуется этот метод использовать только тогда когда вы хотите переопределить отрисовку контрола и нужно заставить вызвать принудительно отрисовку
        /// Контрола
        /// </summary>
        /// <param name="gameTime"></param>
        public void InvokePaint(GameTime gameTime)
        {
            Draw(gameTime);
        }
        /// <summary>
        /// Принудительно вызвать Обновление контрола
        /// </summary>
        /// <param name="gameTime"></param>
        public void InvokeInvalidate(GameTime gameTime)
        {
            Update(gameTime);
        }
        /// <summary>
        /// Провести инициализацию контрола
        /// Внимание, данный метод выполняется автоматически внутри формы в его контейнере
        /// Если нужна повторная инициализация, потребуется повторный вызов.
        /// </summary>
        public virtual void Inicialize()
        {
            Controls.Sort();
            Enabled = Enabled;
            Visibled = Visibled;
        }
        #endregion
    }
}