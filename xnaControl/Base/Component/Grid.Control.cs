using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Base.Component.Controls;

namespace Core.Base.Component
{
    /// <summary>
    /// Контейнер List для хранение Контролов. Обёртка над List.
    /// </summary>
    public class GridControls : IEnumerable<Control>
    {
        /// <summary>
        /// Вызывается когда происходит Добавления контрола в Grid.
        /// </summary>
        public event GridEventHandler ControlsAdded = delegate { };
        /// <summary>
        /// Вызывается когда происходит Удаления контрола из Grid.
        /// </summary>
        public event GridEventHandler ControlsRemoved = delegate { };

        public object Parrent { get; }
        private readonly List<Control> _l = new List<Control>();
        /// <summary>
        /// Конструктор По умолчанию
        /// </summary>
        public GridControls(object parrent)
        {
            Parrent = parrent;
        }
        /// <summary>
        /// Добавить Контрол в Grid.
        /// </summary>
        /// <param name="control"></param>
        public void Add(Control control)
        {
            _l.Add(control);
            ControlsAdded(this, new GridEventArgs(control));
        }
        /// <summary>
        /// Добавить список Контролов в Grid.
        /// </summary>
        /// <param name="listControls"></param>
        public void AddRange(IEnumerable<Control> listControls)
        {
            foreach (var ch in listControls) Add(ch);
        }
        /// <summary>
        /// Удалить Контрол, зная его индекс
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public bool Remove(int index)
        {
            return Remove(this[index]);
        }
        /// <summary>
        /// Удалить контрол
        /// </summary>
        /// <param name="control"></param>
        /// <returns></returns>
        public bool Remove(Control control)
        {
            bool res = _l.Remove(control);
            ControlsRemoved(this, new GridEventArgs(control));
            return res;
        }
        public int IndexOf(Control control) { return _l.IndexOf(control); }
        public Control FindFromName(string name)
        {
            return _l.FirstOrDefault(t => t.Name == name);
        }

        public Control this[int index]
        {
            get
            {
                if (index >= 0 && index < _l.Count) return _l[index];
                return null;
            }
            set { if (index >= 0 && index < _l.Count) _l[index] = value; }
        }
        public IEnumerator<Control> GetEnumerator() { return _l.GetEnumerator(); }
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() { return _l.GetEnumerator(); }
        /// <summary>
        /// Количество контролов
        /// </summary>
        public int Count => _l.Count;
    }
}
