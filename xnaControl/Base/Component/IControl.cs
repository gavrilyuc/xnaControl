using Core.Base.Component.Layout;
using Microsoft.Xna.Framework;

namespace Core.Base.Component
{

    public interface IControl : ITransformation
    {
        string Name { get; }
        IControlLayout Controls { get; }
        bool Enabled { get; }
        bool Drawabled { get; }

        IControl Parent { get; }
    }

    public interface ITransformation
    {
        Vector2 Location { get; }
        Vector2 Size { get; }
        RectangleF ClientSize { get; }
    }
}