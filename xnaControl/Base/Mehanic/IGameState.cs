
namespace Core.Base.Mehanic
{
    public interface IGameState
    {
        string Name { get; }
        void Show();
        void Hide();
        void Change(string gameStateName);
    }
}