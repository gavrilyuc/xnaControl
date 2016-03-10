
using System;
using Core.Base.Component.Controls;
using Microsoft.Xna.Framework;
namespace Core.Base.Component.Layout
{
    public sealed class ProcentLayout : DefaultLayuout
    {
        private readonly Vector2 _procentContainer;
        private static readonly Vector2 FullProcent = new Vector2(100, 100);

        public ProcentLayout(object parrent) : base(parrent)
        {
            ITransformation parentControl = parrent as ITransformation;
            if (parentControl != null)
            {
                _procentContainer = new Vector2 {
                    X = parentControl.ClientSize.Width / 100,
                    Y = parentControl.ClientSize.Height / 100
                };
            }
            else throw new InvalidCastException("Родитель данного контейнера должен реализовывать интерфейс ITransformation");
        }
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