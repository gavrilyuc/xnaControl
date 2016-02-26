using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Base
{
    public abstract class Singelton<T> where T : class
    {
        private static T _instance;
        protected Singelton() { }
        public static T GetInstance
        {
            get
            {
                if (_instance != null) return _instance;
                var s = typeof(T).GetConstructor(new Type[] { });
                _instance = (T)s.Invoke(null);
                return _instance;
            }
        }
    }
}
