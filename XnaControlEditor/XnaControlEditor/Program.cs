using System;
using FormControl.Component.Forms;
using FormControl.Component.Layout;
using Microsoft.Xna.Framework;

namespace XnaControlEditor
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            using (MainForm game = new MainForm(new FormSettings()
            {
                ContentDirectory = "Content",
                FormContolLayout = new DefaultLayuout(),
                ScreenSize = new Point(800, 600),
                WindowMouseView = true,
                Window = new DefaultGameWindow(),
                Windowed = true
            }))
            {
                game.Run();
            }
        }
    }
}