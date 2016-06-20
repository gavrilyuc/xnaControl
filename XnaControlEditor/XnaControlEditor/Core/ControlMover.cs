using System;
using System.Runtime.Remoting.Channels;
using FormControl.Component;
using FormControl.Component.Controls;
using Microsoft.Xna.Framework;

namespace XnaControlEditor.Core
{
    [Flags]
    public enum TypeMover : byte
    {
        Move = 1,
        Resize,

        None = 0
    }

    public class ControlMover
    {
        private static Vector2 _cursorStartPoint;

        private static bool _resizing;
        private static bool _moving;
        private static bool _mouseIsInLeftEdge;
        private static bool _mouseIsInRightEdge;
        private static bool _mouseIsInTopEdge;
        private static bool _mouseIsInBottomEdge;

        #region Main Function Set Moves and Resize Mode From Control
        public static void SetResizeMode(Control control, Control rootControl, TypeMover workType)
        {
            _moving = false;
            _resizing = false;
            _cursorStartPoint = Vector2.Zero;
            _mouseIsInLeftEdge = false;
            _mouseIsInLeftEdge = false;
            _mouseIsInRightEdge = false;
            _mouseIsInTopEdge = false;
            _mouseIsInBottomEdge = false;
            Init(control, rootControl, workType);
        }
        private static void Init(IControl control, Control rootControl, TypeMover workType)
        {
            control.MouseDown += (sender, e) => MouseDown(rootControl, e, workType);
            control.MouseUp += (sender, e) => MouseUp(rootControl, workType);
            control.MouseMove += (sender, e) => MouseMove(rootControl, e, workType);
        }
        #endregion

        #region Cursor
        const int Border = 10;
        private static void UpdateMouseEdgeProperties(Control control, Vector2 mouseLocationInControl, TypeMover workType)
        {
            _mouseIsInLeftEdge = Math.Abs(_cursorStartPoint.X) <= Border;
            _mouseIsInRightEdge = Math.Abs(_cursorStartPoint.X - control.Width) <= Border;
            _mouseIsInTopEdge = Math.Abs(_cursorStartPoint.Y) <= Border;
            _mouseIsInBottomEdge = Math.Abs(_cursorStartPoint.Y - control.Height) <= Border;
        }
        private static void UpdateMouseCursor(Control control, TypeMover workType)
        {
            //if (workType == TypeMover.Move)
            //{
            //    control.Cursor = _moving ? Cursors.Hand : Cursors.Default;
            //    return;
            //}
            //if (_mouseIsInLeftEdge)
            //{
            //    if (_mouseIsInTopEdge) control.Cursor = Cursors.SizeNWSE;
            //    else if (_mouseIsInBottomEdge) control.Cursor = Cursors.SizeNESW;
            //    else control.Cursor = Cursors.SizeWE;
            //}
            //else if (_mouseIsInRightEdge)
            //{
            //    if (_mouseIsInTopEdge) control.Cursor = Cursors.SizeNESW;
            //    else if (_mouseIsInBottomEdge) control.Cursor = Cursors.SizeNWSE;
            //    else control.Cursor = Cursors.SizeWE;
            //}
            //else if (_mouseIsInTopEdge || _mouseIsInBottomEdge) control.Cursor = Cursors.SizeNS;
            //else control.Cursor = Cursors.Default;
        }
        #endregion

        #region Mouse Events
        private static void MouseDown(Control control, MouseEventArgs e, TypeMover workType)
        {
            if (_moving || _resizing) return;

            _cursorStartPoint = e.Coord - control.Location;
            UpdateMouseEdgeProperties(control, e.Coord, workType);
            UpdateMouseCursor(control, workType);

            if ((workType & TypeMover.Resize) == TypeMover.Resize
                && (_mouseIsInRightEdge || _mouseIsInLeftEdge || _mouseIsInTopEdge || _mouseIsInBottomEdge))
                _resizing = true;

            if ((workType & TypeMover.Move) == TypeMover.Move)
                _moving = true;
        }
        private static void MouseMove(Control control, MouseEventArgs e, TypeMover workType)
        {
            if (!_resizing && !_moving)
                UpdateMouseEdgeProperties(control, e.Coord, workType);

            Vector2 cur = e.Coord - control.Location;
            if (_resizing)
            {
                if (_mouseIsInLeftEdge)
                {
                    if (_mouseIsInTopEdge)
                    {
                        control.Width -= (cur.X - _cursorStartPoint.X);
                        control.Left += (cur.X - _cursorStartPoint.X);
                        control.Height -= (cur.Y - _cursorStartPoint.Y);
                        control.Top += (cur.Y - _cursorStartPoint.Y);
                    }
                    else if (_mouseIsInBottomEdge)
                    {
                        control.Width -= (cur.X - _cursorStartPoint.X);
                        control.Left += (cur.X - _cursorStartPoint.X);
                        control.Height = cur.Y + Border;
                    }
                    else
                    {
                        control.Width -= (cur.X - _cursorStartPoint.X);
                        control.Left += (cur.X - _cursorStartPoint.X);
                    }
                }
                else if (_mouseIsInRightEdge)
                {
                    if (_mouseIsInTopEdge)
                    {
                        control.Width = cur.X + Border;
                        control.Height -= (cur.Y - _cursorStartPoint.Y);
                        control.Top += (cur.Y - _cursorStartPoint.Y);
                    }
                    else if (_mouseIsInBottomEdge)
                    {
                        control.Width = cur.X + Border;
                        control.Height = cur.Y + Border;
                    }
                    else control.Width = cur.X + Border;
                }
                else if (_mouseIsInTopEdge)
                {
                    control.Height -= (cur.Y - _cursorStartPoint.Y);
                    control.Top += (cur.Y - _cursorStartPoint.Y);
                }
                else if (_mouseIsInBottomEdge) control.Height = cur.Y + Border;
                else MouseUp(control, workType);
            }
            else if (_moving)
            {
                float x = (e.X - _cursorStartPoint.X);
                float y = (e.Y - _cursorStartPoint.Y);
                
                control.Location = _cursorStartPoint = new Vector2(x, y);
            }

            UpdateMouseCursor(control, workType);
        }
        private static void MouseUp(Control control, TypeMover workType)
        {
            _resizing = false;
            _moving = false;
            //control.Capture = false;
            UpdateMouseCursor(control, workType);
        }
        #endregion
    }
}