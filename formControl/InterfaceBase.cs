using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace FormControl
{
    /// <summary>
    /// Объект который умеет инициализироваться
    /// </summary>
    public interface IInicializator
    {
        /// <summary>
        /// Инициализировать
        /// </summary>
        void Inicialize();
    }
    /// <summary>
    /// Объект который имеет контент
    /// </summary>
    public interface IContent
    {
        /// <summary>
        /// Загрузить контент
        /// </summary>
        /// <param name="content"></param>
        void LoadContent(ContentManager content);
        /// <summary>
        /// Выгрузить контент
        /// </summary>
        void UnloadContent();
    }
    /// <summary>
    /// Представляет Объект Окно
    /// </summary>
    public interface IWindow
    {
        /// <summary>
        /// Объект графики
        /// </summary>
        Graphics Graphics2D { get; }
        /// <summary>
        /// Размеры окна
        /// </summary>
        Point Screen { get; }
        /// <summary>
        /// Девайс отрисовки
        /// </summary>
        GraphicsDevice GraphicsDevice { get; }
        /// <summary>
        /// Контент Менеджер для загрузки контента
        /// </summary>
        ContentManager Content { get; }
    }
}
