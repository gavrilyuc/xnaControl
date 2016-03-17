using System;
using FormControl.Component.Controls.Base;
using FormControl.Component.Layout;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FormControl.Component.Controls
{
    /// <summary>
    /// Контейнер с текстовыми рядками
    /// </summary>
    public class ListBox : BorderedControlBase
    {
        private Vector2 _itemSize;
        private int _sel;
        private bool _isSendered = true;

        /// <summary>
        /// Выделеный Объект
        /// </summary>
        /// 
        public int SelectedIndex
        {
            get { return _sel; }
            set
            {
                _sel = value;
                if (_isSendered)
                    SelectedIndexChanged(this);
            }
        }
        /// <summary>
        /// Выделеный Контрол
        /// </summary>
        public Control SelectedControl
        {
            get
            {
                if (SelectedIndex < 0 || SelectedIndex >= Controls.Count) return null;
                return Controls[SelectedIndex];
            }
        }
        /// <summary>
        /// Автоматически подстраивать размер контрола
        /// </summary>
        public bool AutoSize { get; set; } = true;
        /// <summary>
        /// Размер для каждего Объекта внутри списка
        /// </summary>
        public Vector2 ItemSize
        {
            get { return _itemSize; }
            set
            {
                _itemSize = value;
                _itemSize.X = Size.X;
                if (Controls.Count > 0 && AutoSize)
                    Size = new Vector2(Size.X, _itemSize.Y * Controls.Count);
            }
        }

        /// <summary>
        /// Вызывается когда происходит изменение индекса выделеного Объекта
        /// </summary>
        public event EventHandler SelectedIndexChanged = delegate { }; 

        /// <summary>
        /// ctor
        /// </summary>
        public ListBox()
        {
            MouseDown += ListBox_MouseDown;
            MouseUp += ListBox_MouseUp;
            Controls.ControlsAdded += FixableItemSize;
            Controls.ControlsRemoved += UnFixableItemSize;
        }

        #region Events
        private static void UnFixableItemSize(DefaultLayuout sender, Control utilizingControl)
        {
            utilizingControl.LockedTransformation = false; // UnSet locking Transformations...
        }
        private static void FixableItemSize(DefaultLayuout sender, Control utilizingControl)
        {
            utilizingControl.LockedTransformation = true; // set locking Transformations...
        }
        private void ListBox_MouseUp(Control sender, MouseEventArgs e)
        {
            if (_isSendered) return;
            _isSendered = !_isSendered;
            SelectedIndex = SelectedIndex;
            if (SelectedControl == null) return;
            SelectedControl.Focused = true;
        }
        private void ListBox_MouseDown(Control sender, MouseEventArgs e)
        {
            if (Controls.Count < 1) return;
            _isSendered = false;
            SelectedIndex =
                (int)Math.Ceiling((e.Coord - DrawabledLocation).Y
                                  / ItemSize.Y) - 1; // -1 Array index is begining 0.
        }
        #endregion
    }
}