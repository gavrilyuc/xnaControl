using FormControl.Component.Layout;
using FormControl.Drawing;
using Microsoft.Xna.Framework;

namespace FormControl.Component.Controls
{
    /// <summary>
    /// Контрол выбора перечисления
    /// </summary>
    public class ComboBox : BorderedControlBase
    {
        private Rectangle _selectText;
        private readonly Button _arrow;
        private readonly ListBox _contaier;

        /// <summary>
        /// Контейнер для Контролов
        /// </summary>
        public IControlLayout Container => _contaier.Controls;
        /// <summary>
        /// Выделеный Индекс
        /// </summary>
        public int SelectedIndex => _contaier.SelectedIndex;
        /// <summary>
        /// Выделеный Контрол
        /// </summary>
        public Control SelectedControl => _contaier.SelectedControl;
        /// <summary>
        /// Кисть для рисования Стрелки
        /// </summary>
        public Brush ToogleArrow { get { return _arrow.Background; } set { _arrow.Background = value; } }

        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        public ComboBox(TextBrush textBrush) : base(new ProcentLayout())
        {
            //if (textBrush == null) throw new ArgumentNullException($"ComboBox::{nameof(TextBrush)} is Null");

            _arrow = new Button(textBrush) { Text = string.Empty };
            _contaier = new ListBox() { AutoSize = true };
            Controls.Add(_arrow);
            Controls.Add(_contaier);
            _contaier.Visibled = false;
            _contaier.Enabled = false;
            ResizeControl += ComboBox_ResizeControl;
            _arrow.Click += _arrow_Click;
            BackgroundChanged += ComboBox_BackgroundChanged;
            BorderChanged += ComboBox_BorderChanged;
            Paint += ComboBox_Paint;
            Container.ControlsAdded += Container_ControlsAdded;
            _contaier.Tag = "comboContainer";
        }

        private void Container_ControlsAdded(DefaultLayuout sender, Control utilizingControl)
        {
            utilizingControl.Click += UtilizingControl_Click;
        }
        private void UtilizingControl_Click(Control sender, MouseEventArgs e)
        {
            _arrow_Click(this, e);
        }
        private void ComboBox_Paint(Control sender, TickEventArgs e)
        {
            if(SelectedControl == null) return;
            _selectText = new Rectangle((int)(DrawabledLocation.X + _arrow.Location.X + _arrow.Size.X),
                (int)DrawabledLocation.Y, (int)Size.X, (int)Size.Y);
            TextControlBase f = SelectedControl as TextControlBase;
            if (f != null)
            {
                f.Background.AlgorithmDrawable(e.Graphics, e.GameTime, _selectText);
                f.TextBrush.AlgorithmDrawable(e.Graphics, e.GameTime, _selectText);
            }
            else
                e.Graphics.DrawString(_arrow.TextBrush.Font, SelectedControl.Name, _selectText.Location.ConvertToVector(), _arrow.TextBrush.Color);
        }
        private void ComboBox_BorderChanged(Control sender) => _arrow.Border = _contaier.Border = Border;
        private void ComboBox_BackgroundChanged(Control sender) => _contaier.Background = Background;
        /// <summary></summary><param name="value"></param>
        protected override void OnEnableControlsSetter(bool value)
        {
            if (Controls.Count == 0) return;
            for (int i = 0; i < Controls.Count; i++)
            {
                if (Controls[i] != _contaier)
                    Controls[i].Enabled = value;
            }
            _contaier.Enabled = false;
        }
        /// <summary></summary><param name="value"></param>
        protected override void OnVisibleControlsSetter(bool value)
        {
            if(Controls.Count == 0) return;
            for (int i = 0; i < Controls.Count; i++)
            {
                if (Controls[i] != _contaier)
                    Controls[i].Visibled = value;
            }
            _contaier.Visibled = false;
        }
        private void ComboBox_ResizeControl(Control sender)
        {
            _arrow.LockedTransformation = false;
            _arrow.Size = new Vector2(20, 100);

            _contaier.ItemSize = Size;

            _contaier.LockedTransformation = false;
            _contaier.Size = new Vector2(80, 100);
            _contaier.Location = new Vector2(0, 100);

            ((ProcentLayout)Controls).SetContainerTransformation(this);
        }
        private void _arrow_Click(Control sender, MouseEventArgs e)
        {
            _contaier.Visibled = _contaier.Enabled = !(_contaier.Visibled && _contaier.Enabled);
        }
    }
}