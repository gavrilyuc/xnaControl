using System.Linq;
using Core.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using Core.Base.Component.Layout;

namespace Core.Base.Component.Controls
{
    /// <summary>
    /// Базовый Объект Контрола
    /// </summary>
    public class Control : IControl
    {
        #region Variables
        private bool _isDraged;
        private Vector2 _l, _s;
        private bool _isInputMouse;
        private bool _isEnabled;
        private bool _isDrawabled;
        private Control _parentControl;

        private static Graphics _graphics;
        private static IWindow _window;
        private static bool _isBlockedMouse;
        private static bool _isBlockedKeyBoard;
        private static bool _isClick;
        private static Control _focus;
        private static bool _isMouseDown;
        private static readonly PkInputManager Input = PkInputManager.GetInstance;// Input (Mouse & KeyBoard) Manager.
        #endregion

        #region Constructor
        /// <summary>
        /// Констрктор для Базовой Инициализации
        /// </summary>
        /// <param name="window">Окно в котором будут Отображаться Контролы</param>
        internal Control(IWindow window) : this()
        {
            if (Graphics == null) Graphics = window.Graphics2D;
            if (Window == null) Window = window;
        }
        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        public Control()
        {
            Controls = new DefaultLayuout(this);
            MouseDown += Control_MouseDown;
            MouseUp += Control_MouseUp;
            MouseMove += Control_MouseMove;
            Drawabled = true;
            Enabled = true;
            Focused = false;
            Location = Vector2.Zero;
            Size = Vector2.Zero;
            Controls.ControlsAdded += Controls_ControlsAdded;
        }
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
            Drawabled = true;
            Enabled = true;
            Focused = false;
            Location = Vector2.Zero;
            Size = Vector2.Zero;
            Controls.ControlsAdded += Controls_ControlsAdded;
        }
        #endregion

        #region Event's
        /// <summary>
        /// Задатие мыши на объекту
        /// </summary>
        public event MouseEventHandler MouseDown = delegate { };
        /// <summary>
        /// Отпускание мыши на объекту
        /// </summary>
        public event MouseEventHandler MouseUp = delegate { };
        /// <summary>
        /// Нажатие мыши на объекту
        /// </summary>
        public event MouseEventHandler MouseClick = delegate { };
        /// <summary>
        /// Вызывается когда происходит зажатое движение мыши
        /// </summary>
        public event MouseEventHandler MouseDrag = delegate { };
        /// <summary>
        /// Движение мыши на объекту
        /// </summary>
        public event MouseEventHandler MouseMove = delegate { };
        /// <summary>
        /// Вхождение мыши в Объект
        /// </summary>
        public event MouseEventHandler MouseInput = delegate { };
        /// <summary>
        /// Выход мыши из Объекта
        /// </summary>
        public event MouseEventHandler MouseLeave = delegate { };
        /// <summary>
        /// Скроллинг мыши (Колесико)
        /// </summary>
        public event MouseEventHandler ScrollDelta = delegate { };
        /// <summary>
        /// Нажатие Кнопки
        /// </summary>
        public event KeyEventHandler KeyDown = delegate { };
        /// <summary>
        /// Отпускание Кнопки
        /// </summary>
        public event KeyEventHandler KeyUp = delegate { };
        /// <summary>
        /// Вызывается когда Кнопка зажата(и не отпускается)
        /// </summary>
        public event KeyEventHandler KeyPresed = delegate { };
        /// <summary>
        /// Вызывается вместе с методом Draw
        /// </summary>
        public event TickEventHandler Paint = delegate { };
        /// <summary>
        /// Вызывается вместе с Методом Update
        /// </summary>
        public event TickEventHandler Invalidate = delegate { };
        /// <summary>
        /// Вызывается когда изменяется размеры контрола
        /// </summary>
        public event EventHandler ResizeControl = delegate { };
        /// <summary>
        /// Вызывается когда изменяется позиция контрола
        /// </summary>
        public event EventHandler LocationChangeControl = delegate { };
        /// <summary>
        /// Вызывается когда изменяется контейнер у контрола
        /// </summary>
        public event EventHandler ParentChanged = delegate { };
        #endregion

        #region Realise Event's & XNA Methods
        internal void Draw(GameTime gameTime)
        {
            _isBlockedMouse = false;
            _isBlockedKeyBoard = false;
            if (Drawabled == false) return;
            OnPaint(new TickEventArgs(Window, gameTime));
            for (int i = 0; i < Controls.Count; i++) Controls[i].Draw(gameTime);
        }
        internal void Update(GameTime gameTime)
        {
            if (Enabled == false) return;
            Control item = null;
            for (int i = Controls.Count - 1; i >= 0; i--)
            {
                item = Controls[i];
                if (item.ParrentLocation != ParrentLocation) item.ParrentLocation = DrawabledLocation;
                item.Enabled = Enabled;
                item.Drawabled = Drawabled;

                item.Update(gameTime);
            }

            if (!_isBlockedMouse) Click();
            if (!_isBlockedKeyBoard) Key(gameTime);

            OnInvalidate(new TickEventArgs(Window, gameTime));
        }
        private void Click()
        {
            if (_isBlockedMouse) return;
            Vector2 mouse = new Vector2(Input.MouseState.X, Input.MouseState.Y);
            Vector2 prevMouse = new Vector2(Input.LastMouseState.X, Input.LastMouseState.Y);
            bool isInputPos = new RectangleF(DrawabledLocation.X, DrawabledLocation.Y, Size.X, Size.Y).Contains(new Vector2(Input.MouseState.X, Input.MouseState.Y));
            MouseEventArgs eventArgs = new MouseEventArgs(MouseButton.Left, Input.LastMouseState, Input.MouseState);
            if (isInputPos)
            {
                if (Input.MouseDown(MouseButton.Left))
                    OnMouseDown(new MouseEventArgs(MouseButton.Left, Input.LastMouseState, Input.MouseState));
                else if (Input.MouseDown(MouseButton.Right))
                    OnMouseDown(new MouseEventArgs(MouseButton.Right, Input.LastMouseState, Input.MouseState));
                else if (Input.MouseDown(MouseButton.Midle))
                    OnMouseDown(new MouseEventArgs(MouseButton.Midle, Input.LastMouseState, Input.MouseState));
                else if (Input.MouseUp(MouseButton.Left) && _isMouseDown)
                    OnMouseUp(new MouseEventArgs(MouseButton.Left, Input.LastMouseState, Input.MouseState));
                else if (Input.MouseUp(MouseButton.Right) && _isMouseDown)
                    OnMouseUp(new MouseEventArgs(MouseButton.Right, Input.LastMouseState, Input.MouseState));
                else if (Input.MouseUp(MouseButton.Midle) && _isMouseDown)
                    OnMouseUp(new MouseEventArgs(MouseButton.Midle, Input.LastMouseState, Input.MouseState));
                else if (prevMouse != mouse) OnMouseMove(eventArgs);
                else if (!_isInputMouse) OnMouseInput(eventArgs);

                if (Input.LastMouseState.ScrollWheelValue != Input.MouseState.ScrollWheelValue)
                    OnScrollDelta(eventArgs);
            }
            else if (_isInputMouse) OnMouseLeave(eventArgs);
        }
        private void Key(GameTime gameTime)
        {
            if (_focus != this) return;
            List<Keys> toList = Input.LastKeyboardState.GetPressedKeys().ToList();
            bool isShift = Input.KeyDown(Keys.LeftShift) || Input.KeyDown(Keys.RightShift);
            foreach (Keys ch in Input.KeyboardState.GetPressedKeys())
            {
                if (toList.IndexOf(ch) == -1) OnKeyDown(new KeyEventArgs(ch, KeyState.KeyDown, gameTime, isShift));
                else OnKeyPresed(new KeyEventArgs(ch, KeyState.KeyDown, gameTime, isShift));
            }
            toList = Input.KeyboardState.GetPressedKeys().ToList();
            foreach (Keys ch in Input.LastKeyboardState.GetPressedKeys().Where(ch => toList.IndexOf(ch) == -1))
                OnKeyUp(new KeyEventArgs(ch, KeyState.KeyUp, gameTime, isShift));
        }
        private void Controls_ControlsAdded(DefaultLayuout sender, GridEventArgs e)
        {
            e.UtilizingControl.ParrentLocation = Location;
        }
        private void Control_MouseMove(Control sender, MouseEventArgs e)
        {
            if (_isDraged && Input.MouseState.X != Input.LastMouseState.X || Input.LastMouseState.Y != Input.MouseState.Y)
                OnMouseDrag(e);
        }
        private void Control_MouseUp(Control sender, MouseEventArgs e)
        {
            if (_isDraged) _isDraged = false;
            if (_isClick && _focus == this)
            {
                _isClick = false;
                MouseClick(this, e);
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
        public string Name { get; set; }
        /// <summary>
        /// Контейнер для Контролов
        /// </summary>
        public IControlLayout Controls { get; }
        /// <summary>
        /// Позиция контрола
        /// </summary>
        public Vector2 Location
        {
            get { return _l; }
            set
            {
                _l = value;
                LocationChangeControl(this);
            }
        }
        /// <summary>
        /// Размеры контрола
        /// </summary>
        public Vector2 Size
        {
            get { return _s; }
            set
            {
                _s = value;
                ResizeControl(this);
            }
        }
        private Vector2 ParrentLocation { get; set; }
        /// <summary>
        /// Корректые координаты контрола, относительно окна
        /// </summary>
        protected Vector2 DrawabledLocation => Location + ParrentLocation;
        /// <summary>
        /// Глобальная Клиенская область контрола
        /// </summary>
        protected Rectangle ClientRectangle => new RectangleF(DrawabledLocation.X, DrawabledLocation.Y, Size.X, Size.Y).ToRectangle();
        /// <summary>
        /// Клиенская область контрола
        /// </summary>
        public RectangleF ClientSize => new RectangleF(0f, 0f, Size.X, Size.Y);
        /// <summary>
        /// Ли данный Контрол Содержит Фокус пользователя.
        /// </summary>
        public bool Focused
        {
            get { return _focus == this; }
            set
            {
                if (value) _focus = this;
                else if (_focus == this) _focus = null;
            }
        }
        /// <summary>
        /// Ли должен рисоваться данный Объект
        /// </summary>
        public bool Drawabled
        {
            get { return _isDrawabled; }
            set
            {
                _isDrawabled = value;
                for (int i = 0; i < Controls.Count; i++) Controls[i].Drawabled = value;
            }
        }
        /// <summary>
        /// Должны Ли Обрабатываться События Контрола
        /// </summary>
        public bool Enabled
        {
            get { return _isEnabled; }
            set
            {
                _isEnabled = value;
                for (int i = 0; i < Controls.Count; i++) Controls[i].Enabled = value;
            }
        }
        /// <summary>
        /// Пользовательское Свойство, для хранение пользовательских Данных
        /// </summary>
        public object Tag { get; set; }
        /// <summary>
        /// Объект Графики, для рисования
        /// </summary>
        protected Graphics Graphics
        {
            get { return _graphics; }
            private set { _graphics = value; }
        }
        /// <summary>
        /// Форма, в которой находится контрол
        /// </summary>
        protected IWindow Window
        {
            get { return _window; }
            private set { _window = value; }
        }
        #endregion

        #region Internal Properties
        internal Control ParentControl
        {
            get { return _parentControl; }
            set
            {
                _parentControl = value;
                ParentChanged(this);
            }
        }
        #endregion

        #region Virtuals Events Methods
        protected virtual void OnMouseDown(MouseEventArgs e)
        {
            MouseDown(this, e);
            _isBlockedMouse = true;
            _isMouseDown = true;
        }
        protected virtual void OnMouseUp(MouseEventArgs e)
        {
            MouseUp(this, e);
            _isBlockedMouse = true;
            _isMouseDown = false;
        }
        protected virtual void OnMouseMove(MouseEventArgs e)
        {
            MouseDown(this, e);
            _isBlockedMouse = true;
        }
        protected virtual void OnMouseDrag(MouseEventArgs e)
        {
            MouseDrag(this, e);
        }
        protected virtual void OnMouseInput(MouseEventArgs e)
        {
            MouseInput(this, e);
            _isInputMouse = true;
            _isBlockedMouse = true;
        }
        protected virtual void OnMouseLeave(MouseEventArgs e)
        {
            MouseLeave(this, e);
            _isInputMouse = false;
        }
        protected virtual void OnScrollDelta(MouseEventArgs e)
        {
            ScrollDelta(this, e);
            _isBlockedMouse = true;
        }

        protected virtual void OnKeyDown(KeyEventArgs e)
        {
            KeyDown(this, e);
            _isBlockedKeyBoard = true;
        }
        protected virtual void OnKeyUp(KeyEventArgs e)
        {
            KeyUp(this, e);
            _isBlockedKeyBoard = true;
        }
        protected virtual void OnKeyPresed(KeyEventArgs e)
        {
            KeyPresed(this, e);
            _isBlockedKeyBoard = true;
        }

        protected virtual void OnInvalidate(TickEventArgs e)
        {
            Invalidate(this, e);
        }
        protected virtual void OnPaint(TickEventArgs e)
        {
            Paint(this, e);
        }
        #endregion
    }
}