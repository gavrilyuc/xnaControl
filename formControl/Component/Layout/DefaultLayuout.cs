using System.Collections.Generic;
using System.Linq;
using FormControl.Component.Controls;

namespace FormControl.Component.Layout
{
    /// <summary>
    /// Контейнер для хранение Контролов.
    /// </summary>
    public class DefaultLayuout : IControlLayout
    {
        /// <summary>
        /// Вызывается когда происходит Добавления контрола в Grid.
        /// </summary>
        public event GridEventHandler ControlsAdded = delegate { };
        /// <summary>
        /// Вызывается когда происходит Удаления контрола из Grid.
        /// </summary>
        public event GridEventHandler ControlsRemoved = delegate { };
        
        private readonly List<Control> _containerList = new List<Control>();

        /// <summary>
        /// Добавить Контрол в Grid.
        /// </summary>
        /// <param name="control"></param>
        public virtual void Add(Control control)
        {
            _containerList.Add(control);
            ControlsAdded(this, control);
        }
        /// <summary>
        /// Очистить
        /// </summary>
        public void Clear() => _containerList.Clear();
        /// <summary>
        /// Существует ли данный контрол в контейнере
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Contains(Control item) { return _containerList.Contains(item); }
        /// <summary>
        /// Скопировать массив в контейнер
        /// </summary>
        /// <param name="array"></param>
        /// <param name="arrayIndex"></param>
        public void CopyTo(Control[] array, int arrayIndex) { _containerList.CopyTo(array, arrayIndex); }
        /// <summary>
        /// Добавить список Контролов в Grid.
        /// </summary>
        /// <param name="listControls"></param>
        public void AddRange(IEnumerable<Control> listControls) { foreach (Control ch in listControls) Add(ch); }
        /// <summary>
        /// Удалить Контрол, зная его индекс
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public void Remove(int index) => Remove(this[index]);
        /// <summary>
        /// Удалить список контролов из контейнера
        /// </summary>
        /// <param name="listControls"></param>
        public void RemoveRange(IEnumerable<Control> listControls) { foreach (Control ch in listControls) Remove(ch); }
        /// <summary>
        /// Удалить контрол
        /// </summary>
        /// <param name="control"></param>
        /// <returns></returns>
        public virtual bool Remove(Control control)
        {
            bool res = _containerList.Remove(control);
            control.ParentControl = null;// Delete Parent.
            ControlsRemoved(this, control);
            return res;
        }
        /// <summary>
        /// Поиск индекса контрола в текущем контейнере
        /// </summary>
        /// <param name="control"></param>
        /// <returns></returns>
        public int IndexOf(Control control) => _containerList.IndexOf(control);
        /// <summary>
        /// Вставить контрол
        /// </summary>
        /// <param name="index"></param>
        /// <param name="item"></param>
        public void Insert(int index, Control item) { _containerList.Insert(index, item); }
        /// <summary>
        /// Удалить всё начиная с индекса
        /// </summary>
        /// <param name="index"></param>
        public void RemoveAt(int index) { _containerList.RemoveAt(index); }
        /// <summary>
        /// Поиск контрола по имени.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public Control FindFromName(string name)
        {
            Control t = null;
            for (int i = 0; i < _containerList.Count; i++)
            {
                if (_containerList[i].Name != name) continue;
                t = _containerList[i];
                break;
            }
            return t;
        }
        /// <summary>
        /// Индексатор по Индексу
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public Control this[int index]
        {
            get
            {
                if (index >= 0 && index < _containerList.Count) return _containerList[index];
                return null;
            }
            set { if (index >= 0 && index < _containerList.Count) _containerList[index] = value; }
        }
        /// <summary>
        /// Индексатор по именам контролов
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public Control this[string name]
        {
            get
            {
                if (string.IsNullOrEmpty(name) || string.IsNullOrWhiteSpace(name)) return null;
                return FindFromName(name);
            }
            set
            {
                if (string.IsNullOrEmpty(name) || string.IsNullOrWhiteSpace(name)) return;
                _containerList[IndexOf(FindFromName(name))] = value;
            }
        }

        /// <summary></summary><returns></returns>
        public IEnumerator<Control> GetEnumerator() => _containerList.GetEnumerator();
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => _containerList.GetEnumerator();

        /// <summary>
        /// Количество контролов
        /// </summary>
        public int Count => _containerList.Count;
        /// <summary></summary>
        public bool IsReadOnly => true;
    }
}
