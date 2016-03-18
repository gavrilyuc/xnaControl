using System.Collections.Generic;
using FormControl.Component.Controls;

namespace FormControl.Component.Layout
{
    /// <summary>
    /// Представляет Объект как Контейнер чего-то
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ILayout<T> : IList<T>
    {
        /// <summary>
        /// множественое добавление
        /// </summary>
        /// <param name="listControls"></param>
        void AddRange(IEnumerable<T> listControls);
        /// <summary>
        /// Множественое удаление
        /// </summary>
        /// <param name="listControls"></param>
        void RemoveRange(IEnumerable<T> listControls);
        /// <summary>
        /// Удалить
        /// </summary>
        /// <param name="index"></param>
        void Remove(int index);

        /// <summary>
        /// Событие добавление контрола
        /// </summary>
        event GridEventHandler ControlsAdded;
        /// <summary>
        /// Событие удаление контрола
        /// </summary>
        event GridEventHandler ControlsRemoved;
    }

    /// <summary>
    /// Представляет Объект как Контейнер контролов
    /// </summary>
    public interface IControlLayout : ILayout<Control>
    {
        /// <summary>
        /// Поиск контролов по имени
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        Control FindFromName(string name);
        /// <summary>
        /// Индексатор по имени
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        Control this[string name] { get; set; }
        /// <summary>
        /// Отсортировать Контролы в контейнере по правилам контейнера.
        /// </summary>
        void Sort();
    }
}