using System;
using System.Reflection;

namespace Lof.Common
{
    public class Singleton<T> where T : Singleton<T>
    {
        private static T instance;

        public static T I => instance;

        static Singleton()
        {
            var type = typeof(T);
            var ctors = type.GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic);

            var current = Array.Find(ctors, ctor => ctor.GetParameters().Length == 0);

            if (current == null)
                throw new Exception("can't create singleton instance,have not nonparameter and nonpublic ctor");

            instance = current.Invoke(null) as T;
        }
    }
}
