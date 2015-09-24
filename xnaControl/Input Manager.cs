using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Base.Component
{
    public class InputManager : ISingelton<InputManager>
    {
        private static MouseState curMouse, prevMouse;
        private static KeyboardState keyboardState, lastKeyboardState;
        public KeyboardState KeyboardState { get { return keyboardState; } }
        public KeyboardState LastKeyboardState { get { return lastKeyboardState; } }
        public MouseState MouseState { get { return curMouse; } }
        public MouseState LastMouseState { get { return prevMouse; } }

        public InputManager()
        {
            keyboardState = Keyboard.GetState();
            curMouse = Mouse.GetState();
        }

        internal void Update(GameTime gameTime)
        {
            MouseFlush();
            Flush();
        }

        public void Flush()
        {
            lastKeyboardState = keyboardState;
            keyboardState = Keyboard.GetState();
        }
        public void MouseFlush()
        {
            prevMouse = curMouse;
            curMouse = Mouse.GetState();
        }

        /// <summary>
        /// возращяет ли была отпущена кнопка
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool KeyReleased(Keys key)
        {
            return keyboardState.IsKeyUp(key) &&
                lastKeyboardState.IsKeyDown(key);
        }
        /// <summary>
        /// возращяет ли была нажата и отпущена кнопка
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool KeyPressed(Keys key)
        {
            return keyboardState.IsKeyDown(key) &&
                lastKeyboardState.IsKeyUp(key);
        }
        /// <summary>
        /// возращяет ли зажата кнопка
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool KeyDown(Keys key)
        {
            return keyboardState.IsKeyDown(key);
        }
        /// <summary>
        /// Ли был Клик
        /// </summary>
        /// <param name="type">расматриваемая кнопка мышьи</param>
        /// <returns></returns>
        public bool MouseClick(MouseButton type)
        {
            if (MouseDown(type) && _MouseUp(type, LastMouseState)) return true;
            return false;
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
                case MouseButton.Left:
                    if (MouseState.LeftButton == ButtonState.Pressed) return true;
                    return false;
                case MouseButton.Right:
                    if (MouseState.RightButton == ButtonState.Pressed) return true;
                    return false;
                case MouseButton.Midle:
                    if (MouseState.MiddleButton == ButtonState.Pressed) return true;
                    return false;
            }
            return false;
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
        bool _MouseUp(MouseButton type, MouseState state)
        {
            switch (type)
            {
                case MouseButton.Left:
                    if (state.LeftButton == ButtonState.Released) return true;
                    return false;
                case MouseButton.Right:
                    if (state.RightButton == ButtonState.Released) return true;
                    return false;
                case MouseButton.Midle:
                    if (state.MiddleButton == ButtonState.Released) return true;
                    return false;
            }
            return false;
        }


        public bool isKeyDown(Keys key)
        {
            return this.KeyboardState.IsKeyDown(key);
        }
        public bool isKeyUp(Keys key)
        {
            return this.LastKeyboardState.IsKeyDown(key) && this.KeyboardState.IsKeyUp(key);
        }
        public Keys[] getPresedKeys()
        {
            return KeyboardState.GetPressedKeys();
        }
    }
}
