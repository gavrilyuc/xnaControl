using System;
using System.Collections.Generic;
using System.Linq;
using FormControl.Component.Controls;
using FormControl.Component.Forms;
using FormControl.Drawing.Brushes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace XnaControlEditor
{
    public partial class MainForm : Form
    {

        public MainForm(FormSettings settings): base(settings)
        {
            InicializeComponent();
        }

        protected override void LoadContent()
        {
            InicializeLoadContent();
            base.LoadContent();
        }
    }
}
