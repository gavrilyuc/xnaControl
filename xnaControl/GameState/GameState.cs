using Core.Base.Component;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Base.Mehanic
{
    public interface IGameState
    {
        string Name { get; }
        void Show();
    }
    public class GameState : Control, IGameState
    {
        Form userForm;
        public GameState(Form form)
        {
            userForm = form;
            this.Enabled = false;
            this.Drawabled = false;
            this.Name = "GameState::Control";
        }
        public virtual void Show()
        {
            foreach (Control ch in userForm.Controls)
            {
                var state = ch as GameState;
                if (state == null) continue;
                state.Hide();
            }
            this.Enabled = true;
            this.Drawabled = true;
        }
        public void Hide()
        {
            this.Enabled = false;
            this.Drawabled = false;
        }
        public void Change(string name)
        {
            GameState state = (userForm.Controls.FindFromName(name) as GameState);
            if (state != null) state.Show();
        }
    }
}
/// TODO: Create Class `GameState Manager`
/// and create `TextBox` control class.
/// Then create States:
/// 1) Loading Screen (Loading Screen onChange screen's)
/// 2) Title Screen
/// 3) Options Screen (Option Only Game Properties): KeyBoard, Mouse, Graphics Properties...
/// 4) Game Mehanic Screen
/// 
/// And create Global control `Console`
/// 
/// Thx, i am Dmitry.                           @date: 14.09.2015
///                                             @Time Samp: 22:31