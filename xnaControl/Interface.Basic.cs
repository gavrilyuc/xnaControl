using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Core
{
    public interface IInicializator
    {
        void Inicialize();
    }
    public interface IContent
    {
        void LoadContent(ContentManager content);
        void UnloadContent();
    }

    public interface IWindow
    {
        Graphics Graphics2D { get; }
        Point Screen { get; }
        GraphicsDevice GraphicsDevice { get; }
        ContentManager Content { get; }
    }

}
