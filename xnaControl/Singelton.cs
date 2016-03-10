using System;

namespace Core
{
    public abstract class Singelton<T> where T : class
    {
        private static T _instance;
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
