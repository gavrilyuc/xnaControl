using Core.Base.Component;
using System.Linq;
using Core.Base.Component.Controls;

namespace Core.Base.Mehanic
{
    public interface IGameState
    {
        string Name { get; }
        void Show();
    }
    public class GameState : Control, IGameState
    {
        readonly Form _userForm;
        public GameState(Form form)
        {
            _userForm = form;
            Enabled = false;
            Drawabled = false;
            Name = "GameState::Control";
        }
        public virtual void Show()
        {
            foreach (var state in _userForm.Controls.OfType<GameState>())
            {
                state.Hide();
            }
            Enabled = true;
            Drawabled = true;
        }
        public void Hide()
        {
            Enabled = false;
            Drawabled = false;
        }
        public void Change(string name)
        {
            (_userForm.Controls.FindFromName(name) as GameState)?.Show();
        }
    }
}