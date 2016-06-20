using System;
using System.ComponentModel;
using FormControl.Component.Layout;
using Microsoft.Xna.Framework;

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
                    SelectedIndexChanged?.Invoke(this);
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
                if (_itemSize == value) return;
                for (int i = 0; i < Controls.Count; i++) Controls[i].Size = value;
                _itemSize = value;
                _itemSize.X = Size.X;
                if (Controls.Count > 0 && AutoSize) Size = new Vector2(Size.X, _itemSize.Y * Controls.Count);
            }
        }

        /// <summary>
        /// Вызывается когда происходит изменение индекса выделеного Объекта
        /// </summary>
        public event EventHandler SelectedIndexChanged;

        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        public ListBox() : this(new DefaultLayuout()) { }
        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        public ListBox(IControlLayout layout) : base(layout)
        {
            MouseDown += ListBox_MouseDown;
            MouseUp += ListBox_MouseUp;

            Controls.ControlsAdded += FixableItemSize;
            Controls.ControlsRemoved += UnFixableItemSize;

            ResizeControl += ListBox_ResizeControl;
        }

        #region Events
        private void UnFixableItemSize(DefaultLayuout sender, Control utilizingControl)
        {
            SetControlLockedTransformation(utilizingControl, false);// UnSet locking Transformations...
            InvalidateSize();
        }
        private void FixableItemSize(DefaultLayuout sender, Control utilizingControl)
        {
            utilizingControl.Size = ItemSize;
            SetControlLockedTransformation(utilizingControl); // set locking Transformations...
            utilizingControl.MouseDown += ListBox_MouseDown;

            utilizingControl.MouseUp += ListBox_MouseUp;
            InvalidateSize();
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
        private void ListBox_ResizeControl(Control sender)
        {
            for (int i = 0; i < Controls.Count; i++)
            {
                SetControlLockedTransformation(Controls[i], false);
                Controls[i].Location = new Vector2(0, i * ItemSize.Y);
                SetControlLockedTransformation(Controls[i]);
            }
            InvalidateSize();
        }
        private void InvalidateSize()
        {
            if (!AutoSize) return;
            Vector2 tmp = new Vector2(Size.X, ItemSize.Y * (Controls.Count == 0 ? 1 : Controls.Count));
            if (Size.Y >= tmp.Y) return;
            LockedTransformation = false;
            Size = tmp;
            LockedTransformation = true;
        }
        #endregion

        /// <summary></summary>
        protected override void OnVisibleControlsSetter(bool value)
        {
            base.OnVisibleControlsSetter(value);
            for (int i = 0; i < Controls.Count; i++)
            {
                SetControlLockedTransformation(Controls[i], false);
                Controls[i].Location = new Vector2(0, i * ItemSize.Y);
                Controls[i].Size = new Vector2(Size.X, ItemSize.Y);
                SetControlLockedTransformation(Controls[i]);
            }
            InvalidateSize();
        }
        /// <summary></summary>
        public override void Inicialize()
        {
            base.Inicialize();
            for (int i = 0; i < Controls.Count; i++)
            {
                SetControlLockedTransformation(Controls[i], false);
                Controls[i].Location = new Vector2(0, i * ItemSize.Y);
                SetControlLockedTransformation(Controls[i]);
            }
        }
    }
}