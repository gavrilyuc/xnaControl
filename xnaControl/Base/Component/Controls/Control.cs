using System;
using System.Linq;
using Core.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Core.Base.Component.Controls
{
    /// <summary>
    /// Базовый Объект Контрола
    /// </summary>
    public class Control
    {
        #region Variables

        private bool _isDraged = false;
        private Vector2 _l, _s;
        private bool _isInputMouse = false;
        private bool _isEnabled = false;
        private bool _isDrawabled = false;

        private static Graphics _graphics;
        private static IWindow _window;
        private static bool _isBlockedMouse = false;
        private static bool _isBlockedKeyBoard = false;
        private static bool _isClick = false;
        private static Control _focus = null;
        private static bool _isMouseDown = false;
        private static readonly InputManager Input = InputManager.GetInstance;// Input (Mouse & KeyBoard) Manager.
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
            Controls = new GridControls(this);
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
        /// 
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
        #endregion

        #region Realise Event's & XNA Methods
        internal void Draw(GameTime gameTime)
        {
            _isBlockedMouse = false;
            _isBlockedKeyBoard = false;
            if (Drawabled == false) return;
            Paint(this, new TickEventArgs(Window, gameTime));
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

            if (!_isBlockedMouse) Click(gameTime);
            if (!_isBlockedKeyBoard) Key(gameTime);

            Invalidate(this, new TickEventArgs(Window, gameTime));
        }
        private void Click(GameTime gameTime)
        {
            if (_isBlockedMouse) return;
            Vector2 mouse = new Vector2(Input.MouseState.X, Input.MouseState.Y);
            Vector2 prevMouse = new Vector2(Input.LastMouseState.X, Input.LastMouseState.Y);
            bool isInputPos = new RectangleF(DrawabledLocation.X, DrawabledLocation.Y, Size.X, Size.Y).Contains(new Vector2(Input.MouseState.X, Input.MouseState.Y));
            MouseEventArgs eventArgs = new MouseEventArgs(MouseButton.Left, Input.LastMouseState, Input.MouseState);
            if (isInputPos)
            {
                #region Mouse Down
                if (Input.MouseDown(MouseButton.Left))
                {
                    MouseDown(this, eventArgs = new MouseEventArgs(MouseButton.Left, Input.LastMouseState, Input.MouseState));
                    _isBlockedMouse = true; _isMouseDown = true;
                }
                else if (Input.MouseDown(MouseButton.Right))
                {
                    MouseDown(this, eventArgs = new MouseEventArgs(MouseButton.Right, Input.LastMouseState, Input.MouseState));
                    _isBlockedMouse = true; _isMouseDown = true;
                }
                else if (Input.MouseDown(MouseButton.Midle))
                {
                    MouseDown(this, eventArgs = new MouseEventArgs(MouseButton.Midle, Input.LastMouseState, Input.MouseState));
                    _isBlockedMouse = true; _isMouseDown = true;
                }
                #endregion
                #region Mouse Up
                else if (Input.MouseUp(MouseButton.Left) && _isMouseDown)
                {
                    MouseUp(this, eventArgs = new MouseEventArgs(MouseButton.Left, Input.LastMouseState, Input.MouseState));
                    _isBlockedMouse = true; _isMouseDown = false;
                }
                else if (Input.MouseUp(MouseButton.Right) && _isMouseDown)
                {
                    MouseUp(this, eventArgs = new MouseEventArgs(MouseButton.Right, Input.LastMouseState, Input.MouseState));
                    _isBlockedMouse = true; _isMouseDown = false;
                }
                else if (Input.MouseUp(MouseButton.Midle) && _isMouseDown)
                {
                    MouseUp(this, eventArgs = new MouseEventArgs(MouseButton.Midle, Input.LastMouseState, Input.MouseState));
                    _isBlockedMouse = true; _isMouseDown = false;
                }
                #endregion
                else if (prevMouse != mouse)
                {
                    MouseMove(this, eventArgs);
                    _isBlockedMouse = true;
                }
                else if (!_isInputMouse)
                {
                    MouseInput(this, eventArgs);
                    _isInputMouse = true;
                    _isBlockedMouse = true;
                }
                if (Input.LastMouseState.ScrollWheelValue == Input.MouseState.ScrollWheelValue) return;
                ScrollDelta(this, eventArgs);
                _isBlockedMouse = true;
            }
            else if (_isInputMouse)
            {
                MouseLeave(this, eventArgs);
                _isInputMouse = false;
            }
        }
        private void Key(GameTime gameTime)
        {
            if (_focus != this) return;
            var toList = Input.LastKeyboardState.GetPressedKeys().ToList();
            bool isShift = Input.KeyDown(Keys.LeftShift) || Input.KeyDown(Keys.RightShift);
            foreach (var ch in Input.KeyboardState.GetPressedKeys())
            {
                if (toList.IndexOf(ch) == -1)
                {
                    KeyDown(this, new KeyEventArgs(ch, KeyState.KeyDown, gameTime, isShift));
                    _isBlockedKeyBoard = true;
                }
                else
                {
                    KeyPresed(this, new KeyEventArgs(ch, KeyState.KeyDown, gameTime, isShift));
                    _isBlockedKeyBoard = true;
                }
            }
            toList = Input.KeyboardState.GetPressedKeys().ToList();
            foreach (var ch in Input.LastKeyboardState.GetPressedKeys().Where(ch => toList.IndexOf(ch) == -1))
            {
                KeyUp(this, new KeyEventArgs(ch, KeyState.KeyUp, gameTime, isShift));
                _isBlockedKeyBoard = true;
            }
        }
        private void Controls_ControlsAdded(GridControls sender, GridEventArgs e)
        {
            e.UtilizingControl.ParrentLocation = Location;
        }
        private void Control_MouseMove(Control sender, MouseEventArgs e)
        {
            if (_isDraged && Input.MouseState.X != Input.LastMouseState.X || Input.LastMouseState.Y != Input.MouseState.Y)
                MouseDrag(this, e);
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

        #region Properties
        /// <summary>
        /// Имя Контрола, Используется для распознавания Уникальных Контролов
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Контейнер для Контролов
        /// </summary>
        public GridControls Controls { get; }
        /// <summary>
        /// Позиция контрола
        /// </summary>
        public Vector2 Location
        {
            get { return _l; }
            set
            {
                _l = value;
                LocationChangeControl(this, new EventArgs());
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
                ResizeControl(this, new EventArgs());
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
    }
}