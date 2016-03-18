using FormControl.Component.Controls;
using FormControl.Component.Forms;

namespace FormControl.Mehanic
{
    /// <summary>
    /// Игровое Состояние
    /// </summary>
    public class GameState : Control, IGameState
    {
        private readonly Form _userForm;
        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        /// <param name="userForm"></param>
        public GameState(Form userForm)
        {
            Enabled = false;
            Visibled = false;
            _userForm = userForm;
            Controls.ControlsAdded += Controls_ControlsAdded;
        }
        private static void Controls_ControlsAdded(Component.Layout.DefaultLayuout sender, Control utilizingControl)
        {
            utilizingControl.Enabled = false;
            utilizingControl.Visibled = false;
        }

        /// <summary>
        /// Показать
        /// </summary>
        public virtual void Show()
        {
            GameState state;
            for (int i = 0; i < _userForm.Controls.Count; i++)
            {
                state = _userForm.Controls[i] as GameState;
                state?.Hide();
            }
            Enabled = true;
            Visibled = true;
        }
        /// <summary>
        /// Скрыть
        /// </summary>
        public void Hide()
        {
            Enabled = false;
            Visibled = false;
        }
        /// <summary>
        /// Изменить состояние на другое состояние.
        /// </summary>
        /// <param name="gameStateName">Имя Нового Состояния</param>
        public void Change(string gameStateName)
        {
            (_userForm.Controls.FindFromName(gameStateName) as GameState)?.Show();
        }
    }
}