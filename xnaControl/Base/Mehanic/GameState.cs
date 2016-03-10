using System.Linq;
using Core.Base.Component.Controls;
using Core.Base.Component.Form;

namespace Core.Base.Mehanic
{
    public class GameState : Control, IGameState
    {
        private readonly Form _userForm;

        public GameState(Form form)
        {
            _userForm = form;
            Enabled = false;
            Drawabled = false;
            Name = "GameState::Control";
        }

        public virtual void Show()
        {
            foreach (GameState state in _userForm.Controls.OfType<GameState>())
                state.Hide();
            Enabled = true;
            Drawabled = true;
        }
        public void Hide()
        {
            Enabled = false;
            Drawabled = false;
        }
        public void Change(string gameStateName)
        {
            (_userForm.Controls.FindFromName(gameStateName) as GameState)?.Show();
        }
    }
}