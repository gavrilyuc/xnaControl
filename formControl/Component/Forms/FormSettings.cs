using System;
using FormControl.Component.Layout;
using Microsoft.Xna.Framework;

namespace FormControl.Component.Forms
{
    /// <summary>
    /// Настройки формы
    /// </summary>
    public class FormSettings
    {
        #region Only ALL Screen Sizer
        [NonSerialized] private static readonly Point[] _screens = {
            new Point(640, 480), new Point(768, 480), new Point(800, 480),
            new Point(800, 600), new Point(960, 540), new Point(960, 720), new Point(1152, 720),
            new Point(1152, 900), new Point(1366, 768), new Point(1280, 720), new Point(1280, 768),
            new Point(1280, 800), new Point(1280, 960), new Point(1280, 1024), new Point(1440, 900),
            new Point(1400, 1050), new Point(1440, 960), new Point(1440, 1024), new Point(1440, 1080),
            new Point(1600, 768), new Point(1600, 900), new Point(1600, 1024), new Point(1600, 1200),
            new Point(1680, 1050), new Point(1792, 1344), new Point(1800, 1440), new Point(1856, 1392),
            new Point(1920, 1080), new Point(1920, 1200), new Point(1920, 1280), new Point(1920, 1400),
            new Point(2048, 1152), new Point(2048, 1280), new Point(2538, 1080), new Point(2880, 900),
            //todo: Дописать для широко форматных разшерений.
        };
        /// <summary>
        /// Все возможные размеры для формы
        /// </summary>
        public static Point[] ScreenPoints() => _screens;
        #endregion

        /// <summary>
        /// Размер формы
        /// </summary>
        public Point ScreenSize { get; set; }
        /// <summary>
        /// Оконный режим
        /// </summary>
        public bool Windowed { get; set; }
        /// <summary>
        /// Показывать курсор мыши
        /// </summary>
        public bool WindowMouseView { get; set; }
        /// <summary>
        /// Имя директории с контентом
        /// </summary>
        public string ContentDirectory { get; set; }
        /// <summary>
        /// Контейнер вмещения контролов
        /// </summary>
        public IControlLayout FormContolLayout { get; set; }
        /// <summary>
        /// Объект представления окна
        /// </summary>
        public GameWindow Window { get; set; }
        /// <summary>
        /// Конструкетор по умолчанию
        /// </summary>
        public FormSettings()
        {
            ContentDirectory = "Content";
            WindowMouseView = false;
            Windowed = true;
            ScreenSize = ScreenPoints()[20];
        }
    }
}
