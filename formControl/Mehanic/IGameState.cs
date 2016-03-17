
namespace FormControl.Mehanic
{
    /// <summary>
    /// Представляет игровое состояние
    /// </summary>
    public interface IGameState
    {
        /// <summary>
        /// Имя состояния
        /// </summary>
        string Name { get; }
        /// <summary>
        /// Показать
        /// </summary>
        void Show();
        /// <summary>
        /// Скрыть
        /// </summary>
        void Hide();
        /// <summary>
        /// Изменить на другое состояние
        /// </summary>
        /// <param name="gameStateName">Имя нового состояния</param>
        void Change(string gameStateName);
    }
}