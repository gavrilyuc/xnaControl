using System.Collections.Generic;
using System.Linq;
using Core.Base.Component.Controls;

namespace Core.Base.Component.Layout
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
        /// <summary>
        /// Родитель Контейнера
        /// </summary>
        public virtual object Parent { get; }

        protected readonly List<Control> ContainerList = new List<Control>();

        /// <summary>
        /// Конструктор По умолчанию
        /// </summary>
        public DefaultLayuout(object parrent) { Parent = parrent; }
        /// <summary>
        /// Добавить Контрол в Grid.
        /// </summary>
        /// <param name="control"></param>
        public virtual void Add(Control control)
        {
            control.ParentControl = Parent as Control;
            ContainerList.Add(control);// Set Parent
            ControlsAdded(this, new GridEventArgs(control));
        }
        public void Clear() => ContainerList.Clear();
        public bool Contains(Control item) { return ContainerList.Contains(item); }
        public void CopyTo(Control[] array, int arrayIndex) { ContainerList.CopyTo(array, arrayIndex); }
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
            bool res = ContainerList.Remove(control);
            control.ParentControl = null;// Delete Parent.
            ControlsRemoved(this, new GridEventArgs(control));
            return res;
        }
        /// <summary>
        /// Поиск индекса контрола в текущем контейнере
        /// </summary>
        /// <param name="control"></param>
        /// <returns></returns>
        public int IndexOf(Control control) => ContainerList.IndexOf(control);
        public void Insert(int index, Control item) { ContainerList.Insert(index, item); }
        public void RemoveAt(int index) { ContainerList.RemoveAt(index); }
        /// <summary>
        /// Поиск контрола по имени.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public Control FindFromName(string name) { return ContainerList.FirstOrDefault(t => t.Name == name); }
        public Control this[int index]
        {
            get
            {
                if (index >= 0 && index < ContainerList.Count) return ContainerList[index];
                return null;
            }
            set { if (index >= 0 && index < ContainerList.Count) ContainerList[index] = value; }
        }
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
                ContainerList[IndexOf(FindFromName(name))] = value;
            }
        }
        public IEnumerator<Control> GetEnumerator() => ContainerList.GetEnumerator();
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => ContainerList.GetEnumerator();
        /// <summary>
        /// Количество контролов
        /// </summary>
        public int Count => ContainerList.Count;
        public bool IsReadOnly => true;
    }
}
