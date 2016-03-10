using System.Collections.Generic;
using Core.Base.Component.Controls;

namespace Core.Base.Component.Layout
{
    public interface ILayout<T> : IList<T>
    {
        object Parent { get; }

        void AddRange(IEnumerable<T> listControls);
        void RemoveRange(IEnumerable<T> listControls);
        void Remove(int index);

        event GridEventHandler ControlsAdded;
        event GridEventHandler ControlsRemoved;
    }

    public interface IControlLayout : ILayout<Control>
    {
        Control FindFromName(string name);

        Control this[string name] { get; set; }
    }
}