using FormControl.Component;
using Microsoft.Xna.Framework;

namespace FormControl.Drawing
{
    /// <summary>
    /// Кисть для рисования Свойств Контрола
    /// </summary>
    public abstract class Brush : ICloneable<Brush>
    {
        /// <summary>
        /// Алгоритм отрисовки кисти
        /// </summary>
        /// <param name="graphics"></param>
        /// <param name="gameTime"></param>
        /// <param name="region"></param>
        public abstract void AlgorithmDrawable(Graphics graphics, GameTime gameTime, IDrawablingTransformation region);

        /// <summary>
        /// Клонировать Объект
        /// </summary>
        /// <returns></returns>
        public Brush Clone() => GetInctance();

        /// <summary></summary><returns></returns>
        protected abstract Brush GetInctance();
    }
}