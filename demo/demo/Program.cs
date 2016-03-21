using FormControl.Component.Forms;

namespace demo
{
#if WINDOWS
    static class Program
    {
        static void Main(string[] args)
        {
            // Form Settings
            FormSettings settings = new FormSettings()
            {
                ContentDirectory = "Content",
                ScreenSize = new Microsoft.Xna.Framework.Point(800, 600),
                Windowed = true,
                WindowMouseView = true
            };

            // Run Window
            using (Game1 game = new Game1(settings)) game.Run();
        }
    }
#endif
}

