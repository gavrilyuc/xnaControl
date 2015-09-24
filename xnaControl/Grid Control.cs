using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

        private Object parrent;
        private List<Control> l = new List<Control>();
        /// <summary>
        /// Конструктор По умолчанию
        /// </summary>
        public GridControls(Object Parrent)
        {
            this.parrent = Parrent;
        }
        /// <summary>
        /// Добавить Контрол в Grid.
        /// </summary>
        /// <param name="control"></param>
        public void Add(Control control)
        {
            l.Add(control);
            ControlsAdded(this, new GridEventArgs(control));
        }
        /// <summary>
        /// Добавить список Контролов в Grid.
        /// </summary>
        /// <param name="listControls"></param>
        public void AddRange(IEnumerable<Control> listControls)
        {
            foreach (var ch in listControls) this.Add(ch);
        }
        /// <summary>
        /// Удалить Контрол, зная его индекс
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public bool Remove(int index)
        {
            return this.Remove(this[index]);
        }
        /// <summary>
        /// Удалить контрол
        /// </summary>
        /// <param name="control"></param>
        /// <returns></returns>
        public bool Remove(Control control)
        {
            bool res = l.Remove(control);
            ControlsRemoved(this, new GridEventArgs(control));
            return res;
        }
        public int IndexOf(Control control) { return l.IndexOf(control); }
        public Control FindFromName(string name)
        {
            Control a = null;
            for (int i = 0; i < l.Count; i++)
            {
                if (l[i].Name == name)
                {
                    a = l[i];
                    break;
                }
            }
            return a;
        }
        public Control this[int index]
        {
            get { if (index >= 0 && index < l.Count) return l[index]; else return null; }
            set { if (index >= 0 && index < l.Count) l[index] = value; }
        }
        public IEnumerator<Control> GetEnumerator() { return l.GetEnumerator(); }
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() { return l.GetEnumerator(); }
        /// <summary>
        /// Количество контролов
        /// </summary>
        public int Count { get { return l.Count; } }
    }
}
