using FormControl.Component;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace FormControl.Input
{
    /// <summary>
    /// Менеджер Входных устройств для ПК
    /// </summary>
    public class PkInputManager : Singelton<PkInputManager>
    {
        private static MouseState _curMouse, _prevMouse;
        private static KeyboardState _keyboardState, _lastKeyboardState;
        /// <summary>
        /// Текущее Клавиатурное состояние
        /// </summary>
        public KeyboardState KeyboardState => _keyboardState;
        /// <summary>
        /// Предыдущее Клавиатурное состояние
        /// </summary>
        public KeyboardState LastKeyboardState => _lastKeyboardState;
        /// <summary>
        /// Текущее состояние мыши
        /// </summary>
        public MouseState MouseState => _curMouse;
        /// <summary>
        /// Предыдущее состояние мыши
        /// </summary>
        public MouseState LastMouseState => _prevMouse;

        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        public PkInputManager()
        {
            _keyboardState = Keyboard.GetState();
            _curMouse = Mouse.GetState();
        }

        internal void Update(GameTime gameTime)
        {
            MouseFlush();
            Flush();
        }
        /// <summary>
        /// Обновить клавиатурное состояние
        /// </summary>
        public void Flush()
        {
            _lastKeyboardState = _keyboardState;
            _keyboardState = Keyboard.GetState();
        }
        /// <summary>
        /// Обновить состояние мыши
        /// </summary>
        public void MouseFlush()
        {
            _prevMouse = _curMouse;
            _curMouse = Mouse.GetState();
        }

        /// <summary>
        /// возращяет ли была отпущена кнопка
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool KeyReleased(Keys key)
        {
            return _keyboardState.IsKeyUp(key) &&
                _lastKeyboardState.IsKeyDown(key);
        }
        /// <summary>
        /// возращяет ли была нажата и отпущена кнопка
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool KeyPressed(Keys key)
        {
            return _keyboardState.IsKeyDown(key) &&
                _lastKeyboardState.IsKeyUp(key);
        }
        /// <summary>
        /// возращяет ли зажата кнопка
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool KeyDown(Keys key)
        {
            return _keyboardState.IsKeyDown(key);
        }
        /// <summary>
        /// Ли был Клик
        /// </summary>
        /// <param name="type">расматриваемая кнопка мышьи</param>
        /// <returns></returns>
        public bool MouseClick(MouseButton type)
        {
            return MouseDown(type) && _MouseUp(type, LastMouseState);
        }
        /// <summary>
        /// Ли кнопка зажата
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public bool MouseDown(MouseButton type)
        {
            switch (type)
            {
                case MouseButton.Left: return MouseState.LeftButton == ButtonState.Pressed;
                case MouseButton.Right: return MouseState.RightButton == ButtonState.Pressed;
                case MouseButton.Midle: return MouseState.MiddleButton == ButtonState.Pressed;
                default: return false;
            }
        }
        /// <summary>
        /// Ли кнопка отпущена
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public bool MouseUp(MouseButton type)
        {
            return _MouseUp(type, MouseState);
        }
        private static bool _MouseUp(MouseButton type, MouseState state)
        {
            switch (type)
            {
                case MouseButton.Left: return state.LeftButton == ButtonState.Released;
                case MouseButton.Right: return state.RightButton == ButtonState.Released;
                case MouseButton.Midle: return state.MiddleButton == ButtonState.Released;
                default: return false;
            }
        }
        /// <summary>
        /// нажата ли кнопка
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool IsKeyDown(Keys key) => KeyboardState.IsKeyDown(key);
        /// <summary>
        /// Отпущена ли кнопка
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool IsKeyUp(Keys key) => LastKeyboardState.IsKeyDown(key) && KeyboardState.IsKeyUp(key);
        /// <summary>
        /// Получить список нажатых кнопок
        /// </summary>
        /// <returns></returns>
        public Keys[] GetPresedKeys() => KeyboardState.GetPressedKeys();
    }
}
