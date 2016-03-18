using FormControl.Component.Controls;
using Microsoft.Xna.Framework;

namespace FormControl.Component.Layout
{
    /// <summary>
    /// Контейнер процентного Соотношения, в соответсвии Размерам Вложеного Контейнера
    /// </summary>
    public sealed class ProcentLayout : DefaultLayuout
    {
        private Vector2 _procentContainer;

        private static readonly Vector2 FullProcent = new Vector2(100, 100);

        /// <summary>
        /// Установить Трансформацию для данного контейнера.
        /// </summary>
        public void SetContainerTransformation(ITransformation value)
        {
            _procentContainer = new Vector2 {
                X = value.ClientSize.Width / 100,
                Y = value.ClientSize.Height / 100
            };
            Control item;
            for (int i = 0; i < Count; i++)
            {
                item = this[i];
                item.LockedTransformation = false;
                item.Location = Vector2.Min(Vector2.Max(Vector2.Zero, item.Location), FullProcent);
                item.Location *= _procentContainer;

                item.Size = Vector2.Min(Vector2.Max(Vector2.Zero, item.Size), FullProcent);
                item.Size *= _procentContainer;
                item.LockedTransformation = true;
            }
        }

        /// <summary>
        /// Добавить контрол
        /// </summary>
        /// <param name="item"></param>
        public override void Add(Control item)
        {
            item.LockedTransformation = false;
            item.Location = Vector2.Min(Vector2.Max(Vector2.Zero, item.Location), FullProcent);
            item.Location *= _procentContainer;

            item.Size = Vector2.Min(Vector2.Max(Vector2.Zero, item.Size), FullProcent);
            item.Size *= _procentContainer;
            item.LockedTransformation = true;

            base.Add(item);
        }
    }
}