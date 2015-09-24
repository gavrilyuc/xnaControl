using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Base.Component
{
    public class Control
    {
        #region Variables
        private GridControls __controls;
        private bool __is_draged = false;
        private Vector2 __l, __s;
        private bool __is_input_mouse = false;
        private bool __is_enabled = false;
        private bool __is_drawabled = false;

        private static Graphics __graphics;
        private static IWindow __window;
        private static bool __is_blocked_mouse = false;
        private static bool __is_blocked_keyBoard = false;
        private static bool __isClick = false;
        private static Control __focus = null;
        private static bool __is_mouse_down = false;
        private static InputManager __input = InputManager.GetInstance;// Input (Mouse & KeyBoard) Manager.
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
            __controls = new GridControls(this);
            this.MouseDown += Control_MouseDown;
            this.MouseUp += Control_MouseUp;
            this.MouseMove += Control_MouseMove;
            this.Drawabled = true;
            this.Enabled = true;
            this.Focused = false;
            this.Location = Vector2.Zero;
            this.Size = Vector2.Zero;
            this.Controls.ControlsAdded += Controls_ControlsAdded;
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
            __is_blocked_mouse = false;
            __is_blocked_keyBoard = false;
            if (this.Drawabled == false) return;
            Paint(this, new TickEventArgs(Window, gameTime));
            for (int i = 0; i < this.Controls.Count; i++) this.Controls[i].Draw(gameTime);
        }
        internal void Update(GameTime gameTime)
        {
            if (this.Enabled == false) return;
            Control item = null;

            for (int i = this.Controls.Count - 1; i >= 0; i--)
            {
                item = this.Controls[i];
                if (item.ParrentLocation != this.ParrentLocation) item.ParrentLocation = this.DrawabledLocation;

                item.Enabled = this.Enabled;
                item.Drawabled = this.Drawabled;

                item.Update(gameTime);
            }

            if (!__is_blocked_mouse) click(gameTime);
            if (!__is_blocked_keyBoard) key(gameTime);

            Invalidate(this, new TickEventArgs(Window, gameTime));
        }
        private void click(GameTime gameTime)
        {
            if (__is_blocked_mouse) return;
            Vector2 mouse = new Vector2(__input.MouseState.X, __input.MouseState.Y);
            Vector2 prev_mouse = new Vector2(__input.LastMouseState.X, __input.LastMouseState.Y);
            bool is_input_pos = new RectangleF(DrawabledLocation.X, DrawabledLocation.Y, Size.X, Size.Y).Contains(new Vector2(__input.MouseState.X, __input.MouseState.Y));
            MouseEventArgs eventArgs = new MouseEventArgs(MouseButton.Left, __input.LastMouseState, __input.MouseState);
            if (is_input_pos)
            {
                #region Mouse Down
                if (__input.MouseDown(MouseButton.Left))
                {
                    this.MouseDown(this, eventArgs = new MouseEventArgs(MouseButton.Left, __input.LastMouseState, __input.MouseState));
                    __is_blocked_mouse = true; __is_mouse_down = true;
                }
                else if (__input.MouseDown(MouseButton.Right))
                {
                    this.MouseDown(this, eventArgs = new MouseEventArgs(MouseButton.Right, __input.LastMouseState, __input.MouseState));
                    __is_blocked_mouse = true; __is_mouse_down = true;
                }
                else if (__input.MouseDown(MouseButton.Midle))
                {
                    this.MouseDown(this, eventArgs = new MouseEventArgs(MouseButton.Midle, __input.LastMouseState, __input.MouseState));
                    __is_blocked_mouse = true; __is_mouse_down = true;
                }
                #endregion
                #region Mouse Up
                else if (__input.MouseUp(MouseButton.Left) && __is_mouse_down)
                {
                    this.MouseUp(this, eventArgs = new MouseEventArgs(MouseButton.Left, __input.LastMouseState, __input.MouseState));
                    __is_blocked_mouse = true; __is_mouse_down = false;
                }
                else if (__input.MouseUp(MouseButton.Right) && __is_mouse_down)
                {
                    this.MouseUp(this, eventArgs = new MouseEventArgs(MouseButton.Right, __input.LastMouseState, __input.MouseState));
                    __is_blocked_mouse = true; __is_mouse_down = false;
                }
                else if (__input.MouseUp(MouseButton.Midle) && __is_mouse_down)
                {
                    this.MouseUp(this, eventArgs = new MouseEventArgs(MouseButton.Midle, __input.LastMouseState, __input.MouseState));
                    __is_blocked_mouse = true; __is_mouse_down = false;
                }
                #endregion
                else if (prev_mouse != mouse)
                {
                    this.MouseMove(this, eventArgs);
                    __is_blocked_mouse = true;
                }
                else if (!__is_input_mouse)
                {
                    this.MouseInput(this, eventArgs);
                    __is_input_mouse = true;
                    __is_blocked_mouse = true;
                }
                if (__input.LastMouseState.ScrollWheelValue != __input.MouseState.ScrollWheelValue)
                {
                    this.ScrollDelta(this, eventArgs);
                    __is_blocked_mouse = true;
                }
            }
            else if (__is_input_mouse)
            {
                this.MouseLeave(this, eventArgs);
                __is_input_mouse = false;
            }
        }
        private void key(GameTime gameTime)
        {
            if (__focus == this)
            {
                var toList = __input.LastKeyboardState.GetPressedKeys().ToList();
                bool is_shift = __input.KeyDown(Keys.LeftShift) ? true : __input.KeyDown(Keys.RightShift);
                foreach (var ch in __input.KeyboardState.GetPressedKeys())
                {
                    if (toList.IndexOf(ch) == -1)
                    {
                        this.KeyDown(this, new KeyEventArgs(ch, KeyState.KeyDown, gameTime, is_shift));
                        __is_blocked_keyBoard = true;
                    }
                    else
                    {
                        this.KeyPresed(this, new KeyEventArgs(ch, KeyState.KeyDown, gameTime, is_shift));
                        __is_blocked_keyBoard = true;
                    }
                }
                toList = __input.KeyboardState.GetPressedKeys().ToList();
                foreach (var ch in __input.LastKeyboardState.GetPressedKeys())
                {
                    if (toList.IndexOf(ch) == -1)
                    {
                        this.KeyUp(this, new KeyEventArgs(ch, KeyState.KeyUp, gameTime, is_shift));
                        __is_blocked_keyBoard = true;
                    }
                }
            }
        }
        private void Controls_ControlsAdded(GridControls sender, GridEventArgs e)
        {
            e.UtilizingControl.ParrentLocation = this.Location;
        }
        private void Control_MouseMove(Control sender, MouseEventArgs e)
        {
            if (__is_draged && __input.MouseState.X != __input.LastMouseState.X || __input.LastMouseState.Y != __input.MouseState.Y)
                this.MouseDrag(this, e);
        }
        private void Control_MouseUp(Control sender, MouseEventArgs e)
        {
            if (__is_draged) __is_draged = false;
            if (__isClick && __focus == this)
            {
                __isClick = false;
                this.MouseClick(this, e);
            }
            else
            {
                __isClick = false;
            }
        }
        private void Control_MouseDown(Control sender, MouseEventArgs e)
        {
            if (!__is_draged) __is_draged = true;
            this.Focused = true;
            __isClick = true;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Имя Контрола, Используется для распознавания Уникальных Контролов
        /// </summary>
        public String Name { get; set; }
        /// <summary>
        /// Контейнер для Контролов
        /// </summary>
        public GridControls Controls { get { return this.__controls; } }
        /// <summary>
        /// Позиция контрола
        /// </summary>
        public Vector2 Location
        {
            get { return __l; }
            set
            {
                __l = value;
                LocationChangeControl(this, new EventArgs());
            }
        }
        /// <summary>
        /// Размеры контрола
        /// </summary>
        public Vector2 Size
        {
            get { return __s; }
            set
            {
                __s = value;
                ResizeControl(this, new EventArgs());
            }
        }
        private Vector2 ParrentLocation { get; set; }
        /// <summary>
        /// Корректые координаты контрола, относительно окна
        /// </summary>
        protected Vector2 DrawabledLocation { get { return Location + ParrentLocation; } }
        /// <summary>
        /// Глобальная Клиенская область контрола
        /// </summary>
        protected Rectangle ClientRectangle
        {
            get
            {
                return new RectangleF(this.DrawabledLocation.X, this.DrawabledLocation.Y, this.Size.X, this.Size.Y).toRectangle();
            }
        }
        /// <summary>
        /// Клиенская область контрола
        /// </summary>
        public RectangleF ClientSize { get { return new RectangleF(0f, 0f, Size.X, Size.Y); } }
        /// <summary>
        /// Ли данный Контрол Содержит Фокус пользователя.
        /// </summary>
        public Boolean Focused
        {
            get { return __focus == this; }
            set
            {
                if (value) __focus = this;
                else if (__focus == this) __focus = null;
            }
        }
        /// <summary>
        /// Ли должен рисоваться данный Объект
        /// </summary>
        public Boolean Drawabled
        {
            get { return __is_drawabled; }
            set
            {
                __is_drawabled = value;
                for (int i = 0; i < this.Controls.Count; i++) this.Controls[i].Drawabled = value;
            }
        }
        /// <summary>
        /// Должны Ли Обрабатываться События Контрола
        /// </summary>
        public Boolean Enabled
        {
            get { return __is_enabled; }
            set
            {
                __is_enabled = value;
                for (int i = 0; i < this.Controls.Count; i++) this.Controls[i].Enabled = value;
            }
        }
        /// <summary>
        /// Пользовательское Свойство, для хранение пользовательских Данных
        /// </summary>
        public Object Tag { get; set; }
        /// <summary>
        /// Объект Графики, для рисования
        /// </summary>
        protected Graphics Graphics
        {
            get { return Control.__graphics; }
            private set { Control.__graphics = value; }
        }
        /// <summary>
        /// Форма, в которой находится контрол
        /// </summary>
        protected IWindow Window
        {
            get { return Control.__window; }
            private set { Control.__window = value; }
        }
        #endregion
    }
}