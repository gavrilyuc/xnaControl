
using System;
using FormControl.Component.Controls;
using Microsoft.Xna.Framework;
namespace FormControl.Component.Layout
{
    /// <summary>
    /// Контейнер процентного Соотношения.
    /// </summary>
    public sealed class ProcentLayout : DefaultLayuout
    {
        private readonly Vector2 _procentContainer;
        private static readonly Vector2 FullProcent = new Vector2(100, 100);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="container"></param>
        public ProcentLayout(ITransformation container)
        {
            _procentContainer = new Vector2 {
                X = container.ClientSize.Width / 100,
                Y = container.ClientSize.Height / 100
            };
        }

        /// <summary>
        /// Добавить контрол
        /// </summary>
        /// <param name="control"></param>
        public override void Add(Control control)
        {
            control.Location = Vector2.Min(control.Location, FullProcent);
            control.Location *= _procentContainer;

            control.Size = Vector2.Min(control.Size, FullProcent);
            control.Size *= _procentContainer;

            base.Add(control);
        }
    }
}