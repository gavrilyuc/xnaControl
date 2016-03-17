using System;

namespace FormControl
{
    /// <summary>
    /// Базовый обстрактный класс Одиночки
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class Singelton<T> where T : class
    {
        private static T _instance;
        /// <summary>
        /// Получить Объект
        /// </summary>
        public static T GetInstance
        {
            get
            {
                if (_instance != null) return _instance;
                _instance = (T) typeof(T).GetConstructor(new Type[] {})?.Invoke(null);
                return _instance;
            }
        }
    }
}
